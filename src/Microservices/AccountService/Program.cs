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

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapApiEndpoints();

app.Run();