using System.Collections.Generic;

namespace XPOAPITest
{
    public class PriceResponse
    {
        public string mode { get; set; }
        public double lowestPrice { get; set; }

        public string message { get; set; }
        public IList<Quote> quoteDetails { get; set; }

        public Quote lowestPriceQuote { get; set; }
        public Quote lowestGuaranteedQuotePrice { get; set; }
        public Quote bestDealQuotePrice { get; set; }
    }
}