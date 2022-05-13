using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOAPITest
{
    public class QuoteRequest
    {
        public string xMode { get; set; }
        public string partnerOrderCode { get; set; }

        public string partnerIdentifierCode { get; set; }
        public string equipmentCategoryCode { get; set; }

        public string equipmentTypeCode { get; set; }
        public IList<String> transportationMode { get; set; }

        public string bolNumber { get; set; }
        public string shipmentId { get; set; }

        // public ArrayList contactInformations;

        public IList<QuoteContactInformation> contactInformations { get; set; }


        public IList<QuoteReferenceNumber> referenceNumbers { get; set; }
        public string applicationSource { get; set; }

        public IList<Stop> stops { get; set; }

        public IList<QuoteItem> items { get; set; }

        public IList<String> additionalServices { get; set; }

        public void addContact(QuoteContactInformation contact)
        {
            if (contactInformations is null)
                contactInformations = new List<QuoteContactInformation>();
            contactInformations.Add(contact);

        }
        public void addReferenceNumber(QuoteReferenceNumber referenceNumber)
        {
            if (referenceNumbers is null)
                referenceNumbers = new List<QuoteReferenceNumber>();
            referenceNumbers.Add(referenceNumber);
        }
        public void addTransportationMode(String mode)
        {
            if (transportationMode is null)
                transportationMode = new List<String>();
            transportationMode.Add(mode);
        }
        public void addStop(Stop stop)
        {
            if (stops is null)
                stops = new List<Stop>();
            stops.Add(stop);
        }
        public void addItem(QuoteItem item)
        {
            if (items is null)
                items = new List<QuoteItem>();
            items.Add(item);
        }
        public void addAdditionalService(String additionalService)
        {
            if (additionalServices is null)
                additionalServices = new List<String>();
            additionalServices.Add(additionalService);
        }
    }
}
