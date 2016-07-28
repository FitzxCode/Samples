using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringBLL;
using SGFlooringModels;

namespace SGFlooringUI.WorkFlow
{
    public class RemoveOrderWF
    {
        public void Execute()
        {
            DateTime orderDate = GetOrder.GetDateTime();
            int orderNumber = GetOrder.GetOrderNumber(orderDate);
            Order orderToRemove = GetOrder.GetOrderToEdit(orderDate, orderNumber);
            RemoveOrder(orderToRemove);
        }

        private void RemoveOrder(Order orderToRemove)
        {
            int input;
            OrdersManagement manage = new OrdersManagement();
            do
            {

                ConsoleIO.DisplayOrder(orderToRemove);
                input = ConsoleIO.IntPrompt("Are you sure you want to remove this order? (1)Yes, (2)No:", false);
                if (input == 1)
                {
                    bool successful = manage.RemoveOrder(orderToRemove);
                    if (successful)
                    {
                        ConsoleIO.Prompt("The order was successfully removed");
                    }
                    else
                    {
                        ConsoleIO.Prompt("Something went wrong! The order was not successfully removed.");
                    }
                }
                else if (input == 2)
                {
                    ConsoleIO.Prompt("The order was not removed.");
                }
                else
                {
                    ConsoleIO.Prompt("Please enter an option 1-2.");
                    ConsoleIO.Clear();
                }

            } while (input > 2 || input < 1);
        }
    }
}
