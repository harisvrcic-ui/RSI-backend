using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eParking.Migrations
{
    public partial class UpdateParkingZonesAndSpotsForFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Zone 1 = Vijećnica, Zone 2 = Baščaršija, Zone 3 = Aria
            migrationBuilder.UpdateData(
                table: "ParkingZones",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "Vijećnica");

            // Insert new zones and spots with explicit IDs (IDENTITY_INSERT required for SQL Server)
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT ParkingZones ON;
                IF NOT EXISTS (SELECT 1 FROM ParkingZones WHERE ID = 2)
                    INSERT INTO ParkingZones (ID, CreatedAt, IsActive, Name, UpdatedAt) VALUES (2, GETUTCDATE(), 1, N'Baščaršija', NULL);
                IF NOT EXISTS (SELECT 1 FROM ParkingZones WHERE ID = 3)
                    INSERT INTO ParkingZones (ID, CreatedAt, IsActive, Name, UpdatedAt) VALUES (3, GETUTCDATE(), 1, N'Aria', NULL);
                SET IDENTITY_INSERT ParkingZones OFF;

                SET IDENTITY_INSERT ParkingSpots ON;
                IF NOT EXISTS (SELECT 1 FROM ParkingSpots WHERE ID = 2)
                    INSERT INTO ParkingSpots (ID, CreatedAt, IsActive, ParkingNumber, ParkingSpotTypeId, UpdatedAt, ZoneId) VALUES (2, GETUTCDATE(), 1, 2, 1, NULL, 2);
                IF NOT EXISTS (SELECT 1 FROM ParkingSpots WHERE ID = 3)
                    INSERT INTO ParkingSpots (ID, CreatedAt, IsActive, ParkingNumber, ParkingSpotTypeId, UpdatedAt, ZoneId) VALUES (3, GETUTCDATE(), 1, 3, 1, NULL, 3);
                SET IDENTITY_INSERT ParkingSpots OFF;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkingZones",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkingZones",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "ParkingZones",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "Nulta zona");
        }
    }
}
