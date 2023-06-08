using System.Linq;
using System.Threading.Tasks;
using VoyadoTest.Infrastructure;
using VoyadoTest.Models;
using VoyadoTest.Services;

namespace VoyadoTest.Business;

public class RankingService
{
    private readonly IBingSearchFactory _bingSearchFactory;
    private readonly IGoogleSearch _googleSearch;

    public RankingService(IBingSearchFactory bingSearchFactory, IGoogleSearch googleSearch)
    {
        _bingSearchFactory = bingSearchFactory;
        _googleSearch = googleSearch;
    }

    public async Task<RankingResult> RankQuery(string query)
    {
        var queryWords = query
            .Split(' ')
            .Where(w => !string.IsNullOrWhiteSpace(w))
            .ToArray();
        
        var result = new RankingResult(query);
        
        foreach (var word in queryWords)
        {
            result.GoogleSearchResult += await _googleSearch.Search(word);
        }
        
        var bingSearch = _bingSearchFactory.GetBingSearch();
        foreach (var word in queryWords)
        {
            result.BingSearchResult += await bingSearch.Search(word);
        }

        return result;
    }
}