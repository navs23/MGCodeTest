using MGCodeTestFundLib.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGCodeTestFundLib.Repo
{
    public class BenchmarkRepo : IRepo<IBenchmark>
    {
        private IList<IBenchmark> _repo = new List<IBenchmark>();

        public void Add(IBenchmark record)
        {
            _repo.Add(record);
        }

        public int GetIndexWeightByStock(IStock stock)
        {
            return Repo.Single(s => s.Stock.Symbol == stock.Symbol).IndexWeight;
        }
        public BenchmarkRepo()
        {

        }
        //public BenchmarkRepo(IList<IBenchmark> storage)
        //{
           
        //    _repo = storage;
        //}
        public IEnumerable<IBenchmark> Repo
        {
            get { return _repo; }
            
        }

        public IBenchmark Find(IBenchmark obj)
        {
            return _repo.Where(item => item.Equals(obj)).Single();
        }
        public IBenchmark FindBySymbol(string symbol)
        {
            return _repo.Where(item => item.Stock.Symbol.Equals(symbol)).Single();
        }
        public IBenchmark Find(IStock stock)
        {
            return _repo.Where(item => item.Stock.Equals(stock)).Single();
        }
    }
}
