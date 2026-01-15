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
