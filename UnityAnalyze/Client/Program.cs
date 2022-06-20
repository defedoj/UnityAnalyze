using IgniteUI.Blazor.Controls;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using UnityAnalyze.Client;
using UnityAnalyze.Client.Infrastructure;
using UnityAnalyze.Client.Infrastructure.Providers;
using UnityAnalyze.Client.Infrastructure.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("public",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddHttpClient("UnityAnalyze.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

RegisterServices();


// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("UnityAnalyze.ServerAPI"));
builder.Services.AddScoped(typeof(IIgniteUIBlazor), typeof(IgniteUIBlazor));

builder.Services.AddApiAuthorization();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSyncfusionBlazor();
SyncfusionLicenseProvider.RegisterLicense("NjU0MjY5QDMyMzAyZTMxMmUzMEs1YjV6SzBQU0Q2Tnc2aGZPY0V4dmczOWN3OTFXT3ZRaHNvc3RTUHo0SU09");

await builder.Build().RunAsync();

void RegisterServices()
{
    builder.Services.AddScoped<HttpUserRepository>();
    builder.Services.AddScoped<HttpGameRepository>();
    builder.Services.AddScoped<HttpPlayStoreRepository>();
    builder.Services.AddScoped<HttpMonetizationRepository>();
    builder.Services.AddScoped<ActionStatusService>();
}
