using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;


namespace XPOAPITest
{
    public class Quote
    {
        public string quoteId { get; set; }

        public string carrierCode { get; set; }

        public string carrierName { get; set; }

        public string serviceLevel { get; set; }

        public string pickupDate { get; set; }

        public string deliveryDate { get; set; }
        public string movementType { get; set; }


        public double lineHaul { get; set; }

        public double fsc { get; set; }

        public string scac { get; set; }
        public int estimatedTransitTime { get; set; }

        public IList<Accessorial> accessorials { get; set; }

        public IList<Rate> rateList { get; set; }

        public IList<Stop> legs { get; set; }

        public double accessorialsTotalCost { get; set; }

        public double subTotal { get; set; }

        public double totalCost { get; set; }

        public bool isNmfcCarrier { get; set; }

        public bool isContractRate { get; set; }

        public double totalDistance { get; set; }
        public string currencyCode { get; set; }

        public string expiresOn { get; set; }
        public string createdOn { get; set; }



    }
}
