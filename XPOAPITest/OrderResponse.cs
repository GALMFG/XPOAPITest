using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOAPITest
{
    public class OrderResponse : Response
    {
        public RatedQuoteResponse reRatedQuoteResponse { get; set; }
        public int orderId { get; set; }
        
        public string proNumber { get; set; }

        public string bolNumber { get; set; }

        

    }
}
