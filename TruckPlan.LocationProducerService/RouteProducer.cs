using System.Net.Http.Json;
using TruckPlan.LocationProducerService.Dto;

namespace TruckPlan.LocationProducerService
{
    public class RouteProducer : IHostedService, IAsyncDisposable
    {
        private readonly ILogger<RouteProducer> _logger;
        private HttpClient _httpClientRandomLocation;
        private HttpClient _httpClientTruckPlan;
        private Timer? _timer;
        private int _seconds = 25; 
        private int _executionCount = 0;
        private int _id = 0;

        public RouteProducer(ILogger<RouteProducer> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientRandomLocation = httpClientFactory.CreateClient("RandomLocation.Api");
            _httpClientTruckPlan = httpClientFactory.CreateClient("Truckplan.Api");
        }

        private async void DoWork(object? state)
        {
            int count =  Interlocked.Increment(ref _executionCount);

            _logger.LogInformation("{0} is running, execution count: {1}", nameof(RouteProducer), count);

            await Task.Factory.StartNew(() => PopulateRouteFromTruckPlanOne(count))
                    .ContinueWith((t1) => PopulateRouteFromTruckPlanTwo(count))
                    .ContinueWith((t2) => PopulateRouteFromTruckPlanThree(count));
        }

        private async Task PopulateRouteFromTruckPlanOne(int count)
        {
            var randomLocation = await GetRandomLocationInCountry("NL");

            await AddRouteOnRandomLocation(randomLocation, 1);
        }

        private async Task PopulateRouteFromTruckPlanTwo(int count)
        {
            var randomLocation = await GetRandomLocationInCountry("DK");

            await AddRouteOnRandomLocation(randomLocation, 2);
        }

        private async Task PopulateRouteFromTruckPlanThree(int count)
        {
            var randomLocation = await GetRandomLocationInCountry("DE");

            await AddRouteOnRandomLocation(randomLocation, 3);
        }

        private async Task AddRouteOnRandomLocation(RandomLocationDto randomLocation, int truckPlanId)
        {
            int count = Interlocked.Increment(ref _id);

            var routeDto = new RouteDto
            {
                Lattitude = randomLocation.Nearest.Latt, 
                Longitude = randomLocation.Nearest.Longt,
                LocationTimeStamp = DateTime.Now,
                TruckPlanId = truckPlanId, 
                Country = string.Empty,
                /*Id = count*/ // We should not pass the Id and it should be genrated by database. Since we are using in memory db so passsing it while seeding the data.
            };

            HttpResponseMessage response = await _httpClientTruckPlan.PostAsJsonAsync<RouteDto>("api/TruckPlans", routeDto);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Adding Route failed due to Error: {2}", response.ReasonPhrase);
            }
        }

        private async Task<RandomLocationDto> GetRandomLocationInCountry(string countryCode)
        {
            var response = await _httpClientRandomLocation.GetAsync(@$"randomland.{countryCode}.json"); 

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Error getting random location. Error: {0}", response.ReasonPhrase);
            }

            return await response.Content.ReadFromJsonAsync<RandomLocationDto>();
        }

        public ValueTask DisposeAsync()
        {
            if(_timer is IAsyncDisposable timer)
            {//
                timer.DisposeAsync();
            }

            _timer = null;

            return ValueTask.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("{0} is running.", nameof(RouteProducer));
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_seconds));

            return Task.CompletedTask;
            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("{0} is stopping.", nameof(RouteProducer));
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
