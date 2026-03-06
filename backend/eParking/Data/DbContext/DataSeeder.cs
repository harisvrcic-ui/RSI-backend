using eParking.Data.Models;
using eParking.Helper;
using Microsoft.EntityFrameworkCore;
using System;

namespace eParking.Data
{
    public static class DataSeeder
    {
        private const string DefaultPhoneNumber = "+387 00 000 000";

        /// <summary>
        /// Reads image from disk only if file exists; otherwise returns null (avoids FileNotFoundException during seed).
        /// </summary>
        private static byte[]? SafeGetImageBytes(string folder, string imageName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string imagePath = Path.Combine(currentDirectory, folder, imageName);
            if (!File.Exists(imagePath))
                return null;
            try
            {
                return File.ReadAllBytes(imagePath);
            }
            catch
            {
                return null;
            }
        }

        public static void SeedData(this ModelBuilder modelBuilder)
        {
            // Use a fixed date for all timestamps
            var fixedDate = new DateTime(2026, 11, 1, 0, 0, 0, DateTimeKind.Utc);

            // Generate password hashes and salts for seeded users
            string adminPassword = "admin";
            string userPassword = "user";
            string amarPassword = "amar";
            string harisPassword = "haris";

            string adminSalt = PasswordGenerator.GenerateSalt();
            string userSalt = PasswordGenerator.GenerateSalt();
            string amarSalt = PasswordGenerator.GenerateSalt();
            string harisSalt = PasswordGenerator.GenerateSalt();

            string adminHash = PasswordGenerator.GenerateHash(adminSalt, adminPassword);
            string userHash = PasswordGenerator.GenerateHash(userSalt, userPassword);
            string amarHash = PasswordGenerator.GenerateHash(amarSalt, amarPassword);
            string harisHash = PasswordGenerator.GenerateHash(harisSalt, harisPassword);

            // Seed Users
            modelBuilder.Entity<MyAppUser>().HasData(
                new MyAppUser
                {
                    ID = 1,
                    FirstName = "Denis",
                    LastName = "Mušić",
                    Email = "example1@gmail.com",
                    Username = "admin",
                    PasswordHash = adminHash,
                    PasswordSalt = adminSalt,
                    IsActive = true,
                    CreatedAt = fixedDate,
                    PhoneNumber = DefaultPhoneNumber,
                    GenderId = 1, // Male
                    CityId = 2, // Mostar
                    IsAdmin = true,
                    IsUser = false,
                   
                },
                new MyAppUser
                {
                    ID = 2,
                    FirstName = "Adil",
                    LastName = "Joldić",
                    Email = "example2@gmail.com",
                    Username = "user",
                    PasswordHash = userHash,
                    PasswordSalt = userSalt,
                    IsActive = true,
                    CreatedAt = fixedDate,
                    PhoneNumber = DefaultPhoneNumber,
                    GenderId = 1, // Male
                    CityId = 2, // Mostar
                    IsAdmin = false,
                    IsUser = true,
                },
                new MyAppUser
                {
                    ID = 3,
                    FirstName = "Amar",
                    LastName = "Ramadanovic",
                    Email = "amar.ramadanovic@edu.fit.com",
                    Username = "amar",
                    PasswordHash = amarHash,
                    PasswordSalt = amarSalt,
                    IsActive = true,
                    CreatedAt = fixedDate,
                    PhoneNumber = DefaultPhoneNumber,
                    GenderId = 1, // male
                    CityId = 1, // Sarajevo
                    IsAdmin = true,
                    IsUser = false,
                },
                new MyAppUser
                {
                    ID = 4,
                    FirstName = "Haris",
                    LastName = "Vrcic",
                    Email = "haris.vrcic@edu.fit.com",
                    Username = "haris",
                    PasswordHash = harisHash,
                    PasswordSalt = harisSalt,
                    IsActive = true,
                    CreatedAt = fixedDate,
                    PhoneNumber = DefaultPhoneNumber,
                    GenderId = 1, // male
                    CityId = 2, // Sarajevo
                    IsAdmin = false,
                    IsUser = true,
                }

            );


            // Seed Genders
            modelBuilder.Entity<Gender>().HasData(
                new Gender { ID = 1, Name = "Male" },
                new Gender { ID = 2, Name = "Female" }
            );

            // Seed Countries (Festival-focused countries)
            modelBuilder.Entity<Country>().HasData(
                new Country { ID = 1, Name = "Bosnia and Herzegovina" },
                new Country { ID = 2, Name = "France" },
                new Country { ID = 3, Name = "Germany" },
                new Country { ID = 4, Name = "Italy" },
                new Country { ID = 5, Name = "Norway" },
                new Country { ID = 6, Name = "Portugal" },
                new Country { ID = 7, Name = "Spain" },
                new Country { ID = 8, Name = "Turkey" },
                new Country { ID = 9, Name = "United Kingdom" },
                new Country { ID = 10, Name = "United States" }
            );

            // Seed Cities (5 festival cities per country)
            modelBuilder.Entity<City>().HasData(
                // Bosnia and Herzegovina (ID: 1)
                new City { ID = 1, Name = "Sarajevo", CountryId = 1 },
                new City { ID = 2, Name = "Mostar", CountryId = 1 },
                new City { ID = 3, Name = "Banja Luka", CountryId = 1 },
                new City { ID = 4, Name = "Jajce", CountryId = 1 },
                new City { ID = 5, Name = "Trebinje", CountryId = 1 },

                // France (ID: 2)
                new City { ID = 6, Name = "Paris", CountryId = 2 },
                new City { ID = 7, Name = "Lyon", CountryId = 2 },
                new City { ID = 8, Name = "Nice", CountryId = 2 },
                new City { ID = 9, Name = "Cannes", CountryId = 2 },
                new City { ID = 10, Name = "Marseille", CountryId = 2 },

                // Germany (ID: 3)
                new City { ID = 11, Name = "Berlin", CountryId = 3 },
                new City { ID = 12, Name = "Munich", CountryId = 3 },
                new City { ID = 13, Name = "Hamburg", CountryId = 3 },
                new City { ID = 14, Name = "Cologne", CountryId = 3 },
                new City { ID = 15, Name = "Frankfurt", CountryId = 3 },

                // Italy (ID: 4)
                new City { ID = 16, Name = "Venice", CountryId = 4 },
                new City { ID = 17, Name = "Rome", CountryId = 4 },
                new City { ID = 18, Name = "Milan", CountryId = 4 },
                new City { ID = 19, Name = "Florence", CountryId = 4 },
                new City { ID = 20, Name = "Turin", CountryId = 4 },

                // Norway (ID: 5)
                new City { ID = 21, Name = "Oslo", CountryId = 5 },
                new City { ID = 22, Name = "Bergen", CountryId = 5 },
                new City { ID = 23, Name = "Tromsø", CountryId = 5 },
                new City { ID = 24, Name = "Stavanger", CountryId = 5 },
                new City { ID = 25, Name = "Trondheim", CountryId = 5 },

                // Portugal (ID: 6)
                new City { ID = 26, Name = "Lisbon", CountryId = 6 },
                new City { ID = 27, Name = "Porto", CountryId = 6 },
                new City { ID = 28, Name = "Faro", CountryId = 6 },
                new City { ID = 29, Name = "Coimbra", CountryId = 6 },
                new City { ID = 30, Name = "Braga", CountryId = 6 },

                // Spain (ID: 7)
                new City { ID = 31, Name = "Barcelona", CountryId = 7 },
                new City { ID = 32, Name = "Madrid", CountryId = 7 },
                new City { ID = 33, Name = "Seville", CountryId = 7 },
                new City { ID = 34, Name = "Valencia", CountryId = 7 },
                new City { ID = 35, Name = "Bilbao", CountryId = 7 },

                // Turkey (ID: 8)
                new City { ID = 36, Name = "Istanbul", CountryId = 8 },
                new City { ID = 37, Name = "Ankara", CountryId = 8 },
                new City { ID = 38, Name = "Izmir", CountryId = 8 },
                new City { ID = 39, Name = "Antalya", CountryId = 8 },
                new City { ID = 40, Name = "Bodrum", CountryId = 8 },

                // United Kingdom (ID: 9)
                new City { ID = 41, Name = "London", CountryId = 9 },
                new City { ID = 42, Name = "Edinburgh", CountryId = 9 },
                new City { ID = 43, Name = "Manchester", CountryId = 9 },
                new City { ID = 44, Name = "Bristol", CountryId = 9 },
                new City { ID = 45, Name = "Brighton", CountryId = 9 },

                // United States (ID: 10)
                new City { ID = 46, Name = "New Orleans", CountryId = 10 },
                new City { ID = 47, Name = "Austin", CountryId = 10 },
                new City { ID = 48, Name = "New York", CountryId = 10 },
                new City { ID = 49, Name = "Los Angeles", CountryId = 10 },
                new City { ID = 50, Name = "Miami", CountryId = 10 }
            );


            // Seed Brands 
            modelBuilder.Entity<Brand>().HasData(
                new Brand { ID = 1, Name = "Mercedes-Benz", Logo = SafeGetImageBytes("wwwroot","1.png") },
                new Brand { ID = 2, Name = "BMW", Logo = SafeGetImageBytes("wwwroot","2.png") },
                new Brand { ID = 3, Name = "Volkswagen", Logo = SafeGetImageBytes("wwwroot","3.png") }
            );


            modelBuilder.Entity<ParkingSpotType>().HasData(
              new ParkingSpotType { ID = 1, Name = "Regular", PriceMultiplier = 1.0m },
              new ParkingSpotType { ID = 2, Name = "Disabled", PriceMultiplier = 0.5m },
              new ParkingSpotType { ID = 3, Name = "Compact", PriceMultiplier = 0.9m },
              new ParkingSpotType { ID = 4, Name = "Electric", PriceMultiplier = 1.3m },
              new ParkingSpotType { ID = 5, Name = "Large", PriceMultiplier = 1.2m }
          );

            // In DB: Zone 1 and Zone 2 (Zone 1 = Vijećnica+Baščaršija, Zone 2 = Aria)
            modelBuilder.Entity<ParkingZones>().HasData(
             new ParkingZones { ID = 1, Name = "Zone 1" },
             new ParkingZones { ID = 2, Name = "Zone 2" }
         );
            modelBuilder.Entity<ParkingSpots>().HasData(
             new ParkingSpots { ID = 1, ParkingNumber = 1, ParkingSpotTypeId = 1, ZoneId = 1, DisplayName = "Vijećnica", DisplayNameSearch = "vijecnica" },
             new ParkingSpots { ID = 2, ParkingNumber = 2, ParkingSpotTypeId = 1, ZoneId = 1, DisplayName = "Baščaršija", DisplayNameSearch = "bascarsija" },
             new ParkingSpots { ID = 3, ParkingNumber = 3, ParkingSpotTypeId = 1, ZoneId = 2, DisplayName = "Aria mall", DisplayNameSearch = "aria mall" }
         );
            modelBuilder.Entity<Reservations>().HasData(
             new Reservations
             {
                 ID = 1,
                 CarID = 1,
                 ParkingSpotID = 1,
                 ReservationTypeID = 1,
                 StartDate = new DateTime(2025, 1, 10, 8, 0, 0),
                 EndDate = new DateTime(2025, 1, 10, 10, 0, 0),
                 FinalPrice = 5.00
             }

         );
            modelBuilder.Entity<ReservationType>().HasData(
                  new ReservationType
                  {
                     ID = 1,
                        Name = "Hourly",
                        Price = 5
                  }
         );
            modelBuilder.Entity<Reviews>().HasData(
                  new Reviews
                  {
                      ID = 1,
                      UserId = 1,
                      ReservationId = 1,
                      Rating = 5,
                      Comment = "Parking mjesto je bilo uredno i lako dostupno."
                  }
         );
            modelBuilder.Entity<Cars>().HasData(
                  new Cars
                  {
                      ID = 1,
                      BrandId = 3,
                      ColorId = 1,
                      UserId = 1,
                      Model = "Golf 7",
                      LicensePlate = "E12-K-345",
                      YearOfManufacture = 2018,
                      Picture = SafeGetImageBytes("wwwroot", "golf7.jpg"),
                      IsActive = true
                  },
                  new Cars
                  {
                      ID = 1003,
                      BrandId = 2,
                      ColorId = 1,
                      UserId = 4,
                      Model = "X6",
                      LicensePlate = "021-A-356",
                      YearOfManufacture = 2020,
                      Picture = SafeGetImageBytes("wwwroot", "bmw-x6.jpg"),
                      IsActive = true
                  },
                    new Cars
                    {
                        ID = 1004,
                        BrandId = 3,
                        ColorId = 1,
                        UserId = 2,
                        Model = "Passat 8",
                        LicensePlate = "E11-K-111",
                        YearOfManufacture = 2019,
                        Picture = SafeGetImageBytes("wwwroot", "passat8.jpg"),
                        IsActive = true
                    }
         );
            modelBuilder.Entity<Colors>().HasData(
                  new Colors
                  {
                      ID = 1,
                      Name = "Blue",                                           
                      HexCode = "#0000FF"
                  }
         );
        }
    }
}