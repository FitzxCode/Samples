using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringModels;

namespace SGFlooringData.Products
{
    public class ProductsInFileRepository:IProductRepository
    {
        public Dictionary<string, ProductInfo> Products { get; private set; }
        public ProductInfo ReturnProduct(string productName)
        {
            productName = productName.ToUpper();
            return Products[productName];
        }

        public ProductsInFileRepository()
        {
            Products = new Dictionary<string, ProductInfo>();

            using (StreamReader sr = File.OpenText(@"DataFiles\Product.txt"))
            {
                string inputLine = "";
                string[] inputParts;

                // Reading Header Line
                sr.ReadLine();

                while ((inputLine = sr.ReadLine()) != null)
                {
                    inputParts = inputLine.Split(',');
                    ProductInfo product = new ProductInfo
                    {
                        ProductType = inputParts[0],
                        MaterialCostPerSquareFoot = decimal.Parse(inputParts[1]),
                        LaborCostPerSquareFoot = decimal.Parse(inputParts[2])
                    };

                    Products.Add(product.ProductType.ToUpper(), product);
                }
            } 
        }


    }
}
