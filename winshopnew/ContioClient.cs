/*using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

class ContioClient
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public ContioClient(string token)
    {
        _client = new HttpClient();

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token
            );

        _baseUrl =
            "https://api.demo.winshop.cloud";
    }

    public async Task<string> GetReceiptsRaw()
    {
        var url =
            _baseUrl +
            "/api/documents/pos/shopping-session/filter-paginated";

        var bodyObject = new
        {
            since = "2025-01-01T00:00:00Z",
            until = "2026-01-01T00:00:00Z"
        };

        var jsonBody =
            System.Text.Json.JsonSerializer.Serialize(bodyObject);

        var body = new StringContent(
            jsonBody,
            Encoding.UTF8,
            "application/json"
        );

        var response =
            await _client.PostAsync(url, body);

        var json =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(
                "CONTIO error: " + json
            );
        }

        return json;
    }
}*/

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

class ContioClient
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public ContioClient(string token)
    {
        _client = new HttpClient();

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token
            );

        _baseUrl =
            "https://api.demo.winshop.cloud";
    }

    /* public async Task<string> GetReceiptsRaw()
     {
         var url =
             _baseUrl +
             "/api/documents/pos/shopping-session/filter-paginated";

         // ✅ omezí počet sessions
         var bodyObject = new
         {
             page = 1,
             pageSize = 20
         };

         var jsonBody =
             System.Text.Json.JsonSerializer.Serialize(bodyObject);

         var body = new StringContent(
             jsonBody,
             Encoding.UTF8,
             "application/json"
         );

         var response =
             await _client.PostAsync(url, body);

         var json =
             await response.Content.ReadAsStringAsync();

         if (!response.IsSuccessStatusCode)
         {
             throw new Exception(
                 "CONTIO error: " + json
             );
         }

         return json;
     }*/
    public async Task<string> GetReceiptsRaw()
    {
        var url =
            _baseUrl +
            "/api/documents/pos/shopping-session/filter-paginated";

        var bodyObject = new
        {
            since = "2025-01-01T00:00:00Z",
            until = "2026-01-01T00:00:00Z",

            page = 1,
            pageSize = 20
        };

        var jsonBody =
            System.Text.Json.JsonSerializer.Serialize(bodyObject);

        var body = new StringContent(
            jsonBody,
            Encoding.UTF8,
            "application/json"
        );

        var response =
            await _client.PostAsync(url, body);

        var json =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(
                "CONTIO error: " + json
            );
        }

        return json;
    }


}