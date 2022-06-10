using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace XPOAPITest
{
    [DataContract()]
    public class QuoteItem: Item
    {
        public double declaredValueAmount { get; set; }
        public string productCode { get; set; }
        [DataMember(Name = "class")]
        public string Class { get; set; }
    }
}
