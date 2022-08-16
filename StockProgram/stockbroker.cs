using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace Stock
{
    public class StockBroker
    {
        public string BrokerName { get; set; }


        public List<Stock> stocks = new List<Stock>();

        public static ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();
        readonly string docPath = @"D:\OneDrive\Documents\Lab1_output.txt";

        readonly string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lab1_output.txt");

        public string titles = "Broker".PadRight(10) + "Stock".PadRight(15) +
            "Value".PadRight(10) + "Changes".PadRight(10) + "Date and Time";

        private void printFormat(string brokerName, string stockName, int currentValue, int numberChange)
        {
            Console.WriteLine($"{brokerName} \t {stockName} \t {currentValue} \t {numberChange}");
        }

        public StockBroker(string brokerName)
        {
            BrokerName = brokerName;
        }

        public void AddStock(Stock stock)
        {
            stocks.Append(stock);
            stock.ProcessComplete += Stock_ProcessComplete;
        }

        private void Stock_ProcessComplete(string stockName, int currentValue, int numberChange)
        {
            this.EventHandler(this.BrokerName, stockName, currentValue, numberChange);
        }

        /// <summary>
        ///     The eventhandler that raises the event of a change
        /// </summary>

        void EventHandler(string brokerName, string stockName, int currentValue, int numberChange)
        {
            try
            {
                Monitor.Enter(myLock);
                string statement;
                string output = (brokerName.PadRight(16) + stockName.PadRight(16) + currentValue.ToString().PadRight(16) + numberChange.ToString().PadRight(16) + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
                using (StreamWriter outputFile = new StreamWriter(destPath, true))
                {
                    outputFile.WriteLineAsync(output);
                }
                Console.WriteLine(output);
            }
            finally
            {
                Monitor.Exit(myLock);
            }
        }
    }
}