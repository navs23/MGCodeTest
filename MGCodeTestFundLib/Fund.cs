using MGCodeTestFundLib.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MGCodeTestFundLib
{
    public class Fund : IFund
    {
        private decimal _cash;
        private readonly IRepo<IStockPosition> _repo;
        public string Name { get; set; }

        public Fund(string name)
        {
            Name = name;
        }
        public Fund(string name,IRepo<IStockPosition> repo)
        {
            Name = name;
             _repo=repo;
        }
        public Fund()
        {

        }

        public void AddStock(IStock stock,int qty,decimal price)
        {

            _repo.Add(new StockPosition() {Stock=stock,Qty=qty,Price=price
           });

        }

        public void AddCash(decimal cash)
        {
            _cash = cash;
        }
       
        public decimal CashValuation
        {
            get
            {
                return Math.Round(this._cash/100 ,2);
            }
        }

        public decimal CurrentValuation
        {
            get
            {
                // cash + stock valuation
                // rounding to 2 decimal places
                return Math.Round((this.CashValuation + this.StockValuation),2);

            }
        }

        public decimal GetWeightageByStock(IStock stock)
        {
            // return weightage of single stock to the total value of the fund
            var stockvaluation = this.GetValuationByStock(stock);
            var totalvalation = this.CurrentValuation;
            return Math.Round((stockvaluation / totalvalation) * 100, 2);

        }

        public decimal GetValuationByStock(IStock stock)
        {

            return _repo.Repo.Where(s => s.Stock.Equals(stock))
                .DefaultIfEmpty(new StockPosition() { Stock = stock, Qty = 0, Price = 0 })
                .Single()
                .GetValue();
        }

        public decimal StockValuation
        {
            get {
                return _repo.Repo.Sum(s => s.GetValue());
            
            }
        }

        public IRepo<IStockPosition> Repo
        {
            get
            {
                return _repo;
            }
            
        }
    }

}
