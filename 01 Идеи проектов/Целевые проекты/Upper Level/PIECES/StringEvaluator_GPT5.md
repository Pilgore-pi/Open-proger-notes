Ниже — готовый алгоритм на C# 12 и набор unit‑тестов. Он сравнивает строки по словам (границы — пробел и точка), игнорирует регистр, допускает:
- разный порядок слов (но совпадение порядка повышает схожесть),
- вариации пробел/дефис внутри кодов (например, D 25 ↔ D-25 ↔ D25),
- близкие написания слов: опечатка в 1 символ (Левенштейн ≤ 1) и короткие усечения/расширения по суффиксу (например, ВИНТОВ ↔ ВИНТОВОЙ),
- смешение кириллица/латиница для “похожих” букв, в том числе D ↔ Д.

Метод возвращает число 0..1 — показатель различий (0 = идентичны, 1 = максимально различны). В результате также доступна детализация совпавших единиц.

Код алгоритма
```csharp
// File: StringDiffer.cs
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Similarity
{
    public sealed class StringDiffer
    {
        public sealed record CompareResult(
            double Difference,
            int TokensA,
            int TokensB,
            int MatchedUnits,
            double OrderScore,
            IReadOnlyList<Match> Matches);

        public sealed record Match(
            IReadOnlyList<int> AIndices,
            IReadOnlyList<int> BIndices,
            string AText,
            string BText,
            double Similarity);

        private const double ExactScore = 1.0;
        private const double NearScore = 0.85;    // для префиксного совпадения (усечение/расширение)
        private const double Edit1Score = 0.80;   // для Левенштейн ≤ 1
        private const double MinAcceptScore = 0.75;

        public CompareResult Compare(string a, string b)
        {
            var tokensA = Tokenize(a);
            var tokensB = Tokenize(b);

            var candidatesA = BuildCandidates(tokensA);
            var candidatesB = BuildCandidates(tokensB);

            var allPairs = new List<CandidatePair>(capacity: candidatesA.Count * Math.Min(8, candidatesB.Count));

            foreach (var ca in candidatesA)
            {
                foreach (var cb in candidatesB)
                {
                    var sim = TokenSimilarity(ca, cb);
                    if (sim >= MinAcceptScore)
                    {
                        allPairs.Add(new CandidatePair(ca, cb, sim));
                    }
                }
            }

            // Сортировка пар по убыванию схожести, далее — предпочтение 1-1 перед биграмм/биграмм,
            // далее — меньшая дистанция по позиции (чуть поощряем сохранение порядка).
            allPairs.Sort((x, y) =>
            {
                var c = y.Similarity.CompareTo(x.Similarity);
                if (c != 0) return c;

                var spanX = x.A.Indices.Count + x.B.Indices.Count;
                var spanY = y.A.Indices.Count + y.B.Indices.Count;
                c = spanX.CompareTo(spanY);
                if (c != 0) return c;

                var dx = Math.Abs(x.A.Indices[0] - x.B.Indices[0]);
                var dy = Math.Abs(y.A.Indices[0] - y.B.Indices[0]);
                return dx.CompareTo(dy);
            });

            var usedA = new HashSet<int>();
            var usedB = new HashSet<int>();
            var matches = new List<Match>();

            foreach (var pair in allPairs)
            {
                if (pair.A.Indices.Any(i => usedA.Contains(i))) continue;
                if (pair.B.Indices.Any(i => usedB.Contains(i))) continue;

                foreach (var i in pair.A.Indices) usedA.Add(i);
                foreach (var j in pair.B.Indices) usedB.Add(j);

                matches.Add(new Match(
                    pair.A.Indices,
                    pair.B.Indices,
                    pair.A.DisplayText,
                    pair.B.DisplayText,
                    pair.Similarity));
            }

            // Покрытие в "количестве токенов" (учитывая, что биграмма покрывает 2 токена)
            var coveredA = matches.Sum(m => m.AIndices.Count);
            var coveredB = matches.Sum(m => m.BIndices.Count);

            var precision = tokensA.Count == 0 ? 0 : (double)coveredA / tokensA.Count;
            var recall    = tokensB.Count == 0 ? 0 : (double)coveredB / tokensB.Count;
            var f1Coverage = (precision + recall) == 0 ? 0 : 2 * precision * recall / (precision + recall);

            var avgTokenSim = matches.Count == 0 ? 0 : matches.Average(m => m.Similarity);

            // Оценка сохранения порядка: доля LIS для последовательности позиций B в порядке A
            var orderScore = matches.Count == 0 ? 0 :
                LongestIncreasingSubsequenceLength(matches
                    .OrderBy(m => m.AIndices[0])
                    .Select(m => m.BIndices[0]))
                / (double)matches.Count;

            // Итоговая схожесть: покрытие * (бонус за порядок) * (незначительный учет качества совпаданий)
            var similarity = f1Coverage * Lerp(0.5, 1.0, orderScore) * Lerp(0.8, 1.0, avgTokenSim);
            var difference = 1.0 - similarity;

            return new CompareResult(
                Difference: difference,
                TokensA: tokensA.Count,
                TokensB: tokensB.Count,
                MatchedUnits: matches.Count,
                OrderScore: orderScore,
                Matches: matches);
        }

        // ========== Внутренние типы и логика ==========

        private static double Lerp(double a, double b, double t) => a + (b - a) * t;

        private sealed class Token
        {
            public required int Index { get; init; }          // позиция в исходной последовательности токенов
            public required string Text { get; init; }        // нормализованный текст (UPPER)
            public required string Skeleton { get; init; }    // после сопоставления кир/лат
            public required string Collapsed { get; init; }   // Skeleton без дефисов
            public required bool IsDigits { get; init; }      // состоит только из цифр
        }

        private sealed class Candidate
        {
            public required List<int> Indices { get; init; }  // 1 или 2 индекса токенов
            public required string Skeleton { get; init; }    // Для отображения и префиксной логики
            public required string Collapsed { get; init; }   // Для сопоставления кодов
            public required string DisplayText { get; init; } // Для отчетности
        }

        private sealed record CandidatePair(Candidate A, Candidate B, double Similarity);

        private static List<Token> Tokenize(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return new();

            // Границы слов: пробел и точка
            Span<char> buf = s.ToCharArray();
            for (int i = 0; i < buf.Length; i++)
            {
                var c = buf[i];
                // Нормализация некоторых дефисов
                if (c is '\u2010' or '\u2011' or '\u2012' or '\u2013' or '\u2014' or '\u2212') buf[i] = '-';
                if (c == '.') buf[i] = ' ';
            }

            var upper = new string(buf).ToUpperInvariant();
            var raw = upper.Split([' ', '\t', '\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

            var tokens = new List<Token>(raw.Length);
            for (int i = 0; i < raw.Length; i++)
            {
                var t = raw[i].Trim();
                if (t.Length == 0) continue;

                var skeleton = ToLatinSkeleton(t);
                var collapsed = RemoveHyphensAndSpaces(skeleton);
                var isDigits = IsAllDigits(collapsed);

                tokens.Add(new Token
                {
                    Index = tokens.Count,
                    Text = t,
                    Skeleton = skeleton,
                    Collapsed = collapsed,
                    IsDigits = isDigits
                });
            }
            return tokens;
        }

        private static List<Candidate> BuildCandidates(List<Token> tokens)
        {
            var list = new List<Candidate>(tokens.Count * 2);

            // unigram
            foreach (var t in tokens)
            {
                list.Add(new Candidate
                {
                    Indices = [t.Index],
                    Skeleton = t.Skeleton,
                    Collapsed = t.Collapsed,
                    DisplayText = t.Text
                });
            }

            // bigram (для случаев "Д 25" ↔ "D25")
            for (int i = 0; i + 1 < tokens.Count; i++)
            {
                var a = tokens[i];
                var b = tokens[i + 1];
                var sk = a.Skeleton + b.Skeleton;
                var col = a.Collapsed + b.Collapsed;

                list.Add(new Candidate
                {
                    Indices = [a.Index, b.Index],
                    Skeleton = sk,
                    Collapsed = col,
                    DisplayText = $"{a.Text} {b.Text}"
                });
            }

            return list;
        }

        private static string RemoveHyphensAndSpaces(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            var sb = new StringBuilder(s.Length);
            foreach (var c in s)
            {
                if (c != '-' && c != ' ') sb.Append(c);
            }
            return sb.ToString();
        }

        private static bool IsAllDigits(string s)
        {
            if (s.Length == 0) return false;
            for (int i = 0; i < s.Length; i++)
                if (!char.IsDigit(s[i])) return false;
            return true;
        }

        // Приведение кириллицы к "латинскому" скелету для смешанных кодов и похожих букв
        private static string ToLatinSkeleton(string s)
        {
            var sb = new StringBuilder(s.Length);
            foreach (var c in s)
            {
                sb.Append(c switch
                {
                    // Cyrillic -> Latin lookalikes
                    'А' => 'A',
                    'В' => 'B',
                    'С' => 'C',
                    'Е' => 'E',
                    'К' => 'K',
                    'М' => 'M',
                    'Н' => 'H',
                    'О' => 'O',
                    'Р' => 'P',
                    'Т' => 'T',
                    'Х' => 'X',
                    'У' => 'Y',
                    'Ё' => 'E',
                    // Special mapping frequently seen in codes
                    'Д' => 'D',
                    // leave others as-is
                    _ => c
                });
            }
            return sb.ToString();
        }

        private static double TokenSimilarity(Candidate a, Candidate b)
        {
            // Сначала — “коллапсированные” формы (убрали дефисы/пробелы)
            var ac = a.Collapsed;
            var bc = b.Collapsed;

            if (ac.Length == 0 || bc.Length == 0) return 0;

            if (ac == bc) return ExactScore;

            // Чисто числовые коды: совпали/не совпали
            var aDigits = IsAllDigits(ac);
            var bDigits = IsAllDigits(bc);
            if (aDigits && bDigits) return ac == bc ? ExactScore : 0;

            // Префиксная близость (усечение / расширение) для слов >= 4 символов
            if (PrefixClose(ac, bc)) return NearScore;

            // Левенштейн с ограничением d <= 1 для слов >= 4 символов
            int minLen = Math.Min(ac.Length, bc.Length);
            if (minLen >= 4 && LevenshteinAtMostOne(ac, bc)) return Edit1Score;

            return 0;
        }

        private static bool PrefixClose(string a, string b)
        {
            // Совпадение по префиксу, если общий префикс >= 4, а добавочный хвост не слишком длинный
            int common = 0;
            int max = Math.Min(a.Length, b.Length);
            while (common < max && a[common] == b[common]) common++;

            if (common >= 4)
            {
                int tailA = a.Length - common;
                int tailB = b.Length - common;
                if (tailA == 0 && tailB <= 3) return true; // a префикс b
                if (tailB == 0 && tailA <= 3) return true; // b префикс a
            }
            return false;
        }

        // Быстрая проверка “расстояние Левенштейна ≤ 1”
        private static bool LevenshteinAtMostOne(string a, string b)
        {
            if (a == b) return true;

            int la = a.Length, lb = b.Length;
            if (Math.Abs(la - lb) > 1) return false;

            int i = 0, j = 0;
            bool edited = false;

            while (i < la && j < lb)
            {
                if (a[i] == b[j])
                {
                    i++; j++; continue;
                }
                if (edited) return false;
                edited = true;

                if (la == lb)
                {
                    // подстановка
                    i++; j++;
                }
                else if (la > lb)
                {
                    // удаление из a
                    i++;
                }
                else
                {
                    // удаление из b (вставка в a)
                    j++;
                }
            }

            // Хвост из одного символа допустим
            if (!edited && Math.Abs(la - lb) == 1) return true;

            return true;
        }

        // Длина LIS за O(n log n)
        private static int LongestIncreasingSubsequenceLength(IEnumerable<int> sequence)
        {
            var tails = new List<int>();
            foreach (var x in sequence)
            {
                int idx = tails.BinarySearch(x);
                if (idx < 0) idx = ~idx;
                if (idx == tails.Count) tails.Add(x);
                else tails[idx] = x;
            }
            return tails.Count;
        }
    }
}
```

Пример использования
```csharp
var differ = new Similarity.StringDiffer();

var r1 = differ.Compare("АСБЕСТ П-2-30 ХРИЗОТИЛОВЫЙ", "АСБЕСТ А-6К-30 ХРИЗОТИЛОВЫЙ");
// r1.Difference ~ низкое (2 совпавших слова), r1.Matches — детали

var r2 = differ.Compare("ТЕНЗОПРЕОБРАЗОВАТЕЛЬ D25", "ТЕНЗОПРЕОБРАЗОВАТЕЛЬ Д 25");
// Код D25 распознан как одна совпавшая единица (биграмма + униграмма)

var r3 = differ.Compare("КЛЕММНИК ВИНТОВ. 1984769 Phoenix Contact gmbh", "КЛЕМНИК ВИНТОВОЙ 1984769");
// Совпадут 3 единицы: КЛЕМ(М)НИК, ВИНТОВ(ОЙ), 1984769

var r4 = differ.Compare("УСТРОЙСТВО ВИДЕОЗАХВАТА QuadGrabber-Ext AMP", "ПЛАТА ВИДЕОЗАХВАТА QUADGRABBER-EXT в формате");
// Совпадут ВИДЕОЗАХВАТА и QUADGRABBER-EXT

var r5 = differ.Compare("ПОДШИПНИК ПРИБОРНЫЙ 5-1000084", "ПОДШИПНИК 5-1000084");
// Совпадут ПОДШИПНИК и 5-1000084
```

Unit‑тесты (xUnit)

```csharp
// File: StringDifferTests.cs
using Similarity;
using Xunit;

namespace SimilarityTests
{
    public sealed class StringDifferTests
    {
        private readonly StringDiffer _differ = new();

        [Fact]
        public void Asbestos_Codes_Differ_TwoWordsMatch()
        {
            var a = "АСБЕСТ П-2-30 ХРИЗОТИЛОВЫЙ";
            var b = "АСБЕСТ А-6К-30 ХРИЗОТИЛОВЫЙ";

            var r = _differ.Compare(a, b);

            Assert.True(r.MatchedUnits >= 2);
            Assert.True(r.Difference < 0.6); // Достаточно похожи
            Assert.Contains(r.Matches, m => m.AText.Contains("АСБЕСТ"));
            Assert.Contains(r.Matches, m => m.AText.Contains("ХРИЗОТИЛОВЫЙ"));
        }

        [Fact]
        public void Tensotransducer_Code_D25_Merged()
        {
            var a = "ТЕНЗОПРЕОБРАЗОВАТЕЛЬ D25";
            var b = "ТЕНЗОПРЕОБРАЗОВАТЕЛЬ Д 25";

            var r = _differ.Compare(a, b);

            // Совпадают главное слово и код D25
            Assert.True(r.MatchedUnits >= 2);
            Assert.True(r.Difference < 0.4);
            Assert.Contains(r.Matches, m => m.AText.Contains("ТЕНЗОПРЕОБРАЗОВАТЕЛЬ"));
            Assert.Contains(r.Matches, m => m.AText.Contains("D25") || m.BText.Contains("Д 25"));
        }

        [Fact]
        public void Terminal_Shortened_Word_And_Code_Match()
        {
            var a = "КЛЕММНИК ВИНТОВ. 1984769 Phoenix Contact gmbh";
            var b = "КЛЕМНИК ВИНТОВОЙ 1984769";

            var r = _differ.Compare(a, b);

            // Должно совпасть 3 единицы: КЛЕМ(М)НИК, ВИНТОВ(ОЙ), 1984769
            Assert.True(r.MatchedUnits >= 3);
            Assert.True(r.Difference < 0.4);
            Assert.Contains(r.Matches, m => m.AText.StartsWith("КЛЕМ"));
            Assert.Contains(r.Matches, m => m.AText.StartsWith("ВИНТОВ"));
            Assert.Contains(r.Matches, m => m.AText.Contains("1984769") || m.BText.Contains("1984769"));
        }

        [Fact]
        public void VideoCapture_And_Grabber_Match()
        {
            var a = "УСТРОЙСТВО ВИДЕОЗАХВАТА QuadGrabber-Ext AMP";
            var b = "ПЛАТА ВИДЕОЗАХВАТА QUADGRABBER-EXT в формате";

            var r = _differ.Compare(a, b);

            Assert.True(r.MatchedUnits >= 2);
            Assert.Contains(r.Matches, m => m.AText.Contains("ВИДЕОЗАХВАТА"));
            Assert.Contains(r.Matches, m => m.AText.ToUpperInvariant().Contains("QUADGRABBER-EXT")
                                          || m.BText.ToUpperInvariant().Contains("QUADGRABBER-EXT"));
            Assert.True(r.Difference < 0.6);
        }

        [Fact]
        public void Bearing_With_Or_Without_Attribute_MatchesTwo()
        {
            var a = "ПОДШИПНИК ПРИБОРНЫЙ 5-1000084";
            var b = "ПОДШИПНИК 5-1000084";

            var r = _differ.Compare(a, b);

            Assert.True(r.MatchedUnits >= 2);
            Assert.Contains(r.Matches, m => m.AText.Contains("ПОДШИПНИК"));
            Assert.Contains(r.Matches, m => m.AText.Contains("5-1000084") || m.BText.Contains("5-1000084"));
            Assert.True(r.Difference < 0.5);
        }

        [Fact]
        public void OrderMatters_BonusForSameOrder()
        {
            var a = "A B C D";
            var b1 = "A B C D";   // тот же порядок
            var b2 = "A C B D";   // перемена мест B и C

            var r1 = _differ.Compare(a, b1);
            var r2 = _differ.Compare(a, b2);

            Assert.True(r1.Difference < r2.Difference, "Строки с тем же порядком должны быть ближе.");
            Assert.True(r1.MatchedUnits >= 4);
            Assert.True(r2.MatchedUnits >= 4);
        }

        [Fact]
        public void HyphenSpaceVariantsOfCodes()
        {
            var a = "D-25 X-100";
            var b = "D 25 X100";

            var r = _differ.Compare(a, b);

            Assert.True(r.MatchedUnits >= 2);
            Assert.True(r.Difference < 0.4);
        }

        [Fact]
        public void EmptyAndWhitespace()
        {
            var r = _differ.Compare("", "   ");
            Assert.Equal(1.0, r.Difference); // Нечего сравнивать — считаем максимально различными
            Assert.Equal(0, r.MatchedUnits);
        }
    }
}
```

Пояснения к метрике
- Токенизация: разделители — пробелы и точки; дефис остается частью токена.
- Нормализация: все в верхний регистр, унификация похожих кириллических и латинских букв (включая Д→D), сравнение по “скелету”.
- Для распознавания кодов формата “буквы+цифры” с вариативными разделителями генерируются биграммы (слияние соседних токенов) и сравниваются с униграммами другой стороны.
- Похожие слова учитываются по двум правилам: (1) общий префикс ≥ 4 и хвост ≤ 3 символов; (2) расстояние Левенштейна ≤ 1 для строк длиной ≥ 4.
- Итоговая схожесть = F1 по покрытию токенов × бонус за порядок (по доле LIS) × небольшой множитель среднего качества совпаданий; показатель различий = 1 − схожесть.

Если захотите, можно подправить пороги NearScore/Edit1Score/MinAcceptScore под ваши данные (например, сделать более строгими или мягкими).
