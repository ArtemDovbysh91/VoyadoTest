using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VoyadoTest.Business;
using VoyadoTest.Models;

namespace VoyadoTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly RankingService _rankingService;

        public SearchController(RankingService rankingService)
        {
            _rankingService = rankingService;
        }
        
        [HttpGet]
        public async Task<RankingResult> Get(string query)
        {
            return await _rankingService.RankQuery(query);
        }
    }
}
