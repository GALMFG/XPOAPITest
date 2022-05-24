using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
