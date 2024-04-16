using FluentAssertions;
using TruckPlan.Domain;
using TruckPlan.Domain.Interfaces.Repositories;
using TruckPlan.Infrastructure;
using TruckPlan.Infrastructure.Repository;

namespace TruckPlan.Tests.Repositories
{
    [TestClass]
    public class RouteRepositoryTests
    {
        private IRouteRepository _routeRepository;
        private DbContext _dbContext;

        [TestInitialize]
        public void Init()
        {
            _dbContext = new DbContext();
            _routeRepository = new RouteRepository(_dbContext);
        }

        [TestMethod]
        public async Task AddRouteTest_When_AddRoute_Then_ReturnAddedRoute()
        {
            //Arrange
            string country = "NL";
            Route route = new Route(1, 52.5, 52.5, country, DateTime.Now);

            //Act
            var result = await _routeRepository.AddRouteAsync(route, country);

            //Assert
            _dbContext.Routes.First().Should().NotBeNull();
            _dbContext.Routes.First().Country.Should().Be(country);
            _dbContext.Routes.First().Lattitude.Should().Be(52.5);
            _dbContext.Routes.First().Longitude.Should().Be(52.5);

        }

        [TestMethod]
        public async Task GetAllRoutesByTruckPlanIdTest_When_GetAllRoutes_Then_ReturnAllRoutesForTruckPlanId()
        {
            //Arrange
            string country = "NL";
            Route route1 = new Route(1, 52.5, 52.5, country, DateTime.Now);
            Route route2 = new Route(1, 51.5, 51.5, country, DateTime.Now.AddMinutes(1));
            var result1 = await _routeRepository.AddRouteAsync(route1, country);
            var result2 = await _routeRepository.AddRouteAsync(route2, country);

            //Act
            var routes = await _routeRepository.GetRoutesByTruckPlanIdAsync(1);

            //Assert
            routes.Should().NotBeNull();
            routes.Count().Should().Be(2);
            routes.First().TruckPlanId.Should().Be(route1.TruckPlanId);
            routes.Last().TruckPlanId.Should().Be(route2.TruckPlanId);
            routes.First().Lattitude.Should().Be(52.5);
            routes.Last().Lattitude.Should().Be(51.5);
        }
    }
}