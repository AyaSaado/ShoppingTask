global using ShoppingTask.Core.DTOs;
global using ShoppingTask.Domain.Jwt;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.SignalR;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Configuration;
global using System.Text;
global using MailKit.Net.Smtp;
global using MailKit.Security;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using MimeKit;
global using SixLabors.ImageSharp;
global using ShoppingTask.Core.Models;
global using ShoppingTask.Core.Tools.Mail;
global using ShoppingTask.Infrastructure.Repository.UnitOfWork;
global using System.Security.Claims;
global using MediatR;
global using ShoppingTask.Core.Enums;
global using ShoppingTask.Core.Shared;
global using Microsoft.EntityFrameworkCore; 
global using ShoppingTask.Domain.Tools.Files;
global using System.Linq.Expressions;
global using AutoMapper;
global using ShoppingTask.Services;
global using ShoppingTask.Core.Session;



namespace ShoppingTask.Domain;

public static class Dependencies
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        
        services.AddSignalR();


        services.AddSignalR();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AssemblyReference).Assembly));
        services.Configure<JwtOptions>(options =>
        {
            // Configure your JWT options here
        })
        .ConfigureOptions<JwtOptionsSetup>()
        .ConfigureOptions<JwtBearerOptiosSetup>()
        .AddScoped<IJwtProvider, JwtProvider>()
        .AddTransient<IMailService, MailService>()
        .AddScoped<IFileServices, FileServices>()
        .AddTransient<TokenServices>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.SaveToken = true;
                 options.Events = new JwtBearerEvents
                 {
                     OnMessageReceived = context =>
                     {
                         var accessToken = context.Request.Query["access_token"];
                         var path = context.HttpContext.Request.Path;
                         if (!string.IsNullOrEmpty(accessToken) &&
                              (path.StartsWithSegments("/CartHub")))
                         {
                             context.Token = accessToken;
                         }
                         return Task.CompletedTask;
                     }
                 };
             });

        return services;
    }

}
