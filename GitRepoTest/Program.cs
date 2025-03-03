using System.Globalization;

namespace StringCalculator
{
    public class StringCalculator
    {
        public const string NegativeNumbersExceptionMessage = "Negative numbers are not valid in this context.";
        public const string SubstringNotIntExceptionMessage = "Substring is not an integer";
        public static int ParseSubs(string[] subs)
        {
            int[] numbers = new int[subs.Length];
            int counter = 0;
            for (int i = 0; i < subs.Length; i++)
            {
                if (!int.TryParse(subs[i], out numbers[i]))
                {
                    throw new ArgumentException(SubstringNotIntExceptionMessage);
                }
                if(numbers[i] < 0)
                {
                    throw new ArgumentOutOfRangeException(NegativeNumbersExceptionMessage);
                }
                if (numbers[i] <= 1000)
                    counter += numbers[i];
            }
            return counter;
        }
        public int Calculate(string arg)
        {
            List<string> delimiters = [",", "\n"];

            if(arg == string.Empty) return 0;

            if (arg.Length > 3 && arg[0] == '/' && arg[1] == '/') 
            {
                arg = arg.Substring(2);
                int delimEndIdx = -1;
                if (arg[0] == '[' && arg.Contains(']'))
                {
                    while (arg[0] == '[')
                    {
                        delimEndIdx = arg.IndexOf(']');
                        string newDelim = arg.Substring(1, delimEndIdx - 1);
                        delimiters.Add(newDelim);
                        arg = arg.Substring(delimEndIdx + 1);
                    }
                }
                else
                {
                    delimiters.Add(arg[0].ToString());
                    arg = arg.Substring(1);
                }
            }

            bool single = true;
            foreach (string c in delimiters)
            {
                if (arg.Contains(c))
                {
                    single = false;
                    break;
                }
            }
            if (single)
            {
                if (int.TryParse(arg, out int result))
                {
                    if (result < 0)
                    {
                        throw new ArgumentOutOfRangeException(NegativeNumbersExceptionMessage);
                    }

                    return (result <= 1000) ? result : 0;
                }
                return default;
            }
            string[] subs = arg.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            if (subs.Length == 2 || subs.Length == 3)
            {
                return ParseSubs(subs);
            }
            return default;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the string to be calculated:");
            string arg = "//[.][..][...]789.87...1";
            StringCalculator calc = new();
            Console.WriteLine($"The result is: {calc.Calculate(arg)}");
        }
    }
}
