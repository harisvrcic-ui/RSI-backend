using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eParking.Migrations
{
    /// <summary>
    /// Postavlja vrijednosti PriceMultiplier: Regular 1.0, Disabled 0.5, Compact 0.9, Electric 1.3, Large 1.2.
    /// Pokreni: dotnet ef database update
    /// </summary>
    public partial class UpdatePriceMultiplierValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ako kolona još nije decimal (stara migracija nije primijenjena), prvo promijeni tip
            migrationBuilder.Sql(@"
                IF (SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ParkingSpotTypes' AND COLUMN_NAME = 'PriceMultiplier') = 'int'
                BEGIN
                    ALTER TABLE ParkingSpotTypes ALTER COLUMN PriceMultiplier decimal(18,2) NOT NULL;
                END
            ");
            migrationBuilder.Sql("UPDATE ParkingSpotTypes SET PriceMultiplier = 1.0 WHERE ID = 1");
            migrationBuilder.Sql("UPDATE ParkingSpotTypes SET PriceMultiplier = 0.5 WHERE ID = 2");
            migrationBuilder.Sql("UPDATE ParkingSpotTypes SET PriceMultiplier = 0.9 WHERE ID = 3");
            migrationBuilder.Sql("UPDATE ParkingSpotTypes SET PriceMultiplier = 1.3 WHERE ID = 4");
            migrationBuilder.Sql("UPDATE ParkingSpotTypes SET PriceMultiplier = 1.2 WHERE ID = 5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE ParkingSpotTypes SET PriceMultiplier = 0 WHERE ID IN (1, 2, 3, 4, 5)");
        }
    }
}
