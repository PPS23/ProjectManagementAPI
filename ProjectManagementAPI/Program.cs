using EFCoreAPI.Extensions;
using EFCoreAPI.Repositories;
using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Services;
using EFCoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

AppSettingsGlobal.ConnectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
AppSettingsGlobal.CORS = builder.Configuration["CORS"];
AppSettingsGlobal.JWT = builder.Configuration.GetSection("JWT").Get<JWT>();

// Add services to the container.
// Definir política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS_Config", policy =>
    {
        policy
            .WithOrigins(AppSettingsGlobal.CORS)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//JWT
var key = Encoding.UTF8.GetBytes(AppSettingsGlobal.JWT.Key);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = AppSettingsGlobal.JWT.Issuer,
        ValidAudience = AppSettingsGlobal.JWT.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };  
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CORS_Config");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
