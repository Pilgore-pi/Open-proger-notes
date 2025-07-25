/// <summary>
/// Оценивает, на сколько две строки похожи друг на друга. Оценивается 2 критерия:
/// <br/>- равны ли символы в данной позиции
/// <br/>- количество вставленных или вырезанных символов во второй строке по сравнению с первой
/// </summary>
/// <returns>Число, характеризующее похожесть двух строк</returns>
public static int StringIntersection(string? str1, string? str2) {

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
