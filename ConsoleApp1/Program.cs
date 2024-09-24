using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;
            while (true)
            {
                Console.WriteLine("Give me an input string to test REGEX on:");
                input = Console.ReadLine();
                Console.WriteLine("The result is:");
                Console.WriteLine(ProcessPayee(input) + "\n");
            }
        }

        public static string ProcessPayee(string input)
        {
            // Trying to be smart about my Regex Custom Coding replacements
            string front;
            string back;

            Regex lettersUntilAlpha = new Regex(@"^([^A-Za-z]+(?=[a-zA-Z]))?(.*)");
            Regex symbols = new Regex(@"((?<!(XXX|CHK )\d?\d?\d?)([\d]+([^A-Za-z ]\d+)+)|   +)");
            Regex refnum = new Regex(@"((Confirmation|REF) ?# ?)[^ ]+");
            Regex removeTrailing = new Regex(@"(^%|%$)");

            var match = lettersUntilAlpha.Match(input.Trim()).Groups;
            front = match[1].Value; // This is the front non-alpha items to not match them
            back = match[2].Value; // This is every character after the first A-Z/a-z
            back = symbols.Replace(back, "%");
            back = refnum.Replace(back, "$1%");
            back = removeTrailing.Replace(front + back, "");

            return back;
        }
    }
}
