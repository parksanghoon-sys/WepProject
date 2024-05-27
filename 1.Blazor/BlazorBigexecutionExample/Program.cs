using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorBigexecutionExample;
using Blazored.SessionStorage;
using BlazorBigexecutionExample.Services;
using Microsoft.Extensions.DependencyInjection;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddHttpClient<IUniversityService, UniversityService>(client =>
//{
//    client.BaseAddress = new Uri("http://universities.hipolabs.com");
//});
builder.Services.AddScoped<IUniversityService, UniversityService>();
builder.Services.AddBlazoredSessionStorage();

await builder.Build().RunAsync();
