using ShoppingTask.API;
using ShoppingTask.API.MiddelWares;
using ShoppingTask.API.Seed;
using ShoppingTask.Core;
using ShoppingTask.Domain;
using ShoppingTask.Domain.Hubs;
using ShoppingTask.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddApp();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDomain(builder.Configuration);
builder.Services.AddCore();

// Configure the HTTP request pipeline
var app = builder.Build();

app.UseCors("Default");

app.UseSwagger();
app.UseSwaggerUI(o =>
{
    o.DocExpansion(DocExpansion.None);
});
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseWhen(
    context => context.Request.Path.StartsWithSegments("/api/admin"),
    builder => builder.UseMiddleware<ApiKeyMiddlewareAdmin>()
);
app.UseWhen(
    context => context.Request.Path.StartsWithSegments("/api/public"),
    builder => builder.UseMiddleware<ApiKeyMiddlewareUser>()
);
app.Use(
    async (context, next) =>
    {
        context.Response.Headers.Add("X-Developed-By", "Eng Aya Saado");
        context.Response.Headers.Add("X-Version", "1.0.0");
        await next.Invoke();
    }
);

app.UseStaticFiles("/wwwroot");
app.MapControllers();

app.MapHub<CartHub>("/CartHub");

await SeedData.Seed(app);
app.Run();
