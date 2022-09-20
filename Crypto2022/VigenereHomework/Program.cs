using VigenereHomework;

var plaintext = Helper.GetUserText();
var passphrase = Helper.GetUserPass();

var encryptedText = Helper.VigenereEncode(plaintext, passphrase);
Console.WriteLine("Encrypted text: " + encryptedText);

var decryptedText = Helper.VigenereDecode(encryptedText, passphrase);
Console.WriteLine("Decrypted text: " + decryptedText);
