using MGCodeTestFundLib.Contracts;
using System;
using System.Linq;

namespace MGCodeTestFundLib
{
    public class FundManager
    {
        private IRepo<IBenchmark> _benchmarkRepo;
        private ILogger _logger;
        private IFund _fund;

        public FundManager(IRepo<IBenchmark> benchmarkRepo)
        {
            _benchmarkRepo = benchmarkRepo;
        }

        public FundManager(IFund fund)
        {
            _fund = fund;
        }

        public FundManager(IFund fund, IRepo<IBenchmark> benchmarkRepo)
        {
            _fund = fund;
            _benchmarkRepo = benchmarkRepo;
        }
       
        public FundManager()
        {
           
        }

        public IFund CreateFund(string fundName, IRepo<IStockPosition> fundRepo, IRepo<IBenchmark> benchmarkRepo, ILogger logger=null)
        {
            if (fundRepo == null) throw new Exception("fund repository is not inilialised");

            if (logger != null) _logger = logger;
            try
            {
                
                _fund = new Fund(fundName, fundRepo);
                _benchmarkRepo = benchmarkRepo;
                return _fund;
            }
            catch (Exception)
            {
                
                throw;
            }
           

        }

        public decimal GetTotalFundValuation(IFund fund)
        {
            try
            {
                return Math.Round(_fund.CurrentValuation, 2);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        public PerformanceIndicator CalculateFundPerformanceByStock(IStock stock)
        {
            try
            {

            
            // find index weight for the stock in the benchmark index repo
            var defaultBenchmark = new Benchmark(){Stock=stock,IndexWeight=0};
            var result = from benchmark in _benchmarkRepo.Repo
                      where benchmark.Stock.Equals(stock)
                      
                      select new Benchmark(){
                          IndexWeight=benchmark.IndexWeight, 
                          Stock=stock
                      };
            // get fund weight
            var fundWeight=_fund.GetWeightageByStock(stock);
            // index weight
            var indexWeight = result.DefaultIfEmpty(defaultBenchmark).Single().IndexWeight;

            return new PerformanceIndicator() { 
                Stock = stock,  
                FundWeightage = fundWeight ,
                IndexWeightage =indexWeight
            };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal CalculateFundTrackingError() {
            try
            {
                var fundTrackingError = this._fund.Repo.Repo.Sum(s => this.CalculateFundPerformanceByStock(s.Stock).Error);
                return fundTrackingError;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
      
    }
}
