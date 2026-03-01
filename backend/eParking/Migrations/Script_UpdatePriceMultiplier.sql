-- Pokreni ovaj skript ručno u SQL Serveru (SSMS / Azure Data Studio) ako su vrijednosti i dalje 0.
-- 1) Promijeni tip kolone u decimal (ako je još uvijek int):
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ParkingSpotTypes' AND COLUMN_NAME = 'PriceMultiplier' AND DATA_TYPE = 'int')
BEGIN
    ALTER TABLE ParkingSpotTypes ALTER COLUMN PriceMultiplier decimal(18,2) NOT NULL;
END
GO

-- 2) Postavi vrijednosti:
--    Regular → 1.0, Disabled → 0.5, Compact → 0.9, Electric → 1.3, Large → 1.2
UPDATE ParkingSpotTypes SET PriceMultiplier = 1.0 WHERE ID = 1;
UPDATE ParkingSpotTypes SET PriceMultiplier = 0.5 WHERE ID = 2;
UPDATE ParkingSpotTypes SET PriceMultiplier = 0.9 WHERE ID = 3;
UPDATE ParkingSpotTypes SET PriceMultiplier = 1.3 WHERE ID = 4;
UPDATE ParkingSpotTypes SET PriceMultiplier = 1.2 WHERE ID = 5;
GO
