using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringBLL;
using SGFlooringModels;

namespace SGFlooringUI
{
    public class ConsoleIO
    {
        public static Error errorToLog = new Error();

        public static void MenuDisplay()
        {
            Display("************************\n" +
                    "\tSG Flooring" +
                    "\n1.Display Orders" +
                    "\n2.Add an Order" +
                    "\n3.Edit an Order" +
                    "\n4.Remove an Order" +
                    "\n5.Quit" +
                    "\n************************");
        }

        public static void Display(string message)
        {
            Console.WriteLine(message);
        }

        public static string Prompt(string message)
        {
            Display(message);
            return Console.ReadLine();
        }

        public static int IntPrompt(string message, bool isMenu)
        {
            int inputNumber = 0;
            bool isValid = false;
            
            do
            {
                if (isMenu)
                {
                    MenuDisplay();
                }
                string input = Prompt(message);
                isValid = int.TryParse(input, out inputNumber);
                if (!isValid)
                {
                    errorToLog.UserInput = input;
                    errorToLog.Location = "IntPromt validation";
                    ErrorLogging.PassErrorToBLL(errorToLog);
                    Prompt("That was not a valid input, please enter an number. Hit enter to re-enter.");
                    Clear();
                }
            } while (!isValid);

            return inputNumber;
        }

        public static DateTime DatePrompt(string message)
        {
            bool isValid = false;
            DateTime inputDate;
            string input;

            do
            {
                Clear();
                input = Prompt(message);
                isValid = DateTime.TryParse(input, out inputDate);
                if (!isValid)
                {
                    errorToLog.UserInput = input;
                    errorToLog.Location = "DateTimeTryParse in ConsoleIO for input";
                    ErrorLogging.PassErrorToBLL(errorToLog);
                    Prompt("Please enter in proper date format MM/DD/YYYY. Hit enter to re-enter.");
                    

                }
            } while (!isValid);

            return inputDate;
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public static string StatePrompt(string message)
        {
            bool isValid = false;
            string input = "";
            OrdersManagement manage = new OrdersManagement();
            do
            {
                
                Clear();
                input = Prompt(message).ToUpper();
                errorToLog.UserInput = input;
                if (input == "S")
                {
                    DisplayServiceStates();
                }
                else if (input.Length != 2)
                {
                    errorToLog.Location = "StatePrompt check if length is 2";
                    ErrorLogging.PassErrorToBLL(errorToLog);
                    Prompt("Please enter a states abrevation. Hit enter to re-enter.");
                }
                else if (manage.ValidateState(input))
                {
                    isValid = true;
                }
                else
                {
                    errorToLog.Location = "StatePrompt check if State within our service range";
                    ErrorLogging.PassErrorToBLL(errorToLog);
                    Prompt("That was not a valid State within our service range. Hit enter to re-enter.");
                }
            } while (!isValid);

            return input;
        }

        public static decimal DecimalPrompt(string message)
        {
            bool isValid;
            decimal value;
            string input;

            do
            {
                Clear();
                input = Prompt(message);
                isValid = decimal.TryParse(input, out value);
                if (!isValid || value < 1)
                {
                    errorToLog.Location = "DecimalPrompt checking if a valid decimal > 1";
                    errorToLog.UserInput = input;
                    ErrorLogging.PassErrorToBLL(errorToLog);
                    Prompt("That was not a valid input. Hit enter to re-enter.");
                    isValid = false;
                }
                
            } while (!isValid);
            return value;
        }

        public static ProductInfo ProductPrompt(string message)
        {
            bool isValid = false;
            string input;
            OrdersManagement manage = new OrdersManagement();

            do
            {
                Clear();
                input = Prompt(message);
                input = input.ToUpper();
                if (input == "P")
                {
                    DisplayProductCatalog();
                }
                else if (manage.ValidateProductInfo(input))
                {
                    isValid = true;
                }
                else
                {
                    errorToLog.UserInput = input;
                    errorToLog.Location = "ProductPrompt validating if product within our catalog";
                    Prompt("That was not a valid product in our catalog.");
                }

            } while (!isValid);

            return manage.GetProductInfo(input);
        }

        public static void DisplayOrder(Order order)
        {
            Display($"Customer Name: {order.CustomerName}\n" +
                    $"Order Date: {order.OrderDate}\n" +
                    $"Order State: {order.OrderState.StateName}\n" +
                    $"Product: {order.Product.ProductType}\n" +
                    $"Area: {order.Area}\n" +
                    $"Material Cost: ${order.Total.MaterialCost}\n" +
                    $"Labor Cost: ${order.Total.LaborCost}\n" +
                    $"Tax Rate: {order.OrderState.TaxRate}%\n" +
                    $"Total Tax: ${order.Total.TotalTax}\n" +
                    $"Order Total: ${order.Total.TotalPrice}\n");
        }

        public static void DisplayProductCatalog()
        {
            OrdersManagement manage = new OrdersManagement();
            Dictionary<string, ProductInfo> productCatalog = manage.GetProductCatalog();

            foreach (var product in productCatalog)
            {
                Prompt($"Product Name: {product.Value.ProductType}\n" +
                        $"Material Cost Per Square Foot: {product.Value.MaterialCostPerSquareFoot}\n" +
                        $"Labor Cost Per Square Foot: {product.Value.LaborCostPerSquareFoot}");
            }
        }

        public static void DisplayServiceStates()
        {
            OrdersManagement manage = new OrdersManagement();
            Dictionary<string, State> serviceStates = manage.GetServiceStates();

            foreach (var state in serviceStates)
            {
                Prompt($"State Name: {state.Value.StateName}\n" +
                        $"State Abrevation: {state.Value.StateAbrevation}\n" +
                        $"Tax Rate: {state.Value.TaxRate}");
            }

        }

        public static void ReadLine()
        {
            Console.ReadLine();
        }

    }
}
