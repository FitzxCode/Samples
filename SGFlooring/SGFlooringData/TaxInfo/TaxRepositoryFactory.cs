using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringData.Products;

namespace SGFlooringData.TaxInfo
{
    public class TaxRepositoryFactory
    {
        public static ITaxRepository CreateTaxRepository()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            ITaxRepository repo;

            switch (mode.ToUpper())
            {
                case "TEST":
                    repo = new TaxInMemoryRepository();
                    break;

                case "PROD":
                    repo = new TaxInFileRepository();
                    break;
                default:
                    throw new Exception("I don't know that mode!");
            }
            return repo;
        }
    }
}
