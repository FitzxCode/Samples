using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using SGFlooringModels;

namespace SGFlooringData.TaxInfo
{
    public class TaxInMemoryRepository:ITaxRepository
    {
        public Dictionary<string, State> States { get; private set; }

        public TaxInMemoryRepository()
        {
            States = new Dictionary<string, State>();

            States.Add("OH", new State
            {
                StateAbrevation = "OH",
                StateName = "Ohio",
                TaxRate = 5.75m
            });

            States.Add("NJ", new State
            {
                StateAbrevation = "NJ",
                StateName = "New Jersey",
                TaxRate = 7
            });
        }

        public State GetState(string stateAbv)
        {
            return States[stateAbv];
        }
    }
}
