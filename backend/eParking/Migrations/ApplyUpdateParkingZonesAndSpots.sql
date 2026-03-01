-- Zona 1 = Vijećnica, Zona 2 = Baščaršija, Zona 3 = Aria
-- Pokreni ovu skriptu na bazi eParking (npr. u SSMS ili sqlcmd) ako ne možeš pokrenuti dotnet ef database update (npr. dok je API upaljen).

BEGIN TRANSACTION;

-- 1. Ažuriraj naziv zone 1
UPDATE ParkingZones SET Name = N'Vijećnica' WHERE ID = 1;

-- 2. Dodaj zone 2 i 3 (ako ne postoje)
SET IDENTITY_INSERT ParkingZones ON;
IF NOT EXISTS (SELECT 1 FROM ParkingZones WHERE ID = 2)
    INSERT INTO ParkingZones (ID, CreatedAt, IsActive, Name, UpdatedAt) VALUES (2, GETUTCDATE(), 1, N'Baščaršija', NULL);
IF NOT EXISTS (SELECT 1 FROM ParkingZones WHERE ID = 3)
    INSERT INTO ParkingZones (ID, CreatedAt, IsActive, Name, UpdatedAt) VALUES (3, GETUTCDATE(), 1, N'Aria', NULL);
SET IDENTITY_INSERT ParkingZones OFF;

-- 3. Dodaj parking mjesta 2 i 3 (ako ne postoje)
SET IDENTITY_INSERT ParkingSpots ON;
IF NOT EXISTS (SELECT 1 FROM ParkingSpots WHERE ID = 2)
    INSERT INTO ParkingSpots (ID, CreatedAt, IsActive, ParkingNumber, ParkingSpotTypeId, UpdatedAt, ZoneId) VALUES (2, GETUTCDATE(), 1, 2, 1, NULL, 2);
IF NOT EXISTS (SELECT 1 FROM ParkingSpots WHERE ID = 3)
    INSERT INTO ParkingSpots (ID, CreatedAt, IsActive, ParkingNumber, ParkingSpotTypeId, UpdatedAt, ZoneId) VALUES (3, GETUTCDATE(), 1, 3, 1, NULL, 3);
SET IDENTITY_INSERT ParkingSpots OFF;

-- 4. Zabilježi migraciju da EF ne pokušava ponovo pokrenuti (ako kasnije radiš dotnet ef database update)
IF NOT EXISTS (SELECT 1 FROM [__EFMigrationsHistory] WHERE MigrationId = N'20260228120000_UpdateParkingZonesAndSpotsForFilter')
    INSERT INTO [__EFMigrationsHistory] (MigrationId, ProductVersion) VALUES (N'20260228120000_UpdateParkingZonesAndSpotsForFilter', N'6.0.9');

COMMIT TRANSACTION;
