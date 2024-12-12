global using System.Text.Json.Serialization;
global using MediatR;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.OpenApi.Models;
global using ShoppingTask.Api;
global using ShoppingTask.Core.DTOs;
global using ShoppingTask.Core.Enums;
global using ShoppingTask.Core.Shared;
global using ShoppingTask.Domain.Auth;
global using ShoppingTask.Services.Auth;
global using Swashbuckle.AspNetCore.Annotations;

namespace ShoppingTask.API;

public static class Dependencies
{
    public static IServiceCollection AddApp(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
            );
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            // JWT Bearer Token Definition
            options.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "input Token without **Bearer**",

                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme,
                    },
                }
            );

            // API Key Definition
            options.AddSecurityDefinition(
                "ApiKey",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "x-api-key", // The header name for the API key
                    Type = SecuritySchemeType.ApiKey,
                    Description =
                        "API Key required for access. Enter the key in the format: APIKEY: {your_key}",
                }
            );

            // Global Security Requirements for both JWT and API Key
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                        },
                        Array.Empty<string>()
                    },
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey",
                            },
                            Scheme = "ApiKey",
                            Name = "x-api-key",
                            In = ParameterLocation.Header,
                        },
                        Array.Empty<string>()
                    },
                }
            );

            // Swagger Document Information
            options.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Shopping Task API",
                    Contact = new OpenApiContact
                    {
                        Name = "Eng Aya Saado",
                        Email = "ayasaado188@gmail.com",
                    },
                }
            );
        });

        services.AddCors(o =>
        {
            o.AddPolicy(
                "Default",
                p =>
                {
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                    p.AllowAnyOrigin();
                    p.SetPreflightMaxAge(TimeSpan.FromMinutes(10));
                }
            );
        });
        // services.AddSignalR(options => options.EnableDetailedErrors = true);

        return services;
    }
}
