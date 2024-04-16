using Mapster;
using Microsoft.AspNetCore.Mvc;
using TruckPlan.Domain.Interfaces.Repositories;
using TruckPlan.Web.Dto;

namespace TruckPlan.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoutesController : ControllerBase
    {
        private readonly ILogger<RoutesController> _logger;
        private readonly IRouteRepository _routeRepository;

        public RoutesController(ILogger<RoutesController> logger, IRouteRepository routeRepository)
        {
            _logger = logger;
            _routeRepository = routeRepository;
        }

        [HttpGet(Name = nameof(GetRoutesByTruckPlanId))]
        [ProducesResponseType(typeof(IEnumerable<RouteDto>), 200)]
        public async Task<IActionResult> GetRoutesByTruckPlanId(int truckPlanId)
        {
            var routes = (await _routeRepository.GetRoutesByTruckPlanIdAsync(truckPlanId)).Select(d => d.Adapt<RouteDto>());

            return Ok(routes);
        }
    }
}
