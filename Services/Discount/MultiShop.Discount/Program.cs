using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Discount.Context;
using MultiShop.Discount.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    // OpenId Çaðýracak yer, biz jwt brearer kiminle kullanacaðýmýz yer.
    //IdentityServerUrl -> appsettingjsondan gelecek.
    opt.Authority = builder.Configuration["IdentityServerUrl"];

    // dinleyici key, token'a hangi sayfalar eriþim saðlayacak.
    opt.Audience = "ResourceDiscount";
    opt.RequireHttpsMetadata = false;
});

builder.Services.AddTransient<DapperContext>();
builder.Services.AddTransient<IDiscountService, DiscountService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
