using System.Text.RegularExpressions;

class WordStemCounter
{
    static readonly string[] Endings = {
        "сь", "ся",
        "А", "Е", "И", "Й", "О", "У", "Ы", "Ь", "Ю", "Я", 
        "АМ", "АХ", "АЯ", "ЕВ", "ЕЕ", "ЕЙ", "ЕМ", "ЕТ", "ИЕ", "ИИ", "ИЙ", "ИМ", "ИТ", "ИХ", "ИЮ", "ИЯ", "ОБ",
        "АМИ", "ЕГО", "ЕМУ", "ИЕЙ", "ИЕМ", "ИМИ", "ИТЬ", "ИЯМ", "ИЯХ", "ЛСЯ", "ОГО",
        "АЯСЯ", "ЕЕСЯ", "ЕМСЯ", "ЕНИЕ", "ЕНИИ", "ЕНИЙ", "ЕНИЮ", "ЕНИЯ", "ЕТСЯ", "ИЕСЯ",
        "ЕГОСЯ", "ЕНИЕМ", "ЕНИЯМ", "ЕНИЯХ", "ЕШЬСЯ", "ИТЬСЯ", "ОСТЕЙ", "ОСТЬЮ"
    };

    static void Main()
    {
        Console.WriteLine("Введите текст:");
        string input = Console.ReadLine();
        
        var words = Regex.Matches(input.ToLower(), "[а-я]+")
                         .Select(m => m.Value)
                         .ToList();

        Dictionary<string, int> stemCounts = new Dictionary<string, int>();

        foreach (var word in words)
        {
            string stem = GetStem(word);
            if (stemCounts.ContainsKey(stem))
                stemCounts[stem]++;
            else
                stemCounts[stem] = 1;
        }

        Console.WriteLine("\nОсновы и их количество:");
        foreach (var pair in stemCounts.OrderByDescending(x => x.Value))
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }

    static string GetStem(string word)
    {
        string baseStem = word;

        foreach (var ending in Endings)
        {
            if (word.EndsWith(ending))
            {
                baseStem = word.Substring(0, word.Length - ending.Length);
                break;
            }
        }

        int wordLength = word.Length;
        int maxBaseLength = GetMaxBaseLength(wordLength);
        return baseStem.Length > maxBaseLength ? baseStem.Substring(0, maxBaseLength) : baseStem;
    }

    static int GetMaxBaseLength(int wordLength)
    {
        if (wordLength == 1) return 1;
        if (wordLength == 2) return 2;
        if (wordLength == 3) return 3;
        if (wordLength == 4) return 4;
        if (wordLength == 5) return 5;
        if (wordLength == 6) return 6;
        if (wordLength >= 7 && wordLength <= 10) return 7;
        if (wordLength == 11) return 8;
        if (wordLength >= 12 && wordLength <= 14) return 9;
        if (wordLength >= 15 && wordLength <= 17) return 12;
        if (wordLength >= 18 && wordLength <= 20) return 15;
        if (wordLength >= 21 && wordLength <= 23) return 18;
        if (wordLength > 23) return 21;
        return wordLength;
    }
}
