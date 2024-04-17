using FluentAssertions;
using TruckPlan.Domain;
using TruckPlan.Domain.Interfaces.Repositories;
using TruckPlan.Infrastructure;
using TruckPlan.Infrastructure.Repositories;
using TruckPlan.Infrastructure.Repository;

namespace TruckPlan.Tests.Repositories
{
    [TestClass]
    public class DriverReportRepositoryTests
    {
        private IDriverReportRepository _driverReportRepository;
        private DbContext _dbContext;

        [TestInitialize]
        public void Init()
        {
            _dbContext = new DbContext();
            _driverReportRepository = new DriverReportRepository(_dbContext);
           
        }

        //Usually it might be a good idea to generate the test data for particular scenario so its easier to maintain
        // but generting to test all scenarios for now
        private async Task AddTestData()
        {
            ITruckRepository truckRepository = new TruckRepository(_dbContext);
            Truck truck = new Truck("Test", EngineType.Diesel, 1);
            await truckRepository.AddTruckAsync(truck);
            IDriverRepository driverRepository = new DriverRepository(_dbContext);
            Driver driver = new Driver("Test Driver", DateOnly.FromDateTime(DateTime.Now.AddYears(-51)), "123456789");
            var result = await driverRepository.AddDriverAsync(driver);
            IRouteRepository routeRepository = new RouteRepository(_dbContext);
            ITruckPlanRepository truckPlanRepository = new TruckPlanRpository(_dbContext, routeRepository);
            Domain.TruckPlan truckPlan = new Domain.TruckPlan(1, 1, 1, DateTime.Now);
            await truckPlanRepository.AddTruckPlanAsync(truckPlan);
            Route route1 = new Route(result.Id, 53.717, 9.90089, "DE", DateTime.Now);
            Route route2 = new Route(result.Id, 50.04167, 8.12641, "DE", DateTime.Now.AddMinutes(1));
            await truckPlanRepository.AddRouteToTruckPlanAsync(route1, "DE");
            await truckPlanRepository.AddRouteToTruckPlanAsync(route2, "DE");
            await Task.CompletedTask;
        }

        [TestMethod]
        public async Task GetDriverReport_When_AgeGreaterThan50_Then_ReturnDriverCountAsOne()
        {
            //Arrange
            await AddTestData();

            //Act
            var result = await _driverReportRepository.GetDriverReport(49, DateTime.Now, DateTime.Now, 50, "DE");

            //Assert
            result.Should().Be(1);

        }

        [TestMethod]
        public async Task GetDriverReport_When_AgeGreaterLess50_Then_ReturnDriverCountAsZero()
        {
            //Arrange
            await AddTestData();

            //Act
            var result = await _driverReportRepository.GetDriverReport(52, DateTime.Now, DateTime.Now, 50, "DE");

            //Assert
            result.Should().Be(0);

        }

        [TestMethod]
        public async Task GetDriverReport_When_CountryIsNL_Then_ReturnDriverCountAsZero()
        {
            //Arrange
            await AddTestData();

            //Act
            var result = await _driverReportRepository.GetDriverReport(51, DateTime.Now, DateTime.Now, 50, "NL");

            //Assert
            result.Should().Be(0);

        }

        [TestMethod]
        public async Task GetDriverReport_When_DistanceIsLessThan426_Then_ReturnDriverCountAsZero()
        {
            //Arrange
            await AddTestData();

            //Act
            var result = await _driverReportRepository.GetDriverReport(450, DateTime.Now, DateTime.Now, 50, "NL");

            //Assert
            result.Should().Be(0);

        }
    }
}