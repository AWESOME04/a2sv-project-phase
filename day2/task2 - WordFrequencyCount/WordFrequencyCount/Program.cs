using System;
using System.Collections.Generic;

class WordFrequencyCount
{
    static void Main()
    {
        Console.WriteLine("Enter a word: ");
        string word = Console.ReadLine();

        Dictionary<char, int> counter = new Dictionary<char, int>();

        foreach (char c in word.ToLower())
        {
            if (char.IsLetter(c))
            {
                if (counter.ContainsKey(c))
                {
                    counter[c]++;
                }
                else
                {
                    counter[c] = 1;
                }
            }
        }

        Console.WriteLine("{");
        foreach (var kvp in counter)
        {
            Console.WriteLine($" '{kvp.Key}': {kvp.Value},");
        }
        Console.WriteLine("}");
    }
}
