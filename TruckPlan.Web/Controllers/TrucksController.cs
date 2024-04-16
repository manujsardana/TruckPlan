using Microsoft.AspNetCore.Mvc;
using TruckPlan.Domain;
using Mapster;
using TruckPlan.Web.Dto;
using TruckPlan.Domain.Interfaces.Repositories;
using TruckPlan.Infrastructure.Repository;

namespace TruckPlan.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrucksController : ControllerBase
    {
        private readonly ILogger<TrucksController> _logger;
        private readonly ITruckRepository _truckRepository;

        public TrucksController(ILogger<TrucksController> logger, ITruckRepository truckRepository)
        {
            _logger = logger;
            _truckRepository = truckRepository;
        }

        [HttpPut(Name = nameof(AddTruck))]
        public async Task<IActionResult> AddTruck(TruckDto truckDto)
        {
            if (truckDto is null) return BadRequest();

            var truck = truckDto.Adapt<Truck>();
            await _truckRepository.AddTruckAsync(truck);

            return Ok(truckDto);
        }

        [HttpGet(Name = nameof(GetTrucks))]
        [ProducesResponseType(typeof(IEnumerable<TruckDto>), 200)]
        public async Task<IActionResult> GetTrucks()
        {
            var trucks = (await _truckRepository.GetAllTrucksAsync()).Select(d => d.Adapt<TruckDto>());

            return Ok(trucks);
        }
    }
}
