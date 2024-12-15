using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer("OcelotAuthenticationScheme", opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "ResourceOcelot";
    opt.RequireHttpsMetadata = false;
});

//ocelot.json için registration iþlemleri.
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
builder.Services.AddOcelot(configuration);

var app = builder.Build();

await app.UseOcelot(); // bu da var.


app.MapGet("/", () => "Hello World!");

app.Run();
