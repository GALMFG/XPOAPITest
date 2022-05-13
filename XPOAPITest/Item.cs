using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOAPITest
{
    public class Item
    {
        public string itemDescription { get; set; }
        public string itemNumber { get; set; }
        public int units { get; set; }
        public string unitTypeCode { get; set; }

        public int packageUnits { get; set; }
        public string packageTypeCode { get; set; }
        public int weight { get; set; }
        public string weightUomCode { get; set; }
        public int height { get; set; }

        public string heightUomCode { get; set; }
        public int length { get; set; }
        public string lengthUomCode { get; set; }
        public int width { get; set; }
        public string widthUomCode { get; set; }

        public bool isHazmat { get; set; }
        public HazardousItemInfo hazardousItemInfo { get; set; }

        public bool isTemperatureControlled { get; set; }
        

        public TemperatureInformation temperatureInformation;
        public string sku { get; set; }

        public string itemclass { get; set; }

        public string nmfcCode { get; set; }

        

    }
}
