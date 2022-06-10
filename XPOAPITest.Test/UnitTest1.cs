using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XPOAPITest;

namespace XPOAPITESTTest
{
    [TestClass]
    public class XPOAPITESTTest
    {


            //    [TestMethod]
            //    public static async Task TestQUOTEAPI()
            //    {
            //    //    XPOAPITESTTestInitialize();
            //        QuoteRequest quoteRequest = new QuoteRequest();
            //        quoteRequest.transportationMode.Add(XPOSettings.TransportationMode);
            //        quoteRequest.applicationSource = ApplicationSource.GPAPI.ToString();
            //        quoteRequest.bolNumber = "33232333";
            //        quoteRequest.partnerIdentifierCode = XPOSettings.PartnerIdentifierCode;
            //        quoteRequest.partnerOrderCode = "123543-22";

            //        quoteRequest.equipmentCategoryCode = EquipmentCatagoryCode.VN.ToString(); ;
            //        quoteRequest.equipmentTypeCode = equipmentTypeCode.V.ToString();
            //        quoteRequest.shipmentId = "123543-22";
            //        ContactInformation contactInformation = new();
            //        contactInformation.firstName = "Leslie";
            //        contactInformation.lastName = "Rivera";
            //        contactInformation.isPrimary = true;
            //        contactInformation.email = "leslie.rivera@gal.com";
            //        PhoneNumber phoneNumber = new();
            //        phoneNumber.number = "718 292 9000 x 527";
            //        phoneNumber.type = PhoneNumberType.WORK.ToString();
            //        contactInformation.addPhoneNumber(phoneNumber);
            //        quoteRequest.AddContact(contactInformation);



            //        Stop pickup = new();
            //        pickup.type = TypeOfStop.PICKUP.ToString();
            //        pickup.scheduledTimeFrom = "2022-06-13T18:00:00-04:00";
            //        pickup.scheduledTimeTo = "2022-06-13T20:30:00-04:00";
            //        pickup.sequenceNo = 1;
            //        pickup.note = "This is a note";

            //        StopContactInformation stopContactInformation = new();
            //        stopContactInformation.firstName = "Leslie";
            //        stopContactInformation.lastName = "Rivera";
            //        stopContactInformation.isPrimary = true;
            //        stopContactInformation.email = "leslie.rivera@gal.com";
            //        StopContactPhoneNumber stopPhoneNumber = new();
            //        stopPhoneNumber.number = "6463373449";
            //        stopPhoneNumber.type = PhoneNumberType.MOBILE.ToString();
            //        stopPhoneNumber.isPrimary = true;
            //        stopContactInformation.addPhoneNumber(stopPhoneNumber);
            //        pickup.addContact(stopContactInformation);



            //        AddressInformation addressInformation = new AddressInformation();
            //        addressInformation.locationName = "GAL Manufacturing Company LLC.";
            //        addressInformation.addressLine1 = "50 east 153rd Street";
            //        addressInformation.cityName = "Bronx";
            //        addressInformation.stateCode = "NY";
            //        addressInformation.country = "USA";
            //        addressInformation.zipCode = "10451";
            //        pickup.addressInformations = addressInformation;

            //        quoteRequest.AddStop(pickup);

            //        Stop delivery = new();
            //        delivery.type = TypeOfStop.DELIVERY.ToString();
            //        delivery.scheduledTimeFrom = "2022-06-16T18:00:00-04:00";
            //        delivery.scheduledTimeTo = "2022-06-16T20:30:00-04:00";
            //        delivery.sequenceNo = 2;
            //        delivery.note = "This is a note";

            //        stopContactInformation = new();
            //        stopContactInformation.firstName = "Derek";
            //        stopContactInformation.lastName = "Gielau";
            //        stopContactInformation.isPrimary = true;
            //        stopContactInformation.email = "derek.gielau@schumacherelevator.com";
            //        stopPhoneNumber = new();
            //        stopPhoneNumber.number = "6463373449";
            //        stopPhoneNumber.type = PhoneNumberType.MOBILE.ToString();
            //        stopPhoneNumber.isPrimary = true;
            //        stopContactInformation.addPhoneNumber(stopPhoneNumber);
            //        stopPhoneNumber = new();
            //        stopPhoneNumber.number = "319-984-5676";
            //        stopPhoneNumber.type = PhoneNumberType.WORK.ToString();
            //        stopPhoneNumber.isPrimary = false;
            //        stopContactInformation.addPhoneNumber(stopPhoneNumber);
            //        delivery.addContact(stopContactInformation);

            //        addressInformation = new();
            //        addressInformation.locationName = "SCHUMACHER ELEVATOR CO., INC. ";
            //        addressInformation.addressLine1 = "ONE SCHUMACHER WAY";
            //        addressInformation.cityName = "DENVER";
            //        addressInformation.stateCode = "IA";
            //        addressInformation.country = "USA";
            //        addressInformation.zipCode = "50622";
            //        delivery.addressInformations = addressInformation;

            //        quoteRequest.AddStop(delivery);


            //        QuoteItem item = new QuoteItem();
            //        item.productCode = "340";
            //        item.itemDescription = "Description";
            //        item.itemNumber = "34";
            //        item.units = 4;
            //        item.unitTypeCode = UnitTypeCode.CRTS.ToString();
            //        item.packageUnits = 5;
            //        item.packageTypeCode = PackageTypeCode.CRTS.ToString();
            //        item.weight = 340;
            //        item.weightUomCode = WeightUomCode.LB.ToString();
            //        item.height = 35;
            //        item.heightUomCode = HeightUomCode.IN.ToString();

            //        item.length = 48;
            //        item.lengthUomCode = LengthUomCode.IN.ToString();
            //        item.width = 42;
            //        item.widthUomCode = WidthUomCode.IN.ToString();

            //        quoteRequest.AddItem(item);
            //        String xpoToken = await Token.getToken();
            //        XPO xpo = new XPO();
            //        QuoteResponse quoteResponse = await xpo.getQuote(quoteRequest, xpoToken);
            //        Assert.IsNotNull(quoteRequest);


            //    }
            ////    [ClassInitialize()]
            //    public static void XPOAPITESTTestInitialize() //TestContext testContext)
            //    {
            //        XPOSettings.XAPIKeyToken = "f4b9b130-cd3e-4a81-9cb1-b4d72655e149";
            //        XPOSettings.XAPIKeyRequest = "0c0afcdd-1f9f-41bd-9b36-5dcde7e74c0c";
            //        XPOSettings.ClientId = "xpo-galvantage-integration";
            //        XPOSettings.ClientSecret = "6ywFMhLijCn1CpzAlTX0CWtc6m4xT0nxcZfliDyIfJ9rX6gSvl74FMX1vgh59enh";
            //        XPOSettings.Scope = "xpo-rates-api";
            //        XPOSettings.GrantType = "client_credentials";
            //        XPOSettings.PartnerIdentifierCode = "2-1-GALMNENY";
            //        XPOSettings.XPOConnectURL = "api-uat-xpoconnect.xpo.com";
            //        XPOSettings.TransportationMode = "LTL";
            //    }
        }
    }

