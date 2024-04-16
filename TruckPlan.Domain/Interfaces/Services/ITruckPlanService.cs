namespace TruckPlan.Domain.Interfaces.Services
{
    public interface ITruckPlanService
    {
        Task AddRouteToTruckPlanAsync(Route route);

        Task<TruckPlan> AddTruckPlanAsync(TruckPlan truckPlan);

        Task<List<TruckPlan>> GetAllTruckPlansAsync();
    }
}
