using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringData.Orders;
using SGFlooringData.Products;
using SGFlooringData.TaxInfo;
using SGFlooringModels;

namespace SGFlooringData
{
    public class FileRepository : OrderRepository
    {
        private void CreateDirectory()
        {
            Directory.CreateDirectory(@"DataFiles\Orders");
        }

        public override List<string> ReturnOrdersDates()
        {
            CreateDirectory();
            var orderDates = Directory.EnumerateFiles(@"DataFiles\Orders");
            List<string> dates = new List<string>();
            string[] inputParts;

            foreach (var file in orderDates)
            {
                inputParts = file.Split('\\', '.', '_');
                dates.Add(inputParts[3]);
            }
            return dates;
        }

        public override List<Order> ReturnOrderList(DateTime date)
        {
            CreateDirectory();
            bool exists = ValidateFile(date);
            List<Order> listOfOrders = new List<Order>();
            if (exists)
            {
                ITaxRepository state = TaxRepositoryFactory.CreateTaxRepository();
                IProductRepository product = ProductsRepositoryFactory.CreateProductRepository();


                string orderFile = $"DataFiles\\Orders\\_{date.ToString("MMddyyyy")}.txt";
                using (StreamReader sr = File.OpenText(orderFile))
                {
                    string inputLine = "";
                    string[] inputParts;


                    while ((inputLine = sr.ReadLine()) != null)
                    {
                        inputParts = inputLine.Split('¬');

                        Order order = new Order
                        {
                            OrderNumber = int.Parse(inputParts[0]),
                            CustomerName = inputParts[1],
                            OrderState = state.GetState(inputParts[2]),
                            Area = decimal.Parse(inputParts[3]),
                            OrderDate = DateTime.Parse(inputParts[4]),
                            Product = product.ReturnProduct(inputParts[5]),
                        };
                        order.Total = new CostInfo(order.Product, order.Area, order.OrderState);
                        listOfOrders.Add(order);
                    }
                }
            }
            return listOfOrders;
        }

        public override Order ReturnOrder(int orderNumber, DateTime orderDate)
        {
            Order orderToReturn = new Order();
            List<Order> orders = new List<Order>();
            orders = ReturnOrderList(orderDate);
            foreach (var order in orders)
            {
                if (order.OrderNumber == orderNumber)
                {
                    orderToReturn = order;
                }
            }
            return orderToReturn;
        }

        public override bool AddOrder(Order newOrder)
        {
            bool isSuccess;

            string orderFile = $"DataFiles\\Orders\\_{newOrder.OrderDate.ToString("MMddyyyy")}.txt";

            List<Order> ListOfOrders = new List<Order>();
            ListOfOrders = ReturnOrderList(newOrder.OrderDate);
            ListOfOrders.Add(newOrder);
            WriterAndReader.WriteToFile(ListOfOrders, orderFile);
            isSuccess = ListOfOrders.Contains(newOrder);
            return isSuccess;
        }

        public override bool RemoverOrder(Order orderToRemove)
        {
            bool isSuccess;

            string orderFile = $"DataFiles\\Orders\\_{orderToRemove.OrderDate.ToString("MMddyyyy")}.txt";

            List<Order> listOfOrders = ReturnOrderList(orderToRemove.OrderDate);

            Order orderInList = null;
            foreach (var order in listOfOrders)
            {
                if (order.OrderNumber == orderToRemove.OrderNumber)
                {
                    orderInList = order;
                }
            }
            if (orderInList != null)
            {
                listOfOrders.Remove(orderInList);
            }

            WriterAndReader.WriteToFile(listOfOrders, orderFile);
            if (listOfOrders.Count == 0)
            {
                File.Delete(orderFile);
            }
            isSuccess = listOfOrders.Contains(orderToRemove);
            if (isSuccess)
            {
                return false;
            }
            return true;
        }

        private bool ValidateFile(DateTime date)
        {

            string orderFile = $"DataFiles\\Orders\\_{date.ToString("MMddyyyy")}.txt";

            return File.Exists(orderFile);
        }
    }
}
