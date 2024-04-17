using TruckPlan.Domain;
using TruckPlan.Domain.Interfaces;
using TruckPlan.Domain.Interfaces.Repositories;

namespace TruckPlan.Infrastructure.Repository
{
    public class TruckRepository : ITruckRepository
    {
        private readonly DbContext _dbContext;

        public TruckRepository(DbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Truck> AddTruckAsync(Truck truck)
        {
            //This should be removed when we have actual database and replace with savechangesasync
            (truck as IIdGenerator).SetId(_dbContext.Trucks.Count() + 1);
            _dbContext.Trucks.Add(truck);

            return await Task.FromResult(truck);
        }
        public async Task<List<Truck>> GetAllTrucksAsync()
        {
            return await Task.FromResult(_dbContext.Trucks.ToList());
        }
    }
}
