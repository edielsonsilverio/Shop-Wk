using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shop.Business.Intefaces;
using Shop.Business.Services;
using Shop.Core.DomainObjects;
using Shop.Core.Mediator;
using Shop.Core.Notificacoes;
using Shop.Core.Utils;
using Shop.Core.WebApi.Usuario;
using Shop.DataAccess.Context;
using Shop.DataAccess.Repository;
using Shop.Message.Email.SendGrid.Implements;
using Shop.Message.Email.SendGrid.Interfaces;
using Shop.MessageBus.BusRabbit;
using Shop.MessageBus.EventQueue;
using Shop.WebApi.Application.Events;
using Shop.WebApi.Services;


namespace Shop.WebApi.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services,
                                     IConfiguration configuration)
    {
        // API
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();
        services.AddScoped<INotificador, Notificador>();
        services.AddScoped<AutenticationService>();

        // Data
        services.AddDbContext<ShopDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(5, 7))));

        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();

        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IProdutoService, ProdutoService>();

        //RabbitMQ
        //Carrega as inforamções do arquivo appSettings com a tag RabbitMQSettings
        var appSettings = configuration.GetSection("RabbitMQSettings");
        services.Configure<RabbitMQSettings>(appSettings);

        services.AddTransient<IRabbitMQConnection, RabbitMQConnection>(sp =>
        {
            var optionFactory = sp.GetService<IOptions<RabbitMQSettings>>();
            return new RabbitMQConnection(optionFactory.Value);
        });

        services.AddSingleton<IRabbitEventBus, RabbitEventBus>(scope =>
        {
            var scopeFactory = scope.GetRequiredService<IServiceScopeFactory>();
            return new RabbitEventBus(scope.GetService<IMediator>(), scope.GetService<IRabbitMQConnection>(), scopeFactory);
        });

        services.AddSingleton<ISendGridEmail, SendGridEmail>();

        services.AddTransient<EmailEventHandler>();
        services.AddTransient<IEventHandler<EmailEventQueue>, EmailEventHandler>();

        return services;
    }
}
