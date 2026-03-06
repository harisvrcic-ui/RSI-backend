using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using eParking.Endpoints.AuthEndpoints;
using eParking.Helper.Auth;
using eParking.Services;
using eParking.Data;


var config = new ConfigurationBuilder()
.AddJsonFile("appsettings.json", false)
.Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("db1")));

// Redis distributed cache (IDistributedCache) - Caching with .NET Core and Redis
var redisConnection = config.GetConnectionString("Redis");
if (!string.IsNullOrWhiteSpace(redisConnection))
{
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = redisConnection;
        options.InstanceName = "eParking_";
    });
}
else
{
    builder.Services.AddDistributedMemoryCache(); // fallback when Redis is not configured (e.g. local dev)
}

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.OperationFilter<MyAuthorizationSwaggerHeader>());
builder.Services.AddHttpContextAccessor();

// Register application services
builder.Services.AddTransient<IMyAuthService, MyAuthService>();
builder.Services.AddSingleton<IMyDistributedCacheService, MyDistributedCacheService>();
builder.Services.AddSignalR();

builder.Services.AddFluentValidationAutoValidation();

// Scan all validators from the assembly containing AuthGetEndpoint (any class from this project can be specified)
builder.Services.AddValidatorsFromAssemblyContaining<AuthGetEndpoint>();

var app = builder.Build();

// Apply migrations on startup (e.g. after cloning from Azure DevOps)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try { db.Database.Migrate(); } catch { /* ignore if migrations already applied or DB unavailable */ }
    try { db.Database.ExecuteSqlRaw("UPDATE Cars SET BrandId = 3 WHERE ID = 1 AND Model = N'Golf 7';"); } catch { /* ignore if already updated */ }
    // PriceMultiplier decimal values: Regular 1.0, Disabled 0.5, Compact 0.9, Electric 1.3, Large 1.2
    try
    {
        db.Database.ExecuteSqlRaw(@"
            IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ParkingSpotTypes' AND COLUMN_NAME = 'PriceMultiplier' AND DATA_TYPE = 'int')
                ALTER TABLE ParkingSpotTypes ALTER COLUMN PriceMultiplier decimal(18,2) NOT NULL;
        ");
        db.Database.ExecuteSqlRaw(@"
            UPDATE ParkingSpotTypes SET PriceMultiplier = 1.0 WHERE ID = 1;
            UPDATE ParkingSpotTypes SET PriceMultiplier = 0.5 WHERE ID = 2;
            UPDATE ParkingSpotTypes SET PriceMultiplier = 0.9 WHERE ID = 3;
            UPDATE ParkingSpotTypes SET PriceMultiplier = 1.3 WHERE ID = 4;
            UPDATE ParkingSpotTypes SET PriceMultiplier = 1.2 WHERE ID = 5;
        ");
    }
    catch { /* ignore if already updated */ }

    // Fallback: add DisplayName and DisplayNameSearch if ParkingSpots table exists but columns do not (legacy DB without migrations)
    try
    {
        db.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ParkingSpots' AND COLUMN_NAME = 'DisplayName')
                ALTER TABLE ParkingSpots ADD DisplayName nvarchar(max) NULL;
            IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ParkingSpots' AND COLUMN_NAME = 'DisplayNameSearch')
                ALTER TABLE ParkingSpots ADD DisplayNameSearch nvarchar(max) NULL;
        ");
    }
    catch { /* ignore if table does not exist */ }

    // If user 2 (regular user) has no cars, add one so they can create reservations and see them on the dashboard
    try
    {
        db.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT 1 FROM Cars WHERE UserId = 2)
                INSERT INTO Cars (BrandId, ColorId, UserId, Model, LicensePlate, YearOfManufacture, IsActive, CreatedAt)
                VALUES (3, 1, 2, N'Opel Astra', N'E11-K-111', 2020, 1, GETUTCDATE());
        ");
    }
    catch { /* ignore */ }

}


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(
    options => options
        .SetIsOriginAllowed(x => _ = true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
); //This needs to set everything allowed


app.UseAuthorization();

app.MapControllers();

app.Run();
