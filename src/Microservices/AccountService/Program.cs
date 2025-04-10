using AccountService.Data;
using Common.Extensions;
using Common.Interfaces;
using Common.Middlewares;
using Common.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ------------ Start Parse Configuration ---------------- //
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var jwtKey = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value!);
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value!;
var jwtAudience = builder.Configuration.GetSection("Jwt:Audience").Value!;
// ------------ End Parse Configuration ------------------ //

builder.Logging.AddCustomLogging();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddScoped<ITimeFactory, TimeFactory>();
builder.Services.AddRabbitMq();
builder.Services.AddCustomEndpoints(typeof(Program).Assembly);

builder.Services.AddAuthorization();
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
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
        };
    });

var app = builder.Build();

app.UseAutoMigrations();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapCustomEndpoints();

app.Run();