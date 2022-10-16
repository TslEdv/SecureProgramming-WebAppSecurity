using System.Globalization;
using RSA;

var p = Helper.GetUserP();
var g = Helper.GetUserP();

var n = p*g;
if (n < 255)
{
    Console.WriteLine("N is too small!");
    Environment.Exit(0);
}
var m = (p-1) * (g-1);
var e = Helper.GetValueE(m);

var d = Helper.GetValueD(m, e);

Console.WriteLine("Value e: " + e);
Console.WriteLine("Value d: " + d);

Helper.RsaBruteForce(n,e);

var plainText = Helper.GetUserText();
var encryptedText = Helper.RsaEncryption(plainText, e, n);
foreach (var number in encryptedText)
{
    Console.WriteLine(number.ToString());
}
var decryptedText = Helper.RsaDecryption(encryptedText, d, n);
Console.WriteLine(decryptedText);
