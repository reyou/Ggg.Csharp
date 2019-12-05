namespace intro1
{
    public class StockListing
    {
        public string NASDAQTickerSymbol { get; }
        public decimal Open { get; }
        public decimal High { get; }
        public decimal Low { get; }
        public string MarketCap { get; }

        public StockListing(string nasdaq, decimal open, decimal high, decimal low, string marketCap)
        {
            NASDAQTickerSymbol = nasdaq;
            Open = open;
            High = high;
            Low = low;
            MarketCap = marketCap;
        }
    }
}
