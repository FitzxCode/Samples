using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringBLL;
using SGFlooringModels;

namespace SGFlooringUI.WorkFlow
{
    public class DisplayOrdersWF
    {
        private OrdersManagement _manage = new OrdersManagement();
        public void Execute()
        {
            DateTime ordersDate = ValidDate();
            DisplayOrders(ordersDate);

        }

        public void DisplayOrderDates()
        {
            List<string> orderDates = _manage.OrderDatesList();
            foreach (var date in orderDates)
            {
                ConsoleIO.Prompt($"Date of Orders: {date.Substring(0, 2)}/{date.Substring(2, 2)}/{date.Substring(4)}");
            }
        }

        public void DisplayOrders(DateTime ordersDate)
        {
            
            List<Order> orders = new List<Order>();
            orders = _manage.OrderList(ordersDate);
            foreach (var order in orders)
            {
                ConsoleIO.DisplayOrder(order);
                ConsoleIO.ReadLine();
            }
        }

        private DateTime ValidDate()
        {
            DateTime ordersDate;
            bool isValid;
            string input;

            do
            {
                input = ConsoleIO.Prompt("Enter D to view available order dates, or hit enter to continue.");
                ConsoleIO.Clear();
                if (input.ToUpper() == "D")
                {
                    DisplayOrderDates();
                }
                ordersDate = ConsoleIO.DatePrompt("Please enter an Date (MM/DD/YYYY) for the orders you would like to look at:");
                isValid = _manage.ValidateDate(ordersDate);
                if (!isValid)
                {
                    ConsoleIO.Prompt("There are no orders on that date");
                    ConsoleIO.Clear();
                }

            } while (!isValid);
            return ordersDate;
        }

    }
}
