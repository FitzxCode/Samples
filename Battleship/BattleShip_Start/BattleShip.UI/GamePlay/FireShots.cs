using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;

namespace BattleShip.UI.GamePlay
{
    public class FireShots
    {
        public static bool FireShotPlayer(Player playerA, Player playerB)
        {
            
            FireShotResponse newShot = new FireShotResponse();
            int x = 0;
            int y = 0;
            bool isWin = false;
            // FIRE SHOTS
            PlayerBoard.ShotBoard(playerA, playerB);

                string playerInput = PromptClass.Prompt($"{playerA.Name} Please enter a coordinate to fire at: ");

                Coordinate coord = PlayerBoard.GetCoordinate(playerInput);

                newShot = playerB.ShipBoard.FireShot(coord);
                while (newShot.ShotStatus == ShotStatus.Duplicate || newShot.ShotStatus == ShotStatus.Invalid)
                {
                    playerInput = PromptClass.Prompt($"{playerA.Name} Please enter a new VALID input: ");
                coord = PlayerBoard.GetCoordinate(playerInput);
                ConsoleIO.Clear();
                newShot = playerB.ShipBoard.FireShot(coord);
                }
            switch (newShot.ShotStatus)
            {
                case ShotStatus.Hit:

                    playerA.PlayerGrid.Add(new Coordinate(coord.YCoordinate, coord.XCoordinate), "H");
                    ConsoleIO.Clear();
                    PlayerBoard.ShotBoard(playerA, playerB);
                    ConsoleIO.WriteLine($"You scored a hit on {playerB.Name}.");
                    
                    break;
                case ShotStatus.HitAndSunk:

                    playerA.PlayerGrid.Add(new Coordinate(coord.YCoordinate, coord.XCoordinate), "H");
                    ConsoleIO.Clear();
                    PlayerBoard.ShotBoard(playerA, playerB);
                    ConsoleIO.WriteLine($"You sank {playerB.Name}'s {newShot.ShipImpacted}!");
                    
                    break;
                case ShotStatus.Miss:

                    playerA.PlayerGrid.Add(new Coordinate(coord.YCoordinate, coord.XCoordinate), "M");
                    ConsoleIO.Clear();
                    PlayerBoard.ShotBoard(playerA, playerB);
                    ConsoleIO.WriteLine($"You did not score a hit on {playerB.Name}.");
                    
                    break;
                case ShotStatus.Victory:

                    PlayerBoard.ShotBoard(playerA, playerB);
                    ConsoleIO.WriteLine($"YOU HAVE SUNK ALL OF {playerB.Name}'S SHIPS, CONGRATULATIONS!!!");
                    isWin = true;
                    break;
            }
            ConsoleIO.ReadLine();
            ConsoleIO.Clear();
            
            ConsoleIO.ReadLine();
            return isWin;
        }
    }
}
