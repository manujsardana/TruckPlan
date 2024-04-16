using Mapster;
using System;
using System.Reflection;
using TruckPlan.Domain;
using TruckPlan.Web.Dto;

namespace TruckPlan.Web
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<DriverDto, Driver>.NewConfig()
                    .MapToConstructor(true);

            TypeAdapterConfig<RouteDto, Domain.Route>.NewConfig()
                    .MapToConstructor(true);

            TypeAdapterConfig<TruckDto, Domain.Truck>.NewConfig()
                    .MapToConstructor(true);

            TypeAdapterConfig<TruckPlanDto, Domain.TruckPlan>.NewConfig()
                    .MapToConstructor(true);

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
