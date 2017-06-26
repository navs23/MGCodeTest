using MGCodeTestFundLib.Contracts;
using System;

namespace MGCodeTestFundLib
{
    // very simple poor mans loggers but can be enriched later on
    class Logger:ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
