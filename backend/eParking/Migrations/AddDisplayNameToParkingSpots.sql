-- Dodaj DisplayName i DisplayNameSearch u ParkingSpots za pretragu (Aria mall, Vijećnica, Baščaršija).
-- Pokreni: sqlcmd -S localhost -d eParking -E -i AddDisplayNameToParkingSpots.sql

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('ParkingSpots') AND name = 'DisplayName')
    ALTER TABLE ParkingSpots ADD DisplayName nvarchar(256) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('ParkingSpots') AND name = 'DisplayNameSearch')
    ALTER TABLE ParkingSpots ADD DisplayNameSearch nvarchar(256) NULL;
GO

UPDATE ParkingSpots SET DisplayName = N'Vijećnica', DisplayNameSearch = N'vijecnica' WHERE ID = 1;
UPDATE ParkingSpots SET DisplayName = N'Baščaršija', DisplayNameSearch = N'bascarsija' WHERE ID = 2;
UPDATE ParkingSpots SET DisplayName = N'Aria mall', DisplayNameSearch = N'aria mall' WHERE ID = 3;
GO
