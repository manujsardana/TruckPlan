using Microsoft.AspNetCore.Mvc;
using TruckPlan.Domain.Interfaces.Repositories;

namespace TruckPlan.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverReportsController : ControllerBase
    {
        private readonly ILogger<DriverReportsController> _logger;
        private readonly IDriverReportRepository _driverReportRepository;

        public DriverReportsController(ILogger<DriverReportsController> logger, IDriverReportRepository driverReportRepository)
        {
            _logger = logger;
            _driverReportRepository = driverReportRepository;
        }

        [HttpGet(Name = nameof(GetDriverReport))]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> GetDriverReport(int age, DateTime startDate, DateTime endDate, int distance, string countryCode)
        {
            return Ok(await _driverReportRepository.GetDriverReport(age, startDate, endDate, distance, countryCode));
        }
    }
}
