using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using TruckPlan.Domain.Interfaces.Services;
using TruckPlan.Infrastructure.Exception;

namespace TruckPlan.Infrastructure.Services
{
    public class CountryFromLocationService : ICountryFromLocationService
    {
        private readonly ILogger<CountryFromLocationService> _logger;
        private HttpClient _httpClient;

        public CountryFromLocationService(IHttpClientFactory httpCLientFactory) 
        {
            _httpClient = httpCLientFactory.CreateClient(@"CountryLocation.Api");
        }
        public async Task<string> GetCountryFromLocation(double lattitude, double longitude)
        {
            var response = await _httpClient.GetAsync($"?latitude={lattitude}&longitude={longitude}&localityLanguage=en");

            if(!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error getting country from location, Error: {response.ReasonPhrase}");
                throw new CountryFetchException();
            }

            var countryDto = await response.Content.ReadFromJsonAsync<CountryDto>();
            return countryDto.CountryCode;
        }
    }

    public class CountryDto
    {
        public string CountryCode { get; set; }
    }
}
