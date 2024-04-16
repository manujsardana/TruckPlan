using TruckPlan.Web;
using TruckPlan.Infrastructure.Repository;
using System.Net.Http.Headers;
using TruckPlan.Infrastructure.Services;
using TruckPlan.Infrastructure;
using TruckPlan.Infrastructure.Repositories;
using TruckPlan.Domain.Interfaces.Repositories;
using TruckPlan.Domain.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterMapsterConfiguration();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<ITruckRepository, TruckRepository>();
builder.Services.AddScoped<ITruckPlanRepository, TruckPlanRpository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<ICountryFromLocationService, CountryFromLocationService>();
builder.Services.AddScoped<IDriverReportRepository, DriverReportRepository>();
builder.Services.AddScoped<ITruckPlanService, TruckPlanService>();
builder.Services.AddSingleton(typeof(DbContext));
builder.Services.RegisterMapsterConfiguration();

builder.Services.AddHttpClient(name: "CountryLocation.Api", configureClient: options =>
{
    options.BaseAddress = new Uri(@"https://api.bigdatacloud.net/data/reverse-geocode-client/");
    options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.Run();
