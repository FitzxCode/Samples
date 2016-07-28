using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringModels;

namespace SGFlooringData.Products
{
    public class ProductInMemoryRepository:IProductRepository
    {
        public Dictionary<string, ProductInfo> Products { get; private set; }

        public ProductInMemoryRepository()
        {
            Products = new Dictionary<string, ProductInfo>();


            Products.Add("WOOD", new ProductInfo
            {
                ProductType = "Wood",
                LaborCostPerSquareFoot = 2,
                MaterialCostPerSquareFoot = 3

            });
            Products.Add("TILE", new ProductInfo
            {
                ProductType = "Tile",
                LaborCostPerSquareFoot = 1,
                MaterialCostPerSquareFoot = 2,
            });
        }

        public ProductInfo ReturnProduct(string productName)
        {

            return Products[productName];
        }
    }
}
