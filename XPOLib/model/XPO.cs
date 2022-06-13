using System;
using System.Text.Json;
using System.Threading.Tasks;
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
        public async Task<String> getToken(String xpoURL, String tokenKey, String ClientId, String clientSecret, String scope, String grantType)
        {
            try
            {
                Task<String> token;
                var client = new RestClient("https://" + xpoURL + "/oAuthAPI/rest/v1/token/");
                var request = new RestRequest();
                request.AddHeader("x-api-key", tokenKey);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "*/*");
                request.Method = Method.Post;
                var TokenBody = new TokenBody { client_id = ClientId, client_secret = clientSecret, scope = scope, grant_type = grantType };
                request.AddJsonBody(TokenBody);
                var response = await client.ExecutePostAsync(request);
                TokenResponse? tokenResponse =
                JsonSerializer.Deserialize<TokenResponse>(response.Content);
                return tokenResponse.access_token;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<QuoteResponse> getQuote(QuoteRequest quoteRequest,String xpoToken,String xpoURL,String requestKey)
        {
            var body = JsonSerializer.Serialize(quoteRequest);


            var client = new RestClient("https://" + xpoURL + "/quoteAPI/rest/v1/Create");
            var request = new RestRequest(Method.Post.ToString());
            request.AddHeader("x-api-key", requestKey);
            request.AddHeader("Accept", "*/*");
            request.Method = Method.Post;
            String token = xpoToken.ToString();
            request.AddHeader("xpoauthorization", token);
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
        public async Task<OrderResponse> ConvertToOrder(OrderRequest orderRequest, String trackingNumber, String xpoToken, String xpoURL,String requestKey)
        {
            var body = JsonSerializer.Serialize(orderRequest);
     
            var client = new RestClient("https://" + xpoURL + "/quoteAPI/rest/v1/ConvertToOrder");
            var request = new RestRequest();
            request.AddHeader("x-api-key", requestKey);

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
