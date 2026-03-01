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



builder.Services.AddControllers();
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

// Jednokratno: Golf 7 -> Volkswagen (BrandId 3)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
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
