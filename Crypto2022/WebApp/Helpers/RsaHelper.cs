using System.Text;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Helpers;

public class RsaHelper
{
    private static long GetPrime(long n)
    {
        // Check for input prime
        if (IsPrime(n))
        {
            return n;
        }
        // All prime numbers are odd except two
        if (n % 2 != 0)
        {
            n -= 2;
        }

        else
            n--;

        for (var i = n; i >= 2; i -= 2)
        {
            if (IsPrime(i))
            {
                return i;
            }
        }

        // It will only be executed when n is too small
        return 0;
    }
    
    private static bool IsPrime(long number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (long)Math.Floor(Math.Sqrt(number));
          
        for (long i = 3; i <= boundary; i += 2)
            if (number % i == 0)
                return false;
    
        return true;        
    }
    private static long ModPow(long nmb, long pow, long mod)
    {
        long result = 1;
        for (long i = 0; i < pow; i++)
        {
            result *= nmb % mod;
            result %= mod;
        }

        return result;
    }
    
    private static long GetValueE(long m)
    {
        long ans;
        long e = 2;
        while (true)
        {
            ans = gcd(m, e);
            if (ans == 1)
            {
                break;
            }

            e++;
        }
        return e;
    }
    
    private static long gcd(long a, long b) 
    { 
        if (a == 0) 
            return b; 
        return gcd(b % a, a); 
    }

    public static long CheckPrimesP(long p, long q)
    {
        p = GetPrime(p);
        q = GetPrime(q);
        var n = p*q;
        if (n < 255 || p == q)
        {
            var random = new Random();
            var lowerBound = 100;
            var upperBound = 1001;
            var rNum = random.Next(lowerBound, upperBound);
            p = GetPrime(rNum);
        }
        return p;
    }
    
    public static long CheckPrimesQ(long p, long q)
    {
        p = GetPrime(p);
        q = GetPrime(q);
        var n = p*q;
        if (n < 255 || p == q)
        {
            var random = new Random();
            var lowerBound = 100;
            var upperBound = 1001;
            var rNum = random.Next(lowerBound, upperBound);
            q = GetPrime(rNum);
        }
        return q;
    }
    private static long GetValueD(long m, long e)
    {
        double d;
        long k = 0;
        while (true)
        {
            var interm = k * m;
            d = (double)(1 + interm) / e;
            if (d % 1 == 0)
            {
                break;
            }

            k++;
        }

        return (long)d;
    }
    
    public static string RsaEncryption(string text, long p, long q)
    {
        var n = p*q;
        var m = (p-1) * (q-1);
        var e = GetValueE(m);
        var textBytes = Encoding.UTF8.GetBytes(text);
        var encryptedBytes = new long[textBytes.Length];
        for (var i = 0; i < textBytes.Length; i++)
        {
            var encodedChr = textBytes[i];
            var encryptedChr = ModPow(encodedChr, e, n);
            
            encryptedBytes[i] = encryptedChr;
        }

        var stringValue = "";
        foreach (var encryptedByte in encryptedBytes)
        {
            stringValue += encryptedByte + ",";
        }
        var encodedTextBytes = Encoding.UTF8.GetBytes(stringValue);
        var base64 = Convert.ToBase64String(encodedTextBytes);
        return base64;
    }

    public static string RsaDecryption(string encodedTextBase64, long p, long q)
    {
        var n = p*q;
        var m = (p-1) * (q-1);
        var e = GetValueE(m);
        var d = GetValueD(m, e);

        var decodeBytes = Convert.FromBase64String(encodedTextBase64);
        var decodedString = Encoding.UTF8.GetString(decodeBytes);
        var decodedList = decodedString.Split(",").Where(n => !string.IsNullOrEmpty(n)).ToList();
        var indx = 0;
        long[] encodedText = new long[decodedList.Count];
        foreach (var number in decodedList)
        {
            encodedText[indx] = long.Parse(number);
            indx++;
        }

        
        var plainBytes = new byte[encodedText.Length];
        for (var i = 0; i < encodedText.Length; i++)
        {
            var encodedChr = encodedText[i];
            var decryptedChr = ModPow(encodedChr, d, n);
            var plainChr = Convert.ToByte(decryptedChr % 255);
            plainBytes[i] = plainChr;
        }

        var plainText = Encoding.UTF8.GetString(plainBytes);
        return plainText;
    }
}