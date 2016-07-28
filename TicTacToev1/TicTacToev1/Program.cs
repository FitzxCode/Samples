using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToev1
{
    class Program
    {
        static void Main(string[] args)
        {
            Board bd = new Board();
            Prompts prom = new Prompts();
            Conditions cond = new Conditions();
            Console.WriteLine("Welcome to TicTacToe");
            string player1 = prom.Prompt("Player 1 please enter your name: ");
            string player2 = prom.Prompt("Player 2 please enter your name: ");
            string[] boardNum = new string[9] {"1", "2", "3", "4", "5", "6", "7", "8", "9"};
            bd.DisplayBoard(boardNum);
            int player1Choice = prom.IntPrompt(player1);



            for (int i = 0; i < 5; i++)
            {
                bool win = false;
                player1Choice = cond.CheckConditions(player1Choice, win, boardNum, player1);
                boardNum[player1Choice - 1] = "X";
                Console.Clear();
                bd.DisplayBoard(boardNum);
                win = cond.WinConditions(boardNum);
                if (win)
                {
                    Console.WriteLine("You Win {0}!!!!", player1);
                    break;
                }
                if (i == 4)
                {
                    Console.WriteLine("Better Luck Next Time No Winner.");
                    break;
                }
                int player2Choice = prom.IntPrompt(player2);
                player2Choice = cond.CheckConditions(player2Choice, win, boardNum, player2);
                boardNum[player2Choice - 1] = "O";
                Console.Clear();
                bd.DisplayBoard(boardNum);
                win = cond.WinConditions(boardNum);
                if (win)
                {
                    Console.WriteLine("You Win {0}!!!!", player2);
                    break;
                }
            }

            Console.ReadLine();
        }
    }
}
