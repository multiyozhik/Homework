using _22_ConvertStrToDbl;

var testStr = "-1.45e256";
Console.WriteLine($"{testStr} => {testStr.ConvertStrToDbl()}");

var testStr1 = "-1..45e256";
Console.WriteLine($"{testStr1} => {testStr1.ConvertStrToDbl()}");


