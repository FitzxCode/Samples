using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooringData.Products
{
    public class ProductsRepositoryFactory
    {
        public static IProductRepository CreateProductRepository()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            IProductRepository repo;

            switch (mode.ToUpper())
            {
                case "TEST":
                    repo = new ProductInMemoryRepository();
                    break;

                case "PROD":
                    repo = new ProductsInFileRepository();
                    break;
                default:
                    throw new Exception("I don't know that mode!");
            }
            return repo;
        }
    }
}
