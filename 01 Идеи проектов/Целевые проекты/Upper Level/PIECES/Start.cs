using System.Text;
using System.Text.RegularExpressions;
using Sandbox;

internal static class Start {

    private static string Duplicate(this string str, int times) {
        var sb = new StringBuilder(str);
        for (int i = 1; i < times; i++) {
            sb.Append(str);
        }
        return sb.ToString();
    }

    private static void Main(string[] args) {
        string filter = string.Empty;

        Console.WriteLine(Material.GetHeaders());
        const string percentPattern = @"\+-(\d{1,3}|\d{1,2}\.\d+)%";
        int lastAccuracy = 0;

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Включить режим ручного поиска? (да/нет)");
        Console.ForegroundColor = ConsoleColor.Yellow;
        bool manualSearch = AnswerIsYes(Console.ReadLine()!);

        bool nonPercentMode = false;
        if (manualSearch) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Включить режим удаления процентов? (да/нет)");
            Console.ForegroundColor = ConsoleColor.Yellow;
            nonPercentMode = AnswerIsYes(Console.ReadLine()!);
        }

        List<Material> unmatchedCons = [];

        if (manualSearch) while (true) {
            try {
                Func<string, string> map = nonPercentMode switch {
                    true => s => Regex.Replace(s, percentPattern, string.Empty),
                    _    => s => s
                };

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Введите фильтр и точность поиска (от 0) <разделитель '|' >:\n" + "".PadLeft(18));
                Console.ForegroundColor = ConsoleColor.Yellow;
                var arguments = Console.ReadLine()!.Split(
                    ['|'],
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
                );

                filter = arguments[0];
                int accuracy = arguments.Length > 1
                    ? lastAccuracy = int.Parse(arguments[1])
                    : lastAccuracy;

                var fact = API.GetMatFact()
                    .Where(m => API.StringIntersection(filter, m.Name) <= accuracy)
                    .OrderByDescending(s => API.StringIntersection(filter, s.Name))
                    .ToArray()
                    ?? throw new NullReferenceException();

                    Console.ForegroundColor = ConsoleColor.Blue;

                    foreach (var f in fact) Console.WriteLine("    " + f);

                    Console.WriteLine($"\n    Found {fact.Length} records; Accuracy: {accuracy}; Filter: {filter}");

            } catch (NullReferenceException) {
                Console.WriteLine($"Factual material record has not been found with specified filter: `{filter}`");
                return;
            } catch (Exception ex) {
                Console.WriteLine("Возникло исключение: " + ex + "\nMESSAGE:" + ex.Message);
            }
        }
        
        else {
            const int accuracy = 20;
            var cons            = API.GetMaterials(API.MAT_CONS_CSV_LOCATION);
            var unfilteredFacts = API.GetMaterials(API.MAT_FACT_CSV_LOCATION);

            Console.WriteLine("Всего конструкторских комплектующих без соответствий: " + cons.Length);
            int i = 0;
            foreach (var con in cons) {
                
                var facts = unfilteredFacts
                       .Where(m => {
                           int upBound = Math.Min(m.Name.Length, 4);
                           var substr = m.Name[0..upBound];

                           int intersection = API.StringIntersection(con.Name, m.Name);
                           return intersection <= accuracy
                            && con.Name.StartsWith(substr, StringComparison.CurrentCultureIgnoreCase);
                       })
                       .OrderBy(s => API.StringIntersection(con.Name, s.Name))
                       .Take(10)
                   ?? throw new NullReferenceException();


                Console.ForegroundColor = ConsoleColor.Yellow;
                if (facts.Any() == false) {
                    unmatchedCons.Add(con);
                    continue;
                }
                string conRepr = $"\n{++i:D3} {con.ToString('=', '=')}";

                Console.WriteLine(conRepr);
                Console.WriteLine("_".Duplicate(conRepr.Length));

                Console.ForegroundColor = ConsoleColor.Blue;
                foreach (var fact in facts) {
                    if (fact.Name?.ToLower() == con.Name?.ToLower()) {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("----------->" + fact);
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    Console.WriteLine(string.Empty.PadLeft(4) + fact);
                }
            }
        }

        Console.WriteLine("\n\nКонструкторские материалы, для которых не найдено похожих фактических материалов:\n");
        for(int i = 0; i < unmatchedCons.Count; i++) {
            string conRepr = $"\n{i:D3} {unmatchedCons[i].ToString('=', '=')}";
            Console.WriteLine(conRepr);
        }

        Console.WriteLine("\nПроцесс завершен");
        Console.ReadLine();
        return;

#if false
var matFact = new[] { fact };
var matCons = API.GetMatCons();

var queryResult =
    from matF in matFact
    from matC in matCons
    where API.StringIntersection(matF.Name, matC.Name) < 10
    select new QueryResult {
        MatFact_Code = matF.Code,
        MatFact_Name = matF.Name,
        MatFact_Gost = matF.Gost,
        MatCons_Code = matC.Code,
        MatCons_Name = matC.Name,
        MatCons_Gost = matC.Gost,
        Delta = API.StringIntersection(matF.Name, matC.Name)
    };

queryResult = queryResult.Take(100);

var queryResultArray = queryResult as QueryResult[] ?? queryResult.ToArray();
File.WriteAllText(API.QUERY_RESULT_CSV_LOCATION, CSV.Serialize(queryResultArray, API.Settings));


foreach (QueryResult rec in queryResultArray)
    Console.WriteLine(rec);
Console.WriteLine("\nQuery result has been successfully written to the CSV file");
#endif

        static bool AnswerIsYes(string userAnswer) => userAnswer.ToLower() is "да" or "yes" or "y" or "+" or "1";
    }
}
