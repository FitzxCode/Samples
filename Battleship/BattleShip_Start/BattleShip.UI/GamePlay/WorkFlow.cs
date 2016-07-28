using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI.GamePlay
{
    public class WorkFlow
    {
        public void SomeMethod()
        {


            ConsoleIO.ForegroundColor(ConsoleColor.Cyan);
            
            PlaceShip place = new PlaceShip();


            ConsoleIO.WriteLine("Welcome to Battleship!\n Press enter to continue to the game:");
            ConsoleIO.ReadLine();
            ConsoleIO.Clear();


            string playerName = PromptClass.Prompt("Player 1 please enter your name: ");
            if (playerName.Length < 1)
            {
                playerName = "Player1";
            }
            Player player1 = new Player(playerName);
            
            ConsoleIO.Clear();
            playerName = PromptClass.Prompt("Player 2 please enter your name: ");
            if (playerName.Length < 1)
            {
                playerName = "Player2";
            }
            Player player2 = new Player(playerName);

            ConsoleIO.Clear();

            // Player 1 place ships
            place.PlayerPlaceShip(player1);
            PromptClass.ClearBoard();
            
            ConsoleIO.WriteLine("Please hit enter to proceed to player 2 ship placement.");
            ConsoleIO.ReadLine();
            ConsoleIO.Clear();


            //player2 place ships
            place.PlayerPlaceShip(player2);
            PromptClass.ClearBoard();

            // Fireshots Start
            bool isWin = false;
            player1.PlayerGrid = new Dictionary<Coordinate, string>();
            player2.PlayerGrid = new Dictionary<Coordinate, string>();
            do
            {
                ConsoleIO.Clear();
                ConsoleIO.WriteLine($"Hit enter to continue to {player1.Name}'s turn.");
                ConsoleIO.ReadLine();


                isWin = FireShots.FireShotPlayer(player1, player2);
                
                if (isWin)
                {
                    break;
                }


                ConsoleIO.Clear();
                ConsoleIO.WriteLine($"Hit enter to continue to {player2.Name}'s turn.");
                ConsoleIO.ReadLine();

                isWin = FireShots.FireShotPlayer(player2, player1);
               
                
            } while (!isWin);
        }
    }
}