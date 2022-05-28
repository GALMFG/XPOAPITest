using System.Collections.Generic;
namespace XPOAPITest
{
    public class OrderItem : Item
    {
        public string itemCode{ get; set; }
        public string htsCode{ get; set; }
        public string purchaseOrderNumber{ get; set; }
        public bool isOversized{ get; set; }
        public bool isStackable{ get; set; }
        public string classCode{ get; set; }
        public string nmfcCode{ get; set; }
        public double declaredValue{ get; set; }
        public string declaredValueCurrencyCode{ get; set; }
        public double itemPrice{ get; set; }
        public string itemPriceCurrencyCode{ get; set; }
        public IList<Service> itemServices{ get; set; }
        public IList<ItemReferenceNumber> itemReferenceNumbers{ get; set; }
        public IList<Note> itemNotes{ get; set; }
       }
}
