// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

class PalindromeCheck
{
    static void Main()
    {
        Console.WriteLine("Enter a word: ");
        string palindrome = Console.ReadLine();

        if (isPalindrome(palindrome))
        {
            Console.WriteLine("It is a palindrome.");
        }
        else
        {
            Console.WriteLine("It is not a palindrome.");
        }

    }

    static bool isPalindrome(string input)
    {
        string lowerInput = input.ToLower();

        char[] charArray = lowerInput.ToCharArray();
        Array.Reverse(charArray);
        string reversed = new string(charArray);

        return lowerInput == reversed;
    }
}
