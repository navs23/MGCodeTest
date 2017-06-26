using MGCodeTestFundLib.Contracts;
using System;
using System.Collections.Generic;
namespace MGCodeTestFundLib.Contracts
{
   public interface IFund
    {
        void AddCash(decimal cash);
        void AddStock(IStock stock,int qty,decimal price);
        decimal CashValuation { get; }
        decimal CurrentValuation { get; }
        decimal GetValuationByStock(IStock stock);
        decimal GetWeightageByStock(IStock stock);
        string Name { get; set; }
        decimal StockValuation { get; }
        IRepo<IStockPosition> Repo { get;  }
        //IEnumerable<IStockPosition> Stocks { get; }
    }
}
