using System.Collections;
using System.Text;

namespace RSA;

public class Helper
{
    public static long GetUserP()
    {
        while(true)
        {
            Console.Write("Enter value for prime (if number inserted is not a prime, will take the closest prime): ");
            var input = Console.ReadLine();
            var success = long.TryParse(input, out var p);
            if (success)
            {
                p = GetPrime(p);
                if (p == 0)
                {
                    Console.WriteLine("Number to find prime is too small!");
                }
                else
                {
                    Console.WriteLine($"Prime number is: {p}");
                    return p;
                }
            }
            Console.WriteLine("Incorrect input!");
        }
    }
    
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

    public static long ModPow(long nmb, long pow, long mod)
    {
        long result = 1;
        for (long i = 0; i < pow; i++)
        {
            result *= nmb % mod;
            result %= mod;
        }

        return result;
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

    public static long GetValueE(long m)
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

    public static long GetValueD(long m, long e)
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
    private static long gcd(long a, long b) 
    { 
        if (a == 0) 
            return b; 
        return gcd(b % a, a); 
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

    public static long[] RsaEncryption(string text, long e, long n)
    {
        var textBytes = Encoding.UTF8.GetBytes(text);
        var encryptedBytes = new long[textBytes.Length];
        for (var i = 0; i < textBytes.Length; i++)
        {
            var encodedChr = textBytes[i];
            var encryptedChr = ModPow(encodedChr, e, n);
            
            encryptedBytes[i] = encryptedChr;
        }
        return encryptedBytes;
    }

    public static string RsaDecryption(long[] encodedText, long d, long n)
    {
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

    public static void RsaBruteForce(long n, long e)
    {
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        var limit = Math.Sqrt(n);
        var biggestPrime = GetPrime((long)limit);
        var primeList = GetPrimesInRange(biggestPrime);
        foreach (var prime1 in primeList)
        {
            var p = prime1;
            var test = (double) n / p;
            if (test % 1 == 0)
            {
                long q = (long)test;
                if (IsPrime(q))
                {
                    var m = (p-1) * (q-1);
                    watch.Stop();
                    Console.WriteLine($"Found the d - {GetValueD(m, e)}");
                    Console.WriteLine($"Time to bruteforce: {watch.ElapsedMilliseconds} ms");
                    return;
                }
            }

        }
    }
    private static List<long> GetPrimesInRange(long num) 
    {
        List<long> primes = new List<long>();
        primes.Add(2);
        for (long i = 1; i <= num; i = i + 2)
        {
            if (IsPrime(i) == true && i > 1)
            {
                primes.Add(i);
            }
        }
        return primes;
    }
}