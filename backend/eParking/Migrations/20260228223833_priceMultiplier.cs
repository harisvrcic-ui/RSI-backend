using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eParking.Migrations
{
    public partial class priceMultiplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add columns only if they do not exist (may already exist from manual migration)
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ParkingSpots' AND COLUMN_NAME = 'DisplayName')
                    ALTER TABLE ParkingSpots ADD DisplayName nvarchar(max) NULL;
                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ParkingSpots' AND COLUMN_NAME = 'DisplayNameSearch')
                    ALTER TABLE ParkingSpots ADD DisplayNameSearch nvarchar(max) NULL;
            ");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(4004));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(6605));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(7834));

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "BrandId", "CreatedAt" },
                values: new object[] { 3, new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(9092) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3932));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3935));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3936));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3936));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3937));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3938));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3938));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3939));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3940));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3940));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3941));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3942));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3942));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3943));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3944));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3944));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3945));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3945));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3946));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3947));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3947));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3948));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3949));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3949));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 26,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3951));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 27,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3951));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 28,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3952));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 29,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3952));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 30,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3953));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 31,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3954));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 32,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3954));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 33,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3955));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 34,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3956));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 35,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3956));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 36,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3957));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 37,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3957));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 38,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3958));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 39,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3959));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 40,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3959));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 41,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3960));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 42,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3961));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 43,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3961));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 44,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3962));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 45,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3962));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 46,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3963));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 47,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3964));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 48,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3964));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 49,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3965));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 50,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3966));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 882, DateTimeKind.Utc).AddTicks(944));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3838));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3839));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3841));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3841));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3842));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3843));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3843));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3844));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3844));

            migrationBuilder.UpdateData(
                table: "Genders",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3811));

            migrationBuilder.UpdateData(
                table: "Genders",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(3812));

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "GuCrQH5+a9TKbaW13XYG+KMGVbEJva5IFC5WzbynaDM=", "TmyJj/Sj5KSoEmnDq9eR+omw1v1aPg7alcSAq/uoCrU=" });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "HGws0ZkyLh9FgEYbYHVgJiF41ufZxmzpvbwm01hY4L8=", "8tdj5iUs2O1lx+8QLn81r+hEozlXZmWyCHwO9F5Etew=" });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "cCq03sau1ZPyU1Ml2vHd7E+9s0LqmwsJyAbh0oIFFWo=", "eCj3fV8BD+L5OZcBjDShsuiEMV/O8cRXbm9hAUWPdwk=" });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "V1JdGeeSB8oD7JRvYZnQGicNAAmI2BwJYnRA2CP8xz0=", "fbEXp5p6zrluZXA5jwub0+1+uN+g2acsGOqwrFZYOyk=" });

            migrationBuilder.UpdateData(
                table: "ParkingSpotTypes",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "ParkingSpotTypes",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(8920));

            migrationBuilder.UpdateData(
                table: "ParkingSpotTypes",
                keyColumn: "ID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(8922));

            migrationBuilder.UpdateData(
                table: "ParkingSpotTypes",
                keyColumn: "ID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(8923));

            migrationBuilder.UpdateData(
                table: "ParkingSpotTypes",
                keyColumn: "ID",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(8924));

            migrationBuilder.UpdateData(
                table: "ParkingSpots",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DisplayName", "DisplayNameSearch" },
                values: new object[] { new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(8987), "Vijećnica", "vijecnica" });

<<<<<<< HEAD
            // Insert ParkingSpots 2 and 3 only if they do not exist (explicit ID requires IDENTITY_INSERT ON)
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT ParkingSpots ON;
=======
            // Insert ParkingSpots 2 and 3 only if they do not exist (may exist from earlier migration)
            migrationBuilder.Sql(@"
>>>>>>> 9d8f07312ad0d0046110f2fb150f74fa5ef7b7f9
                IF NOT EXISTS (SELECT 1 FROM ParkingSpots WHERE ID = 2)
                    INSERT INTO ParkingSpots (ID, CreatedAt, DisplayName, DisplayNameSearch, IsActive, ParkingNumber, ParkingSpotTypeId, UpdatedAt, ZoneId)
                    VALUES (2, '2026-02-28T22:38:32.8818991Z', N'Baščaršija', N'bascarsija', 1, 2, 1, NULL, 1);
                IF NOT EXISTS (SELECT 1 FROM ParkingSpots WHERE ID = 3)
                    INSERT INTO ParkingSpots (ID, CreatedAt, DisplayName, DisplayNameSearch, IsActive, ParkingNumber, ParkingSpotTypeId, UpdatedAt, ZoneId)
                    VALUES (3, '2026-02-28T22:38:32.8818993Z', N'Aria mall', N'aria mall', 1, 3, 1, NULL, 2);
<<<<<<< HEAD
                SET IDENTITY_INSERT ParkingSpots OFF;
=======
>>>>>>> 9d8f07312ad0d0046110f2fb150f74fa5ef7b7f9
            ");

            migrationBuilder.UpdateData(
                table: "ParkingZones",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(8958), "Zone 1" });

            // Insert Zone 2 only if it does not exist
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM ParkingZones WHERE ID = 2)
                    INSERT INTO ParkingZones (ID, CreatedAt, IsActive, Name, UpdatedAt) VALUES (2, '2026-02-28T22:38:32.8818959Z', 1, N'Zone 2', NULL);
            ");

            migrationBuilder.UpdateData(
                table: "ReservationTypes",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(9043));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(9018));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 22, 38, 32, 881, DateTimeKind.Utc).AddTicks(9068));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkingZones",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "ParkingSpots");

            migrationBuilder.DropColumn(
                name: "DisplayNameSearch",
                table: "ParkingSpots");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8750));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 476, DateTimeKind.Utc).AddTicks(504));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 476, DateTimeKind.Utc).AddTicks(1296));

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "BrandId", "CreatedAt" },
                values: new object[] { 1, new DateTime(2026, 1, 4, 16, 13, 33, 476, DateTimeKind.Utc).AddTicks(2234) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8659));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8661));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8662));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8663));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8664));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8664));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8665));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8666));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8666));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8667));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8668));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8669));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8669));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8670));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8671));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8671));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8672));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8673));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8673));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8674));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8675));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8675));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8676));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8677));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8677));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 26,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8678));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 27,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8679));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 28,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8679));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 29,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8680));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 30,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8680));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 31,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8681));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 32,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8682));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 33,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8683));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 34,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8683));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 35,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8684));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 36,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8684));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 37,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8685));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 38,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8686));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 39,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8686));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 40,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8687));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 41,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8688));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 42,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8688));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 43,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 44,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8690));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 45,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8690));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 46,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8691));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 47,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8692));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 48,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8692));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 49,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8693));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 50,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8693));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 476, DateTimeKind.Utc).AddTicks(3583));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8630));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8631));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8632));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8632));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8633));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8633));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8634));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8635));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8635));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "ID",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8636));

            migrationBuilder.UpdateData(
                table: "Genders",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8608));

            migrationBuilder.UpdateData(
                table: "Genders",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 475, DateTimeKind.Utc).AddTicks(8610));

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "4ae3IPr+WzL3YCqQWFgARYcsW7KYscFruRHcsEPmlIg=", "FnZnVb0TuH0mYuZWlE4WuYcqhThQQl+QnmBFqogl5nU=" });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "daTn9Xfuhg73xHqfw9XcL9n0XcIKb1yFCgVqZUVnCKQ=", "+TluGd831LQth0CvT7Dl239wiLIXO+lwY5GOI+Ra7s4=" });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "fg/ee3FfjeHvLaT/NyMKzsKl7/M3jd+GPZiA9vL1GgU=", "3L2zD6e6Q3ftu/O1KLHjPnb6lkg0y9LK/T/OSaEg3fM=" });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "jJUweZGa2aN6dKsyKcegadEXPGvHO3SF9PZ0CXJxfGs=", "4p3WtNgIbfnDPDfwwzNwbS17bA5cBE013GPd6ZNqU2k=" });

            migrationBuilder.UpdateData(
                table: "ParkingSpotTypes",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 476, DateTimeKind.Utc).AddTicks(2098));

            migrationBuilder.UpdateData(
                table: "ParkingSpotTypes",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 476, DateTimeKind.Utc).AddTicks(2100));

            migrationBuilder.UpdateData(
                table: "ParkingSpotTypes",
                keyColumn: "ID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 476, DateTimeKind.Utc).AddTicks(2100));

            migrationBuilder.UpdateData(
                table: "ParkingSpotTypes",
                keyColumn: "ID",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 476, DateTimeKind.Utc).AddTicks(2101));

            migrationBuilder.UpdateData(
                table: "ParkingSpotTypes",
                keyColumn: "ID",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 476, DateTimeKind.Utc).AddTicks(2102));

            migrationBuilder.UpdateData(
                table: "ParkingSpots",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 476, DateTimeKind.Utc).AddTicks(2130));

            migrationBuilder.UpdateData(
                table: "ParkingZones",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 1, 4, 16, 13, 33, 476, DateTimeKind.Utc).AddTicks(2154), "Nulta zona" });

            migrationBuilder.UpdateData(
                table: "ReservationTypes",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 476, DateTimeKind.Utc).AddTicks(2199));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 476, DateTimeKind.Utc).AddTicks(2176));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 4, 16, 13, 33, 476, DateTimeKind.Utc).AddTicks(2216));
        }
    }
}
