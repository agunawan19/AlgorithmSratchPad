namespace AlgorithmLibrary.BestTimeToBuyAndSellStocks
{
    /// <summary>
    /// Say you have an array for which the ith element is the price of a given stock on day i.
    /// If you were only permitted to complete at most one transaction (i.e., buy one and sell one share of the stock), design an algorithm to find the maximum profit.
    ///
    /// Note that you cannot sell a stock before you buy one.
    ///
    /// Example 1:
    ///
    /// Input: [7,1,5,3,6,4]
    /// Output: 5
    /// Explanation: Buy on day 2 (price = 1) and sell on day 5 (price = 6), profit = 6-1 = 5.
    /// Not 7-1 = 6, as selling price needs to be larger than buying price.
    ///
    /// Example 2:
    ///
    /// Input: [7,6,4,3,1]
    /// Output: 0
    /// Explanation: In this case, no transaction is done, i.e. max profit = 0.
    /// </summary>
    public class BestTimeToBuyAndSellStock1
    {
        /// <summary>
        /// Time complexity: O(n^2). Loop run n(n - 1) / 2 times.
        /// Space complexity: O(i). Only two variables - maxProfit and profit are used.
        /// </summary>
        /// <param name="prices">Prices in Array of integer</param>
        /// <returns>Max profit</returns>
        public static int MaxProfitBruteForce(int[] prices)
        {
            var maxProfit = 0;

            for (var i = 0; i < prices.Length - 1; i++)
            {
                for (var j = i + 1; j < prices.Length; j++)
                {
                    int profit = prices[j] - prices[i];
                    if (profit > maxProfit) maxProfit = profit;
                }
            }

            return maxProfit;
        }

        /// <summary>
        /// Time complexity: O(n). Only a single pass is needed.
        /// Space complexity: O(i). Only two variables are used.
        /// </summary>
        /// <param name="prices">Prices in Array of integer</param>
        /// <returns>Max profit</returns>
        public static int MaxProfitOnePass(int[] prices)
        {
            int minPrice = int.MaxValue;
            int maxProfit = 0;

            foreach (var price in prices)
            {
                if (price < minPrice)
                    minPrice = price;
                else if (price - minPrice > maxProfit)
                    maxProfit = price - minPrice;
            }

            return maxProfit;
        }
    }
}
