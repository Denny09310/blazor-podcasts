using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOutputCache();

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddPodcastIndexTransform();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseOutputCache();

app.MapReverseProxy()
   .CacheOutput(p => p.Expire(TimeSpan.FromMinutes(5)));

app.MapStaticAssets();
app.MapFallbackToFile("index.html");

app.Run();