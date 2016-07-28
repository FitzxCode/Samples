using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringBLL;

namespace SGFlooringUI.WorkFlow
{
    public class MainMenu
    {
        public void StartMenu()
        {
            bool inMenu = true;
            do
            {
                int userChoice;
                do
                {
                    
                    userChoice = ConsoleIO.IntPrompt("Please chose an action:", true);
                    if (userChoice < 1 || userChoice > 5)
                    {
                        ConsoleIO.Prompt("Please enter a number 1-5. Hit enter to re-enter.");
                        ConsoleIO.Clear();
                    }
                } while (userChoice < 1 || userChoice > 5);
                if (OrdersExist() || userChoice == 2 || userChoice == 5)
                {
                    inMenu = ProcessChoice(userChoice);
                }
                else
                {
                    ConsoleIO.Prompt(
                        "There are no current orders in file to be edited, displayed, or removed. Please add a new order.");
                }
                ConsoleIO.Clear();
            } while (inMenu);
        }

        private bool ProcessChoice(int userChoice)
        {
            switch (userChoice)
            {
                case 1:
                    DisplayOrdersWF display = new DisplayOrdersWF();
                    display.Execute();
                    break;
                case 2:
                    AddOrderWF add = new AddOrderWF();
                    add.Execute();
                    break;
                case 3:
                    EditOrderWF edit = new EditOrderWF();
                    edit.Execute();
                    break;
                case 4:
                    RemoveOrderWF remove = new RemoveOrderWF();
                    remove.Execute();
                    break;
                case 5:
                    ConsoleIO.Prompt("We look forward to continue helping you.");
                    return false;
                default:
                    ConsoleIO.Prompt("Something when wrong and issue has been logged");
                    break;
            }
            return true;
        }

        private bool OrdersExist()
        {
            OrdersManagement manage = new OrdersManagement();
            List<string> orderFiles = manage.OrderDatesList();

            if (orderFiles.Count < 1)
            {
                return false;
            }
            return true;
        }
    }
}
