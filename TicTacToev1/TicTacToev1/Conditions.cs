using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToev1
{
    public class Conditions
    {
        public bool WinConditions(string[] boardNum)
        {
            if ((boardNum[0] == boardNum[1] && boardNum[1] == boardNum[2]) ||
                (boardNum[0] == boardNum[4] && boardNum[4] == boardNum[8]) ||
                (boardNum[4] == boardNum[1] && boardNum[1] == boardNum[7]) ||
                (boardNum[0] == boardNum[3] && boardNum[3] == boardNum[6]) ||
                (boardNum[2] == boardNum[5] && boardNum[5] == boardNum[8]) ||
                (boardNum[2] == boardNum[4] && boardNum[4] == boardNum[6]) ||
                (boardNum[3] == boardNum[4] && boardNum[4] == boardNum[5]) ||
                (boardNum[6] == boardNum[7] && boardNum[7] == boardNum[8]))
            {
                return true;
            }
            return false;
        }

        public int CheckConditions(int playerChoice, bool win, string[] boardNum, string player)
        {
            Prompts prom = new Prompts();
            do
            {

                if (boardNum[playerChoice - 1] == "X" || boardNum[playerChoice - 1] == "O")
                {
                    Console.WriteLine("You can not repeat a location, go again: ");
                    playerChoice = prom.IntPrompt(player);
                }
            } while (boardNum[playerChoice - 1] == "X" || boardNum[playerChoice - 1] == "O" || win);
            return playerChoice;
        }
    }
}
