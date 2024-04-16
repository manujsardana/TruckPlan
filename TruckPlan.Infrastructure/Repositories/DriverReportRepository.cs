using TruckPlan.Domain.Interfaces.Repositories;

namespace TruckPlan.Infrastructure.Repositories
{
    public class DriverReportRepository : IDriverReportRepository
    {
        private readonly DbContext _dbContext;

        public DriverReportRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetDriverReport(int age, DateTime startDate, DateTime endDate, int distance, string country)
        {
            var birthdayBefore = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(age * -1));


            var drivers = _dbContext.Drivers.Where(x => x.DateOfBirth <= birthdayBefore).ToDictionary(x => x.Id, y => y);

            var truckPlans = _dbContext.TruckPlans.Where(x => drivers.ContainsKey(x.DriverId)
                                                            && x.StartDate.Date >= startDate.Date
                                                            && (x.EndDate <= endDate.Date || x.EndDate is null)
                                                            && x.Distance > distance).ToDictionary(x => x.Id, y => y);

            return _dbContext.Routes.Where(x => x.Country == country
                                            && truckPlans.ContainsKey(x.TruckPlanId)).GroupBy(x => x.TruckPlanId).Count();

        }
    }
}
