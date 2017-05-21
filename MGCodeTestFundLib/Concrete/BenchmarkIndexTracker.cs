using MGCodeTestFundLib.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGCodeTestFundLib
{
    public class PerformanceIndicator
    {
        public IStock Stock
        {
            get;
            set;
        }

        public decimal FundWeightage
        {
            get;
            set;
        }

        public decimal IndexWeightage
        {
            get;
            set;
        }

        public decimal Error { get {
            return Math.Abs((this.FundWeightage - this.IndexWeightage));
        } }

        // override object.Equals
        public override bool Equals(object obj)
        {

            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var temp = obj as PerformanceIndicator;

            return (temp.IndexWeightage == this.IndexWeightage && temp.FundWeightage == this.FundWeightage) && (temp.Stock == this.Stock);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {

            return this.Stock.GetHashCode() + this.IndexWeightage.GetHashCode() + this.FundWeightage.GetHashCode();
        }
    }
}
