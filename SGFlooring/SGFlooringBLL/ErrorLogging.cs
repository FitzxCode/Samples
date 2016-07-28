using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringData;
using SGFlooringModels;

namespace SGFlooringBLL
{
    public class ErrorLogging
    {
        public static void PassExceptionToBLL(Exception ex)
        {
            WriterAndReader.WriteExceptionToLogFile(ex);
        }

        public static void PassErrorToBLL(Error errorToLog)
        {
            WriterAndReader.WriteErrorToLogFile(errorToLog);
        }
    }
}
