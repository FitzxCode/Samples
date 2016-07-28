using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringModels;

namespace SGFlooringData.TaxInfo
{
    public class TaxInFileRepository:ITaxRepository
    {
        public Dictionary<string, State> States { get; private set; }


        public TaxInFileRepository()
        {
            States = new Dictionary<string, State>();
            using (StreamReader sr = File.OpenText(@"DataFiles\State.txt"))
            {
                string inputLine = "";
                string[] inputParts;

                // Reading Header
                sr.ReadLine();

                while ((inputLine = sr.ReadLine()) != null)
                {
                    inputParts = inputLine.Split(',');

                    State state = new State
                    {
                        StateAbrevation = inputParts[0],
                        StateName = inputParts[1],
                        TaxRate = decimal.Parse(inputParts[2])
                    };

                    States.Add(state.StateAbrevation, state);
                }
            }
        }

        public State GetState(string stateAbv)
        {
            

            State state = States[stateAbv];
            
            return state;
        }

    }
}
