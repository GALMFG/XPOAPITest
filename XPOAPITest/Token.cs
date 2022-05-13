using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RestSharp;

namespace XPOAPITest
{
    public static class Token
    {
 

        public async static Task<string> getToken(String? token)
        {
            try
            {
                var client = new RestClient("https://" +XPOSettings.XPOConnectURL+ "/oAuthAPI/rest/v1/token/");

                // client.Timeout = -1;

                var request = new RestRequest();
               // request.Method = Method.Post;

                request.AddHeader("x-api-key", XPOSettings.XAPIKeyToken);

                request.AddHeader("Content-Type", "application/json");

                request.AddHeader("Accept", "*/*");
                request.Method = Method.Post;

                //request.AddParameter("application/json", "{\r\n\"client_id\": \"" + XPOSettings.ClientId + "\",\r\n\"client_secret\": \"" + XPOSettings.ClientSecret + "\",\r\n\"scope\":\""+ XPOSettings.Scope+"\",\r\n\"grant_type\": \"client_credentials\"\r\n}", ParameterType.RequestBody);
                //   request.AddParameter("application/json", "{\"client_id\":\"xpo-galvantage-integration\",\"client_secret\":\"6ywFMhLijCn1CpzAlTX0CWtc6m4xT0nxcZfliDyIfJ9rX6gSvl74FMX1vgh59enh\",\"scope\":\"xpo-rates-api\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);

                var TokenBody = new TokenBody { client_id = "xpo-galvantage-integration", client_secret = "6ywFMhLijCn1CpzAlTX0CWtc6m4xT0nxcZfliDyIfJ9rX6gSvl74FMX1vgh59enh", scope = "xpo-rates-api", grant_type = "client_credentials" };


                request.AddJsonBody(TokenBody);

                var response =  await client.ExecutePostAsync(request);


                TokenResponse? tokenResponse =
                JsonSerializer.Deserialize<TokenResponse>(response.Content);
                token = tokenResponse.access_token;
                return token;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
        
    