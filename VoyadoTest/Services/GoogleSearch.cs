using System;
using System.Threading.Tasks;
using Google.Apis.CustomSearchAPI.v1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace VoyadoTest.Services;

public class GoogleSearch : IGoogleSearch
{
    private readonly IConfiguration _config;
    private readonly ILogger<GoogleSearch> _logger;
    private readonly CustomSearchAPIService _googleService;

    public GoogleSearch(CustomSearchAPIService googleService, IConfiguration config, ILogger<GoogleSearch> logger) =>
        (_googleService, _config, _logger) = (googleService, config, logger);

    public async Task<float> Search(string query)
    {
        try
        {
            var request = _googleService.Cse.List();
            request.Cx = _config["Search:Google:cx"];
            request.Q = query;
            request.Num = 1;

            var result = await request.ExecuteAsync();
            return float.Parse(result.SearchInformation.TotalResults);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred");
            Console.WriteLine(e);
        }
        
        return (float)0;
    }
}