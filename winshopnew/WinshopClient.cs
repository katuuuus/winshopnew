using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class WinShopClient
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public WinShopClient(string token)
    {
        _client = new HttpClient();

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        _baseUrl = "https://api.demo.winshop.cloud";
    }

    public async Task<string> SendSession(Session s)
    {
        var url =
            _baseUrl +
            "/api/documents/pos/shopping-session";

        // ✅ POVINNÝ MODEL PRO WINSHOP
        /* var body = new
         {
             dateOfCreation = s.dateOfPurchase,

             customerId = s.customerId,

             branchId = s.branchId
         };
        */
        var body = new
        {
            dateOfCreation =
         s.dateOfPurchase.ToString("o"),

            posId = 36,

            customerId = 0,

            branchId = 0
        };

        var json = JsonSerializer.Serialize(body);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PostAsync(url, content);

        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(
                "WINSHOP error: " + result
            );
        }

        return result;
    }
}