using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringData.Orders;
using SGFlooringData.Products;
using SGFlooringData.TaxInfo;
using SGFlooringModels;

namespace SGFlooringData
{
    public class InMemoryRepository : OrderRepository
    {
        private static IProductRepository _product = new ProductInMemoryRepository();

        private static ITaxRepository _state = new TaxInMemoryRepository();

        static InMemoryRepository()
        {




            OrderList.Add(new Order
            {
                Area = 15,
                Product = _product.ReturnProduct("TILE"),
                CustomerName = "Bob's",
                OrderDate = DateTime.Parse("01/12/1992"),
                OrderNumber = 1,
                OrderState = _state.GetState("NJ"),
                Total = new CostInfo(_product.ReturnProduct("TILE"), 20, _state.GetState("NJ"))
            });
            OrderList.Add(new Order
            {
                Area = 20,
                Product = _product.ReturnProduct("WOOD"),
                CustomerName = "Brendan's",
                OrderDate = DateTime.Parse("01/12/1992"),
                OrderNumber = 2,
                OrderState = _state.GetState("OH"),
                Total = new CostInfo(_product.ReturnProduct("WOOD"), 20, _state.GetState("OH"))
            });
            OrderList.Add(new Order
            {
                Area = 25,
                Product = _product.ReturnProduct("TILE"),
                CustomerName = "Victor's",
                OrderDate = DateTime.Parse("06/28/2016"),
                OrderNumber = 3,
                OrderState = _state.GetState("OH"),
                Total = new CostInfo(_product.ReturnProduct("TILE"), 20, _state.GetState("OH"))
            });
        }

        public override List<string> ReturnOrdersDates()
        {
            var result = OrderList.Select(o => o.OrderDate).Distinct();

            List<string> dates = new List<string>();

            foreach (var date in result)
            {
                dates.Add(date.ToString("MMddyyyy"));
            }
            return dates;
        }

        public override List<Order> ReturnOrderList(DateTime date)
        {
            List<Order> listOfOrders = new List<Order>();
            foreach (var order in OrderList)
            {
                if (order.OrderDate == date)
                {
                    listOfOrders.Add(order);
                }
            }
            return listOfOrders;
        }

        public override Order ReturnOrder(int orderNumber, DateTime date)
        {
            Order returnOrder = new Order();
            foreach (var order in OrderList)
            {
                
                if (order.OrderDate == date && order.OrderNumber == orderNumber)
                {
                    returnOrder = order;
                }
            }
            return returnOrder;
        }

        public override bool AddOrder(Order newOrder)
        {
            OrderList.Add(newOrder);
            if (OrderList.Contains(newOrder))
            {
                return true;
            }
            return false;
        }

        public override bool RemoverOrder (Order orderToRemove)
        {
            List<Order> listOfOrders = ReturnOrderList(orderToRemove.OrderDate); 
            foreach (var order in listOfOrders)
            {
                if (order.OrderNumber == orderToRemove.OrderNumber)
                {
                    OrderList.Remove(order);
                }
            }
            if (OrderList.Contains(orderToRemove))
            {
                return false;
            }
            return true;
        }

        public override int GetNewOrderNumber(DateTime currentOrderDate)
        {
            int newOrderNumber = 0;
            List<Order> listOfOrders = ReturnOrderList(currentOrderDate);
            if (listOfOrders.Count > 0)
            {
                newOrderNumber = listOfOrders.OrderByDescending(o => o.OrderNumber).Select(o => o.OrderNumber).First();
            }
            newOrderNumber++;
            return newOrderNumber;
        }
    }
}
