using System.Text.RegularExpressions;

namespace _22_ConvertStrToDbl
{
    public static class StringExtension
    {
        public static double ConvertStrToDbl(this string str)
        {
            if (str.Length == 0)
                throw new Exception("Пустую строку невозможно преобразовать в Double");

            var pattern = new Regex(
                @"^(?<sign>[+-]?)(?<intGroup>\d*)[.,]?(?<fractionGroup>\d*)?[eE]?(?<signExp>[+-]?)(?<expGroup>\d+)?$");

            var match = Regex.Match(str, pattern.ToString());
            if (!match.Success)
                throw new Exception($"Строку {str} невозможно преобразовать в Double");

            var sign = match.Groups["sign"]?.Value;
            var intStr = match.Groups["intGroup"]?.Value;
            var fractionStr = match.Groups["fractionGroup"]?.Value;
            var expStr = match.Groups["expGroup"]?.Value;
            var signExp = match.Groups["signExp"]?.Value;

            var koef = (sign is not null && sign == "-") ? -1 : 1;
            var koefExp = (signExp is not null && signExp == "-") ? -1 : 1;

            var intPart = (!string.IsNullOrEmpty(intStr)) ? Parsing(intStr) : 0;
            var fractionPart = (!string.IsNullOrEmpty(fractionStr)) ? Parsing(fractionStr.TrimEnd()) / Math.Pow(10, fractionStr.Length) : 0.0;
            var expPart = (!string.IsNullOrEmpty(expStr)) ? Math.Pow(10, koefExp * Parsing(expStr)) : 1;

            return koef * (intPart + fractionPart) * expPart;
        }
            
        const int encordingDigit0 = '0';

        private static double Parsing(string str)
        {
            var sum = 0.0;           
            foreach( char ch in str ) 
                sum = sum * 10 + (ch - encordingDigit0);
            return sum;
        }
    }
}
