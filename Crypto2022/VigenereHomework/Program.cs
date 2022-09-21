using VigenereHomework;

var plaintext = Helper.GetUserText();
var passphrase = Helper.GetUserPass();

var encryptedText = Helper.VigenereEncode(plaintext, passphrase);
Console.WriteLine("Encrypted text with char into int: " + encryptedText);

var decryptedText = Helper.VigenereDecode(encryptedText, passphrase);
Console.WriteLine("Decrypted text with char into int: " + decryptedText);

var encryptedTextB = Helper.VigenereEncodeByteShift(plaintext, passphrase);
Console.WriteLine("Encrypted text with byte shift into base64: " + encryptedTextB);

var decryptedTextB = Helper.VigenereDecodeByteShift(encryptedTextB, passphrase);
Console.WriteLine("Decrypted text with byte shift into base64: " + decryptedTextB);