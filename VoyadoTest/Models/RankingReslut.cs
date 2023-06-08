namespace VoyadoTest.Models;

public class RankingResult
{
    public RankingResult(string query)
    {
        Query = query;
    }
    
    public string Query { get; set; }
    public float GoogleSearchResult { get; set; }
    public float BingSearchResult { get; set; }
}