using System;
using System.Collections.Generic;

class WordFrequencyCount
{
    static Dictionary<string, int> CalcWordFreq(string str)
    {
        Dictionary<string, int> hash = new Dictionary<string, int> ();
        string[] words = str.Split (' ');
        foreach (string word in words)
        {
            if (hash.ContainsKey(word))
            {
                hash[word] ++;
            }
            else
            {
                hash.Add(word, 1);
            }
            
        }

        return hash;
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a sentence: ");
        string sentence = Console.ReadLine();
        
        Dictionary<string, int> hash = CalcWordFreq(sentence);
        foreach (KeyValuePair<string, int> kvp in hash)
        {
            Console.WriteLine($"Word: {kvp.Key},  Count: {kvp.Value}");

        }
        

    }
}
