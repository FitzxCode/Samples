using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToev1
{
    public class Board
    {
        public void DisplayBoard(string[] boardNum)
        {
            string board = "";
            for (int i = 1; i <= boardNum.Length + 2; i++)
            {
                if (i % 3 == 0)
                {
                    board += "  " + boardNum[i - 3] + "  |  " + boardNum[i - 2] + "  |  " + boardNum[i - 1] + "\n";
                }
                else if (i % 4 == 0)
                {
                    board += "-----------------\n";
                }
                else
                {
                    board += "     |     |\n";
                }
            }
            Console.WriteLine(board);
        }
    }
}
