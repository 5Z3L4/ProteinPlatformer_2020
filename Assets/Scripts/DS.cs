using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

public static class DS
{
    private static System.Random random = new System.Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public static string RandomSeed(int length)
    {
        const string chars = "ADEFHIJNOPQSTUVYabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public static int DecryptScore(string score)
    {
        if (string.IsNullOrEmpty(score))
        {
            return 0;
        }
        string newScore = score.ToString();
        newScore = Regex.Replace(newScore, @"[^XZCMKRGBLW]", "");
        newScore = new StringBuilder(newScore)
            .Replace("X", "1")
            .Replace("Z", "2")
            .Replace("C", "3")
            .Replace("M", "4")
            .Replace("K", "5")
            .Replace("R", "6")
            .Replace("G", "7")
            .Replace("B", "8")
            .Replace("L", "9")
            .Replace("W", "0")
            .ToString();
        if (string.IsNullOrEmpty(newScore))
        {
            return 0;
        }
        return Convert.ToInt32(newScore);
    }
    public static string EncryptScore(int score)
    {
        string newScore = score.ToString();
        newScore = new StringBuilder(newScore)
            .Replace("1", "X")
            .Replace("2", "Z")
            .Replace("3", "C")
            .Replace("4", "M")
            .Replace("5", "K")
            .Replace("6", "R")
            .Replace("7", "G")
            .Replace("8", "B")
            .Replace("9", "L")
            .Replace("0", "W")
            .ToString();
        newScore = RandomizeString(newScore);
        return newScore;
    }

    private static string RandomizeString(string newScore)
    {
        List<char> charList = new List<char>();
        string firstChars = RandomSeed(random.Next(6));
        foreach (var character in firstChars)
        {
            charList.Add(character);
        }
        foreach (var letter in newScore)
        {
            if (random.Next(10) % 3 != 0)
            {
                string newChars = RandomSeed(random.Next(6));
                foreach (var character in newChars)
                {
                    charList.Add(character);
                }
            }
            charList.Add(letter);
        }
        string lastChars = RandomSeed(random.Next(6));
        foreach (var character in lastChars)
        {
            charList.Add(character);
        }
        return string.Join("",charList);
    }
}

