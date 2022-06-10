
using System.Runtime.Serialization;

namespace XPOAPITest
{
    public class Item
    {
        public Item()
        {
            hazardousItemInfo = new HazardousItemInfo();

            temperatureInformation = new TemperatureInformation();
        }

        public string itemDescription{ get; set; }

        public string itemNumber{ get; set; }

        public int units{ get; set; }

        public string unitTypeCode{ get; set; }

        public string Class { get; set; }

        public int packageUnits{ get; set; }
        public string packageTypeCode{ get; set; }
        public int weight{ get; set; }
        public string weightUomCode{ get; set; }
        public int height{ get; set; }
        public string heightUomCode{ get; set; }

        public int length{ get; set; }
        public string lengthUomCode{ get; set; }
        public int width{ get; set; }
        public string widthUomCode{ get; set; }

        public bool isHazmat { get; set; }
        public HazardousItemInfo hazardousItemInfo { get; set; }

        public bool isTemperatureControlled { get; set; }

        public TemperatureInformation temperatureInformation { get; set; }
        public string sku { get; set; }

        public string nmfcCode { get; set; }


    }
}
