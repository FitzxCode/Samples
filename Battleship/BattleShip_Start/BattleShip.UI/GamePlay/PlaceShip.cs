using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;

namespace BattleShip.UI.GamePlay
{
    public class PlaceShip
    {
        public void PlayerPlaceShip(Player player)
        {
            
            int shipCountplayer = 0;
            PlayerBoard bd = new PlayerBoard();
            bd.DisplayBoard(player);
            do
            {


                string player1Placement =
                    PromptClass.CoordPrompt(
                        $"{player.Name} Please choose a coordinate to place your {player.Ships[shipCountplayer]}: ");

                Coordinate coord = PlayerBoard.GetCoordinate(player1Placement);
                int direction = PromptClass.DirectionPrompt(player.Name);
                var newShip = new PlaceShipRequest()


                {
                    Coordinate = coord,
                    ShipType = bd.SetShipType(shipCountplayer),
                    Direction = bd.SetShipDirection(direction)
                };
                switch (player.ShipBoard.PlaceShip(newShip))
                {
                    case ShipPlacement.NotEnoughSpace:
                        ConsoleIO.WriteLine("Please enter a loction that is within the board.");
                        ConsoleIO.ReadLine();
                        break;
                    case ShipPlacement.Overlap:
                        ConsoleIO.WriteLine("You cannot overlap another ship, please enter valid input.");
                        ConsoleIO.ReadLine();
                        break;
                    case ShipPlacement.Ok:
                        ConsoleIO.WriteLine($"Your {player.Ships[shipCountplayer]} has been successfully placed.");
                        bd.ChangeGrid(shipCountplayer, direction, player, coord);
                        shipCountplayer++;
                        break;
                }

                ConsoleIO.Clear();
                bd.DisplayBoard(player);
            } while (shipCountplayer < 5);
        }
    }
}
