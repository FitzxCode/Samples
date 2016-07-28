using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;

namespace BattleShip.UI.GamePlay
{
    public class Player
    {
        public string Name { get; set; }
        public Board ShipBoard { get; set; }
        public Dictionary<Coordinate, string> PlayerGrid { get; set; }
        public string [] Ships { get; set; }

        public Player(string Name)
        {
            this.Name = Name;
            ShipBoard = new Board();
            PlayerGrid = new Dictionary<Coordinate, string>();
            Ships =
                new string[5] { "Battleship which is 4 spaces", "Submarine which is 3 spaces", "Carrier which is 5 spaces", "Destroyer which is 2 spaces", "Cruiser which is 3 spaces" };

        }
    }
}
