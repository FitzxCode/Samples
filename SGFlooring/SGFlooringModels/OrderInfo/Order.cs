using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooringModels
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public State OrderState { get; set; }
        public decimal Area { get; set; }
        public ProductInfo Product { get; set; }
        public CostInfo Total { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
