using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using eParking.Endpoints.AuthEndpoints;
using eParking.Helper.Auth;
using eParking.Services;
using eParking.Data;
//using Bookhana.SignalR;

var config = new ConfigurationBuilder()
.AddJsonFile("appsettings.json", false)
.Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("db1")));



<<<<<<< HEAD
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase);
=======
builder.Services.AddControllers();
>>>>>>> 9d8f07312ad0d0046110f2fb150f74fa5ef7b7f9
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.OperationFilter<MyAuthorizationSwaggerHeader>());
builder.Services.AddHttpContextAccessor();

//dodajte va�e servise
builder.Services.AddTransient<IMyAuthService, MyAuthService>();
builder.Services.AddSignalR();

builder.Services.AddFluentValidationAutoValidation();

//pretrazuje sve validatore iz DLL fajla (tj. projekta) koji sadr�i AuthGetEndpoint.css
builder.Services.AddValidatorsFromAssemblyContaining<AuthGetEndpoint>();//moze se navesti bilo koja klasa iz ovog projekta
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        policy =>
//        {
//            policy
//                .AllowAnyOrigin()
//                .AllowAnyHeader()
//                .AllowAnyMethod();
//        });
//});


var app = builder.Build();

<<<<<<< HEAD
// Primjena migracija pri startu (npr. nakon kloniranja s Azure DevOps)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try { db.Database.Migrate(); } catch { /* ignoriraj ako migracije već primijenjene ili DB nedostupan */ }

=======
// Jednokratno: Golf 7 -> Volkswagen (BrandId 3)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
>>>>>>> 9d8f07312ad0d0046110f2fb150f74fa5ef7b7f9
    try { db.Database.ExecuteSqlRaw("UPDATE Cars SET BrandId = 3 WHERE ID = 1 AND Model = N'Golf 7';"); } catch { /* ignoriraj ako već ažurirano */ }
    // PriceMultiplier decimal + vrijednosti: Regular 1.0, Disabled 0.5, Compact 0.9, Electric 1.3, Large 1.2
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
    catch { /* ignoriraj ako već ažurirano */ }
<<<<<<< HEAD

    // Fallback: dodaj DisplayName i DisplayNameSearch ako tablica ParkingSpots postoji ali kolone ne (stara baza bez migracija)
    try
    {
        db.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ParkingSpots' AND COLUMN_NAME = 'DisplayName')
                ALTER TABLE ParkingSpots ADD DisplayName nvarchar(max) NULL;
            IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ParkingSpots' AND COLUMN_NAME = 'DisplayNameSearch')
                ALTER TABLE ParkingSpots ADD DisplayNameSearch nvarchar(max) NULL;
        ");
    }
    catch { /* ignoriraj ako tablica ne postoji */ }

    // Ako user 2 (obični user) nema nijedan auto, dodaj jedno da može kreirati rezervacije i vidjeti ih na dashboardu
    try
    {
        db.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT 1 FROM Cars WHERE UserId = 2)
                INSERT INTO Cars (BrandId, ColorId, UserId, Model, LicensePlate, YearOfManufacture, IsActive, CreatedAt)
                VALUES (3, 1, 2, N'Opel Astra', N'E11-K-111', 2020, 1, GETUTCDATE());
        ");
    }
    catch { /* ignoriraj */ }

    // Chevrolet brand i auto za Harisa (UserId 4): Chevrolet Aveo, tablice 021-A-356, 2008
    try
    {
        db.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT 1 FROM Brands WHERE Name = N'Chevrolet')
                INSERT INTO Brands (Name, IsActive, CreatedAt) VALUES (N'Chevrolet', 1, GETUTCDATE());
            IF NOT EXISTS (SELECT 1 FROM Cars WHERE UserId = 4 AND LicensePlate = N'021-A-356')
                INSERT INTO Cars (BrandId, ColorId, UserId, Model, LicensePlate, YearOfManufacture, IsActive, CreatedAt)
                SELECT b.ID, 1, 4, N'Aveo', N'021-A-356', 2008, 1, GETUTCDATE()
                FROM Brands b WHERE b.Name = N'Chevrolet';
        ");
    }
    catch { /* ignoriraj */ }
=======
>>>>>>> 9d8f07312ad0d0046110f2fb150f74fa5ef7b7f9
}

//app.UseAuthentication();

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
//app.MapHub<MySignalrHub>("/mysginalr-hub-path");

app.Run();
