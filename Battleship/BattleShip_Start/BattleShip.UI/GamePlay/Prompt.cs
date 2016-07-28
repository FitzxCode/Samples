using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI.GamePlay
{



    public class PromptClass
    {
        public static string Prompt(string message)
        {
            ConsoleIO.Write(message);
            return ConsoleIO.ReadLine();
        }

        public static int DirectionPrompt(string player)
        {
            string input =
                Prompt($"Please enter a direction you would like to place your ship {player}.\n 1-Up, 2-Down, 3-Left, 4-Right:  ");
            bool isValid;
            int direction;

            do
            {
                isValid = int.TryParse(input, out direction);

                if (!isValid || direction < 1 || direction > 4)
                {
                    isValid = false;
                    input = Prompt("Please enter a number 1-4: ");
                }
            } while (!isValid);
            return direction;
        }

        public static string CoordPrompt(string message)
        {
            string input = "";
            do
            {

                ConsoleIO.Write(message);
                input = ConsoleIO.ReadLine();

            } while (input.Length < 2);
            return input;
        }

        public static int PlayAgain(string input)
        {
            bool playAgain = false;
            int result = 0;
            do
            {

                playAgain = int.TryParse(input, out result);
                if (!playAgain || result < 1 || result > 2)
                {
                    playAgain = false;
                    input = Prompt("Would you like to play again?\nType 1:Play Again or 2:Quit:   ");
                    
                }
            } while (!playAgain);
            return result;
        }

        public static void ClearBoard()
        {
            ConsoleIO.WriteLine("Press enter to clear your board.");
            ConsoleIO.ReadLine();
            ConsoleIO.Clear();
        }

    }
}
