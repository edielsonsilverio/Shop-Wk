﻿using Shop.Core.WebApi.Identidade;

namespace Shop.WebApi.Configuration;
public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddCors(options =>
        {
            options.AddPolicy("Development",
                builder =>
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());


            //options.AddPolicy("Production",
            //    builder =>
            //        builder
            //            .WithMethods("GET")
            //            .WithOrigins("http://localhost")
            //            .SetIsOriginAllowedToAllowWildcardSubdomains()
            //            //.WithHeaders(HeaderNames.ContentType, "x-custom-header")
            //            .AllowAnyHeader());
        });

        return services;
    }

    public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseCors("Development");
            app.UseDeveloperExceptionPage();
            app.UseHsts();
            app.UseHttpsRedirection();
        }
        else
        {
            app.UseCors("Development"); // Usar apenas nas demos => Configuração Ideal: Production
        }

        app.UseRouting();

        app.UseAuthConfiguration();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}

