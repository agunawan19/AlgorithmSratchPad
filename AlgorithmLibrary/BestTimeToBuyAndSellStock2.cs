namespace AlgorithmLibrary
{
    /// <summary>
    /// Say you have an array prices for which the ith element is the price of a given stock on day i.
    ///
    /// Design an algorithm to find the maximum profit. You may complete as many transactions as you like (i.e., buy one and sell one share of the stock multiple times).
    ///
    /// Note: You may not engage in multiple transactions at the same time (i.e., you must sell the stock before you buy again).
    ///
    /// Example 1:
    ///
    /// Input: [7,1,5,3,6,4]
    /// Output: 7
    /// Explanation: Buy on day 2 (price = 1) and sell on day 3 (price = 5), profit = 5-1 = 4.
    /// Then buy on day 4 (price = 3) and sell on day 5 (price = 6), profit = 6-3 = 3.
    ///
    /// Example 2:
    ///
    /// Input: [1,2,3,4,5]
    /// Output: 4
    /// Explanation: Buy on day 1 (price = 1) and sell on day 5 (price = 5), profit = 5-1 = 4.
    /// Note that you cannot buy on day 1, buy on day 2 and sell them later, as you are
    /// engaging multiple transactions at the same time. You must sell before buying again.
    ///
    /// Example 3:
    ///
    /// Input: [7,6,4,3,1]
    /// Output: 0
    /// Explanation: In this case, no transaction is done, i.e. max profit = 0.
    /// </summary>
    public class BestTimeToBuyAndSellStock2
    {
        /// <summary>
        /// Time complexity: O(n^2). Recursive function is called n^n times.
        /// Space complexity: O(n). Dept of recursion is n.
        /// </summary>
        /// <param name="prices">Prices in Array of integer</param>
        /// <returns>Max profit</returns>
        public static int MaxProfitBruteForce(int[] prices) => Calculate(prices, 0);

        private static int Calculate(int[] prices, int s)
        {
            var max = 0;
            if (prices == null || s >= prices.Length) return max;

            for (var start = s; start < prices.Length; start++)
            {
                int maxProfit = 0;

                for (var i = start; i < prices.Length; i++)
                {
                    if (prices[start] >= prices[i]) continue;

                    var profit = Calculate(prices, i + 1) + prices[i] - prices[start];
                    if (profit > maxProfit) maxProfit = profit;
                }

                if (maxProfit > max) max = maxProfit;
            }

            return max;
        }

        /// <summary>
        /// Time complexity: O(n). Single pass.
        /// Space complexity: O(1). Const space required.
        /// </summary>
        /// <param name="prices">Prices in Array of integer</param>
        /// <returns>Max profit</returns>
        public static int MaxProfitPeakValleyApproach(int[] prices)
        {
            var i = 0;
            var maxProfit = 0;

            while (prices != null && i < prices.Length - 1)
            {
                while (i < prices.Length - 1 && prices[i] >= prices[i + 1]) i++;
                var valley = prices[i];
                while (i < prices.Length - 1 && prices[i] <= prices[i + 1]) i++;
                var peak = prices[i];
                maxProfit += peak - valley;
            }

            return maxProfit;
        }

        /// <summary>
        /// Time complexity: O(n). Single pass.
        /// Space complexity: O(i). Constant space needed.
        /// </summary>
        /// <param name="prices">Prices in Array of integer</param>
        /// <returns>Max profit</returns>
        public static int MaxProfitOnePass(int[] prices)
        {
            var maxProfit = 0;
            if (prices == null) return 0;
            for (var i = 1; i < prices.Length; i++)
                if (prices[i] > prices[i - 1])
                    maxProfit += prices[i] - prices[i - 1];

            return maxProfit;
        }

    }
}
