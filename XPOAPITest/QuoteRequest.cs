﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XPOAPITest
{
    public class QuoteRequest
    {
        public QuoteRequest()
        {
            transportationMode = new List<String>();
            contactInformations = new List<ContactInformation>();
            //referenceNumbers = new  List<CustomerReferenceNumber>();

            stops= new List<Stop>();
            items= new List<QuoteItem>();
            //additionalServices= new  List<String>() ;
    }
        //[JsonPropertyName("x-mode")]
        //public string x_mode { get; set; }
        public string partnerOrderCode { get; set; }
        public string partnerIdentifierCode { get; set; }
        public string equipmentCategoryCode { get; set; }
        public string equipmentTypeCode { get; set; }
        public IList<String> transportationMode { get; set; }
        public string bolNumber { get; set; }
        public string shipmentId { get; set; }
        public IList<ContactInformation> contactInformations { get; set; }
        //public IList<CustomerReferenceNumber> referenceNumbers { get; set; }
        public string applicationSource { get; set; }
        public IList<Stop> stops { get; set; }
        public IList<QuoteItem> items { get; set; }
        //public IList<String> additionalServices { get; set; }




        public void AddContact(ContactInformation contact)
        {
            if (contactInformations is not null)
            contactInformations.Add(contact);

        }
        public void AddReferenceNumber(CustomerReferenceNumber referenceNumber)
        {
            //if (referenceNumbers is not null)
            //referenceNumbers.Add(referenceNumber);
        }
        public void AddTransportationMode(String mode)
        {
            if (transportationMode is not  null)
            transportationMode.Add(mode);
        }
        public void AddStop(Stop stop)
        {
            if (stops is not null)
                stops.Add(stop);
        }
        public void AddItem(QuoteItem item)
        {
            if (items is not null)
            items.Add(item);
        }
        public void AddAdditionalService(String additionalService)
        {
            //if (additionalServices is not null)
            //additionalServices.Add(additionalService);
        }
    }
}
