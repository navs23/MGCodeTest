using MGCodeTestFundLib.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGCodeTestFundLib
{
    public class Benchmark : IBenchmark
    {
        public string Name
        {
            get;
            set;
        }
        public int IndexWeight
        {
            get;
            set;
        }
        public IStock Stock
        {
            get;
            set;
        }

        
    }
}
