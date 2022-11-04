using Admin.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
    "NjA5NDU3QDMyMzAyZTMxMmUzME1aemRzVXdYRzlDd3AvYlFYRHdzSGtQNFFodDdaWCtZbGE1UHVMTWJkWEk9");
builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

await builder.Build().RunAsync();
