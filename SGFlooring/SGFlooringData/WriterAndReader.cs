using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGFlooringModels;

namespace SGFlooringData
{
    public class WriterAndReader
    {
        public static void WriteToFile(List<Order> orderList, string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                foreach (var order in orderList)
                {
                    sw.WriteLine(
                        $"{order.OrderNumber}¬{order.CustomerName}¬{order.OrderState.StateAbrevation}¬{order.Area}¬{order.OrderDate}¬{order.Product.ProductType}");
                }
            }
        }

        public static void WriteExceptionToLogFile(Exception ex)
        {
            string logFile = @"DataFiles\log.txt";

            using (StreamWriter sw = new StreamWriter(logFile, true))
            {
                sw.WriteLine("======== Error ========");
                sw.WriteLine($"Error occurred on: {DateTime.Now}");
                sw.WriteLine($"Message: {ex.Message}, Source: {ex.Source}");
                sw.WriteLine($"Method Base: { ex.TargetSite}, Inner Exception: { ex.InnerException}");
                sw.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        public static void WriteErrorToLogFile(Error errorToLog)
        {
            string logFile = @"DataFiles\log.txt";

            using (StreamWriter sw = new StreamWriter(logFile, true))
            {
                sw.WriteLine("======== Error ========");
                sw.WriteLine($"Error occurred on: {DateTime.Now}");
                sw.WriteLine($"Location of Error: {errorToLog.Location}");
                sw.WriteLine($"Users Input: {errorToLog.UserInput}");
            }
        }
    }
}
