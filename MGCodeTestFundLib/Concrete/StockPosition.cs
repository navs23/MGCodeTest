using MGCodeTestFundLib.Contracts;
using System;

namespace MGCodeTestFundLib
{
    public class StockPosition : IStockPosition
    {

        public int Qty
        {
            get;
            set;
        }

        public decimal Price
        {
            get;
            set;
        }

        public decimal GetValue()
        {
            return Math.Round(this.Price * this.Qty, 2);
        }
        
        public IStock Stock
        {
            get;
            set;
        }
                
    }
}
