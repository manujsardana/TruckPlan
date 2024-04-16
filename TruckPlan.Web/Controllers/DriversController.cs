using Mapster;
using Microsoft.AspNetCore.Mvc;
using TruckPlan.Domain;
using TruckPlan.Web.Dto;
using TruckPlan.Domain.Interfaces.Repositories;

namespace TruckPlan.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly ILogger<DriversController> _logger;
        private readonly IDriverRepository _driverRepsitory;

        public DriversController(ILogger<DriversController> logger, IDriverRepository driverRepository)
        {
            _logger = logger;
            _driverRepsitory = driverRepository;
        }

        [HttpGet(Name = nameof(GetDrivers))]
        [ProducesResponseType(typeof(IEnumerable<DriverDto>), 200)]
        public async Task<IActionResult> GetDrivers()
        {
            var drivers = (await _driverRepsitory.GetAllDriversAsync()).Select(d => d.Adapt<DriverDto>());

            return Ok(drivers);
        }

        [HttpPut(Name = nameof(AddDriver))]
        public async Task<IActionResult> AddDriver(DriverDto driverDto)
        {
            if (driverDto is null) return BadRequest();

            var driver = driverDto.Adapt<Driver>();
            await _driverRepsitory.AddDriverAsync(driver);

            return Ok();
        }
    }
}
