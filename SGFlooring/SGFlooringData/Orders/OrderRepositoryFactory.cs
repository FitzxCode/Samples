using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringData.Orders;

namespace SGFlooringData
{
    public class OrderRepositoryFactory
    {
        public static OrderRepository CreateRepository()
        {
            var mode = ConfigurationManager.AppSettings["Mode"];

            OrderRepository repo;

            switch (mode)
            {
                case "TEST":
                    repo = new InMemoryRepository();
                    break;

                case "PROD":
                    repo = new FileRepository();
                    break;
                default:
                    throw new Exception("I don't know that mode!");
            }
            return repo;
        }
    }
}
