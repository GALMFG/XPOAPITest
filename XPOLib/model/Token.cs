using System;
using System.Text.Json;
using System.Threading.Tasks;
using RestSharp;

namespace XPOAPITest
{
    public static class Token
    {
         public async static Task<string> getToken()
        {
            try
            {
                String? token;
                var client = new RestClient("https://" +XPOSettings.XPOConnectURL+ "/oAuthAPI/rest/v1/token/");
                var request = new RestRequest();
                request.AddHeader("x-api-key", XPOSettings.XAPIKeyToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "*/*");
                request.Method = Method.Post;
                var TokenBody = new TokenBody { client_id = XPOSettings.ClientId, client_secret = XPOSettings.ClientSecret, scope = XPOSettings.Scope, grant_type = XPOSettings.GrantType};
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
        
    