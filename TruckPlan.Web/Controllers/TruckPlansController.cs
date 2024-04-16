using Mapster;
using Microsoft.AspNetCore.Mvc;
using TruckPlan.Domain;
using TruckPlan.Web.Dto;
using TruckPlan.Infrastructure.Exception;
using TruckPlan.Domain.Interfaces.Services;

namespace TruckPlan.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TruckPlansController : Controller
    {
        private readonly ILogger<TruckPlansController> _logger;
        private ITruckPlanService _truckPlanService;

        public TruckPlansController(ILogger<TruckPlansController> logger, 
                                    ITruckPlanService truckPlanServie)
        {
            _logger = logger;
            _truckPlanService = truckPlanServie;
        }

        [HttpGet(Name = nameof(GetTruckPlans))]
        [ProducesResponseType(typeof(IEnumerable<TruckPlanDto>), 200)]
        public async Task<IActionResult> GetTruckPlans()
        {
            var truckPlans = (await _truckPlanService.GetAllTruckPlansAsync()).Select(d => d.Adapt<TruckPlanDto>());

            return Ok(truckPlans);
        }

        [HttpPut(Name = nameof(AddTruckPlan))]
        public async Task<IActionResult> AddTruckPlan(TruckPlanDto truckPlanDto)
        {
            if (truckPlanDto is null) return BadRequest();

            var truckPlan = truckPlanDto.Adapt<Domain.TruckPlan>();
            await _truckPlanService.AddTruckPlanAsync(truckPlan);

            return Ok(truckPlanDto);
        }

        [HttpPost(Name = nameof(AddRouteToTruckPlan))]
        public async Task<IActionResult> AddRouteToTruckPlan(RouteDto routeDto)
        {
            if (routeDto is null) return BadRequest();

            try
            {
                var route = routeDto.Adapt<Domain.Route>();
                await _truckPlanService.AddRouteToTruckPlanAsync(route);

                _logger.LogInformation($"Added route with Lattitude: {route.Lattitude}, Longitude: {route.Longitude}, for TruckPlan: {route.TruckPlanId}");
            }
            catch (CountryFetchException ex)
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
