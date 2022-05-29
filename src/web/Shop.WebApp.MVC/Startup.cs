using Shop.WebApp.MVC.Configurations;
using Shop.WebApp.MVC.Controllers.Configurations;

namespace Shop.WebApp.MVC;

public class Startup : Shop.Core.WebApi.IStartup
{
    public IConfiguration Configuration { get; }

    public Startup(IHostEnvironment hostEnvironment)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(hostEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

        if (hostEnvironment.IsDevelopment())
            builder.AddUserSecrets<Startup>();

        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddIdentityConfiguration();

        services.AddMvcConfiguration(Configuration);

        services.RegisterServices(Configuration);
    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseMvcConfiguration(env);
    }
}