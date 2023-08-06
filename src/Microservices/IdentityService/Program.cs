using Common.Extensions;

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
builder.Services.AddScoped<ICredentialService, CredentialService>();
builder.Services.AddRabbitMq();

builder.Services.AddScoped<IJwtService, JwtService>(_ => new JwtService(
    jwtKey: jwtKey,
    jwtIssuer: jwtIssuer,
    jwtAudience: jwtAudience));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapApiEndpoints();

app.Run();
