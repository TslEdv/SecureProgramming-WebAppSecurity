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