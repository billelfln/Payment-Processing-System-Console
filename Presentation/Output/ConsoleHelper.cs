namespace PaymentProcessingSystem.Presentation.Output;

public static class ConsoleHelper
{
    public static void WriteHeader(string title)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(new string('=', 60));
        Console.WriteLine(title.PadLeft((60 + title.Length) / 2));
        Console.WriteLine(new string('=', 60));
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void WriteSection(string title)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"\n--- {title} ---");
        Console.ResetColor();
    }

    public static void WriteSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void WriteWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void WriteInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static string ReadRequiredString(string label)
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{label}: ");
            Console.ResetColor();

            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            WriteWarning("This field is required. Please try again.");
        }
    }

    public static decimal ReadDecimal(string label)
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{label}: ");
            Console.ResetColor();

            var input = Console.ReadLine();

            if (decimal.TryParse(input, out var value) && value > 0)
                return value;

            WriteWarning("Please enter a valid positive number.");
        }
    }

    public static int ReadInt(string label)
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{label}: ");
            Console.ResetColor();

            var input = Console.ReadLine();

            if (int.TryParse(input, out var value))
                return value;

            WriteWarning("Please enter a valid number.");
        }
    }

    public static void Pause()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Press any key to continue...");
        Console.ResetColor();
        Console.ReadKey();
    }
}