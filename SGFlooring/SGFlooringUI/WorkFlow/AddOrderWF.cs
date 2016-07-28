using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringBLL;
using SGFlooringModels;

namespace SGFlooringUI.WorkFlow
{
    public class AddOrderWF
    {
        private OrdersManagement _manage = new OrdersManagement();
        private Error _error = new Error();

        public void Execute()
        {

            string custName = CustomerPrompt();
            DateTime date = GetOrderDate();
            State orderState = GetState();
            decimal area = GetArea();
            ProductInfo product = GetProduct();
            CostInfo cost = new CostInfo(product, area, orderState);

            Order newOrder = new Order
            {
                CustomerName = custName,
                OrderNumber = GetOrderNumber(date),
                OrderDate = date,
                OrderState = orderState,
                Area = area,
                Product = product,
                Total = cost
            };
            bool submit = DisplayNewOrder(newOrder);

            if (submit)
            {
                ConsoleIO.Prompt("The order was succefully added.");
            }
            else
            {
                ConsoleIO.Prompt("The order was disregarded.");
            }
        }

        private int GetOrderNumber(DateTime date)
        {
            int newOrderNumber = _manage.GetNewOrderNumber(date);
            return newOrderNumber;
        }

        private string CustomerPrompt()
        {

            string companyName;
            do
            {
                ConsoleIO.Clear();
                companyName = ConsoleIO.Prompt("Please enter the customer name:");
                if (string.IsNullOrEmpty(companyName))
                {
                    ConsoleIO.Prompt("Please fill this field in.");
                }
            } while (string.IsNullOrEmpty(companyName));
            return companyName;
        }

        private State GetState()
        {
            string input = ConsoleIO.StatePrompt("What state is this in, enter S for the States that we service\n" +
                                                 "Please enter abrevation:");
            State newState = _manage.GetNewStateInfo(input);
            return newState;
        }

        private decimal GetArea()
        {
            return ConsoleIO.DecimalPrompt("Please enter the total area for the order:");
        }

        private ProductInfo GetProduct()
        {
            ProductInfo product = ConsoleIO.ProductPrompt("Which product will be used, enter P to view product catalog:");
            return product;
        }

        private bool DisplayNewOrder(Order newOrder)
        {
            int input;
            do
            {
                ConsoleIO.Clear();
                ConsoleIO.DisplayOrder(newOrder);

                input = ConsoleIO.IntPrompt($"To proceed with submitting order enter (1). To cancel order enter (2):", false);
                if (input < 1 || input > 2)
                {
                    ConsoleIO.Prompt("Please enter a choice either 1 or 2.");
                }
            } while (input < 1 || input > 2);
            if (input == 1)
            {
                return AddNewOrder(newOrder);
            }
            return false;
        }

        private bool AddNewOrder(Order newOrder)
        {
            bool success = _manage.AddOrder(newOrder);
            if (!success)
            {
                ConsoleIO.Prompt("Something went wrong!");
            }
            return success;
        }

        private DateTime GetOrderDate()
        {
            string input;
            bool isValid;
            DateTime date;

            do
            {
                input = ConsoleIO.Prompt("When was this order placed, hit enter for current date or enter date:");

                if (string.IsNullOrEmpty(input))
                {
                    return _manage.GetCurrentDateTime();
                }
                isValid = DateTime.TryParse(input, out date);
                if (!isValid)
                {
                    _error.Location = "AddingOrder DatePrompt";
                    _error.UserInput = input;
                    ConsoleIO.Prompt("That was not a valid Date please enter in MM/DD/YYYY format");
                    ConsoleIO.Clear();
                }

            } while (!isValid);
            return date;
        }
    }
}
