namespace ConsoleApp01;

public static class Helper
{
    public static string GetUserAlphabet(string defaultAlphabet)
    {
        bool validInput;
        string? userAlphabet;
        do
        {
            validInput = true;
            Console.WriteLine($"Input alphabet (enter for default) [{defaultAlphabet}]");

            userAlphabet = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userAlphabet) ||
                userAlphabet.Length == 1)
            {
                userAlphabet = defaultAlphabet;
            }

            var charHashset = new HashSet<char>();

            foreach (var chr in userAlphabet)
            {
                if (charHashset.Add(chr) == false)
                {
                    validInput = false;
                    Console.WriteLine($"Alphabet contains duplicate letter {chr}");
                    break;
                }

                charHashset.Add(chr);
            }
        } while (validInput == false);

        return userAlphabet;
    }

    public static int GetCesarShift(int alphabetLength)
    {
        bool validInput;
        int shiftAmount;
        do
        {
            validInput = true;
            Console.WriteLine("Cesar shift amount (enter for 1): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                shiftAmount = 1;
                continue;
            }
            var success = int.TryParse(input, out shiftAmount);
            if (success)
            {
                if (shiftAmount % alphabetLength == 0)
                {
                    Console.WriteLine("Incorrect input, length of alphabet and shift is same amount!");
                    validInput = false;
                }
                else
                {
                    shiftAmount %= alphabetLength;
                }
            }
            else
            {
                Console.WriteLine("Incorrect input, integer numbers only!");
                validInput = false;
            }

        } while (validInput == false);

        return shiftAmount;
    }
}