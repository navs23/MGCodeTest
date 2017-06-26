using MGCodeTestFundLib.Contracts;

namespace MGCodeTestFundLib
{
    public class Stock : IStock
    {
        public string Symbol { get; set; }
        // override object.Equals
        public override bool Equals(object obj)
        {
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var temp = obj as Stock;
            return temp.Symbol.Equals(this.Symbol);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here

            return this.Symbol.GetHashCode();
        }

    }

}
