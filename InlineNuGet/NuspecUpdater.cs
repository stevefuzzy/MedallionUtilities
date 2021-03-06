﻿using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Medallion.Tools
{
    public class NuspecUpdater
    {
        public static string RewriteNuspec(string nuspecPath, Project project, string codeFileName)
        {
            var nuspecText = File.ReadAllText(nuspecPath);
            var substituted = ReplaceTokens(nuspecText, project);
            
            var parsed = XDocument.Parse(substituted);

            // update id
            var idElement = parsed.Descendants("id").Single();
            idElement.SetValue(idElement.Value + ".Inline");

            // update dev dependency
            XElement developmentDependencyElement;
            var existingDevelopmentDependencyElement = parsed.Descendants("developmentDependency").SingleOrDefault();
            if (existingDevelopmentDependencyElement != null)
            {
                developmentDependencyElement = existingDevelopmentDependencyElement;
            }
            else
            {
                developmentDependencyElement = new XElement("developmentDependency");
                parsed.Descendants("metadata").Single().Add(developmentDependencyElement);
            }
            developmentDependencyElement.SetValue("true");

            // update files
            XElement filesElement;
            var existingFilesElement = parsed.Element("package").Element("files");
            if (existingFilesElement != null)
            {
                var docXmlElement = existingFilesElement.Elements("file")
                    .FirstOrDefault(f => StringComparer.OrdinalIgnoreCase.Equals(Path.GetFileName(f.Attribute("src").Value), Path.GetFileNameWithoutExtension(project.FilePath) + ".xml"));
                if (docXmlElement != null)
                {
                    docXmlElement.Remove();
                    Console.WriteLine($"Removed reference to {docXmlElement.Attribute("src").Value}");
                }

                filesElement = existingFilesElement;
            }
            else
            {
                filesElement = new XElement("files");
                parsed.Element("package").Add(filesElement);
            }

            var codeFileElement = XElement.Parse($"<file src=\"{WebUtility.HtmlEncode(Path.GetFileName(codeFileName))}\" target=\"content\" />");
            filesElement.Add(codeFileElement);

            var result = parsed.ToString();
            return result;
        }

        private static string ReplaceTokens(string nuspecText, Project project)
        {
            var compilation = project.GetCompilationAsync().Result;

            var result = nuspecText.Replace("$id$", WebUtility.HtmlEncode(compilation.Assembly.Name))
                .Replace(
                    "$version$", 
                    WebUtility.HtmlEncode(
                        (string)(
                            FindAssemblyAttribute(compilation, typeof(AssemblyInformationalVersionAttribute))
                            ?? FindAssemblyAttribute(compilation, typeof(AssemblyVersionAttribute))
                        )
                        .ConstructorArguments
                        .Single().Value
                    )
                )
                .Replace("$author$", WebUtility.HtmlEncode((string)FindAssemblyAttribute(compilation, typeof(AssemblyCompanyAttribute)).ConstructorArguments.Single().Value))
                .Replace("$description$", WebUtility.HtmlEncode((string)FindAssemblyAttribute(compilation, typeof(AssemblyDescriptionAttribute)).ConstructorArguments.Single().Value));

            return result;
        }

        private static AttributeData FindAssemblyAttribute(Compilation compilation, Type type)
        {
            var result = compilation.Assembly.GetAttributes()
                .Single(a => a.AttributeClass.ContainingNamespace + "." + a.AttributeClass.Name == type.ToString());
            return result;
        }
    }
}
