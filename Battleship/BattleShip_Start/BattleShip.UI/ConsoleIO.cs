using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    public class ConsoleIO
    {
        public static void Write(string message)
        {
            Console.Write(message);
        }

        public static void WriteChar(char message)
        {
            Console.Write(message);
        }

        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        public static void ForegroundColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}
