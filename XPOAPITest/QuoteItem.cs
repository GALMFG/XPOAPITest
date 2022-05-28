namespace XPOAPITest
{
    public class QuoteItem: Item
    {
        public double declaredValueAmount { get; set; }
        public string productCode { get; set; }
     //   [JsonPropertyName("class")]
        public string classcode { get; set; }
    }
}
