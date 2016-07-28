using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringBLL;
using SGFlooringModels;

namespace SGFlooringUI.WorkFlow
{
    public class EditOrderWF
    {
        private static OrdersManagement _manage = new OrdersManagement();
        private static Error _errorToLog = new Error();

        public void Execute()
        {
            DateTime date = GetOrder.GetDateTime();
            int orderNumber = GetOrder.GetOrderNumber(date);
            Order orderToEdit = GetOrder.GetOrderToEdit(date, orderNumber);
            ConsoleIO.Clear();
            ConsoleIO.DisplayOrder(orderToEdit);
            ConsoleIO.Prompt("Hit enter to start editing this order:");
            ConsoleIO.Clear();
            Order editedOrder = GenerateEditOrder(orderToEdit);
            int choice = PromptEdit(editedOrder);
            if (choice == 1)
            {
                EditOrder(editedOrder, orderToEdit);
            }


        }

        private string EditCustomerName(string customerName)
        {
            string input =
                ConsoleIO.Prompt($"Enter a new customer name or hit enter to keep current name: {customerName}");
            if (string.IsNullOrEmpty(input))
            {
                return customerName;
            }
            return input;
        }

        private State EditState(State state)
        {
            string input;
            bool validState = false;
            do
            {
                input =
                    ConsoleIO.Prompt(
                        $"Enter a new State abrevation, enter S to view service states\n" +
                        $"Or hit enter to keep current State: {state.StateName}").ToUpper();
                _errorToLog.UserInput = input;
                if (string.IsNullOrEmpty(input))
                {
                    return state;
                }
                if (input == "S")
                {
                    ConsoleIO.DisplayServiceStates();
                    ConsoleIO.Clear();
                }
                else if (input.Length != 2)
                {
                    _errorToLog.Location = "EditState not a valid State abrevation";
                    ConsoleIO.Prompt("That was not a valid State abrevation. Hit enter to re-enter.");
                    ConsoleIO.Clear();
                    validState = false;
                }
                else if (!_manage.ValidateState(input))
                {
                    _errorToLog.Location = "EditState not a valid state within our service range";
                    ConsoleIO.Prompt("That State was not within our service range.");
                    ConsoleIO.Clear();
                    validState = false;
                }
                else
                {
                    validState = true;
                }

            } while (!validState);
            state = _manage.GetNewStateInfo(input);
            return state;
        }

        private decimal EditArea(decimal area)
        {
            string input;
            bool isValid;
            do
            {

                input = ConsoleIO.Prompt($"Enter a new area or hit enter to keep current area {area}:");
                if (string.IsNullOrEmpty(input))
                {
                    return area;
                }
                isValid = decimal.TryParse(input, out area);
                if (!isValid || area < 1)
                {
                    _errorToLog.Location = "EditArea not a valid decimal input > 1";
                    _errorToLog.UserInput = input;
                    ConsoleIO.Prompt("That was not a valid decimal value. Hit enter to re-enter.");
                    ConsoleIO.Clear();
                    isValid = false;
                }

            } while (!isValid);
            return area;
        }

        private ProductInfo EditProduct(ProductInfo product)
        {
            string input;
            bool isValid = false;

            do
            {
                input =
                    ConsoleIO.Prompt(
                        $"Enter a new product, enter P to view product catalog\n" +
                        $"Or hit enter to keep the current product {product.ProductType}:");
                if (string.IsNullOrEmpty(input))
                {
                    return product;
                }
                input = input.ToUpper();
                if (input == "P")
                {
                    ConsoleIO.DisplayProductCatalog();
                    ConsoleIO.Clear();
                }
                else if (!_manage.ValidateProductInfo(input))
                {
                    _errorToLog.Location = "EditProduct was not a valid product within our catalog";
                    ConsoleIO.Prompt("That was not a valid product within our catalog. Hit enter to re-enter.");
                    ConsoleIO.Clear();
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }

            } while (!isValid);
            product = _manage.GetProductInfo(input);

            return product;
        }

        private CostInfo GetNewCostInfo(ProductInfo product, decimal area, State state)
        {
            CostInfo cost = new CostInfo(product, area, state);
            return cost;
        }

        private Order GenerateEditOrder(Order orderToEdit)
        {
            string custName = EditCustomerName(orderToEdit.CustomerName);
            DateTime date = EditOrderDate(orderToEdit.OrderDate);
            State state = EditState(orderToEdit.OrderState);
            decimal area = EditArea(orderToEdit.Area);
            ProductInfo product = EditProduct(orderToEdit.Product);
            CostInfo totalCost = GetNewCostInfo(product, area, state);
            int orderNumber = orderToEdit.OrderNumber;

            if (date != orderToEdit.OrderDate)
            {
                orderNumber = _manage.GetNewOrderNumber(date);
            }
            
            Order editedOrder = new Order
            {
                CustomerName = custName,
                OrderDate = date,
                OrderState = state,
                Area = area,
                Product = product,
                Total = totalCost,
                OrderNumber = orderNumber
            };
            return editedOrder;
        }

        private int PromptEdit(Order editedOrder)
        {
            int input;
            bool isValid;

            do
            {

                ConsoleIO.Clear();
                ConsoleIO.DisplayOrder(editedOrder);
                input = ConsoleIO.IntPrompt("Are you sure you would like to process changes: (1)Yes, (2)No", false);
                if (input < 1 || input > 2)
                {
                    
                    ConsoleIO.Prompt("Please enter either 1:yes, 2:no.");
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }
            } while (!isValid);

            return input;
        }

        private void EditOrder(Order editedOrder, Order orderToEdit)
        {
            bool success = _manage.RemoveOrder(orderToEdit);
            if (success)
            {
                success = _manage.AddOrder(editedOrder);
            }
            if (!success)
            {
                ConsoleIO.Prompt("Something when wrong with submitting your edits.");
            }
            else
            {
                ConsoleIO.Prompt("Your order was successfully edited.");
            }
        }

        private DateTime EditOrderDate(DateTime orderDate)
        {
            string input;
            bool isValid;
            DateTime date;

            do
            {
                input = ConsoleIO.Prompt($"When was this order placed, hit enter to keep current date: {orderDate.ToString("MM")}/{orderDate.ToString("dd")}/{orderDate.ToString("yy")}");

                if (string.IsNullOrEmpty(input))
                {
                    return orderDate;
                }
                isValid = DateTime.TryParse(input, out date);
                if (!isValid)
                {
                    _errorToLog.Location = "EditingOrder DatePrompt";
                    _errorToLog.UserInput = input;
                    ConsoleIO.Prompt("That was not a valid Date please enter in MM/DD/YYYY format. Hit enter to re-enter.");
                    ConsoleIO.Clear();
                }

            } while (!isValid);
            return date;
        }
    }
}

