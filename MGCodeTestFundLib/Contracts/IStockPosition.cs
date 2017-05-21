using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGCodeTestFundLib.Contracts
{
    public interface IStockPosition
    {
         IStock Stock { get; set; }
        int Qty { get; set; }
        decimal Price { get; set; } // market price
        decimal GetValue();
    }
}
