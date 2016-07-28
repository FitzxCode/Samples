using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooringModels
{
    public class CostInfo
    {
        public decimal LaborCost { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalPrice { get; set; }

        public CostInfo(ProductInfo product, decimal area, State state)
        {
            decimal laborCost = (area*product.LaborCostPerSquareFoot);
            decimal materialCost = (area*product.MaterialCostPerSquareFoot);
            LaborCost = Math.Round(laborCost, 2);
            MaterialCost = Math.Round(materialCost, 2);
            decimal totalTax = ((state.TaxRate/100) * (laborCost + materialCost));
            TotalTax = Math.Round(totalTax, 2);
            TotalPrice = (LaborCost + MaterialCost + TotalTax);

        }
    }
}
