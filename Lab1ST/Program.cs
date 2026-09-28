using System;

class Program
{
    static int Minimum(int a, int b) => a < b ? a : b;
    static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;
    static int Minimum(int a, int b, int c, int d) => Minimum(Minimum(a, b), Minimum(c, d));
    static int DamerauLevenshteinDistance(string firstText, string secondText)
    {
        if (firstText == null) throw new ArgumentNullException(nameof(firstText));
        if (secondText == null) throw new ArgumentNullException(nameof(secondText));

        int n = firstText.Length;
        int m = secondText.Length;

        var d = new int[n + 1, m + 1];

        for (int i = 0; i <= n; i++) d[i, 0] = i;
        for (int j = 0; j <= m; j++) d[0, j] = j;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = firstText[i - 1] == secondText[j - 1] ? 0 : 1;

                d[i, j] = Minimum(
                    d[i - 1, j] + 1,
                    d[i, j - 1] + 1,
                    d[i - 1, j - 1] + cost
                );

                if (i > 1 && j > 1
                    && firstText[i - 1] == secondText[j - 2]
                    && firstText[i - 2] == secondText[j - 1])
                {
                    d[i, j] = Minimum(d[i, j], d[i - 2, j - 2] + cost);
                }
            }
        }

        return d[n, m];
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Примеры работы алгоритма ===\n");

        var examples = new (string a, string b)[]
        {
            ("кот", "кто"),
            ("кот", "код"),
            ("кот", "котик"),
            ("кот", "от"),
            ("abcdef", "abcfde"),
            ("hello", "world"),
            ("", "abc"),
            ("тест", "тест")
        };

        foreach (var (a, b) in examples)
        {
            int distance = DamerauLevenshteinDistance(a, b);
            Console.WriteLine($"\"{a}\" → \"{b}\" : расстояние = {distance}");
        }

        Console.WriteLine("\n=== Интерактивный режим ===");
        Console.Write("Первое слово: ");
        string? w1 = Console.ReadLine() ?? string.Empty;

        Console.Write("Второе слово: ");
        string? w2 = Console.ReadLine() ?? string.Empty;

        Console.WriteLine(
            $"Расстояние Дамерау-Левенштейна между \"{w1}\" и \"{w2}\": " +
            $"{DamerauLevenshteinDistance(w1, w2)}");

        Console.WriteLine("\nНажмите Enter для выхода...");
        Console.ReadLine();
    }
}