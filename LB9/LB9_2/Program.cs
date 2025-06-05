using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введіть шлях до текстового файлу: ");
        string textPath = Console.ReadLine();

        Console.Write("Введіть шлях до файлу зі словами-цензорами: ");
        string censorPath = Console.ReadLine();

        if (!File.Exists(textPath) || !File.Exists(censorPath))
        {
            Console.WriteLine("Один або обидва файли не знайдено.");
            return;
        }

        string text = File.ReadAllText(textPath);
        string[] badWords = File.ReadAllLines(censorPath);

        string censoredText = CensorText(text, badWords);

        Console.WriteLine("\n🛑 Цензурований текст:\n");
        Console.WriteLine(censoredText);
        
        string resultPath = Path.Combine(Path.GetDirectoryName(textPath), "result.txt");
        File.WriteAllText(resultPath, censoredText);
        Console.WriteLine($"\n✅ Результат збережено у файл: {resultPath}");
    }

    static string CensorText(string text, string[] badWords)
    {
        foreach (string word in badWords)
        {
            if (string.IsNullOrWhiteSpace(word))
                continue;

            string cleanWord = word.Trim();
            string pattern = $@"\b{Regex.Escape(cleanWord)}\b";
            string replacement = new string('*', cleanWord.Length);
            text = Regex.Replace(text, pattern, replacement, RegexOptions.IgnoreCase);
        }
        return text;
    }
}