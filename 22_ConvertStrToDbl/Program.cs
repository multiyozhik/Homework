//потренироваться написать на метод модульные тесты
//м.б. слишком длинная строка, м.б. выход за Double.Max(), Min()

using System.Text.RegularExpressions;

double ConvertStrToDbl(string str)
{
    var parsingStringKoef = 1;
    var parsingString = GetParsingStr(str, out parsingStringKoef);

    var regex1 = new Regex(@"^-?\d+$"); //-12 целая часть
    var regex2 = new Regex(@"^-?\d*[.,]\d*$"); //-5.03 вещественная часть

    bool IsExp = ContainsExp(parsingString);
    if (!IsExp)
    {
        try
        {
            if (regex1.IsMatch(parsingString))
                return parsingStringKoef * ParseStr(parsingString);
            if (regex2.IsMatch(parsingString))
            {
                var index = str.IndexOfAny(new char[] { '.', ',' });
                var startSubstr = parsingString.Substring(0, index);
                var endSubstring = parsingString.Substring(index + 1);
                var intNumber = ParseStr(startSubstr);
                                
                var fractionNumber = ParseFractionStr(endSubstring);

                return parsingStringKoef * (intNumber + fractionNumber);
            }
            else
            {
                throw new Exception("Невозможно преобразовать строку в число");
            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        } 
    }
    else
    {
        var index = str.IndexOfAny(new char[] { 'e', 'E' });
        var startSubstr = parsingString.Substring(0, index - 1);
        var endSubstring = parsingString.Substring(index + 1, parsingString.Length - 1);

        int fractionKoef = 1;
        endSubstring = GetParsingStr(endSubstring, out fractionKoef);

        var intNumber = ParseStr(startSubstr);
        var degreeNumber = ParseStr(endSubstring);

        if (fractionKoef == 1)
            return intNumber * Math.Pow(10, degreeNumber);
        else
            return intNumber * Math.Pow(10, -degreeNumber);
    }


    //метод, который определяет положительное или отрицательное число и возвращает строку,
    //которую уже нужно парсить
    string GetParsingStr(string str, out int parsingStrKoef)
    {
        parsingStrKoef = 1;
        if (str.StartsWith("-"))
            parsingStrKoef = -1;      
        if (str.StartsWith("-") || str.StartsWith("+"))       
            return str[1..];     
        else
            return str;
    }

    //метод, который возвращает, число в экспоненциальной записи или нет
    bool ContainsExp(string str)
    {       
        var regex = new Regex(@"^-?\d*([.,]\d*)?[eE]-?\d+$");
        var contains = regex.IsMatch(str);
        return contains;
    }

    //метод парсит целую часть числа
    double ParseStr(string str)
    {
        var number = 0.0;
        var strLenght = str.Length;

        foreach (char ch in str)
        {
            strLenght--;
            if (char.IsDigit(ch))
            {
                var ch0 = '0';
                var encordingDigit0 = (int)ch0;
                var encordingDigit = (int)ch;
                var currentDigit = encordingDigit - encordingDigit0;
                number += currentDigit * Math.Pow(10, strLenght);
            }
        }
        return number;
    }

    //метод парсит дробную часть числа
    double ParseFractionStr(string str)
    {
        var number = 0.0;
        var strLenght = 0;

        foreach (char ch in str)
        {
            strLenght++;
            if (char.IsDigit(ch))
            {
                var ch0 = '0';
                var encordingDigit0 = (int)ch0;
                var encordingDigit = (int)ch;
                var currentDigit = encordingDigit - encordingDigit0;
                number += currentDigit / Math.Pow(10, strLenght);
            }            
        }
        return number;
    }
}

Console.WriteLine($"{ ConvertStrToDbl("523")} - { Convert.ToDouble("523")}");
Console.WriteLine($"{ConvertStrToDbl("-12")} - {Convert.ToDouble("-12")}");
Console.WriteLine($"{ConvertStrToDbl("14.564")} - {Convert.ToDouble("14.564")}");
Console.WriteLine($"{ConvertStrToDbl("-75.100")} - {Convert.ToDouble("-75.100")}");
Console.WriteLine($"{ConvertStrToDbl("5.02e+02")} - {Convert.ToDouble("5.02e+02")}");
Console.WriteLine($"{ConvertStrToDbl("-6e-003")} - {Convert.ToDouble("-6e-003")}");
Console.WriteLine($"{ConvertStrToDbl("06e06")} - {Convert.ToDouble("06e06")}");

