class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введіть повний шлях до файлу: ");
        string path = Console.ReadLine();

        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не знайдено!");
            return;
        }

        string text = File.ReadAllText(path);

        int sentenceCount = CountSentences(text);
        int upperCount = 0, lowerCount = 0, vowelCount = 0, consonantCount = 0, digitCount = 0;

        foreach (char c in text)
        {
            if (char.IsUpper(c)) upperCount++;
            else if (char.IsLower(c)) lowerCount++;

            if (IsVowel(c)) vowelCount++;
            else if (IsConsonant(c)) consonantCount++;

            if (char.IsDigit(c)) digitCount++;
        }

        Console.WriteLine("\n📊 Статистика:");
        Console.WriteLine($"- Кількість речень: {sentenceCount}");
        Console.WriteLine($"- Великих літер: {upperCount}");
        Console.WriteLine($"- Маленьких літер: {lowerCount}");
        Console.WriteLine($"- Голосних літер: {vowelCount}");
        Console.WriteLine($"- Приголосних літер: {consonantCount}");
        Console.WriteLine($"- Цифр: {digitCount}");
    }

    static int CountSentences(string text)
    {
        char[] sentenceEndings = { '.', '!', '?' };
        int count = 0;
        foreach (char c in text)
        {
            if (Array.Exists(sentenceEndings, end => end == c))
                count++;
        }
        return count;
    }

    static bool IsVowel(char c)
    {
        return "AEIOUYАЕЄИІЇОУЮЯaeiouyаеєиіїоуюя".IndexOf(c) >= 0;
    }

    static bool IsConsonant(char c)
    {
        return char.IsLetter(c) && !IsVowel(c);
    }
}