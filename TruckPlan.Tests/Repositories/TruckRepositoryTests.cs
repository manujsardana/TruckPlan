using FluentAssertions;
using TruckPlan.Domain;
using TruckPlan.Domain.Interfaces.Repositories;
using TruckPlan.Infrastructure;
using TruckPlan.Infrastructure.Repository;

namespace TruckPlan.Tests.Repositories
{
    [TestClass]
    public class TruckRepositoryTests
    {
        private ITruckRepository _truckRepository;
        private DbContext _dbContext;

        [TestInitialize]
        public void Init()
        {
            _dbContext = new DbContext();
            _truckRepository = new TruckRepository(_dbContext);
        }

        [TestMethod]
        public async Task AddTruckTest_When_AddTruck_Then_ReturnAddedTruck()
        {
            //Arrange
            Truck truck = new Truck("Test", EngineType.Diesel, 1);

            //Act
            await _truckRepository.AddTruckAsync(truck);

            //Assert
            _dbContext.Trucks.First().Should().NotBeNull();
            _dbContext.Trucks.First().Manufacturer.Should().Be(truck.Manufacturer);
            _dbContext.Trucks.First().TruckEngineType.Should().Be(truck.TruckEngineType);
            _dbContext.Trucks.First().GpsDeviceId.Should().Be(truck.GpsDeviceId);

        }
    }
}