using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MGCodeTestFundLib;
using System.Collections.Generic;
using MGCodeTestFundLib.Repo;

namespace MGCodeTest.tests
{
    [TestClass]
    public class PositionAgainstBenchmarkTests
    {
        //Fund fund;
        FundManager fm;
        BenchmarkRepo benchmarkRepo;
        [TestInitialize]
        public void Init()
        {
            // setup, create sample benchmark index  repo
            fm = new FundManager();
            var indexName = "UK Corporate Benchmark";
            benchmarkRepo = new BenchmarkRepo();

            benchmarkRepo.Add(new Benchmark() { IndexWeight = 50, Name = indexName, Stock = new Stock() { Symbol = "UNDE" } });
            benchmarkRepo.Add(new Benchmark() { IndexWeight = 25, Name = indexName, Stock = new Stock() { Symbol = "ROBO" } });
            benchmarkRepo.Add(new Benchmark() { IndexWeight = 10, Name = indexName, Stock = new Stock() { Symbol = "ABAC" } });
            benchmarkRepo.Add(new Benchmark() { IndexWeight = 10, Name = indexName, Stock = new Stock() { Symbol = "DODO" } });
            benchmarkRepo.Add(new Benchmark() { IndexWeight = 5, Name = indexName, Stock = new Stock() { Symbol = "ZIZA" } });
            
            // create a mock fund
            var fund = fm.CreateFund("UK Equity Tracker", new FundRepo(), benchmarkRepo);

            // add cash tot the fund
            fund.AddCash(50023.00M);
            // add some stock to the newly created fund
            fund.AddCash(50023.00M);
            fund.AddStock(new Stock() { Symbol = "ROBO" }, qty: 100, price: 8.22M);
            fund.AddStock(new Stock() { Symbol = "UNDE" }, qty: 500, price: 3.11M);
            fund.AddStock(new Stock() { Symbol = "ABAC" }, qty: 600, price: .39M);
            fund.AddStock(new Stock() { Symbol = "DODO" }, qty: 30, price: 12M);
            fund.AddStock(new Stock() { Symbol = "XYZ" }, qty: 100, price: 10M );
        }


        [TestMethod]
        public void Given_UK_Equity_Tracker_closely_resembles_the_UK_Corporate_Benchmark_When_there_is_a_stock_symbol_within_both_the_fund_and_the_benchmark_Then_return_the_stock_symbol_with_both_the_position_weighting_and_the_benchmark_weighting()
        {

            //Criteria 2A
            //* Given the UK Equity Tracker closely resembles the UK Corporate Benchmark
            //* When there is a stock symbol within both the fund and the benchmark
            //* Then return the stock symbol with both the position weighting (as %) and the benchmark weighting (as %)
            //Arrange
            var robo = new Stock() { Symbol = "ROBO" };
            //Act
            var actual = fm.CalculateFundPerformanceByStock(robo);
            var expected = new PerformanceIndicator() { 
                FundWeightage = 18.38M, 
                IndexWeightage = 25.00M, 
                Stock = robo };

            // Assert
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void Given_UK_Equity_Tracker_closely_resembles_the_UK_Corporate_Benchmark_When_there_is_a_stock_symbol_within_just_the_fund_Then_return_the_stock_symbol_with_both_the_position_weighting_and_the_benchmark_weighting_as_zero_percent()
        {
            // Criteria 2B

            //* Given the UK Equity Tracker closely resembles the UK Corporate Benchmark
            //* When there is a stock symbol within just the fund
            //* Then return the stock symbol with both the position weighting (as %) and the benchmark weighting with 0%
            //Arrange
            var fundOnlyStock = new Stock() { Symbol = "XYZ" };
            //Act
            var actual = fm.CalculateFundPerformanceByStock(fundOnlyStock);
            var expected = new PerformanceIndicator() { 
                FundWeightage = 22.37M, 
                IndexWeightage = 0.00M, 
                Stock = fundOnlyStock };

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Given_UK_Equity_Tracker_closely_resembles_the_UK_Corporate_Benchmark_When_there_is_a_stock_symbol_within_just_the_benchmark_Then_return_the_stock_symbol_with_both_the_position_weighting_with_0_percentage_and_the_benchmark_weighting_percentage() { 
        
        // Criteria 2C
        //* Given the UK Equity Tracker closely resembles the UK Corporate Benchmark
        //* When there is a stock symbol within just the benchmark
        //* Then return the stock symbol with both the position weighting with 0% and the benchmark weighting (as %)

            var fundOnlyStock = new Stock() { Symbol = "ZIZA" };
            //Act
            var actual = fm.CalculateFundPerformanceByStock(fundOnlyStock);
            var expected = new PerformanceIndicator() { 
                                                     FundWeightage = 0.00M, 
                                                     IndexWeightage = 5, 
                                                     Stock = fundOnlyStock 
                                                    };

                // Assert
                Assert.AreEqual(expected, actual);

        }
    }
}
