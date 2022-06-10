
using System.Collections.Generic;

namespace XPOAPITest
{
    public class OrderRequest
    {
        public int orderId { get; set; }


        public string orderDate { get; set; }

        public string accountId { get; set; }
        public string orderNumber { get; set; }

        public string shipmentId { get; set; }

        public string transportationMode { get; set; }
        public string contractId { get; set; }

        public IList<OrderReferenceNumber> orderReferenceNumbers { get; set; }
        public IList<Note> orderNotes { get; set; }

        public IList<ContactInformation> orderContactInformations { get; set; }

        public IList<Service> additionalServices { get; set; }
        public IList<OrderDocument> orderDocuments { get; set; }

        public string crossSellOrderNumber { get; set; }

        public IList<Contact> salesReps { get; set; }

        public IList<OrderItem> items { get; set; }

        public string requestedDeliveryDate { get; set; }
        public string isPartOfConsolidatedShipment { get; set; }

        public string ordersOnConsolidatedShipment { get; set; }

        public string isExpedited { get; set; }
        public string isXpoManaged { get; set; }

        public string paymentMethod { get; set; }
        public string equipmentCategoryCode { get; set; }

        public string equipmentTypeCode { get; set; }


        public string quoteId { get; set; }

        public string invoiceDate { get; set; }
        public string invoiceNumber { get; set; }

        public string emailBOL { get; set; }
        public string applicationSource { get; set; }

        public string partnerIdentifierCode { get; set; }

        public IList<Stop> stops { get; set; }

        public void addOrderNote(Note note)
        {
            if (orderNotes is null)
                orderNotes = new List<Note>();
            orderNotes.Add(note);
        }
        public void addOrderContact(ContactInformation contact)
        {
            if (orderContactInformations is null)
                orderContactInformations = new List<ContactInformation>();
            orderContactInformations.Add(contact);
        }
        public void addOrderDocument(OrderDocument document)
        {
            if (orderDocuments is null)
                orderDocuments = new List<OrderDocument>();
            orderDocuments.Add(document);
        }
        public void addAdditionalService(Service additionalService)
        {
            if (additionalServices is null)
                additionalServices = new List<Service>();
            additionalServices.Add(additionalService);
        }
        public void addSalesRep(Contact salesRep)
        {
            if (salesReps is null)
                salesReps = new List<Contact>();
            salesReps.Add(salesRep);
        }
        public void addStop(Stop stop)
        {
            if (stops is null)
                stops = new List<Stop>();
            stops.Add(stop);
        }
        public void addItem(OrderItem item)
        {
            if (items is null)
                items = new List<OrderItem>();
            items.Add(item);
        }
        public void addReferenceNumber(OrderReferenceNumber referenceNumber)
        {
            if (orderReferenceNumbers is null)
                orderReferenceNumbers = new List<OrderReferenceNumber>();
            orderReferenceNumbers.Add(referenceNumber);
        }

    }
}
