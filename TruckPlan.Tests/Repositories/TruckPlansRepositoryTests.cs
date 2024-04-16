using FluentAssertions;
using TruckPlan.Domain;
using TruckPlan.Domain.Interfaces.Repositories;
using TruckPlan.Infrastructure;
using TruckPlan.Infrastructure.Repository;

namespace TruckPlan.Tests.Repositories
{
    [TestClass]
    public class TruckPlansRepositoryTests
    {
        private ITruckPlanRepository _truckPlanRepository;
        private IRouteRepository _routeRepository;
        private DbContext _dbContext;

        [TestInitialize]
        public void Init()
        {
            _dbContext = new DbContext();
            _routeRepository = new RouteRepository(_dbContext);
            _truckPlanRepository = new TruckPlanRpository(_dbContext, _routeRepository);
        }

        [TestMethod]
        public async Task AddTruckPlanAsyncTest_When_TruckPlanAdded_Then_AddTruckPlan()
        {
            //Arrange
            Domain.TruckPlan truckPlan = new Domain.TruckPlan(1, 1, 1, DateTime.Now);

            //Act
            await _truckPlanRepository.AddTruckPlanAsync(truckPlan);

            //Assert
            _dbContext.TruckPlans.First().Should().NotBeNull();
            _dbContext.TruckPlans.First().DriverId.Should().Be(truckPlan.DriverId);
            _dbContext.TruckPlans.First().GpsDeviceId.Should().Be(truckPlan.GpsDeviceId);
            _dbContext.TruckPlans.First().TruckId.Should().Be(truckPlan.TruckId);

        }

        [TestMethod]
        public async Task AddRouteToTruckPlanTest_When_RouteAdded_Then_VerifyDistance()
        {
            //Arrange
            Domain.TruckPlan truckPlan = new Domain.TruckPlan(1, 1, 1, DateTime.Now);
            var result = await _truckPlanRepository.AddTruckPlanAsync(truckPlan);
            Route route1 = new Route(result.Id, 53.717, 9.90089, "DE", DateTime.Now);
            Route route2 = new Route(result.Id, 50.04167, 8.12641, "DE", DateTime.Now.AddMinutes(1));

            //Act
            await _truckPlanRepository.AddRouteToTruckPlanAsync(route1, "DE");
            await _truckPlanRepository.AddRouteToTruckPlanAsync(route2, "DE");

            //Assert
            _dbContext.TruckPlans.First().Should().NotBeNull();
            _dbContext.TruckPlans.First().Distance.Should().Be(426);
            _dbContext.TruckPlans.First().Routes.First().Lattitude.Should().Be(53.717);
            _dbContext.TruckPlans.First().Routes.Last().Lattitude.Should().Be(50.04167);

        }
    }
}