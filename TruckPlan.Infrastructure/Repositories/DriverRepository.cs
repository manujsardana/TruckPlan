using TruckPlan.Domain;
using TruckPlan.Domain.Interfaces;
using TruckPlan.Domain.Interfaces.Repositories;

namespace TruckPlan.Infrastructure.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private readonly DbContext _dbContext;

        public DriverRepository(DbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<Driver> AddDriverAsync(Driver driver)
        {
            //This should be removed when we have actual database and replace with savechangesasync
            (driver as IIdGenerator).SetId(_dbContext.Drivers.Count() + 1);
            _dbContext.Drivers.Add(driver);

            return await Task.FromResult(driver);
        }

        public async Task<List<Driver>> GetAllDriversAsync()
        {
            return await Task.FromResult(_dbContext.Drivers.ToList());
        }
    }
}
