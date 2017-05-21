using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MGCodeTestFundLib;
using System.Collections.Generic;
using MGCodeTestFundLib.Repo;

namespace MGCodeTest.tests
{
    
    // #3 - Ability to show the portfolio's tracking error

    //  As a fund manager I would like to view the portfolio's tracking error, i.e. 
    //  how much it deviates from the benchmark So that I know whether to generate 
    //  more trades to bring the portfolio more in line with the benchmark as per the fund's mandate
    
    [TestClass]
    public class PportfolioTrackingErrorTests
    {
        
        FundManager fm;
        BenchmarkRepo benchmarkRepo;
        [TestInitialize]
        public void Init()
        {
            // setup, create sample benchmark index 
            fm = new FundManager();
            var indexName = "UK Corporate Benchmark";
            //var repo = new List<Benchmark>();
            benchmarkRepo = new BenchmarkRepo();

            benchmarkRepo.Add(new Benchmark() { IndexWeight = 50, Name = indexName, Stock = new Stock() { Symbol = "UNDE" } });
            benchmarkRepo.Add(new Benchmark() { IndexWeight = 25, Name = indexName, Stock = new Stock() { Symbol = "ROBO" } });
            benchmarkRepo.Add(new Benchmark() { IndexWeight = 10, Name = indexName, Stock = new Stock() { Symbol = "ABAC" } });
            benchmarkRepo.Add(new Benchmark() { IndexWeight = 10, Name = indexName, Stock = new Stock() { Symbol = "DODO" } });
            benchmarkRepo.Add(new Benchmark() { IndexWeight = 5, Name = indexName, Stock = new Stock() { Symbol = "ZIZA" } });

            // create a test fund
            var fund = fm.CreateFund("UK Equity Tracker", new FundRepo(), benchmarkRepo);

            // add cash
            fund.AddCash(50023.00M);
            // add some stock to the newly created fund
            fund.AddStock(new Stock() { Symbol = "ROBO" }, qty: 100, price: 8.22M);
            fund.AddStock(new Stock() { Symbol = "UNDE" }, qty: 500, price: 3.11M);
            fund.AddStock(new Stock() { Symbol = "ABAC" }, qty: 600, price: .39M);
            fund.AddStock(new Stock() { Symbol = "DODO" }, qty: 30, price: 12M);
        }


        [TestMethod]
        public void Given_in_a_portfolio_a_position_weighting_may_be_larger_or_smaller_than_its_benchmark_weighting_for_a_given_stock_When_the_tracking_error_for_one_stock_will_always_be_a_positive_number()
        {

        // Criteria 3A

        //* Given a position's weighting (%) may be larger or smaller than it's equivalent benchmark 
        //* weighting (%) for a given stock

        //* When the tracking error for one stock is calculated

        //* Then this will always be a positive number (i.e. an absolute difference)
            //Arrange
            var robo = new Stock() { Symbol = "ROBO" };
            //Act
            var actual = fm.CalculateFundPerformanceByStock(robo);
            // Fund weight of ROBO stock = 23.68
            // Benchmark Index weightage for ROBO = 25
            // Error = Fund weightage - Index weightage = -1.32
            // Expected absolute value of Error = 1.32

            var expected = 1.32M;
            // Assert
            Assert.AreEqual(expected, actual.Error);

        }
        [TestMethod]
        public void Given_in_a_portfolio_there_will_be_a_portfolio_weighting_and_benchmark_weighting_percentage_for_a_given_stock_When_the_portfolio_tracking_error_will_be_the_sum_of_the_tracking_error_across_all_stocks_in_that_portfolio()
        {
            
        // Criteria 3B
        //* Given there will be a portfolio weighting (%) and benchmark weighting (%) for a given stock
        //* When the portfolio's tracking error is calculated
        //* Then it will be the sum of the tracking error across all stocks on that portfolio
            var actual = fm.CalculateFundTrackingError();
            var expected = 10.15M;

            Assert.AreEqual<decimal>(expected, actual);


        }

       
    }
}
