using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringBLL;
using SGFlooringUI.WorkFlow;

namespace SGFlooringUI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MainMenu menu = new MainMenu();
                menu.StartMenu();
            }
            catch (Exception ex)
            {
                ConsoleIO.Prompt("Something went wrong, the issue has been logged to the log file.");
                ErrorLogging.PassExceptionToBLL(ex);
            }
            
        }
    }
}
