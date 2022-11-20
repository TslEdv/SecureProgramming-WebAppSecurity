namespace WebApp.Helpers;

public class DiffieHellmanHelper
{
    public static long GetPrime(long n)
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
}