using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;

namespace XPOAPITest.Tests
{
    public class Tests
    {
        XPO xpo;
        QuoteRequest quoteRequest;
        String quoteId;
        List<ContactInformation> addressList;
        List<String> referenceNumbers;
        List<String> additionalServices;
        List<Stop> stops;
        List<Stop> stopsIntermediate;
        List<Stop> stopsDelivery;
        Stop stopPickup;
        DateTime pickupDate;
        DateTime intermediateDate;
        DateTime deliveryDate;
        List<QuoteItem> items;
        String xpoToken;
        Random rnd;
        [SetUp]
        public async Task Setup()
        {
            InitializeSettings();
            xpo = new XPO();
            string jsonString = File.ReadAllText("sample data\\customer contacts.json");
            addressList= JsonSerializer.Deserialize<List<ContactInformation>>(jsonString);
            jsonString = File.ReadAllText("sample data\\stops.json");
            stops = JsonSerializer.Deserialize<List<Stop>>(jsonString);
            jsonString = File.ReadAllText("sample data\\items.json");
            items = JsonSerializer.Deserialize<List<QuoteItem>>(jsonString);
            jsonString = File.ReadAllText("sample data\\referencenumbers.json");
            referenceNumbers = JsonSerializer.Deserialize<List<String>>(jsonString);
            jsonString = File.ReadAllText("sample data\\additionalservices.json");
            additionalServices = JsonSerializer.Deserialize<List<String>>(jsonString);
            stopsIntermediate = (from s in stops
                                    where s.type.Equals("INTERMEDIATE") select s).ToList();
            stopsDelivery = (from s in stops
                                 where s.type.Equals("DELIVERY")
                                 select s).ToList();
            stopPickup = (from s in stops
                             where s.type.Equals("PICKUP")
                             select s).FirstOrDefault<Stop>();
            rnd = new Random();

        }

        void InitializeXPORequest()
        {
            quoteRequest = new QuoteRequest();
            quoteRequest.transportationMode.Add(XPOSettings.TransportationMode);
            quoteRequest.applicationSource = XPOSettings.ApplicationSource;
            pickupDate = DateTime.Today.AddDays(1);
            quoteRequest.bolNumber = rnd.Next(200000, 1000000).ToString();
            quoteRequest.partnerIdentifierCode = XPOSettings.PartnerIdentifierCode;
            quoteRequest.bolNumber = "33232333";
            quoteRequest.partnerOrderCode = rnd.Next(200000, 1000000).ToString() +"-" + rnd.Next(22, 44).ToString();
            quoteRequest.equipmentCategoryCode = EquipmentCatagoryCode.VN.ToString(); ;
            quoteRequest.equipmentTypeCode = equipmentTypeCode.V.ToString();
            quoteRequest.shipmentId = quoteRequest.partnerOrderCode;
        }
        void InitializeSettings()
        {
            XPOSettings.XAPIKeyToken = "f4b9b130-cd3e-4a81-9cb1-b4d72655e149";
            XPOSettings.XAPIKeyRequest = "0c0afcdd-1f9f-41bd-9b36-5dcde7e74c0c";
            XPOSettings.ClientId = "xpo-galvantage-integration";
            XPOSettings.ClientSecret = "6ywFMhLijCn1CpzAlTX0CWtc6m4xT0nxcZfliDyIfJ9rX6gSvl74FMX1vgh59enh";
            XPOSettings.Scope = "xpo-rates-api";
            XPOSettings.GrantType = "client_credentials";
            XPOSettings.PartnerIdentifierCode = "2-1-GALMNENY";
            XPOSettings.XPOConnectURL = "api-uat-xpoconnect.xpo.com";
            XPOSettings.TransportationMode = "LTL";
            XPOSettings.ApplicationSource = "GPAPI";
        }
        [Test, Order(1)]
        public async Task TestTokenRequest()
        {
            xpoToken = await xpo.getToken(XPOSettings.XPOConnectURL, XPOSettings.XAPIKeyToken, XPOSettings.ClientId, XPOSettings.ClientSecret, XPOSettings.Scope, XPOSettings.GrantType);
        }
        [Test, Order(2)]
        public async Task TestQuoteRequestWithMimumParameters()
        {
            InitializeXPORequest();
            addContactInformation(1);
            addStop("PICKUP", 0,null,null);
            addStop("DELIVERY", 0,null,null);
            addItem(1,null,null);
            QuoteResponse quoteResponse = await getQuote();
            Assert.IsNotNull(quoteResponse);
        }
        [Test, Order(3)]
        public async Task TestOrderRequestWithMimumParameters()
        {
            OrderResponse orderResponse = await ConvertToOrder();
            Assert.IsNotNull(orderResponse);
        }

        [Test, Order(4)]
        public async Task TestQuoteRequestWithMultipleContacts()
        {
            InitializeXPORequest();
            addContactInformation(rnd.Next(2, 4));
            addStop("PICKUP", 0, null, null);
            addStop("DELIVERY", 0, null, null);
            addItem(1,null,null);
            QuoteResponse quoteResponse = await getQuote();
            Assert.IsNotNull(quoteResponse);
        }
        [Test, Order(5)]
        public async Task TestQuoteRequestWithMultipleItems()
        {
            InitializeXPORequest();
            addContactInformation(1);
            addStop("PICKUP", 0, null, null);
            addStop("DELIVERY", 0, null, null);
            addItem(rnd.Next(2, 11),null,null);
            QuoteResponse quoteResponse = await getQuote();
            Assert.IsNotNull(quoteResponse);
        }
        [Test, Order(6)]
        public async Task TestQuoteRequestWithIntermediateStops()
        {
            InitializeXPORequest();
            addContactInformation(1);
            addStop("PICKUP", 2, null, null);
            addStop("INTERMEDIATE", 2, null, null);
            addStop("DELIVERY", 2, null, null);
            addItem(1,null,null);
            QuoteResponse quoteResponse = await getQuote();
            Assert.IsNotNull(quoteResponse);
        }
        [Test, Order(7)]
        public async Task TestQuoteRequestWithLiftGateDelivery()
        {
            InitializeXPORequest();
            addContactInformation(1);
            addStop("PICKUP", 0, null,null);
            StopSpecialRequirement stopSpecialRequirement = new StopSpecialRequirement();
            stopSpecialRequirement.code = "LFD: Liftgate Delivery";
            stopSpecialRequirement.value = "2";
            addStop("DELIVERY", 0, stopSpecialRequirement, null);
            addItem(1,null,null);
            QuoteResponse quoteResponse = await getQuote();
            Assert.IsNotNull(quoteResponse);
        }
        [Test, Order(8)]
        public async Task TestQuoteRequestWithLiftGatePickup()
        {
            InitializeXPORequest();
            addContactInformation(1);
            StopSpecialRequirement stopSpecialRequirement = new StopSpecialRequirement();
            stopSpecialRequirement.code = "LFP: Liftgate Pickup";
            stopSpecialRequirement.value = "2";
            addStop("PICKUP", 0, stopSpecialRequirement, null);
            addStop("DELIVERY", 0, null, null);
            addItem(1,null,null);
            QuoteResponse quoteResponse = await getQuote();
            Assert.IsNotNull(quoteResponse);
        }
        [Test, Order(9)]
        public async Task TestQuoteRequestWithResidentialDelivery()
        {
            InitializeXPORequest();
            addContactInformation(1);
            addStop("PICKUP", 0, null, null);
            StopSpecialRequirement stopSpecialRequirement = new StopSpecialRequirement();
            stopSpecialRequirement.code = "RSD: Residential Delivery";
            stopSpecialRequirement.value = "2";
            addStop("DELIVERY", 0, stopSpecialRequirement, null);
            addItem(1,null,null);
            QuoteResponse quoteResponse = await getQuote();
            Assert.IsNotNull(quoteResponse);
        }
        [Test, Order(10)]
        public async Task TestQuoteRequestWithResidentialPickup()
        {
            InitializeXPORequest();
            addContactInformation(1);
            StopSpecialRequirement stopSpecialRequirement = new StopSpecialRequirement();
            stopSpecialRequirement.code = "RSP: Residential Pickup";
            stopSpecialRequirement.value = "2";
            addStop("PICKUP", 0, stopSpecialRequirement, null);
            addStop("DELIVERY", 0, null, null);
            addItem(1,null,null);
            QuoteResponse quoteResponse = await getQuote();
            Assert.IsNotNull(quoteResponse);
        }
        [Test, Order(11)]
        public async Task TestQuoteRequestWithHazardousInformation()
        {
            InitializeXPORequest();
            addContactInformation(1);
            addStop("PICKUP", 0, null, null);
            addStop("DELIVERY", 0, null, null);
            HazardousItemInfo hazardousItemInfo = new HazardousItemInfo();
            hazardousItemInfo.containerType = "aa";
            hazardousItemInfo.hazardousClass = "dd";
            hazardousItemInfo.hazardousDescription = "ff";
            hazardousItemInfo.hazardousPhoneNumber = "9087642051";
            hazardousItemInfo.numberofReceptacles =1;
            hazardousItemInfo.packingGroup = 1;
            hazardousItemInfo.receptacleSize = 5;
            hazardousItemInfo.shippingName = "heyon";
            hazardousItemInfo.unitofMeasure = "IN";
            hazardousItemInfo.unNumber = 55;
            addItem(1, hazardousItemInfo, null);
            QuoteResponse quoteResponse = await getQuote();
            Assert.IsNotNull(quoteResponse);
        }
        [Test, Order(12)]
        public async Task TestQuoteRequestWithTemperatureInformation()
        {
            InitializeXPORequest();
            addContactInformation(1);
            addStop("PICKUP", 0, null, null);
            addStop("DELIVERY", 0, null, null);
            TemperatureInformation temperatureInformation = new TemperatureInformation();
            temperatureInformation.high = "99";
            temperatureInformation.low = "70";
            temperatureInformation.highUom = "F";
            temperatureInformation.lowUom = "F";
            addItem(1,null, temperatureInformation);
            QuoteResponse quoteResponse = await getQuote();
            Assert.IsNotNull(quoteResponse);
        }
        [Test, Order(13)]
        public async Task TestQuoteRequestWithCustomerReferenceNumber()
        {
            InitializeXPORequest();
            addContactInformation(1);
            addCustomerReferenceNumbers();
            addStop("PICKUP", 0, null, null);
            addStop("DELIVERY", 0, null, null);            
            addItem(1, null, null);
            QuoteResponse quoteResponse = await getQuote();
            Assert.IsNotNull(quoteResponse);
        }
        [Test, Order(14)]
        public async Task TestQuoteRequestWithAdditionalServices()
        {
            InitializeXPORequest();
            addContactInformation(1);
            addAdditionalServices();
            addStop("PICKUP", 0, null, null);
            addStop("DELIVERY", 0, null, null);
            addItem(1, null, null);
            QuoteResponse quoteResponse = await getQuote();
            Assert.IsNotNull(quoteResponse);
        }
        [Test, Order(15)]
        public async Task TestQuoteRequestWithStopReferenceCode()
        {
            InitializeXPORequest();
            addContactInformation(1);
            addAdditionalServices();
            List<String> addedReferenceNumbers = new List<String>();
            List<StopReferenceTypeCode> stopReferenceTypeCodes = new List<StopReferenceTypeCode>();
            int count = rnd.Next(1, 3);
            for (int i = 0; i < count; i++)
            {
                int position = rnd.Next(0, referenceNumbers.Count);
                String referenceNumber = referenceNumbers.ToArray()[position];
                if (!addedReferenceNumbers.Contains(referenceNumber))
                {
                    addedReferenceNumbers.Add(referenceNumber);
                    StopReferenceTypeCode stopReferenceTypeCode = new StopReferenceTypeCode();
                    stopReferenceTypeCode.typeCode = referenceNumber;
                    stopReferenceTypeCode.value = rnd.Next(100, 999).ToString();
                    stopReferenceTypeCodes.Add(stopReferenceTypeCode);
                }
            }
            addStop("PICKUP", 0, null, stopReferenceTypeCodes);
            stopReferenceTypeCodes = new List<StopReferenceTypeCode>();
            count = rnd.Next(1, 3);
            for (int i = 0; i < count; i++)
            {
                int position = rnd.Next(0, referenceNumbers.Count);
                String referenceNumber = referenceNumbers.ToArray()[position];
                if (!addedReferenceNumbers.Contains(referenceNumber))
                {
                    addedReferenceNumbers.Add(referenceNumber);
                    StopReferenceTypeCode stopReferenceTypeCode = new StopReferenceTypeCode();
                    stopReferenceTypeCode.typeCode = referenceNumber;
                    stopReferenceTypeCode.value = rnd.Next(100, 999).ToString();
                    stopReferenceTypeCodes.Add(stopReferenceTypeCode);
                }
            }
            addStop("DELIVERY", 0, null, stopReferenceTypeCodes);

            addItem(1, null, null);
            QuoteResponse quoteResponse = await getQuote();
            Assert.IsNotNull(quoteResponse);
        }
        [Test, Order(16)]
        public async Task TestOrderRequestWithContactInformation()
        {
            InitializeXPORequest();
            addContactInformation(1);
            addStop("PICKUP", 0, null, null);
            addStop("DELIVERY", 0, null, null);
            addItem(1, null, null);
            QuoteResponse quoteResponse = await getQuote();
            OrderRequest orderRequest = new();
            orderRequest.partnerIdentifierCode = XPOSettings.PartnerIdentifierCode;
            orderRequest.transportationMode = XPOSettings.TransportationMode;
            orderRequest.applicationSource = XPOSettings.ApplicationSource;
            orderRequest.quoteId = quoteResponse.masterQuoteId.ToString();
            IList<ContactInformation> orderContactInformations = quoteRequest.contactInformations;
            foreach(ContactInformation contactInformation in orderContactInformations)
            orderRequest.addOrderContact(contactInformation);
            OrderResponse orderResponse = await ConvertToOrder();
            Assert.IsNotNull(orderResponse);
        }
        public void  addContactInformation(int count )
        {
            List<String> addedContactsLists = new List<String>();
            for(int i=0; i < count; i++)
            {
                int position = rnd.Next(0, addressList.Count);
                ContactInformation contactInformation = addressList.ToArray()[position];
                if (!addedContactsLists.Contains(contactInformation.email))
                {
                    addedContactsLists.Add(contactInformation.email);
                    quoteRequest.AddContact(contactInformation);
                }
            }   
        }
        public void addCustomerReferenceNumbers()
        {
            List<String> addedReferenceNumbers = new List<String>();
            int count = rnd.Next(1, 3);
            for (int i = 0; i < count; i++)
            {
                int position = rnd.Next(0, referenceNumbers.Count);
                String referenceNumber = referenceNumbers.ToArray()[position];
                if (!addedReferenceNumbers.Contains(referenceNumber))
                {
                    addedReferenceNumbers.Add(referenceNumber);
                CustomerReferenceNumber customerReferenceNumber = new CustomerReferenceNumber();
                customerReferenceNumber.typeCode = referenceNumber;
                customerReferenceNumber.value = rnd.Next(100, 999).ToString();
                    quoteRequest.AddReferenceNumber(customerReferenceNumber);
                }
            }
        }
        public void addAdditionalServices()
        {
            List<String> addedAdditionalServices = new List<String>();
            int count = rnd.Next(1, 2);
            for (int i = 0; i < count; i++)
            {
                int position = rnd.Next(0, additionalServices.Count);
                String service = additionalServices.ToArray()[position];
                if (!addedAdditionalServices.Contains(service))
                {
                    addedAdditionalServices.Add(service);
                    AdditionalService additionalService = new AdditionalService();
                    additionalService.code = service;
                    quoteRequest.AddAdditionalService(additionalService);
                }
            }
        }
        public async Task<OrderResponse> ConvertToOrder()
        {
            OrderRequest orderRequest = new();
            orderRequest.partnerIdentifierCode = XPOSettings.PartnerIdentifierCode;
            orderRequest.transportationMode = XPOSettings.TransportationMode;
            orderRequest.applicationSource = XPOSettings.ApplicationSource;
            orderRequest.quoteId = quoteId;
            OrderResponse orderResponse = await xpo.ConvertToOrder(orderRequest, rnd.Next(45634578, 95634578).ToString(), xpoToken, XPOSettings.XPOConnectURL, XPOSettings.XAPIKeyRequest);
            return orderResponse;
        }
        public async Task<QuoteResponse> getQuote()
        {
            QuoteResponse quoteResponse = await xpo.getQuote(quoteRequest, xpoToken, XPOSettings.XPOConnectURL, XPOSettings.XAPIKeyRequest);
            quoteId = quoteResponse.masterQuoteId.ToString();
            return quoteResponse;
        }
        public void addStop( String type, int numberOfIntermediateStops, StopSpecialRequirement specialRequirement,IList<StopReferenceTypeCode> stopReferenceTypeCodes)
        {
            List<String> addedStops = new List<String>();
            Stop stop;
            switch (type)
            {
                case "PICKUP":
                    if (pickupDate.DayOfWeek == DayOfWeek.Sunday)
                        pickupDate = pickupDate.AddDays(1);
                    if (pickupDate.DayOfWeek == DayOfWeek.Saturday)
                        pickupDate = pickupDate.AddDays(2);
                    stopPickup.sequenceNo = 1;
                    if (specialRequirement is not null)
                    {
                        stopPickup.addSpecialRequirement(specialRequirement);
                    }
                    if(stopReferenceTypeCodes is not null)
                    {
                        foreach(StopReferenceTypeCode stopReferenceTypeCode in stopReferenceTypeCodes)
                        {
                            stopPickup.addStopReferenceNumber(stopReferenceTypeCode);
                        }
                    }
                    stopPickup.scheduledTimeFrom = pickupDate.AddHours(8).ToString("yyyy-MM-ddTHH:mm:ss") + "-04:00";
                    stopPickup.scheduledTimeTo = pickupDate.AddHours(17).ToString("yyyy-MM-ddTHH:mm:ss") + "-04:00";
                    quoteRequest.AddStop(stopPickup);
                    break;
                case "INTERMEDIATE":
                    for (int i = 0; i < numberOfIntermediateStops; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                intermediateDate = pickupDate.AddDays(1);
                                break;
                            default:
                                intermediateDate = intermediateDate.AddDays(1);
                                break;
                        }
                        if (intermediateDate.DayOfWeek == DayOfWeek.Sunday)
                            intermediateDate = intermediateDate.AddDays(1);
                        if (intermediateDate.DayOfWeek == DayOfWeek.Saturday)
                            intermediateDate = intermediateDate.AddDays(2);
                        stop = stopsIntermediate.ToArray()[rnd.Next(0, stopsIntermediate.Count)];
                        stop.sequenceNo = i + 2;
                        stop.scheduledTimeFrom = intermediateDate.AddHours(8).ToString("yyyy-MM-ddTHH:mm:ss") + "-04:00";
                        stop.scheduledTimeTo = intermediateDate.AddHours(17).ToString("yyyy-MM-ddTHH:mm:ss") + "-04:00";
                        if (!addedStops.Contains(stop.addressInformations.addressLine1 + "," + stop.addressInformations.cityName + "," + stop.addressInformations.zipCode))
                        {
                            addedStops.Add(stop.addressInformations.addressLine1 + "," + stop.addressInformations.cityName + "," + stop.addressInformations.zipCode);
                            quoteRequest.AddStop(stop);
                        }
                    }
                    break;
                case "DELIVERY":
                    switch (numberOfIntermediateStops)
                    {
                        case 0:
                            deliveryDate = pickupDate.AddDays(2);
                            break;
                        default:
                            deliveryDate = intermediateDate.AddDays(1);
                            break;
                    }
                    if (deliveryDate.DayOfWeek == DayOfWeek.Sunday)
                        deliveryDate = deliveryDate.AddDays(1);
                    if (deliveryDate.DayOfWeek == DayOfWeek.Saturday)
                        deliveryDate = deliveryDate.AddDays(2);
                    stop = stopsDelivery.ToArray()[rnd.Next(0, stopsDelivery.Count)];
                    if (specialRequirement is not null)
                    {
                        stop.addSpecialRequirement(specialRequirement);
                    }
                    if (stopReferenceTypeCodes is not null)
                    {
                        foreach (StopReferenceTypeCode stopReferenceTypeCode in stopReferenceTypeCodes)
                        {
                            stop.addStopReferenceNumber(stopReferenceTypeCode);
                        }
                    }
                    stop.sequenceNo = numberOfIntermediateStops + 2;
                    stop.scheduledTimeFrom = deliveryDate.AddHours(8).ToString("yyyy-MM-ddTHH:mm:ss") + "-04:00";
                    stop.scheduledTimeTo = deliveryDate.AddHours(17).ToString("yyyy-MM-ddTHH:mm:ss") + "-04:00";
                    quoteRequest.AddStop(stop);
                    break;
            }
        }
        public void addItem( int count,HazardousItemInfo hazardousItemInfo,TemperatureInformation temperatureInformation)
        {
           for(int i=0; i< count; i++)
            {
                QuoteItem quoteItem = items.ToArray()[i];
                if (i == 0)
                {
                    if (hazardousItemInfo is not null)
                        quoteItem.hazardousItemInfo = hazardousItemInfo;
                    if (temperatureInformation is not null)
                        quoteItem.temperatureInformation = temperatureInformation;
                    quoteRequest.AddItem(quoteItem);
                }
            }
        }
    }
 }