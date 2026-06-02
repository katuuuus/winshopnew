using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class WinShopAuth
{
    public static async Task<string> GetToken(
        string clientId,
        string clientSecret
    )
    {
        using (var client = new HttpClient())
        {
            var url =
                "https://api.demo.winshop.cloud/connect/token";

            var body =
                new FormUrlEncodedContent(
                    new[]
                    {
                        new KeyValuePair<string, string>(
                            "grant_type",
                            "client_credentials"
                        ),

                        new KeyValuePair<string, string>(
                            "client_id",
                            clientId
                        ),

                        new KeyValuePair<string, string>(
                            "client_secret",
                            clientSecret
                        )
                    });

            var response =
                await client.PostAsync(url, body);

            var json =
                await response.Content.ReadAsStringAsync();

            var parsed =
                JObject.Parse(json);

            return parsed["access_token"]
                ?.ToString() ?? "";
        }
    }
}