using BrokerApi.Connection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using BrokerApi.Interfaces;
using BrokerApi.Services;
using BrokerApi.Repositories;
using BrokerApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
options.AddPolicy(name: MyAllowSpecificOrigins, build =>
{
    build.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyOrigin();
}));
builder.Services.AddHealthChecks();
// Add interface and class to tell what to use

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();


//Add data context
builder.Services.AddScoped<UserDataContext>();
string conn = builder.Configuration.GetConnectionString("dbmaster");
builder.Services.AddDbContext<UserDataContext>(options => options.UseSqlServer(conn));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UserDataContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddAutoMapper(typeof(Program));

//jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//Google auth
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
