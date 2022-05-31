
using System.Runtime.Serialization;

namespace XPOAPITest
{
    [DataContract]
    public class Item
    {
        public Item()
        {
            hazardousItemInfo = new HazardousItemInfo();

            temperatureInformation = new TemperatureInformation();
        }
        [DataMember(Name = "itemDescription")]
        public string itemDescription{ get; set; }
        [DataMember(Name = "itemNumber")]
        public string itemNumber{ get; set; }
        [DataMember(Name = "units")]
        public int units{ get; set; }
        [DataMember(Name = "unitTypeCode")]
        public string unitTypeCode{ get; set; }
        [DataMember(Name = "packageUnits")]

        public int packageUnits{ get; set; }
        [DataMember(Name = "packageTypeCode")]
        public string packageTypeCode{ get; set; }
        [DataMember(Name = "weight")]
        public int weight{ get; set; }
        [DataMember(Name = "weightUomCode")]
        public string weightUomCode{ get; set; }
        [DataMember(Name = "height")]
        public int height{ get; set; }
        [DataMember(Name = "heightUomCode")]
        public string heightUomCode{ get; set; }
        [DataMember(Name = "length")]
        public int length{ get; set; }
        [DataMember(Name = "lengthUomCode")]
        public string lengthUomCode{ get; set; }
        [DataMember(Name = "width")]
        public int width{ get; set; }
        [DataMember(Name = "widthUomCode")]
        public string widthUomCode{ get; set; }
        [DataMember(Name = "isHazmat")]

        public bool isHazmat { get; set; }
        [DataMember(Name = "hazardousItemInfo")]
        public HazardousItemInfo hazardousItemInfo { get; set; }
        [DataMember(Name = "isTemperatureControlled")]

        public bool isTemperatureControlled { get; set; }
        [DataMember(Name = "temperatureInformation")]

        public TemperatureInformation temperatureInformation { get; set; }
        [DataMember(Name = "sku")]
        public string sku { get; set; }
        [DataMember(Name = "nmfcCode")]

        public string nmfcCode { get; set; }


    }
}
