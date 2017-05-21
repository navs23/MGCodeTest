using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MGCodeTestFundLib;
using System.Collections.Generic;
using MGCodeTestFundLib.Contracts;
using MGCodeTestFundLib.Repo;

namespace MGCodeTest.tests
{
    [TestClass]
    public class PositionWeighingTests
    {
        Fund fund;
        
        //#1 - Ability to know the weighting of each position in relation to the fund

        //As a fund manager I would like to view the weighting of each position So that I can easily monitor when I am close to breaching certain compliance rules

        [TestInitialize]
        public void Init() {
            fund = new Fund("UK Equity Tracker",new FundRepo());
                        
            fund.AddCash(50023.00M);
            fund.AddStock(new Stock() { Symbol = "ROBO" }, qty:100, price: 8.22M );
            fund.AddStock(new Stock() { Symbol = "UNDE" }, qty : 500, price: 3.11M );
            fund.AddStock(new Stock() {  Symbol = "ABAC" }, qty : 600, price :.39M );
            fund.AddStock(new Stock() {  Symbol = "DODO" }, qty : 30, price : 12M );
        }
        [TestMethod]
        
        public void Given_a_portfolio_contains_a_stock_position_When_position_has_holdings_and_market_price_Then_valuation_is_holding_times_market_price()
        {

            // Criteria 1A
            //* Given stock positions contain a holding and a market price
            // Arrange
            var roboStock = new StockPosition() { Stock = new Stock() { Symbol = "ROBO" }, Qty = 100, Price = 8.22M };
            //* When a position has both holding and market price
            //* Then the valuation of the position is holding x market price
            //Act
            var actual = roboStock.GetValue();
            var expected = 822M;
            //Assert
            Assert.AreEqual<decimal>(expected, actual);

        }
        [TestMethod]
        public void Given_a_portfolio_has_cash_and_the_holding_amount_is_in_pennies_When_the_weighting_of_the_cash_position_is_required_Then_the_cash_valuation_is_holding_amount_divideby_100()
        {
            //  Criteria 1B
            //* Given cash is held in the fund used for purchasing more stock and the holding amount is in pennies
            // Arrange
            //* When the weighting of the cash position is required
            //* Then the cash valuation is holding / 100
            //Act
            var actual = fund.CashValuation;
            var expected = 500.23M;
            
            //Assert

            Assert.AreEqual<decimal>(expected,actual);

        }
        [TestMethod]
        public void Given_a_portfolio_is_a_basket_of_stock_and_cash_positions_When_the_value_of_a_portfolio_is_required_Then_the_sum_of_all_position_valuations_will_be_returned()
        {

            //  Criteria 1C
            //* Given a portfolio is a basket of stock/cash positions
            //* When the value of a portfolio is required
            var expected = 3471.23M;
            var actual = fund.CurrentValuation;
            //* Then the sum of all position valuations will be returned
            
            Assert.AreEqual<decimal>(expected,actual );

        }
        [TestMethod]
        public void Given_a_portfolio_value_is_the_sum_of_all_position_valuations_When_the_weighting_of_a_position_is_required_Then_that_position_valuation_divided_by_the_portfolio_valuation()
        {

            // Criteria 1D
            //* Given a portfolio value is the sum of all position valuations
            var expected = 23.68M;
            var actual = Math.Round(fund.GetWeightageByStock(new Stock() { Symbol = "ROBO" }), 2);
            //* When the weighting of a position is required
            //* Then that position's valuation divided by the portfolio valuation will be returned
            Assert.AreEqual<decimal>(expected,actual );
        }

    }
}
