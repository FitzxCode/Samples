using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Ships;

namespace BattleShip.UI.GamePlay
{
    public class PlayerBoard
    {
        
        public void DisplayBoard(Player player)
        {
            string board = "";
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        Console.Write("   ");
                    }
                    else if (i == 0)
                    {
                        Console.Write($" {(char)(j + 64)} |");
                    }
                    else if(j == 0 && i == 10)
                    {
                        board += "" + i + "|";
                    }
                    else if (j == 0)
                    {
                        board += " " + i + "|";
                    }
                    else if (player.PlayerGrid.ContainsKey(new Coordinate(i, j)))
                    {
                        board += " " + player.PlayerGrid[new Coordinate(i, j)] + " |";
                    }
                    else
                    {
                        board += "   |";
                    }
                }
                board += "\n-------------------------------------------\n";
            
        }
            ColorChange(board);



        }
        public static void ShotBoard(Player player1, Player player2)
        {
            string board = "";
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        ConsoleIO.Write("   ");
                    }
                    else if (i == 0)
                    {
                        ConsoleIO.Write($" {(char)(j + 64)} |");
                    }
                    else if (j == 0 && i == 10)
                    {
                        board += "" + i + "|";
                    }
                    else if (j == 0)
                    {
                        board += " " + i + "|";
                    }
                    else if (player2.ShipBoard.ShotHistory.ContainsKey(new Coordinate(i, j)))
                    {
                        board += " " + player1.PlayerGrid[new Coordinate(i, j)] + " |";
                    }
                    else
                    {
                        board += "   |";
                    }
                }
                board += "\n-------------------------------------------\n";

            }

            ColorChange(board);
            


        }


        public static int XCoordinate(string playerInput)
        {
            int xCoordinate;
            do
            {
                char input = playerInput.ToUpper()[0];
                int xCoord = (int) input;
                xCoordinate = xCoord - 64;
                if (xCoordinate < 1 || xCoordinate > 10)
                {
                    playerInput = PromptClass.Prompt("Please enter a valid Coordinate: ");
                }
            } while (xCoordinate < 1 || xCoordinate > 10);
            return xCoordinate;
        }

        public static Coordinate GetCoordinate(string playerInput)
        {
            bool isValid;
            int yCoordinate;
            int xCoordinate;
            do
            {
                do
                {


                    if (playerInput.Length < 2)
                    {
                        playerInput = PromptClass.CoordPrompt("Please input a valid Coordinate: ");
                    }
                } while (playerInput.Length < 2); 
                char input = playerInput.ToUpper()[0];
                int xCoord = (int) input;
                xCoordinate = xCoord - 64;
                isValid = int.TryParse(playerInput.Substring(1), out yCoordinate);
                if (!isValid || yCoordinate > 10 || yCoordinate < 1 || xCoordinate <1 || xCoordinate > 10)
                {
                    playerInput = PromptClass.CoordPrompt("Please input a valid Coordinate: ");
                }
            } while (!isValid || yCoordinate > 10 || yCoordinate < 1 || xCoordinate < 1 || xCoordinate > 10);
            return new Coordinate(xCoordinate, yCoordinate);
        }

        public ShipDirection SetShipDirection(int direction)
        {
            switch (direction)
            {
                case 1:
                    return ShipDirection.Up;
                case 2:
                    return ShipDirection.Down;
                case 3:
                    return ShipDirection.Left;

            }
            return ShipDirection.Right;
        }

        public ShipType SetShipType(int ship)
        {
            switch (ship)
            {
                case 0:
                    return ShipType.Battleship;
                case 1:
                    return ShipType.Submarine;
                case 2:
                    return ShipType.Carrier;
                case 3:
                    return ShipType.Destroyer;
            }
            return ShipType.Cruiser;
        }

        public void ChangeGrid(int shipCount, int direction, Player player, Coordinate coord)
        {
            int y = 0;
            int x = 0;
            int n = 0;
            String symbol = "";
            switch (shipCount)
            {
                case 3:
                    n = 2;
                    symbol = "D";
                    break;
                case 4:
                    n = 3;
                    symbol = "C";
                    break;
                case 1:
                    n = 3;
                    symbol = "S";
                    break;
                case 0:
                    n = 4;
                    symbol = "B";
                    break;
                case 2:
                    n = 5;
                    symbol = "A";
                    break;
            }
            switch (direction)
            {
                case 1:
                    do
                    {
                        Coordinate newCoord = new Coordinate(coord.YCoordinate, coord.XCoordinate);
                        player.PlayerGrid.Add(newCoord, symbol);
                        coord.YCoordinate--;
                        n--;
                    } while (n > 0);
                    break;
                case 2:
                    do
                    {
                        Coordinate newCoord = new Coordinate(coord.YCoordinate, coord.XCoordinate);
                        player.PlayerGrid.Add(newCoord, symbol);
                        coord.YCoordinate++;
                        n--;
                    } while (n > 0);
                    break;
                case 3:
                    do
                    {
                        Coordinate newCoord = new Coordinate(coord.YCoordinate, coord.XCoordinate);
                        player.PlayerGrid.Add(newCoord, symbol);
                        coord.XCoordinate--;
                        n--;
                    } while (n > 0);
                    break;
                case 4:
                    do
                    {
                        Coordinate newCoord = new Coordinate(coord.YCoordinate, coord.XCoordinate);
                        player.PlayerGrid.Add(newCoord, symbol);
                        coord.XCoordinate++;
                        n--;
                    } while (n > 0);
                    break;
            }
        }

        private static void ColorChange(string board)
        {
            foreach (var c in board)
            {
                switch (c)
                {
                    case 'D':
                    case 'A':
                    case 'B':
                    case 'S':
                    case 'C':
                        ConsoleIO.ForegroundColor(ConsoleColor.Blue);
                        ConsoleIO.WriteChar(c);
                        ConsoleIO.ForegroundColor(ConsoleColor.Cyan);
                        break;
                    case 'H':
                        ConsoleIO.ForegroundColor(ConsoleColor.DarkRed);
                        ConsoleIO.WriteChar(c);
                        ConsoleIO.ForegroundColor(ConsoleColor.Cyan);
                        break;
                    case 'M':
                        ConsoleIO.ForegroundColor(ConsoleColor.DarkYellow);
                        ConsoleIO.WriteChar(c);
                        ConsoleIO.ForegroundColor(ConsoleColor.Cyan);
                        break;
                    default:
                        ConsoleIO.WriteChar(c);
                        break;
                }
            }
        }
    }
}
