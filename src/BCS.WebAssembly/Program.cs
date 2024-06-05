using BCS.Infrastructure.Interface;
using BCS.WebAssembly;
using BCS.WebAssembly.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7269") });

builder.Services.AddScoped<IStatus, StatusService>();
builder.Services.AddScoped<IType, TypeService>();
builder.Services.AddScoped<IStructure, StructureService>();
builder.Services.AddScoped<ICity, CityService>();
builder.Services.AddScoped<IStreet, StreetService>();
builder.Services.AddScoped<IComplaint, ComplaintService>();
builder.Services.AddScoped<ISuggestion, SuggestionService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

await builder.Build().RunAsync();
