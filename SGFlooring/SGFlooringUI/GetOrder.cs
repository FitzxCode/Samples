using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringBLL;
using SGFlooringModels;
using SGFlooringUI.WorkFlow;

namespace SGFlooringUI
{
    public class GetOrder
    {
        private static OrdersManagement _manage = new OrdersManagement();
        private static DisplayOrdersWF _display = new DisplayOrdersWF();

        public static DateTime GetDateTime()
        {
            bool validDate = false;
            DateTime date;
            string input;

            do
            {

                input = ConsoleIO.Prompt("To view possible order dates enter D, else hit enter to continue.");
                if (input.ToUpper() == "D")
                {
                    
                    _display.DisplayOrderDates();
                }
                date = ConsoleIO.DatePrompt("Which date are we pulling the order from:");
                validDate = _manage.ValidateDate(date);
                if (!validDate)
                {
                    ConsoleIO.Prompt("That was not a valid order date.");
                }
            } while (!validDate);

            return date;
        }

        public static int GetOrderNumber(DateTime date)
        {
            bool validNumber = false;
            
            int orderNumber;

            do
            {
                orderNumber = ConsoleIO.IntPrompt("Please enter the order number of the order we are pulling. To view possible orders enter 0:", false);
                if (orderNumber == 0)
                {
                    _display.DisplayOrders(date);
                }
                else
                {

                    validNumber = _manage.ValidateOrderNumber(date, orderNumber);
                    if (!validNumber)
                    {
                        ConsoleIO.Prompt("That was not a valid order number.");
                    }
                }

            } while (!validNumber);
            return orderNumber;
        }

        public static Order GetOrderToEdit(DateTime date, int orderNumber)
        {
            Order order = _manage.RetrieveOrder(date, orderNumber);
            return order;
        }
    }
}
