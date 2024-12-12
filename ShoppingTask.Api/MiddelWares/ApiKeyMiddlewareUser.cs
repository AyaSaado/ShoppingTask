namespace ShoppingTask.API.MiddelWares;

public class ApiKeyMiddlewareUser(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    private const string APIKEY = "x-api-key";

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key was not provided.");
            return;
        }
        var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = appSettings.GetSection("APIKEY").GetValue<string>("Userkey")!;

        if (!apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized client.");
            return;
        }

        await _next(context);
    }
}
