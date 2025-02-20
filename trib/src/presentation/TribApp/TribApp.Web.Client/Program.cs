using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TribApp.Shared.Services;
using TribApp.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the TribApp.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
