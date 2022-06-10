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
