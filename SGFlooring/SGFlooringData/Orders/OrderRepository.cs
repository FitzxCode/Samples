using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringModels;

namespace SGFlooringData.Orders
{
    public abstract class OrderRepository
    {
        protected static List <Order> OrderList { get; private set; }

        static OrderRepository()
        {
            OrderList = new List<Order>();
        }

        public abstract List<string> ReturnOrdersDates();
        
        public virtual List<Order> ReturnOrderList(DateTime date)
        {
            List<Order> ListOfOrders = new List<Order>();
            for (int i = 0; i < OrderList.Count; i++)
            {
                if (OrderList[i].OrderDate == date)
                {
                    ListOfOrders.Add(OrderList[i]);
                }
            }
            return ListOfOrders;
        }

        public virtual Order ReturnOrder(int orderNumber, DateTime date)
        {
            Order returnOrder = new Order();
            for (int i = 0; i < OrderList.Count; i++)
            {
                if (OrderList[i].OrderDate == date && OrderList[i].OrderNumber == orderNumber)
                {
                    returnOrder = OrderList[i];
                }
            }
            return returnOrder;
        }

        public virtual bool AddOrder(Order newOrder)
        {
            OrderList.Add(newOrder);
            if (OrderList.Contains(newOrder))
            {
                return true;
            }
            return false;
        }

        public abstract bool RemoverOrder(Order orderToRemove);

        public virtual int GetNewOrderNumber(DateTime currentOrderDate)
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
