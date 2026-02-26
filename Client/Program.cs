using BlazorBlueprint.Components;
using Client.Components;
using Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorBlueprintComponents();
builder.Services.AddScoped<ToastService>();

builder.Services.AddPulseState(typeof(Program).Assembly);

builder.Services.AddHttpClient<PodcastIndexClient>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "podcastindex/");
});

await builder.Build().RunAsync();