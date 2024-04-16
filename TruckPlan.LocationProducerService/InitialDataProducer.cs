using System.Net.Http.Json;
using TruckPlan.LocationProducerService.Dto;
using TruckPlan.LocationProducvice.Dto;

namespace TruckPlan.LocationProducerService
{
    public class InitialDataProducer : IHostedService, IAsyncDisposable
    {
        private readonly ILogger<InitialDataProducer> _logger;
        private readonly HttpClient _httpClient;
        private Timer? _timer;
        private int _seconds = 5;
        private int _executionCount = 0;

        public InitialDataProducer(ILogger<InitialDataProducer> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient(name: "Truckplan.Api");
        }

        private async void DoWork(object? state)
        {
            AddDrivers();

            AddTrucks();

            AddTruckPlans();
        }

        private async void AddTrucks()
        {
            var trucks = new List<TruckDto>
            {
               new TruckDto { GpsDeviceId = 1, Manufacturer = "Scania", TruckEngineType = EngineType.Diesel},
               new TruckDto { GpsDeviceId = 2, Manufacturer = "VW", TruckEngineType = EngineType.Hybrid},
               new TruckDto { GpsDeviceId = 3, Manufacturer = "Mercedes", TruckEngineType = EngineType.Diesel}, 
               new TruckDto { GpsDeviceId = 4, Manufacturer = "Scania", TruckEngineType = EngineType.Petrol}
            };

            foreach (var truck in trucks)
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync<TruckDto>("api/Trucks", truck);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Adding truck with Id:{0} failed due to Error: {2}", truck.Id, response.ReasonPhrase);
                }
            }
        }

        private async void AddDrivers()
        {
            var drivers = new List<DriverDto>
            {
                new DriverDto { Name = "Test Driver 1", DateOfBirth = new DateOnly(1956, 11, 02), NationalId = "123456789" },
                new DriverDto { Name = "Test Driver 2", DateOfBirth = new DateOnly(1975, 11, 29), NationalId = "456789123" },
                new DriverDto { Name = "Test Driver 3", DateOfBirth = new DateOnly(1999, 10, 02), NationalId = "345678912" },
                new DriverDto { Name = "Test Driver 4", DateOfBirth = new DateOnly(2000, 09, 23), NationalId = "234567891" }
            };

            foreach (var driver in drivers)
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync<DriverDto>("api/Drivers", driver);
                if(!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Adding driver with Id:{0} failed due to Error: {2}", driver.Id, response.ReasonPhrase);
                }
            }
        }

        private async void AddTruckPlans()
        {
            var truckPlans = new List<TruckPlanDto>
            {
               new TruckPlanDto
               {
                   DriverId = 1,
                   GpsDeviceId = 1,
                   StartDate = new DateTime(2018, 5, 1),
                   EndDate = null,
                   TruckId = 1
               },
               new TruckPlanDto
               {
                   DriverId = 2,
                   GpsDeviceId = 2,
                   StartDate = new DateTime(2018, 10, 1),
                   EndDate = null,
                   TruckId = 2
               },
                new TruckPlanDto
               {
                   DriverId = 3,
                   GpsDeviceId = 3,
                   StartDate = new DateTime(2018, 1, 1),
                   EndDate = null,
                   TruckId = 3
               }

            };

            foreach (var truckPlan in truckPlans)
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync<TruckPlanDto>("api/TruckPlans", truckPlan);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Adding truckplan with Id:{0} failed due to Error: {2}", truckPlan.Id, response.ReasonPhrase);
                }
            }
        }

        public ValueTask DisposeAsync()
        {
            if (_timer is IAsyncDisposable timer)
            {
                timer.DisposeAsync();
            }

            _timer = null;

            return ValueTask.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("{0} is running.", nameof(InitialDataProducer));
            _timer = new Timer(DoWork, null, 0, Timeout.Infinite);

            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("{0} is stopping.", nameof(InitialDataProducer));
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
