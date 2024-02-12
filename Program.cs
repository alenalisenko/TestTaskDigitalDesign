using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите путь к текстовому файлу:");
        string inputFilePath = Console.ReadLine();

        if (File.Exists(inputFilePath))
        {
            Dictionary<string, int> wordFrequency = CountWordFrequency(inputFilePath);

            string outputFilePath = Path.Combine(Path.GetDirectoryName(inputFilePath), "WordFrequency.txt");
            WriteWordFrequencyToFile(wordFrequency, outputFilePath);

            Console.WriteLine($"Результат сохранен в файле: {outputFilePath}");
        }
        else
        {
            Console.WriteLine("Указанный файл не существует.");
        }
    }

    static Dictionary<string, int> CountWordFrequency(string filePath)
    {
        Dictionary<string, int> wordFrequency = new Dictionary<string, int>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) is not null)
            {
                string[] words = SplitLineIntoWords(line);
                foreach (string word in words)
                {
                    string cleanedWord = CleanWord(word);
                    if (!string.IsNullOrEmpty(cleanedWord))
                    {
                        if (wordFrequency.ContainsKey(cleanedWord))
                        {
                            wordFrequency[cleanedWord]++;
                        }
                        else
                        {
                            wordFrequency[cleanedWord] = 1;
                        }
                    }
                }
            }
        }

        return wordFrequency;
    }

    static string[] SplitLineIntoWords(string line)
    {
        char[] separators = { ' ', '\t', '.', ',', ';', ':', '!', '?', '"', '(', ')', '[', ']', '{', '}' };
        return line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
    }

    static string CleanWord(string word)
    {
        string cleanedWord = new string(word.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray());
        return cleanedWord.ToLower();
    }

    static void WriteWordFrequencyToFile(Dictionary<string, int> wordFrequency, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var entry in wordFrequency.OrderByDescending(pair => pair.Value))
            {
                writer.WriteLine($"{entry.Key}\t\t{entry.Value}");
            }
        }
    }
}
