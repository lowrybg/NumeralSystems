/*
Problem 7. One system to any other

    Write a program to convert from any numeral system of given base s to any other numeral system of base d (2 ≤ s, d ≤ 16).
*/
using System;
using System.Linq;
class Base
{
  
    static void Main()
    {
        Console.Write("Number's base s: ");
        int baseS = int.Parse(Console.ReadLine());
        if (baseS < 2) throw new ArgumentOutOfRangeException();
        Console.Write("\nEnter a non-negative integer number [base {0}]: ", baseS);
        string number = StringParse();
        Console.Write("\nConvert to base D: ");
        int baseD = int.Parse(Console.ReadLine());
        if (baseD < 2 || baseD > 16) throw new ArgumentOutOfRangeException();
        string result = ConvertFromDecimalToBaseY(ConvertToDecimal(number.ToArray(), baseS), baseD);
        if (IsValidInput(number, result, baseS, baseD))
        {
            Console.Write("\nResult -> {0} [base {1}] converted to [base {2}] => {3}\n\n", number, baseS, baseD, result);
        }
        else
        {
            Console.WriteLine("\n-> You have entered an invalid number!\n");
        }
    }
    static string StringParse()
    {
        string number = Console.ReadLine();
        // Check for incorrect number
        for (int i = 0; i < number.Length; i++)
            if (number[i] < 'A' && number[i] > 'Z' && number[i] < 'a' && number[i] > 'z' && number[i] < '0' && number[i] > '9')
                throw new ArgumentException();
        number = MakeAllLettersLarge(number);
        return number;
    }
    // aff == AFF => valid input number
    static string MakeAllLettersLarge(string number)
    {
        char[] digits = number.ToArray();
        for (int i = 0; i < digits.Length; i++)
            digits[i] = char.ToUpper(number[i]);
        return string.Join("", digits);
    }
    #region [Essential Part - Conversion]
    // Convert number [base X] to number [base 10]
    static int ConvertToDecimal(char[] number, int baseX)
    {
        int result = 0;
        for (int i = number.Length - 1, pow = 1; i >= 0; i--, pow *= baseX)
            result += (number[i] >= 'A') ? (number[i] - 'A' + 10) * pow : (number[i] - '0') * pow;
        return result;
    }
    // Convert number [base 10] to number [base Y]
    static string ConvertFromDecimalToBaseY(int number, int baseY)
    {
        string result = string.Empty;
        while (number > 0)
        {
            int remainder = number % baseY;
            result = remainder >= 10 ? (char)('A' + remainder - 10) + result : remainder + result;
            number /= baseY;
        }
        return result;
    }
    #endregion
    // Convert the result from BaseY to baseX and compare the new result with the old result (baseX to baseY)
    static bool IsValidInput(string number, string result, int baseX, int baseY)
    {
        return string.Compare(ConvertFromDecimalToBaseY(ConvertToDecimal(result.ToArray(), baseY), baseX), number) == 0 ? true : false;
    }
}