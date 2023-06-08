using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace VoyadoTest.Services;

public class BingSearch : IBingSearch
{
    private const string BingSearchUri = "https://api.bing.microsoft.com/v7.0/search?q=";

    private readonly ILogger<BingSearch> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;
   
    public BingSearch(IHttpClientFactory httpClientFactory, IConfiguration config, ILogger<BingSearch> logger) =>
        (_httpClientFactory, _config, _logger) = (httpClientFactory, config, logger);

    public async Task<float> Search(string query)
    {
        return await BingSearchWithClientFactory(query);
    }

    private async Task<float> BingSearchWithClientFactory(string query)
    {
        var subscriptionKey = _config["Search:Bing:subscriptionKey"];
        var uriQuery = BingSearchUri + Uri.EscapeDataString(query);
        
        var client = _httpClientFactory.CreateClient();
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uriQuery)
        {
            Headers =  {  { "Ocp-Apim-Subscription-Key", subscriptionKey } }
        };

        try
        {
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                httpResponseMessage.EnsureSuccessStatusCode();
                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                dynamic parsedJson = JObject.Parse(responseBody);
                return (float)parsedJson["webPages"]["totalEstimatedMatches"];
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred");
            Console.WriteLine(e);
        }
        
        return (float)0;
    }
}