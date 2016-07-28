using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToev1
{
    public class Prompts
    {
        public int IntPrompt(string player)
        {
            Console.WriteLine("{0} please enter the location you would like to go: ", player);
            string input = Console.ReadLine();
            int result = 0;
            bool isValid;

            do
            {
                isValid = int.TryParse(input, out result);
                if (!isValid || result > 9 || result < 1)
                {
                    Console.WriteLine("That is an incorrect input, please enter a valid input");
                    input = Console.ReadLine();
                }
            } while (!isValid || result > 9 || result < 1);
            return result;
        }

        public string Prompt(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}
