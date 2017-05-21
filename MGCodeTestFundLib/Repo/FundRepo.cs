using MGCodeTestFundLib.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGCodeTestFundLib.Repo
{
    public class FundRepo:IRepo<IStockPosition>
    {
        // using in memory list to store the data
        private readonly IList<IStockPosition> _repo = new List<IStockPosition>();
        public FundRepo(IList<IStockPosition> repo)
        {
            _repo = repo;
        }
        public FundRepo()
        {

        }
        public void Add(IStockPosition item)
        {
            // save stock item to the storage
            _repo.Add(item);
        }

        public IEnumerable<IStockPosition> Repo
        {
            get
            {
                return _repo;
            }
           
        }


        public IStockPosition Find(IStockPosition obj)
        {
            return _repo.Where(item => item.Equals(obj)).Single();
        }

        public IStockPosition FindBySymbol(string symbol)
        {
            //throw new NotImplementedException();
            return _repo.Where(item => item.Stock.Symbol.Equals(symbol)).Single();
        }
    }
}
