using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization();

builder.Services.AddOutputCache(options =>
{
    options.AddPodcastIndexPolicy();
});

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
app.UseRequestLocalization();

app.UseOutputCache();

app.MapReverseProxy()
   .CacheOutput("podcastindexPolicy");

app.MapStaticAssets();
app.MapFallbackToFile("index.html");

app.Run();