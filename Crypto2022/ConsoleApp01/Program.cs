using ConsoleApp01;

Console.WriteLine("Hello, Cesar Cipher!");

const string defaultAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ!?.";

var alphabet = Helper.GetUserAlphabet(defaultAlphabet);

Console.WriteLine(alphabet);

var shiftAmount = Helper.GetCesarShift(alphabet.Length);

Console.WriteLine($"Real shift amount: {shiftAmount}");