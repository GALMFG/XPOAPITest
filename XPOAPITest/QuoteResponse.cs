using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOAPITest
{
    public class QuoteResponse : Response
    {
        public int masterQuoteId { get; set; }
        public IList<PriceResponse> priceSearchResponse { get; set; }

        public IList<String> errorDetails { get; set; }
    }
}
