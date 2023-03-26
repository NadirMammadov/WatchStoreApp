using CatalogService.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
namespace CatalogService.Application;
public static class ConfigureService
{
    public static IServiceCollection IApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //var mapperConfig = new MapperConfiguration(mc =>
        //{
        //    mc.AddProfile(new MappingProfile());
        //});
        //IMapper mapper = mapperConfig.CreateMapper();
        //services.AddSingleton(mapper);
        services.AddSingleton<IDatabaseSettings>(sp =>
        {
            return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
        });

        services.AddTransient(typeof(ICollectionDatabase<>), typeof(CollectionDataBase<>));
        return services;
    }
}
