﻿using Medallion.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Medallion
{
    /// <summary>
    /// Verifies our java random implementation against known sequences generated by the JRE 
    /// implementation with different seeds
    /// </summary>
    public class JavaRandomCompatibilityTest
    {
        #region ---- Overlapping Methods ----
        [Fact]
        public void TestNext()
        {
            var random = Rand.CreateJavaRandom(long.MaxValue);
            var values = Enumerable.Range(0, 100)
                .Select(_ => random.Next())
                .ToArray();
            Assert.True(
                values.SequenceEqual(new[] { 577549913, 943952225, 26349579, 1176895439, 1421815604, 1290198438, 894294477, 857465478, 1774177765, 121412731, 1730951808, 665220512, 121954317, 149520196, 176890342, 1481972250, 1552485069, 1938353972, 1201094686, 1570944655, 1286832617, 754206257, 1997687263, 975495143, 1335117720, 720466900, 499582545, 6265644, 378035946, 325038220, 1742348424, 1490692733, 512654038, 226558178, 232543901, 1495693083, 1280529375, 753494743, 2029142123, 617859745, 470117987, 185130857, 170887139, 1121888592, 1605548928, 928184884, 923208973, 1882898312, 275717795, 668457270, 1863258011, 397472851, 388596142, 785608363, 1710233643, 1067111196, 847626693, 426561303, 818346778, 1210424957, 837574326, 246750562, 1814949631, 1272610358, 535131907, 1544410909, 891703499, 1968069177, 1298348684, 1201169122, 987516795, 1999836385, 1836621277, 1984750298, 56056183, 682423875, 1874573330, 1207201113, 356369803, 703216285, 1809831267, 1470295459, 444584319, 161994220, 1233774672, 227479250, 2057678932, 44932761, 883073898, 1533719247, 2034106910, 964505939, 588522939, 1959040894, 690335609, 26345464, 26592211, 2040785910, 767017641, 1271274924, }),
                string.Join(",", values)
            );
        }

        [Fact]
        public void TestNextWithBound()
        {
            var random = Rand.CreateJavaRandom(9000000000L);
            var values = Enumerable.Range(1, count: 100)
                .Select(i => random.Next(i))
                .ToArray();
            Assert.True(
                values.SequenceEqual(new[] { 0, 0, 2, 2, 4, 1, 6, 7, 1, 5, 4, 4, 9, 6, 5, 1, 13, 3, 0, 3, 19, 21, 2, 18, 0, 15, 23, 25, 16, 9, 0, 30, 32, 14, 13, 9, 33, 20, 14, 19, 8, 30, 31, 14, 43, 16, 45, 26, 1, 41, 24, 18, 24, 38, 37, 17, 36, 8, 21, 16, 6, 60, 44, 30, 50, 57, 8, 19, 52, 51, 33, 58, 12, 66, 49, 74, 41, 30, 6, 56, 41, 9, 1, 7, 17, 54, 15, 29, 69, 69, 35, 14, 46, 91, 70, 80, 95, 12, 8, 46, }),
                string.Join(",", values)
            );

            var random2 = Rand.CreateJavaRandom(123456789);
            var values2 = Enumerable.Range(0, count: 1000)
                .Select(_ => random2.Next(365))
                .ToArray();
            Assert.Equal(
                actual: values2,
                expected: new[] { 130, 75, 188, 259, 175, 169, 278, 202, 212, 47, 46, 81, 34, 272, 354, 316, 205, 339, 85, 94, 102, 321, 63, 184, 221, 345, 268, 275, 99, 153, 245, 338, 331, 140, 231, 189, 32, 179, 230, 358, 213, 273, 11, 8, 119, 169, 331, 295, 248, 279, 255, 303, 30, 299, 250, 358, 113, 321, 358, 195, 94, 345, 333, 226, 233, 19, 309, 132, 80, 253, 251, 165, 209, 61, 26, 327, 173, 64, 48, 296, 169, 123, 225, 100, 358, 140, 346, 22, 51, 83, 218, 187, 241, 283, 91, 28, 275, 142, 334, 207, 357, 238, 146, 324, 28, 216, 206, 21, 9, 334, 28, 189, 344, 286, 343, 98, 323, 351, 358, 35, 106, 188, 239, 209, 70, 145, 268, 45, 32, 59, 105, 313, 349, 330, 70, 195, 218, 62, 363, 148, 255, 361, 182, 274, 146, 201, 172, 163, 155, 255, 358, 333, 262, 187, 10, 265, 204, 277, 320, 3, 129, 327, 192, 190, 218, 255, 185, 139, 7, 25, 83, 294, 328, 10, 18, 204, 199, 198, 94, 355, 180, 248, 237, 252, 356, 305, 216, 247, 47, 301, 292, 3, 362, 105, 180, 246, 317, 130, 228, 342, 309, 54, 0, 280, 323, 88, 203, 363, 133, 50, 277, 3, 275, 105, 290, 258, 359, 275, 301, 327, 241, 354, 257, 128, 205, 0, 107, 31, 107, 33, 18, 83, 174, 149, 35, 129, 354, 364, 95, 202, 320, 222, 148, 322, 16, 12, 318, 90, 258, 14, 269, 339, 50, 153, 159, 343, 331, 252, 238, 22, 53, 277, 312, 353, 356, 179, 326, 348, 222, 15, 175, 266, 247, 160, 240, 82, 351, 70, 339, 264, 222, 264, 176, 146, 116, 39, 298, 311, 42, 326, 228, 78, 325, 331, 350, 88, 167, 153, 122, 288, 228, 359, 139, 348, 185, 5, 348, 275, 7, 239, 330, 349, 186, 221, 143, 201, 308, 165, 96, 263, 171, 246, 157, 59, 8, 133, 310, 152, 76, 93, 330, 116, 42, 354, 90, 237, 356, 300, 145, 195, 15, 173, 304, 31, 152, 62, 206, 358, 12, 168, 188, 29, 166, 109, 209, 191, 212, 248, 228, 326, 214, 304, 262, 187, 131, 328, 59, 359, 192, 266, 303, 221, 54, 300, 278, 305, 147, 21, 135, 100, 158, 26, 22, 294, 88, 180, 310, 44, 65, 233, 248, 289, 176, 84, 277, 296, 241, 121, 354, 135, 132, 173, 77, 26, 188, 227, 49, 18, 46, 286, 127, 127, 145, 65, 242, 266, 52, 134, 43, 281, 189, 252, 164, 300, 355, 301, 36, 340, 349, 299, 262, 172, 347, 361, 287, 60, 67, 184, 136, 43, 207, 110, 63, 350, 138, 284, 160, 5, 59, 53, 163, 35, 73, 86, 311, 152, 323, 215, 259, 331, 228, 345, 163, 109, 360, 27, 240, 317, 231, 341, 151, 297, 32, 24, 291, 346, 226, 6, 91, 18, 101, 315, 127, 332, 242, 80, 244, 317, 76, 101, 347, 272, 236, 126, 314, 50, 59, 289, 212, 99, 102, 206, 250, 313, 6, 6, 61, 143, 282, 180, 326, 143, 50, 10, 291, 359, 280, 120, 175, 340, 309, 269, 183, 5, 40, 124, 215, 229, 193, 233, 60, 119, 343, 13, 267, 145, 16, 324, 27, 129, 268, 178, 111, 250, 38, 35, 118, 290, 271, 102, 320, 38, 80, 84, 295, 99, 70, 335, 328, 339, 273, 106, 118, 267, 24, 303, 82, 17, 259, 222, 235, 177, 42, 162, 108, 265, 82, 223, 312, 238, 117, 89, 100, 215, 224, 261, 165, 239, 251, 46, 66, 122, 322, 301, 313, 108, 355, 91, 170, 252, 301, 290, 347, 259, 322, 132, 69, 39, 105, 332, 169, 345, 70, 22, 265, 210, 189, 11, 228, 314, 189, 102, 277, 237, 245, 108, 344, 343, 73, 257, 119, 354, 98, 345, 130, 125, 128, 358, 130, 51, 116, 16, 161, 208, 246, 101, 114, 70, 344, 169, 80, 120, 170, 31, 267, 15, 117, 306, 212, 53, 317, 43, 285, 85, 92, 44, 158, 332, 278, 162, 290, 123, 189, 180, 181, 274, 144, 358, 13, 50, 148, 334, 57, 208, 174, 189, 82, 38, 248, 354, 36, 195, 118, 86, 19, 17, 217, 32, 195, 187, 285, 207, 116, 182, 84, 347, 122, 179, 228, 24, 241, 121, 223, 289, 340, 261, 364, 119, 64, 338, 355, 363, 60, 343, 287, 307, 268, 80, 280, 320, 209, 118, 235, 47, 145, 276, 321, 181, 51, 215, 129, 75, 352, 175, 151, 130, 164, 110, 325, 352, 205, 364, 81, 183, 252, 209, 18, 156, 249, 355, 114, 173, 20, 165, 197, 116, 138, 290, 298, 254, 58, 114, 229, 276, 11, 271, 363, 202, 209, 4, 167, 217, 231, 328, 89, 71, 346, 340, 176, 300, 39, 176, 247, 357, 112, 61, 347, 21, 354, 224, 96, 319, 226, 51, 125, 207, 101, 84, 61, 268, 41, 328, 297, 9, 223, 15, 66, 134, 41, 262, 122, 46, 158, 175, 96, 193, 52, 159, 136, 217, 40, 281, 53, 359, 78, 315, 47, 148, 52, 140, 86, 89, 125, 191, 335, 194, 247, 68, 328, 87, 254, 37, 171, 164, 165, 167, 228, 9, 87, 236, 306, 63, 32, 38, 148, 351, 26, 259, 120, 318, 361, 315, 206, 241, 187, 65, 339, 138, 265, 15, 225, 254, 40, 354, 241, 52, 363, 320, 28, 39, 96, 200, 139, 288, 111, 147, 245, 142, 230, 120, 15, 182, 290, 177, 312, 81, 142, 96, 340, 201, 75, 113, 350, 32, 251, 163, 167, 2, 76, 207, 82, 292, 319, 359, 46, 207, 351, 281, 340, 134, 183, 315, 264, 326, 274, 48, 65, 308, 321, 160, 81, 126, 67, 157, 223, 309, 282, 244, 274, 316, 48, 139, 91, 197, 1, 362, 86, 235, 247, 231, 133, 306, 228, 37, 183, 142, 127, 54, 183, 18, 145, 285, 90, 196, 334, 143, 167, 311, 140, 227, 65, 310, 230, 89, 131, 279, 281, 274, 276, 158, 309, 53, 361, 166, 162, 296, 311, 92, 112, 100 }
            );
        }

        [Fact]
        public void TestNextDouble()
        {
            var random = Rand.CreateJavaRandom(12345);
            Assert.Equal(actual: random.NextDouble(), expected: 0.3618031071604718, precision: 15);
            Assert.Equal(actual: random.NextDouble(), expected: 0.932993485288541, precision: 15);
            Assert.Equal(actual: random.NextDouble(), expected: 0.8330913489710237, precision: 15);
        }

        [Fact]
        public void TestNextBytes()
        {
            var random = Rand.CreateJavaRandom(long.MinValue);
            var bytes = new byte[100];
            random.NextBytes(bytes);
            var sBytes = bytes.Select(b => unchecked((sbyte)b)).ToArray();
            Assert.True(
                sBytes.SequenceEqual(new sbyte[] { 96, -76, 32, -69, 56, 81, -39, -44, 122, -53, -109, 61, -66, 112, 57, -101, -10, -55, 45, -93, 58, -16, 29, 79, -73, 112, -23, -116, 3, 37, -12, 29, 62, -70, -8, -104, 109, -89, 18, -56, 43, -51, 77, 85, 75, -16, -75, 64, 35, -62, -101, 98, 77, -23, -17, -100, 47, -109, 30, -4, 88, 15, -102, -5, 8, 27, 18, -31, 7, -79, -24, 5, -14, -76, -11, -16, -15, -48, 12, 45, 15, 98, 99, 70, 112, -110, 28, 80, 88, 103, -1, 32, -10, -88, 51, 94, -104, -81, -121, 37 }),
                string.Join(",", sBytes)
            );
        }
        #endregion

        #region ---- Java-only Methods ----
        [Fact]
        public void TestNextBoolean()
        {
            var random = Rand.CreateJavaRandom(-1L);
            var text = string.Join(string.Empty, Enumerable.Range(0, 150).Select(_ => random.NextBoolean() ? '1' : '0'));
            Assert.Equal(
                actual: text,
                expected: "000111001010000111111010100000110001101000011001001000100001001101011101110011001100101001100100010101010000100000110100001101110101010000101111101111"
            );
        }

        [Fact]
        public void TestNextInt32()
        {
            var random = Rand.CreateJavaRandom(unchecked((long)0xaaaaaaaaaaaaaaaaUL));
            var values = Enumerable.Range(0, 50).Select(_ => random.NextInt32()).ToArray();
            Assert.Equal(
                actual: values,
                expected: new[] { -160299939, -621102429, -514766960, -1585973727, 1850643089, -242283619, -76335135, 2036374813, -929653626, 1474433229, -2066460749, 391003457, 841252568, 2057474302, -1928849546, -1192505696, -462353813, 1516538903, 1375907981, 891017933, -47884590, 65906611, -1835005867, 386846093, -1861429516, 1594052849, -700663963, 1033968846, -1946053652, -988239993, -463007465, -1419805736, 1420210737, 1082623517, 1930635079, -557937214, -2124176138, 1675705304, -953891238, -252261594, 1716827091, 1016646452, -1093751422, 1674350359, -133450075, -166608362, 527861567, -1365803181, -1488573519, -1623458015, }
            );
        }

        [Fact]
        public void TestNextInt64()
        {
            var random = Rand.CreateJavaRandom(-12345);

            var longs = Enumerable.Range(0, 50).Select(_ => random.NextInt64()).ToArray();
            Assert.Equal(
                actual: longs,
                expected: new[] { -6677394249205543830, 9027049167818715576, 677142854244467891, 7797062463273896581, -8146118902147941788, 8541563469759364503, -7887521925227135271, 7085088793470598970, -9004582945846855961, -5026785897581235574, 946852964286362140, 3914004843753249884, 1946839661014790731, 4818372777409355870, -748616985533749433, -2041155283140072324, -324902792424725779, -1583259276745116027, -2135178866985711257, -8683706266286395378, -3484795942468779601, -1098596652140882794, -8144534119733286318, -5542843131863796246, 3252294794835269238, -694787915639182645, 8521676944337934110, 9053634726238913485, -7862218998941084501, 6536009830126594689, -2630951074978448219, -678476696384827690, 2899669187879258043, -281328628757215557, 7468557800667028488, -9138763286251912183, 4282660729586700183, 6640613567269209248, 3279328004619751209, 3581944763145330895, -4647615857183675083, 8821213228945464313, -283534543217861329, 8889645167143787380, 3667002347789147917, 2196301899268057012, 7231250551090618118, 6726383595063172008, 7597312644876864924, -2382934589678016925, }
            );
        }

        [Fact]
        public void TestNextSingle()
        {
            var random = Rand.CreateJavaRandom(2);
            for (var i = 0; i < 1000; ++i)
            {
                random.NextInt32(); // spin
            }

            var values = Enumerable.Range(0, 50).Select(_ => random.NextSingle()).ToArray();
            Assert.Equal(
                actual: values,
                expected: new[] { 0.94287306f, 0.8903044f, 0.97903967f, 0.012356639f, 0.17383873f, 0.6793508f, 0.8875068f, 0.06914234f, 0.7914038f, 0.9430413f, 0.25484967f, 0.9709232f, 0.2722515f, 0.008735001f, 0.5167838f, 0.17015874f, 0.8142477f, 0.3411495f, 0.2899512f, 0.29670966f, 0.358271f, 0.1576863f, 0.38329488f, 0.90439373f, 0.46732223f, 0.9526825f, 0.30414647f, 0.4749437f, 0.21356344f, 0.7139307f, 0.2368964f, 0.7848527f, 0.58981174f, 0.45460987f, 0.5398049f, 0.81236047f, 0.36067104f, 0.939894f, 0.22092265f, 0.9285346f, 0.33370495f, 0.74295545f, 0.66506624f, 0.89385194f, 0.28724986f, 0.68099236f, 0.6538195f, 0.08884382f, 0.90368855f, 0.24754298f, },
                comparer: EqualityComparers.Create((float a, float b) => Math.Abs(a - b) < 1e-7)
            );
        }

        [Fact]
        public void TestNextGaussian()
        {
            var random = Rand.CreateJavaRandom(0);

            var sequence = Enumerable.Range(0, 50).Select(_ => random.NextGaussian()).ToArray();
            
            Assert.Equal(
                actual: sequence,
                expected: new[] { 0.8025330637390305, -0.9015460884175122, 2.080920790428163, 0.7637707684364894, 0.9845745328825128, -1.6834122587673428, -0.027290262907887285, 0.11524570286202315, -0.39016704137993785, -0.6433888131264491, 0.052460907198835775, 0.5213420769298895, -0.8239670026881707, 0.26071819402835644, -0.4529877558422544, 1.4031473817209366, 0.27113061707020236, -0.007054015349837401, 0.9049586994113287, 0.8568542481006806, 0.3723340814425109, 0.3976728390023819, 0.06294576961546386, 0.9414599976474556, 0.44110379103508873, -0.7318797311599887, -0.01176361185227962, -0.15736219614735453, -0.5822582291186266, -0.2059701784999411, -0.39990122591137445, 0.8913156150655253, 0.41076063425965825, -1.1712365002966285, -0.3905082189100106, 0.49014040388330665, 0.9597752538041666, 0.7523861952143763, -0.657956415573505, 0.6450323331598297, -0.3154523215417022, 1.054894794114192, 0.5957831787424875, 1.0225509680217193, -2.3561969031359187, -1.5250681153426493, 1.1808572722180044, 0.006140951070945433, -0.13698941007400853, -0.42220793207202106, },
                comparer: EqualityComparers.Create((double a, double b) => Math.Abs(a - b) < 1e-15)
            );
        }
        #endregion

        #region ---- Interaction Test ----
        [Fact]
        public void TestMethodInteractions()
        {
            var random = Rand.CreateJavaRandom(9876543210L);
            var results = new List<object>();
            for (int i = 0; i < 100; ++i)
            {
                switch (random.Next(6))
                {
                    case 0: results.Add(random.NextDouble()); break;
                    case 1: results.Add(random.NextInt32()); break;
                    case 2: results.Add(random.Next(25)); break;
                    case 3: results.Add(random.NextSingle()); break;
                    case 4: results.Add(random.NextInt64()); break;
                    case 5:
                        var bytes = new byte[random.Next(20)];
                        random.NextBytes(bytes);
                        results.Add(bytes);
                        break;
                    default: throw new InvalidOperationException();
                }
            }

            var expected = new object[]
            {
                0.22235656f, 15, new sbyte[] {-112, -39, 69, 73, 124, 120}, 0.87158465f, 392443554, 0.5530938676725138, 0.276456f, 0.5584179281955568, -1263857096, 0.028805852f, 0.8705907f, 799798325, 0.7302692f, 0.42888653f, new sbyte[] {-46, 103, -114, -26, -46}, -6162143214704419847L, 0.72098625f, -1779022269, 0.01500620383851925, -212673233, 0.7016371f, 0.16402835f, -7033073429723626524L, -2052633725, -7319427878813742697L, 6, 7, -3958782141511214914L, -1898560812, -1419901826, new sbyte[] {}, 0.064554036f, 13, 16, -1859039780, -1827951722, 0.7669755f, 7758216996397670229L, 5, 887902656, 8, -6537007272669193844L, -4319733269095355190L, 0.004248589101292555, 0.05673757993524342, 6, 0.7310552f, 0.6643923981380373, 14, 18, 0.2851647f, -3300786939026981835L, 201123046388075681L, 13, 0.033833623f, 8706069765238303400L, 0.8469192961060149, 2230788809142790615L, 1338850410, 1782767858530924861L, -8109470990093977613L, new sbyte[] {38, -20, 24, -46, -56, 57, 119, -12, 24, -124, -20, -91, -74, 32, -32, -39}, 11, new sbyte[] {-105, 105, -32, -117, 68, 6, 95}, 12, 0.05193502820181761, -54126722, -3153642900385149135L, 0.7114215263772223, -297447891, 0.8277129560022595, new sbyte[] {-36, -23, -17, 43, -33, 66, -109, 107, 94, 80, 41, -7, 64, -99}, -2075020675, 965635255, 0.2195617714066206, new sbyte[] {-105, 3, 80, 39, -99, 74, 90, 27, 59, 1, -115, 44, 111, -91, 28, 118, 110, -85}, 16, 1201648096, 21, 20, 0.47422476108990375, -34808281, 0.4882499049885092, 472297768, 8435822503611394303L, 0, 0.8597744281854867, 0.08467994904274723, 0.34579873f, new sbyte[] {104}, 0.9391819f, 0.5270473f, 0.7547262f, new sbyte[] {9, -107, -29, -43, -30, -92, 103, 111, -18, 71, -4, -90}, 79784985, 8765261912927475818L, -5263412735162964951L, new sbyte[] {43, 56, -46, -125, 81, -3, -102, -15, 127, 8}, 9, new sbyte[] {85, 38, -28, 103, -9, 101, 24, -83, -115},
            };

            results.Count.ShouldEqual(expected.Length);

            for (var i = 0; i < results.Count; ++i)
            {
                if (results[i] is byte[])
                {
                    Assert.Equal(actual: ((byte[])results[i]).Select(b => unchecked((sbyte)b)), expected: (sbyte[])expected[i]);
                }
                else
                {
                    results[i].GetType().ShouldEqual(expected[i].GetType());
                    if (results[i] is double || results[i] is float) 
                    {
                        var difference = Math.Abs((double)((dynamic)results[i] - (dynamic)expected[i]));
                        (difference < 1e-8).ShouldEqual(true);
                    }
                    else
                    {
                        results[i].ShouldEqual(expected[i]);
                    }
                }
            }
        }
        #endregion
    }
}
