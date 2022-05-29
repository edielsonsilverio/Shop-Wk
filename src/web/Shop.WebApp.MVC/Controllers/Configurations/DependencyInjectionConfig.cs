using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Shop.Core.WebApi.Usuario;
using Shop.WebApp.MVC.Services;
using Shop.WebApp.MVC.Services.Handlers;

namespace Shop.WebApp.MVC.Controllers.Configurations;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        #region HttpServices

        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IAutenticacaoService, AutenticacaoService>()
            .AddPolicyHandler(PollyExtensions.EsperarTentar())
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddHttpClient<ICategoriaService, CategoriaService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(PollyExtensions.EsperarTentar())
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddHttpClient<IProdutoService, ProdutoService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(PollyExtensions.EsperarTentar())
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


        #endregion
    }

    #region PollyExtension


    public class PollyExtensions
    {
        //Método usuando a biblioteca Polly, serve para manipular o request em caso de erro.
        public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
        {
            var retry = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                }, (outcome, timespan, retryCount, context) =>
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Tentando pela {retryCount} vez!");
                    Console.ForegroundColor = ConsoleColor.White;
                });

            return retry;
        }
    }

    #endregion
}

#region Refit

//services.AddHttpClient("Refit",
//        options =>
//        {
//            options.BaseAddress = new Uri(configuration.GetSection("CatalogoUrl").Value);
//        })
//    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
//.AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);

#endregion