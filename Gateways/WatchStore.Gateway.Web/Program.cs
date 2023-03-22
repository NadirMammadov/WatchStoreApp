using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", opt =>
{
    opt.Authority = builder.Configuration["IdentityServerURL"];
    opt.Audience = "resource_gateway";
    opt.RequireHttpsMetadata = false;
});
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
    .AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName}.json")
    .AddEnvironmentVariables();
});



builder.Services.AddOcelot();
var app = builder.Build();
await app.UseOcelot();
app.MapGet("/", () => "Hello World!");

app.Run();
