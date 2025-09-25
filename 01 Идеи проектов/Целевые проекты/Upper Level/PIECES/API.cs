using System.Text;
using CSVFile;

namespace Sandbox;

public static class API {

    static API() {
        Settings = new() {
            Encoding = Encoding.UTF8,
            AllowNull = false,
            FieldDelimiter = ';',
            DateTimeFormat = "dd.MM.yyyy HH:mm:ss",
            HeaderRowIncluded = true,
            HeadersCaseSensitive = true,
            IgnoreEmptyLineForDeserialization = true,
        };
    }

    public static readonly CSVSettings Settings;

    /// <summary>
    /// Оценивает, на сколько две строки похожи друг на друга. Оценивается 2 критерия:
    /// <br/>- равны ли символы в данной позиции
    /// <br/>- количество вставленных или вырезанных символов во второй строке по сравнению с первой
    /// <br/><br/>Символы в нижнем и верхнем регистре воспринимаются одинаково
    /// </summary>
    /// <returns>Число, характеризующее похожесть двух строк</returns>
    public static int StringIntersection(string? str1, string? str2) {

        str1 = str1?.ToLower();
        str2 = str2?.ToLower();

        if (str1 == str2) return 0;
        if ((str1 == null) ^ (str2 == null)) return int.MaxValue;

        int len1 = str1!.Length;
        int len2 = str2!.Length;

        // Создаем матрицу для хранения значений расстояний между префиксами строк
        int[,] dp = new int[len1 + 1, len2 + 1];

        // Инициализация краёв
        for (int i = 0; i <= len1; i++) dp[i, 0] = i; // удаление i символов
        for (int j = 0; j <= len2; j++) dp[0, j] = j; // вставка j символов

        // Заполнение матрицы
        for (int i = 1; i <= len1; i++) {
            for (int j = 1; j <= len2; j++) {
                int cost = (str1[i - 1] == str2[j - 1]) ? 0 : 1;

                dp[i, j] = Math.Min(
                    Math.Min(
                        dp[i - 1, j] + 1,     // удаление
                        dp[i, j - 1] + 1      // вставка
                    ),
                    dp[i - 1, j - 1] + cost   // замена или совпадение
                );
            }
        }

        return dp[len1, len2];
    }

    public static Material[] GetMatFact() {
        return CsvToMaterials(File.ReadAllText(MAT_FACT_CSV_LOCATION));
    }

    public static Material[] GetMatCons() {
        return CsvToMaterials(File.ReadAllText(MAT_CONS_CSV_LOCATION));
    }

    public static Material[] GetUnpairedComponentsCons() { //// 0.7k
        return CsvToMaterials(File.ReadAllText(MAT_UNPAIRED_COMPONENTS_LOCATION));
    }

    public static Material[] GetUnpairedSecondaryMaterialsCons() { //// 0.3k
        return CsvToMaterials(File.ReadAllText(MAT_UNPAIRED_SECONDARY_LOCATION));
    }

    public static Material[] GetUnpairedMiscMaterialsCons() { //// 0.17k
        return CsvToMaterials(File.ReadAllText(MAT_UNPAIRED_MISC_LOCATION));
    }

    public static Material[] GetUnpairedMainMaterialsCons() { //// 3k
        return CsvToMaterials(File.ReadAllText(MAT_UNPAIRED_MAIN_LOCATION));
    }

    public static Material[] CsvToMaterials(string csvText) {
        return CSV.Deserialize<Material>(csvText, Settings).ToArray();
    }

    public static Material[] GetMaterials(string path) {
        return CSV.Deserialize<Material>(File.ReadAllText(path), Settings).ToArray();
    }

    public const string MAT_UNPAIRED_COMPONENTS_LOCATION = @"C:\Users\UnpairedConMaterials.csv";
    public const string MAT_UNPAIRED_SECONDARY_LOCATION  = @"C:\Users\UnpairedSecMaterials.csv";
    public const string MAT_UNPAIRED_MISC_LOCATION       = @"C:\Users\UnpairedMiscMaterials.csv";
    public const string MAT_UNPAIRED_MAIN_LOCATION       = @"C:\Users\UnpairedMainMaterials.csv";
    public const string MAT_FACT_CSV_LOCATION            = @"C:\Users\AllFactMaterials.csv";
    public const string MAT_CONS_CSV_LOCATION            = @"C:\Users\ConsSecondaryMaterials.csv";
    public const string QUERY_RESULT_CSV_LOCATION        = @"C:\Users\QueryResult.csv";
}
