using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsReseller.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] [Required] StatisticsQueryContract query, CancellationToken cancellationToken)
        {
            var financialStatistic = await _statisticsRepository.GetAsync(query, cancellationToken);
            
            return Ok(financialStatistic);
        }
    }
}