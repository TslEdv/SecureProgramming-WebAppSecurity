using System.Text;

namespace WebApp.Helpers;

public class CesarHelper
{
    private static int Mod(int x, int m) {
        return (x%m + m)%m;
    }
    public static int GetCesarShift(int shiftAmount)
    {
        shiftAmount = Mod(shiftAmount, 255);
        return shiftAmount;
    }
    public static string CesarEncodeByteShift(string text, int shiftAmount)
    {
        var textBytes = Encoding.UTF8.GetBytes(text);
        var encryptedBytes = new byte[textBytes.Length];
        for (var i = 0; i < textBytes.Length; i++)
        {
            var encodedChr = textBytes[i];
            
            encryptedBytes[i] = Convert.ToByte((encodedChr + shiftAmount) % 255);
        }

        var base64 = Convert.ToBase64String(encryptedBytes);
        return base64;
    }
    public static string CesarDecodeByteShift(string encodedText, int shiftAmount)
    {
        var encodedBytes = Convert.FromBase64String(encodedText);
        var plainBytes = new byte[encodedBytes.Length];
        for (var i = 0; i < encodedBytes.Length; i++)
        {
            var encodedChr = encodedBytes[i];
            var plainChr = Convert.ToByte((encodedChr - shiftAmount + 255) % 255);
            plainBytes[i] = plainChr;
        }

        var plainText = Encoding.UTF8.GetString(plainBytes);
        return plainText;
    }
}