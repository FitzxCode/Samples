using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooringModels
{
    public class Error
    {
        public string Location { get; set; }
        public string UserInput { get; set; }

        public Error()
        {
            Location = "";
            UserInput = "";
        }
    }
}
