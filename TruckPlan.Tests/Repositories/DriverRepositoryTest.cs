using FluentAssertions;
using TruckPlan.Domain;
using TruckPlan.Domain.Interfaces.Repositories;
using TruckPlan.Infrastructure;
using TruckPlan.Infrastructure.Repository;

namespace TruckPlan.Tests.Repositories
{
    [TestClass]
    public class DriverRepositoryTests
    {
        private IDriverRepository _driverRepository;
        private DbContext _dbContext;

        [TestInitialize]
        public void Init()
        {
            _dbContext = new DbContext();
            _driverRepository = new DriverRepository(_dbContext);
        }

        [TestMethod]
        public async Task AddDriverTest_When_AddDriver_Then_ReturnAddedDriver()
        {
            //Arrange
            Driver driver = new Driver("Test Driver", DateOnly.FromDateTime(DateTime.Now), "123456789");

            //Act
            var result = await _driverRepository.AddDriverAsync(driver);

            //Assert
            _dbContext.Drivers.First().Should().NotBeNull();
            _dbContext.Drivers.First().Name.Should().Be(result.Name);
            _dbContext.Drivers.First().DateOfBirth.Should().Be(result.DateOfBirth);
            _dbContext.Drivers.First().NationalId.Should().Be(result.NationalId);

        }

        [TestMethod]
        public async Task GetAllDrivers_When_GetAllDrivers_Then_ReturnAllDrivers()
        {
            //Arrange
            Driver driver1 = new Driver("Test Driver 1", DateOnly.FromDateTime(DateTime.Now), "123456789");
            Driver driver2 = new Driver("Test Driver 2", DateOnly.FromDateTime(DateTime.Now).AddDays(-1), "987654321");
            var result1 = await _driverRepository.AddDriverAsync(driver1);
            var result2 = await _driverRepository.AddDriverAsync(driver2);

            //Act
            var result = await _driverRepository.GetAllDriversAsync();

            //Assert
            result.Count.Should().Be(2);
            result.First().Should().NotBeNull();
            result.Last().Should().NotBeNull();
            
            result.First().Name.Should().Be(driver1.Name);
            result.Last().Name.Should().Be(driver2.Name);

        }
    }
}