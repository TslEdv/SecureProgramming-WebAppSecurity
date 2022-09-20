using ConsoleApp01;

Console.WriteLine("Hello, Cesar Cipher!");

const string defaultAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ!?.";

var alphabet = Helper.GetUserAlphabet(defaultAlphabet);

Console.WriteLine(alphabet);

var shiftAmount = Helper.GetCesarShift(alphabet.Length);

Console.WriteLine($"Real shift amount: {shiftAmount}");

Console.WriteLine("Plaintext: ");
var inputText = Console.ReadLine()?.ToUpper();
var plainText = "";

if (inputText != null)
{
    foreach (var chr in inputText)
    {
        if (alphabet.Contains(chr))
        {
            plainText += chr;
        }
    }
    
    Console.WriteLine("Fixed text: " + plainText);

    var encryptedText = "";
    foreach (var chr in plainText)
    {
        var pos = (alphabet.IndexOf(chr) +
                  shiftAmount) % alphabet.Length;
        encryptedText = encryptedText + alphabet[pos];
    }
    Console.WriteLine("Encrypted text: " + encryptedText);
}