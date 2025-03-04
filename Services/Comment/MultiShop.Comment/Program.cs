using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Comment.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    // OpenId Çağıracak yer, biz jwt brearer kiminle kullanacağımız yer.
    //IdentityServerUrl -> appsettingjsondan gelecek.
    opt.Authority = builder.Configuration["IdentityServerUrl"];

    // dinleyici key, token'a hangi sayfalar erişim sağlayacak.
    opt.Audience = "ResourceComment";
    opt.RequireHttpsMetadata = false;
});

// Add services to the container.
builder.Services.AddDbContext<CommentContext>();

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
