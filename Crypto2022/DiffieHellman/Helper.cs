namespace DiffieHellman;

public class Helper
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

    public static long GetUserG()
    {
        while (true)
        {
            Console.Write("Enter value for Base number: ");
            var input = Console.ReadLine();
            var success = long.TryParse(input, out var value);
            if (success)
            {
                return value;
            }
            Console.WriteLine("Incorrect input!");
        }
    }
    public static long GetUserValueA()
    {
        while (true)
        {
            Console.Write("Enter value for calculation (PersonX): ");
            var input = Console.ReadLine();
            var success = long.TryParse(input, out var value);
            if (success)
            {
                return value;
            }
            Console.WriteLine("Incorrect input!");
        }
    }
    public static long GetUserValueB()
    {
        while (true)
        {
            Console.Write("Enter value for calculation (PersonY): ");
            var input = Console.ReadLine();
            var success = long.TryParse(input, out var value);
            if (success)
            {
                return value;
            }
            Console.WriteLine("Incorrect input!");
        }
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

    public static void BruteForce(long g, long p, long personX)
    {
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        var i = 0;
        while (true)
        {
            if (ModPow(g, i, p) == personX)
            {
                watch.Stop();
                Console.WriteLine($"Found the a - {i}");
                Console.WriteLine($"Time to bruteforce: {watch.ElapsedMilliseconds} ms");
                return;
            }
            i++;
        }
    }
}