namespace TheRepeatingStream;

/*Objectives:
   • Make a RecentNumbers class that holds at least the two most recent numbers.
   • Make a method that loops forever, generating random numbers from 0 to 9 once a second. Hint:
   Thread.Sleep can help you wait.
   • Write the numbers to the console window, put the generated numbers in a RecentNumbers object,
   and update it as new numbers are generated.
   • Make a thread that runs the above method.
   • Wait for the user to push a key in a second loop (on the main thread or another new thread). When
   the user presses a key, check if the last two numbers are the same. If they are, tell the user that they
   correctly identified the repeat. If they are not, indicate that they got it wrong.
   • Use lock statements to ensure that only one thread accesses the shared data at a time.*/

class Program
{
    static void Main(string[] args)
    {
        RecentNumbers recentNumbers = new();
        Thread thread = new(recentNumbers.GenerateNumbers);
        thread.Start();

        while (true)
        {
            Console.Write("Enter any key to check for repeating numbers: ");
            var input = Console.ReadKey().Key;
            if (input != ConsoleKey.Escape)
            {
                if (recentNumbers.One == recentNumbers.Two)
                    Console.WriteLine("\nREPEAT IDENTIFIED");
                else
                    Console.WriteLine("\nNo Repeat...");
            }
            else
            {
                break;
            }
        }
    }
}

public class RecentNumbers
{
    private readonly Lock _numberLock = new();
    private int _one;

    public int One
    {
        get
        {
            lock (_numberLock)
            {
                return _one;
            }
        }
    }

    private int _two;

    public int Two
    {
        get
        {
            lock (_numberLock)
            {
                return _two;
            }
        }
    }

    public void GenerateNumbers()
    {
        Random random = new();
        while (true)
        {
            _one = random.Next(0, 10);
            Thread.Sleep(1000);
            //Console.WriteLine($"First number: {One} | Second number: {Two}");
            _two = _one;
        }
    }
}