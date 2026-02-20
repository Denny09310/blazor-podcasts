using Client.Components;
using Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IPersistentCache, LocalStorageCache>();

builder.Services.AddPulseState(typeof(Program).Assembly);

builder.Services.AddPodcastIndex();

await builder.Build().RunAsync();
