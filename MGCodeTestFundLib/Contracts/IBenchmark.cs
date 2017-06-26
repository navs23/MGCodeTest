using System;
namespace MGCodeTestFundLib.Contracts
{
    public interface IBenchmark
    {
        int IndexWeight { get; set; }
        string Name { get; set; }
        IStock Stock { get;  set;  }
    }
}
