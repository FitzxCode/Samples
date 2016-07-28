using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringModels;

namespace SGFlooringData.TaxInfo
{
    public interface ITaxRepository
    {
        Dictionary<string, State> States { get; }

        State GetState(string stateAbv);

    }
}
