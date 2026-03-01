-- Preimenuj zone u bazi u "Zone 1" i "Zone 2"; ostavi samo 2 zone (ukloni treću).
-- Pokreni na bazi eParking (npr. sqlcmd -S localhost -d eParking -E -i RenameZonesToZone1Zone2.sql)

BEGIN TRANSACTION;

-- 1. Prvo prebaci parking mjesta: spot 2 u zonu 1, spot 3 u zonu 2
UPDATE ParkingSpots SET ZoneId = 1 WHERE ID = 2;
UPDATE ParkingSpots SET ZoneId = 2 WHERE ID = 3;

-- 2. Preimenuj zone u "Zone 1" i "Zone 2"
UPDATE ParkingZones SET Name = N'Zone 1' WHERE ID = 1;
UPDATE ParkingZones SET Name = N'Zone 2' WHERE ID = 2;

-- 3. Obriši treću zonu (ako postoji)
DELETE FROM ParkingZones WHERE ID = 3;

COMMIT TRANSACTION;
