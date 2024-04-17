using Microsoft.Extensions.Logging;
using TruckPlan.Domain;
using TruckPlan.Domain.Interfaces.Repositories;
using TruckPlan.Domain.Interfaces.Services;

namespace TruckPlan.Infrastructure.Services
{
    public class TruckPlanService : ITruckPlanService
    {
        private readonly ICountryFromLocationService _countryFromLocationService;
        private readonly ITruckPlanRepository _truckPlanRepository;

        public TruckPlanService(ICountryFromLocationService countryFromLOcationService,
                                ITruckPlanRepository truckPlanRepository,
                                ILogger<TruckPlanService> logger) 
        {
            _countryFromLocationService = countryFromLOcationService;
            _truckPlanRepository = truckPlanRepository;
        }

        public async Task AddRouteToTruckPlanAsync(Route route)
        {
            var country = await _countryFromLocationService.GetCountryFromLocation(route.Lattitude, route.Longitude);

            await _truckPlanRepository.AddRouteToTruckPlanAsync(route, country);
        }

        public async Task<Domain.TruckPlan> AddTruckPlanAsync(Domain.TruckPlan truckPlan)
        {
            return await _truckPlanRepository.AddTruckPlanAsync(truckPlan);
        }

        public async Task<List<Domain.TruckPlan>> GetAllTruckPlansAsync()
        {
            return await _truckPlanRepository.GetAllTruckPlansAsync();
        }
    }
}
