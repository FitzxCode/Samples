using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringModels;

namespace SGFlooringData
{
    public interface IProductRepository
    {
        Dictionary<string, ProductInfo> Products { get; }
        ProductInfo ReturnProduct(string productName);

    }
}
