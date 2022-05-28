using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace XPOAPITest
{
    enum EquipmentCatagoryCode
    {
        DB,
        CTN,
        VN,
        FB,
        CS,
        RF,
        TRACTORONL,
        OTH,
        Sprinter,
        Cargo,
        Van,
        LS,
        LSR,
        TRR
    }
    enum TypeOfStop
    {
        PICKUP,
        INTERMEDIATE,
        DELIVERY
    }
    enum PaymentMethod
    {
        COL,
        PPD,
        THIRDPARTY
    }
    enum ApplicationSource
    {
        GPAPI,
        XpoConnect,
        EDI
    }
    enum equipmentTypeCode {
        AC,
        BT,
        C,
        CI,
        CONT,
        CR,
        DD,
        DT,
        F,
        F45,
        F48,
        F53,
        F60,
        FA,
        FD,
        FH,
        FM,
        FR,
        FT,
        FZ,
        HB,
        HOP,
        LA,
        LB,
        MV,
        MX,
        NU,
        OT,
        PO,
        R,
        R2,
        R48,
        R53,
        RA,
        RG,
        RL,
        RM,
        RN,
        RZ,
        SD,
        SDCK,
        ST,
        TA,
        TN,
        TRAC,
        TS,
        TT,
        V,
        V2,
        VA,
        VAN,
        VB,
        VC,
        VF,
        VG,
        VH,
        VI,
        VL,
        VM,
        VN,
        VN53,
        VR48,
        VR53,
        VS,
        VT,
        VV,
        VZ,
        VR,
        VRM,
        VRZ,
        VHC,
        COFC,
        CS,
        RGE,
        SD48,
        SD53,
        C20,
        C40,
        C53,
        LTL,
        OD,
        INT,
        AIR,
        LCL,
        CC20,
        CC40,
        CC48,
        CC53,
        CH20,
        CH40,
        CH48,
        CH53,
        HEAT,
        CONE,
        NA,
        CargoVan,
        Sprinter,
        LS,
        LSR,
        HC40,
        HCOC,
        HHCC,
        TRR,
        MNV,
        CBV,
        EIGHTEEN_D,
        TWENTYTWO_D,
        TWENTYFOUR_D,
        TWENTYEIGHT_D,
        VSPR,
        VZM,
        RZM
    }
    enum WeightUomCode
    {
        KG,
        LB
    }

    enum PhoneNumberType
    {
        MOBILE,
        WORK,
        HOME
    }
    enum LengthUomCode
    {
        CM,
        IN,
        FT
    }
    enum WidthUomCode
    {
        CM,
        IN,
        FT
    }
    enum HeightUomCode
    {
        CM,
        IN,
        FT
    }
    enum UnitTypeCode
    {
        ACCE,
        DRUM,
        SKIDS,
        ATTCH,
        ENVE,
        SLIP,
        BAGS,
        FEET,
        SPRS,
        BALE,
        FIRK,
        TOTE,
        BASK,
        GYLD,
        TRKL,
        BHDS,
        GAYL,
        TRLR,
        BNDL,
        KEGS,
        TRNK,
        BOXES,
        LOOS,
        TRAY,
        BRRL,
        OCTA,
        TUBE,
        BUCK,
        PACK,
        UNIT,
        CARB,
        PAIL,
        UNPK,
        CASES,
        PIEC,
        VEH,
        CASE,
        PALLETS,
        BINS,
        CHST,
        PLTS,
        COIL,
        RACK,
        CARTONS,
        RACKS,
        CRTS,
        REEL,
        CTN,
        ROLL,
        CYLS,
        SKID
    }
    enum PackageTypeCode
    { 
    ACCE,
    DRUM,
    SKIDS,
    ATTCH,
    ENVE,
    SLIP,
    BAGS,
    FEET,
    SPRS,
    BALE,
    FIRK,
    TOTE,
    BASK,
    GYLD,
    TRKL,
    BHDS,
    GAYL,
    TRLR,
    BNDL,
    KEGS,
    TRNK,
    BOXES,
    LOOS,
    TRAY,
    BRRL,
    OCTA,
    TUBE,
    BUCK,
    PACK,
    UNIT,
    CARB,
    PAIL,
    UNPK,
    CASES,
    PIEC,
    VEH,
    CASE,
    PALLETS,
    BINS,
    CHST,
    PLTS,
    COIL,
    RACK,
    CARTONS,
    RACKS,
    CRTS,
    REEL,
    CTN,
    ROLL,
    CYLS,
    SKID
}


    public class XPO
    {

        public XPO()
        {


        }
        public async Task<QuoteResponse> getQuote(QuoteRequest quoteRequest)
        {
            var body = JsonSerializer.Serialize(quoteRequest);
            string xpoToken = null;
            xpoToken = await Token.getToken(xpoToken);

            var client = new RestClient("https://" + XPOSettings.XPOConnectURL + "/quoteAPI/rest/v1/Create");
            var request = new RestRequest(Method.Post.ToString());
            request.AddHeader("x-api-key", XPOSettings.XAPIKeyRequest);
            request.AddHeader("Accept", "*/*");
            request.Method = Method.Post;
            request.AddHeader("xpoauthorization", xpoToken);
            request.AddHeader("Content-Type", "application/json");
            QuoteResponse? quoteResponse;
            try
            {
                request.AddJsonBody(quoteRequest);

                var response = await client.ExecuteAsync(request);
                quoteResponse =
JsonSerializer.Deserialize<QuoteResponse>(response.Content);
                return quoteResponse;
            }
            catch(Exception e)
            {

            }
            return null;            
        }
        public  async Task<QuoteResponse> getQuote()
        {
            QuoteRequest quoteRequest = new();
            quoteRequest.partnerIdentifierCode = XPOSettings.PartnerIdentifierCode;
            quoteRequest.partnerOrderCode = "45565555";
            quoteRequest.equipmentCategoryCode = EquipmentCatagoryCode.VN.ToString(); ;
            quoteRequest.equipmentTypeCode =equipmentTypeCode.V.ToString();
            quoteRequest.AddTransportationMode("LTL");
            quoteRequest.bolNumber = "45567";
            quoteRequest.shipmentId = "45565555";
            ContactInformation contactInformation = new();
            contactInformation.firstName = "Leslie";
            contactInformation.lastName = "Rivera";
            contactInformation.isPrimary = true;
            contactInformation.email = "leslie.rivera@gal.com";
            PhoneNumber phoneNumber = new();
            phoneNumber.number = "718 292 9000 x 527";
            phoneNumber.type = PhoneNumberType.WORK.ToString();
            contactInformation.addPhoneNumber(phoneNumber);
            quoteRequest.AddContact(contactInformation);

            CustomerReferenceNumber referenceNumber = new();
            referenceNumber.typeCode = "SW";
            referenceNumber.value = "344";
            quoteRequest.AddReferenceNumber(referenceNumber);

            quoteRequest.applicationSource = ApplicationSource.GPAPI.ToString();

            Stop pickup = new ();
            pickup.type = TypeOfStop.PICKUP.ToString();
            pickup.scheduledTimeFrom = "2022-05-31T18:00:00-04:00";
            pickup.scheduledTimeTo = "2022-05-31T20:30:00-04:00";
            pickup.sequenceNo = 1;
            pickup.note = "This is a note";

            StopContactInformation stopContactInformation = new();
            stopContactInformation.firstName = "Leslie";
            stopContactInformation.lastName = "Rivera";
            stopContactInformation.isPrimary = true;
            stopContactInformation.email = "leslie.rivera@gal.com";
            StopContactPhoneNumber stopPhoneNumber = new();
            stopPhoneNumber.number = "6463373449";
            stopPhoneNumber.type = PhoneNumberType.MOBILE.ToString();
            stopPhoneNumber.isPrimary = true;
            stopContactInformation.addPhoneNumber(stopPhoneNumber);
            pickup.addContact(stopContactInformation);

            SpecialRequirementType srt = SpecialRequirementType.LFD;
            StopSpecialRequirement sr = new ();
            sr.code = srt.GetString();
            sr.value = "2334";
            pickup.addSpecialRequirement(sr);

            AddressInformation addressInformation = new AddressInformation();
            addressInformation.locationName = "GAL Manufacturing Company LLC.";
            addressInformation.addressLine1 = "50 east 153rd Street";
            addressInformation.cityName = "Bronx";
            addressInformation.stateCode = "NY";
            addressInformation.country = "USA";
            addressInformation.zipCode = "10451";
            pickup.addressInformations = addressInformation;

            StopReferenceTypeCode stopReferenceNumber = new StopReferenceTypeCode();
            stopReferenceNumber.typeCode = "AN";
            stopReferenceNumber.value = "78777";
            pickup.addStopReferenceNumber(stopReferenceNumber);
            quoteRequest.AddStop(pickup);

            Stop delivery = new ();
            delivery.type = TypeOfStop.DELIVERY.ToString();
            delivery.scheduledTimeFrom = "2022-06-02T18:00:00-04:00";
            delivery.scheduledTimeTo = "2022-06-02T20:30:00-04:00";
            delivery.sequenceNo = 2;
            delivery.note = "This is a note";

            stopContactInformation = new();
            stopContactInformation.firstName = "Derek";
            stopContactInformation.lastName = "Gielau";
            stopContactInformation.isPrimary = true;
            stopContactInformation.email = "derek.gielau@schumacherelevator.com";
            stopPhoneNumber = new();
            stopPhoneNumber.number = "6463373449";
            stopPhoneNumber.type = PhoneNumberType.MOBILE.ToString();
            stopPhoneNumber.isPrimary = true;
            stopContactInformation.addPhoneNumber(stopPhoneNumber);
            stopPhoneNumber = new();
            stopPhoneNumber.number = "319-984-5676";
            stopPhoneNumber.type = PhoneNumberType.WORK.ToString();
            stopPhoneNumber.isPrimary = false;
            stopContactInformation.addPhoneNumber(stopPhoneNumber);
            delivery.addContact(stopContactInformation);
            srt = SpecialRequirementType.LFD;
            sr = new StopSpecialRequirement();
            sr.code = srt.GetString();
            sr.value = "2334";
            delivery.addSpecialRequirement(sr);

            addressInformation = new ();
            addressInformation.locationName = "SCHUMACHER ELEVATOR CO., INC. ";
            addressInformation.addressLine1 = "ONE SCHUMACHER WAY";
            addressInformation.cityName = "DENVER";
            addressInformation.stateCode = "IA";
            addressInformation.country = "USA";
            addressInformation.zipCode = "50622";
            delivery.addressInformations = addressInformation;

            stopReferenceNumber = new StopReferenceTypeCode();
            stopReferenceNumber.typeCode = "AN";
            stopReferenceNumber.value = "5555";

            stopReferenceNumber.value = "78777";
            delivery.addStopReferenceNumber(stopReferenceNumber);
            quoteRequest.AddStop(delivery);


            QuoteItem item = new QuoteItem();
            item.productCode = "340";
            item.itemDescription = "Description";
            item.itemNumber = "34";
            item.units = 4;
            item.unitTypeCode = UnitTypeCode.CRTS.ToString();
            item.packageUnits = 5;
            item.packageTypeCode = PackageTypeCode.CRTS.ToString();
            item.weight = 340;
            item.weightUomCode = WeightUomCode.LB.ToString();
            item.height = 35;
            item.heightUomCode = HeightUomCode.IN.ToString();

            item.length = 48;
            item.lengthUomCode = LengthUomCode.IN.ToString();
            item.width = 42;
            item.widthUomCode = WidthUomCode.IN.ToString();
            
            //item.isHazmat = true;

            //HazardousItemInfo hazardousItemInfo = new();
            //hazardousItemInfo.containerType = "";
            //hazardousItemInfo.hazardousClass = "";
            //hazardousItemInfo.hazardousDescription = "";
            //hazardousItemInfo.hazardousPhoneNumber = "";
            //hazardousItemInfo.numberofReceptacles = 3;
            //hazardousItemInfo.packingGroup = 4;
            //hazardousItemInfo.receptacleSize = 3;
            //hazardousItemInfo.shippingName = "errr";
            //hazardousItemInfo.unitofMeasure = "22";
            //hazardousItemInfo.unNumber = 34;
            //item.hazardousItemInfo = hazardousItemInfo;
            //TemperatureInformation temperature = new TemperatureInformation();
            //temperature.high = "45";
            //temperature.highUom = "ee";
            //temperature.low = "66";
            //temperature.lowUom = "eee";
            //item.temperatureInformation = temperature;


            //item.nmfcCode = "345";


           // item.sku = "34";
           // item.itemclass = "23";

            quoteRequest.AddItem(item);

            var body = JsonSerializer.Serialize(quoteRequest);
            string xpoToken = null;
            xpoToken= await Token.getToken(xpoToken);

            var client = new RestClient("https://" + XPOSettings.XPOConnectURL + "/quoteAPI/rest/v1/Create");
            //client.Timeout = -1;

            var request = new RestRequest();
            request.AddHeader("x-api-key", XPOSettings.XAPIKeyRequest);

            //request.AddHeader("Content-Type", "application/json");

            request.AddHeader("Accept", "*/*");
            request.Method = Method.Post;
            request.AddHeader("xpoauthorization", xpoToken);
            //request.AddHeader("Content-Type", "text/plain");
            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
              request.AddHeader("Content-Type", "application/json");

            //var TokenBody = new TokenBody { client_id = "xpo-galvantage-integration", client_secret = "6ywFMhLijCn1CpzAlTX0CWtc6m4xT0nxcZfliDyIfJ9rX6gSvl74FMX1vgh59enh", scope = "xpo-rates-api", grant_type = "client_credentials" };

            //body= "{ \"partnerOrderCode\":\"718956-22\",\"partnerIdentifierCode\":\"2-1-GALMNENY\",\"equipmentCategoryCode\":\"VN\",\"equipmentTypeCode\":\"V\",\"transportationMode\":[\"LTL\"],\"applicationSource\":\"GPAPI\",\"contactInformations\":[{ \"firstName\":\"Leslie\",\"email\":\"leslie.rivera@gal.com\",\"phoneNumbers\":[{ \"type\":\"WORK\",\"number\":\"718 292 9000 x 527\"}]}],\"stops\":[{ \"type\":\"PICKUP\",\"scheduledTimeFrom\":\"2022-05-11T18:00:00-04:00\",\"scheduledTimeTo\":\"2022-05-11T20:30:00-04:00\",\"sequenceNo\":1,\"addressInformations\":{ \"locationName\":\"GAL Manufacturing Company LLC.\",\"addressLine1\":\"50 East 153rd Street\",\"cityName\":\"Bronx\",\"stateCode\":\"NY\",\"country\":\"USA\",\"zipCode\":\"10451\"},\"stopContactInformations\":[{ \"firstName\":\"Leslie\",\"lastName\":\"Rivera\",\"email\":\"leslie.rivera@gal.com\",\"phoneNumbers\":[{ \"Type\":\"MOBILE\",\"number\":\"6463373449\",\"isPrimary\":true}]}]},{ \"type\":\"DELIVERY\",\"scheduledTimeFrom\":\"2022-05-14T08:00:00-04:00\",\"scheduledTimeTo\":\"2022-05-14T15:00:00-04:00\",\"sequenceNo\":2,\"addressInformations\":{ \"locationName\":\"SCHUMACHER ELEVATOR CO., INC. \",\"addressLine1\":\"ONE SCHUMACHER WAY            \",\"cityName\":\"DENVER              \",\"stateCode\":\"IA\",\"country\":\"USA\",\"zipCode\":\"50622\"},\"stopContactInformations\":[{ \"firstName\":\"Derek\",\"lastName\":\"Gielau\",\"email\":\"derek.gielau@schumacherelevator.com\",\"phoneNumbers\":[{ \"Type\":\"MOBILE\",\"number\":\"6463373449\",\"isPrimary\":true},{ \"Type\":\"WORK\",\"number\":\"319-984-5676\",\"isPrimary\":true}]}]}],\"items\":[{ \"productCode\":\"1 CONTROLLER ETC  1\",\"units\":1,\"unitTypeCode\":\"CRTS\",\"packageUnits\":1,\"packageTypeCode\":\"CRTS\",\"weight\":331,\"weightUomCode\":\"LB\",\"height\":35,\"heightUomCode\":\"IN\",\"length\":48,\"lengthUomCode\":\"IN\",\"width\":42,\"widthUomCode\":\"IN\",\"class\":\"55\"}]}";


            try
            {
                request.AddJsonBody(quoteRequest);
                //request.AddBody(body);
                var response = await client.ExecuteAsync(request);


                QuoteResponse? quoteResponse =
    JsonSerializer.Deserialize<QuoteResponse>(response.Content);

                return quoteResponse;
            }catch(Exception e)
            {

            }
            return null;
        }

        public async Task<OrderResponse> convertToOrder(TabPage tabPage)
        {
            OrderRequest orderRequest = new();
            orderRequest.partnerIdentifierCode = XPOSettings.PartnerIdentifierCode;
            //   orderRequest.equipmentCategoryCode = EquipmentCatagoryCode.VN.ToString(); ;
            //   orderRequest.equipmentTypeCode = equipmentTypeCode.V.ToString();
            orderRequest.transportationMode = XPOSettings.TransportationMode;
            orderRequest.applicationSource = ApplicationSource.GPAPI.ToString();
            Label lblQuoteId = tabPage.Controls["lblQuoteIdValue"] as Label;
            orderRequest.quoteId = lblQuoteId.Text;
            //    orderRequest.shipmentId = "45565555";
            //    orderRequest.paymentMethod = PaymentMethod.COL.ToString();


            // List<OrderContactInformation> contacts = new List<OrderContactInformation>();
            //OrderContactInformation contactInformation = new();
            //contactInformation.firstName = "Leslie";
            //contactInformation.lastName = "Rivera";
            //contactInformation.isPrimary = true;
            //contactInformation.email = "leslie.rivera@gal.com";
            //PhoneNumber phoneNumber = new();
            //phoneNumber.number = "718 292 9000 x 527";
            //phoneNumber.type = PhoneNumberType.WORK.ToString();
            //contactInformation.addPhoneNumber(phoneNumber);
            //   orderRequest.addOrderContact(contactInformation);


      //      OrderReferenceNumber referenceNumber = new();
      //      referenceNumber.code = "PRO";
      //      referenceNumber.type = "PRO";
      //      referenceNumber.value = "887551205";
      //      orderRequest.addReferenceNumber(referenceNumber);

      //      Note orderNote = new();
      //      orderNote.type = "HY";
      //      orderNote.value = "SW";
      // //     orderRequest.addOrderNote(orderNote);

      //      OrderDocument orderDocument = new();
      //      orderDocument.key = "4443";
      //      orderDocument.type = "WR";
      //      orderDocument.name = "344";
      ////      orderRequest.addOrderDocument(orderDocument);

      //      Service additionalService = new();
      //      additionalService.code = "SW";
      //      additionalService.name = "Software";
      //      additionalService.description = "Something";
      //      additionalService.quantity = 12;
      //      additionalService.unitOfMeasure = "IN";
     //       orderRequest.addAdditionalService(additionalService);


            Stop pickup = new();
            pickup.type = TypeOfStop.PICKUP.ToString();
            pickup.scheduledTimeFrom = "2022-05-13T18:00:00-04:00";
            pickup.scheduledTimeTo = "2022-05-13T20:30:00-04:00";
            pickup.sequenceNo = 1;
            pickup.note = "This is a note";

            StopContactInformation stopContactInformation = new();
            stopContactInformation.firstName = "Leslie";
            stopContactInformation.lastName = "Rivera";
            stopContactInformation.isPrimary = true;
            stopContactInformation.email = "leslie.rivera@gal.com";
            StopContactPhoneNumber stopPhoneNumber = new();
            stopPhoneNumber.number = "6463373449";
            stopPhoneNumber.type = PhoneNumberType.MOBILE.ToString();
            stopPhoneNumber.isPrimary = true;
            stopContactInformation.addPhoneNumber(stopPhoneNumber);
            pickup.addContact(stopContactInformation);

            SpecialRequirementType srt = SpecialRequirementType.LFD;
            StopSpecialRequirement sr = new();
            sr.code = srt.GetString();
            sr.value = "2334";
            pickup.addSpecialRequirement(sr);

            AddressInformation addressInformation = new AddressInformation();
            addressInformation.locationName = "GAL Manufacturing Company LLC.";
            addressInformation.addressLine1 = "50 east 153rd Street";
            addressInformation.cityName = "Bronx";
            addressInformation.stateCode = "NY";
            addressInformation.country = "USA";
            addressInformation.zipCode = "10451";
            pickup.addressInformations = addressInformation;

            StopReferenceTypeCode stopReferenceNumber = new StopReferenceTypeCode();
            stopReferenceNumber.typeCode = "AN";

            stopReferenceNumber.value = "78777";
            pickup.addStopReferenceNumber(stopReferenceNumber);
            //        orderRequest.addStop(pickup);

            Stop delivery = new();
            delivery.type = TypeOfStop.DELIVERY.ToString();
            delivery.scheduledTimeFrom = "2022-05-18T18:00:00-04:00";
            delivery.scheduledTimeTo = "2022-05-18T20:30:00-04:00";
            delivery.sequenceNo = 2;
            delivery.note = "This is a note";

            stopContactInformation = new();
            stopContactInformation.firstName = "Derek";
            stopContactInformation.lastName = "Gielau";
            stopContactInformation.isPrimary = true;
            stopContactInformation.email = "derek.gielau@schumacherelevator.com";
            stopPhoneNumber = new();
            stopPhoneNumber.number = "6463373449";
            stopPhoneNumber.type = PhoneNumberType.MOBILE.ToString();
            stopPhoneNumber.isPrimary = true;
            stopContactInformation.addPhoneNumber(stopPhoneNumber);
            stopPhoneNumber = new();
            stopPhoneNumber.number = "319-984-5676";
            stopPhoneNumber.type = PhoneNumberType.WORK.ToString();
            stopPhoneNumber.isPrimary = false;
            stopContactInformation.addPhoneNumber(stopPhoneNumber);
            delivery.addContact(stopContactInformation);



            srt = SpecialRequirementType.LFD;
            sr = new StopSpecialRequirement();
            sr.code = srt.GetString();
            sr.value = "2334";
            delivery.addSpecialRequirement(sr);

            addressInformation = new();
            addressInformation.locationName = "SCHUMACHER ELEVATOR CO., INC. ";
            addressInformation.addressLine1 = "ONE SCHUMACHER WAY";
            addressInformation.cityName = "DENVER";
            addressInformation.stateCode = "IA";
            addressInformation.country = "USA";
            addressInformation.zipCode = "50622";
            
            delivery.addressInformations = addressInformation;

            stopReferenceNumber = new StopReferenceTypeCode();
            stopReferenceNumber.typeCode = "AN";
            stopReferenceNumber.value = "78777";
            delivery.addStopReferenceNumber(stopReferenceNumber);
      //      orderRequest.addStop(delivery);


            OrderItem item = new OrderItem();
            item.itemCode = "HH-77";
            item.itemDescription = "Description";
            item.itemNumber = "34";
            item.units = 4;
            item.unitTypeCode = UnitTypeCode.CRTS.ToString();
            item.packageUnits = 5;
            item.packageTypeCode = PackageTypeCode.CRTS.ToString();
            item.declaredValue = 400;
            item.declaredValueCurrencyCode = "USD";
            item.weight = 340;
            item.weightUomCode = WeightUomCode.LB.ToString();
            item.height = 35;
            item.heightUomCode = HeightUomCode.IN.ToString();

            item.length = 48;
            item.lengthUomCode = LengthUomCode.IN.ToString();
            item.width = 42;
            item.widthUomCode = WidthUomCode.IN.ToString();
            item.nmfcCode = "345";
     //       orderRequest.addItem(item);

            var body = JsonSerializer.Serialize(orderRequest);
            string xpoToken = null;
            xpoToken = await Token.getToken(xpoToken);

            var client = new RestClient("https://" + XPOSettings.XPOConnectURL + "/quoteAPI/rest/v1/ConvertToOrder");
            //client.Timeout = -1;

            var request = new RestRequest();
            request.AddHeader("x-api-key", XPOSettings.XAPIKeyRequest);

            request.AddHeader("Accept", "*/*");
            request.Method = Method.Post;
            request.AddHeader("xpoauthorization", xpoToken);
            request.AddHeader("Content-Type", "application/json");

            try
            {
                request.AddJsonBody(orderRequest);
                var response = await client.ExecuteAsync(request);

                OrderResponse? orderResponse =
JsonSerializer.Deserialize<OrderResponse>(response.Content);
                return orderResponse;
            }
            catch (Exception e)
            {

            }
            return null;

        }
        public async Task<OrderResponse> convertToOrder(TabPage tabPage, QuoteRequest quoteRequest,String trackingNumber)
        {
            OrderRequest orderRequest = new();
            orderRequest.partnerIdentifierCode = XPOSettings.PartnerIdentifierCode;
            orderRequest.transportationMode = XPOSettings.TransportationMode;
            orderRequest.applicationSource = ApplicationSource.GPAPI.ToString();
            Label lblQuoteId = tabPage.Controls["lblQuoteIdValue"] as Label;
            orderRequest.quoteId = lblQuoteId.Text;

            //IList<ContactInformation> contacts = quoteRequest.contactInformations;
            //orderRequest.orderContactInformations= quoteRequest.contactInformations;

            OrderReferenceNumber referenceNumber = new();
            referenceNumber.code = "PRO";
            referenceNumber.type = "PRO";
            referenceNumber.value = trackingNumber;
            orderRequest.addReferenceNumber(referenceNumber);

            //IList<Stop> stops = quoteRequest.stops;
            //foreach (Stop stop in stops)
            //{
            //    orderRequest.addStop(stop);

            //}
            //IList<QuoteItem> items = quoteRequest.items;
            //foreach (QuoteItem item in items)
            //{
            //    OrderItem orderItem = new OrderItem();
            //    orderItem.itemCode = item.productCode;
            //    orderItem.itemDescription = item.itemDescription;
            //    orderItem.itemNumber = item.itemNumber;
            //    orderItem.units = item.units;
            //    orderItem.packageUnits = item.packageUnits;
            //    orderItem.unitTypeCode = item.unitTypeCode;
            //    orderItem.packageTypeCode = item.packageTypeCode;
            //    orderItem.weight = item.weight;
            //    orderItem.weightUomCode = item.weightUomCode;
            //    orderItem.height = item.height;
            //    orderItem.heightUomCode = item.heightUomCode;
            //    orderItem.length = item.length;
            //    orderItem.lengthUomCode = item.lengthUomCode;
            //    orderItem.width = item.width;
            //    orderItem.widthUomCode = item.widthUomCode;
            //    //orderItem.isHazmat = false;
            //    //orderItem.hazardousItemInfo = item.hazardousItemInfo;
            //    orderItem.htsCode = "";
            //    orderItem.purchaseOrderNumber ="";


            //    orderItem.isOversized = false;
            //    orderItem.isStackable = false;
            //    //orderItem.isTemperatureControlled = item.isTemperatureControlled;
            //    //orderItem.temperatureInformation = item.temperatureInformation;
            //    //orderItem.sku = item.sku;
            //    orderItem.classCode = "";
            //    orderItem.nmfcCode = "";
            //    orderItem.declaredValue = 20;
            //    orderItem.itemDescription = item.itemDescription;
            //    orderItem.declaredValueCurrencyCode = "";
            //    orderItem.itemPrice = 20;
            //    orderItem.itemPriceCurrencyCode = "";



            //    orderItem.itemServices = null;
            //    orderItem.itemReferenceNumbers =null;
            //    orderItem.itemNotes = null;

            //    orderRequest.addItem(orderItem);
            //}

            var body = JsonSerializer.Serialize(orderRequest);
            string xpoToken = null;
            xpoToken = await Token.getToken(xpoToken);

            var client = new RestClient("https://" + XPOSettings.XPOConnectURL + "/quoteAPI/rest/v1/ConvertToOrder");
            //client.Timeout = -1;

            var request = new RestRequest();
            request.AddHeader("x-api-key", XPOSettings.XAPIKeyRequest);

            request.AddHeader("Accept", "*/*");
            request.Method = Method.Post;
            request.AddHeader("xpoauthorization", xpoToken);
            request.AddHeader("Content-Type", "application/json");

            try
            {
                request.AddJsonBody(orderRequest);
                var response = await client.ExecuteAsync(request);

                OrderResponse? orderResponse =
JsonSerializer.Deserialize<OrderResponse>(response.Content);
                return orderResponse;
            }
            catch (Exception e)
            {

            }
            return null;

        }
    }
}
