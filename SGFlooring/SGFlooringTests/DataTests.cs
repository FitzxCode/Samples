using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGFlooringData;
using SGFlooringData.Orders;
using SGFlooringData.Products;
using SGFlooringData.TaxInfo;
using SGFlooringModels;

namespace SGFlooringTests
{
    [TestFixture]
    class DataTests
    {
        [Test]
        public void GetState()
        {
            State test = new State
            {
                StateName = "Ohio",
                StateAbrevation = "OH",
                TaxRate = .0575m
            };

            TaxInMemoryRepository tax = new TaxInMemoryRepository();
            State result = tax.GetState("OH");

            Assert.AreEqual(result.StateName, test.StateName);
        }

        [Test]
        public void ReturnProduct()
        {
            ProductInfo product = new ProductInfo()
            {
                ProductType = "Wood",
                LaborCostPerSquareFoot = 2,
                MaterialCostPerSquareFoot = 3
            };
            ProductInMemoryRepository products = new ProductInMemoryRepository();
            ProductInfo result = products.ReturnProduct("WOOD");

            Assert.AreEqual(result.ProductType, product.ProductType);
        }

        [Test]
        public void GetNewOrderNumber()
        {
            InMemoryRepository repo = new InMemoryRepository();

            DateTime date = new DateTime(1992, 01, 12);
            int result = repo.GetNewOrderNumber(date);

            Assert.AreEqual(result, 3);
        }

        [Test]
        public void AddOrder()
        {
            InMemoryRepository repo = new InMemoryRepository();

            ProductInMemoryRepository product = new ProductInMemoryRepository();

            TaxInMemoryRepository state = new TaxInMemoryRepository();

            DateTime date = new DateTime(1992, 12, 01);
            Order orderToAdd = new Order()
            {
                CustomerName = "BOB",
                Area = 54,
                OrderDate = date,
                OrderNumber = repo.GetNewOrderNumber(date),
                OrderState = state.GetState("OH"),
                Product = product.ReturnProduct("WOOD"),
                Total = new CostInfo(product.ReturnProduct("WOOD"), 54, state.GetState("OH"))
            };

            repo.AddOrder(orderToAdd);

            Assert.Contains(orderToAdd, repo.ReturnOrderList(date));
        }

        [Test]
        public void RemoveOrder()
        {
            InMemoryRepository repo = new InMemoryRepository();

            DateTime date = new DateTime(1992, 01, 12);

            Order orderToRemove = repo.ReturnOrder(1, date);

            Assert.IsTrue(repo.RemoverOrder(orderToRemove));
        }

        [Test]
        public void CheckCostInfo()
        {
            ProductInMemoryRepository product = new ProductInMemoryRepository();

            TaxInMemoryRepository state = new TaxInMemoryRepository();

            State newJersey = state.GetState("NJ");

            ProductInfo wood = product.ReturnProduct("WOOD");

            decimal area = 12;

            decimal totalLabor = Math.Round((wood.LaborCostPerSquareFoot*area), 2);

            decimal totalMaterial = Math.Round((wood.MaterialCostPerSquareFoot*area), 2);

            decimal totalTax = Math.Round(((totalMaterial + totalLabor)*newJersey.TaxRate/100), 2);

            decimal totalCost = totalTax + totalLabor + totalMaterial;

            CostInfo test = new CostInfo(wood, area, newJersey);

            Assert.AreEqual(test.TotalPrice, totalCost);
        }
    }
}
