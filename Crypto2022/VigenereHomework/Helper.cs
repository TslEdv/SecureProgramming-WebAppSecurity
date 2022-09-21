using System.Text;

namespace VigenereHomework;

public class Helper
{
    public static string VigenereEncode(string text, string key)
    {
        var encryptedResult = "";
        for (var i = 0; i < text.Length; i++)
        {
            var encodedKeyChr = key[i % key.Length];
            var encodedChr = (char)(text[i] + encodedKeyChr % 256);
            encryptedResult += encodedChr;
        }

        return encryptedResult;
    }

    public static string VigenereDecode(string encodedText, string key)
    {
        var plainText = "";
        for (var i = 0; i < encodedText.Length; i++)
        {
            var keyChr = key[i % key.Length];
            var plainChr = (char)((encodedText[i] - keyChr + 256) % 256);
            plainText += plainChr;
        }

        return plainText;
    }
    
    public static string VigenereEncodeByteShift(string text, string key)
    {
        var textBytes = Encoding.UTF8.GetBytes(text);
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var encryptedBytes = new byte[textBytes.Length];
        for (var i = 0; i < textBytes.Length; i++)
        {
            var encodedKeyChr = keyBytes[i % keyBytes.Length];
            var encodedChr = textBytes[i];
            
            encryptedBytes[i] = Convert.ToByte((encodedChr + encodedKeyChr) % 255);
        }

        var base64 = Convert.ToBase64String(encryptedBytes);
        return base64;
    }

    public static string VigenereDecodeByteShift(string encodedText, string key)
    {
        var encodedBytes = Convert.FromBase64String(encodedText);
        var keyBytes = Encoding.UTF8.GetBytes(key); 
        var plainBytes = new byte[encodedBytes.Length];
        for (var i = 0; i < encodedBytes.Length; i++)
        {
            var keyChr = keyBytes[i % keyBytes.Length];
            var encodedChr = encodedBytes[i];
            var plainChr = Convert.ToByte((encodedChr - keyChr + 255) % 255);
            plainBytes[i] = plainChr;
        }

        var plainText = Encoding.UTF8.GetString(plainBytes);
        return plainText;
    }

    public static string GetUserText()
    {
        while(true)
        {
            Console.Write("Plaintext: ");
            var plaintext = Console.ReadLine();
            if (plaintext != null)
            {
                return plaintext;
            }
            Console.WriteLine("Incorrect input!");
        }
    }

    public static string GetUserPass()
    {
        while (true)
        {
            Console.Write("Passphrase: ");
            var pass = Console.ReadLine();
            if (pass != null)
            {
                return pass;
            }

            Console.WriteLine("Incorrect input!");
        }
    }
}