using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SGFlooringData;
using SGFlooringData.Orders;
using SGFlooringData.Products;
using SGFlooringData.TaxInfo;
using SGFlooringModels;

namespace SGFlooringBLL
{
    public class OrdersManagement
    {
        private OrderRepository _orderRepo = OrderRepositoryFactory.CreateRepository();
        private ITaxRepository _taxRepo = TaxRepositoryFactory.CreateTaxRepository();
        private IProductRepository _productRepo = ProductsRepositoryFactory.CreateProductRepository();

        public List<Order> OrderList(DateTime orderDate)
        {

            return _orderRepo.ReturnOrderList(orderDate);
        }

        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now.Date;
        }

        public int GetNewOrderNumber(DateTime orderDate)
        {
            
            return _orderRepo.GetNewOrderNumber(orderDate);
        }

        public bool ValidateState(string input)
        {
            return  _taxRepo.States.ContainsKey(input);
        }

        public State GetNewStateInfo(string input)
        {
            return _taxRepo.GetState(input);
        }

        public bool ValidateProductInfo(string input)
        {
            return _productRepo.Products.ContainsKey(input);
        }

        public ProductInfo GetProductInfo(string input)
        {
            return _productRepo.ReturnProduct(input);
        }

        public bool AddOrder(Order orderToAdd)
        {
            return _orderRepo.AddOrder(orderToAdd);
        }

        public bool RemoveOrder(Order orderToRemove)
        {
            bool isRemoved = _orderRepo.RemoverOrder(orderToRemove);
            return isRemoved;
        }

        public bool ValidateDate(DateTime date)
        {
            List<Order> dateList = OrderList(date);
            if (dateList.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool ValidateOrderNumber(DateTime date, int orderNumber)
        {
            List<Order> orderList = OrderList(date);
            for (int i = 0; i < orderList.Count; i++)
            {
                if (orderList[i].OrderNumber == orderNumber)
                {
                    return true;
                }
            }
            return false;
        }

        public Order RetrieveOrder(DateTime date, int orderNumber)
        {
            List<Order> orderList = OrderList(date);
            Order order = orderList.Where(o => o.OrderNumber == orderNumber).Select(o => o).First();
            return order;
        }

        public Dictionary<string, ProductInfo> GetProductCatalog()
        {
            return _productRepo.Products;
        }

        public Dictionary<string, State> GetServiceStates()
        {
            return _taxRepo.States;
        }

        public List<string> OrderDatesList()
        {
            return _orderRepo.ReturnOrdersDates();
        }
    }
}
