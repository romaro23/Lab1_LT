using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        string text = File.ReadAllText("file_in.txt");
        string[] words = text.Split(new char[] { ' ', ',', '.', '!', '?', '\r', '\n', '(', ')', '-', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);

        var wordsWithUniqueLetters = FindWordsWithUniqueLetters(words);

        foreach (var pair in wordsWithUniqueLetters)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
            File.AppendAllText("file_out.txt", $"{pair.Key}: {pair.Value}\n");
        }
    }

    static Dictionary<string, int> FindWordsWithUniqueLetters(string[] words)
    {
        Dictionary<string, int> wordCounts = new Dictionary<string, int>();

        foreach (string word in words)
        {
            int uniqueLetterCount = word.Distinct().Count();

            if (!wordCounts.ContainsKey(word))
            {
                wordCounts[word] = uniqueLetterCount;
            }
        }

        int maxCount = wordCounts.Max(kv => kv.Value);
        var wordsWithMaxCount = wordCounts.Where(kv => kv.Value == maxCount);

        return wordsWithMaxCount.ToDictionary(kv => kv.Key, kv => kv.Value);
    }
}
