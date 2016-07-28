using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;
using BattleShip.UI.GamePlay;

namespace BattleShip.UI
{
    public static class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            WorkFlow wk = new WorkFlow();
            
            do
            {
                wk.SomeMethod();
                string input = PromptClass.Prompt("Would you like to play again?\nType 1:Play Again or 2:Quit:   ");
                i = PromptClass.PlayAgain(input);
                if (i == 1)
                {
                    Console.WriteLine(
                        "I don't know why you would put yourself through this painful expierence again, but here you go!");
                    
                }
                if (i == 2)
                {
                    Console.WriteLine("Thank you for playing this frustrating game, GOOD DAY!");
                }
            } while (i == 1);
        }
    }
}
