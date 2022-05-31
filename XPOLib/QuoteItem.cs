using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace XPOAPITest
{
    [DataContract]
    public class QuoteItem: Item
    {
        [DataMember(Name = "declaredValueAmount")]
        public double declaredValueAmount { get; set; }
        [DataMember(Name = "productCode")]
        public string productCode { get; set; }
        [DataMember(Name = "class")]
        public string classcode { get; set; }
    }
}
