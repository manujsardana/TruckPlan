using System.Net.Http.Headers;
using TruckPlan.LocationProducerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<RouteProducer>();
builder.Services.AddHostedService<InitialDataProducer>();

builder.Services.AddHttpClient(name: "Truckplan.Api", configureClient: options =>
{
    options.BaseAddress = new Uri(@"http://localhost:5166/");
    options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
});

builder.Services.AddHttpClient(name: "RandomLocation.Api", configureClient: options =>
{
    options.BaseAddress = new Uri(@"https://api.3geonames.org/");
    options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
});

var host = builder.Build();
host.Run();
