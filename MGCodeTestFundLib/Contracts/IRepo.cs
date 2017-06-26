using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGCodeTestFundLib.Contracts
{
    public interface IRepo<T>
    {
        IEnumerable<T> Repo { get;  }
        void Add(T record);
        T Find(T item);
    }
}