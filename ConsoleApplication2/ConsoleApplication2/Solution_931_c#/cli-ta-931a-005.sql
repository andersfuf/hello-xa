EXEC SQL 
--:,;.

SELECT model, price, speed FROM PC;

SELECT maker
SELECT *  FROM Product;
SELECT *  FROM laptop;

SELECT model, price, speed FROM PC;
SELECT l.model, speed, ram, hd, screen, price, maker
             FROM Laptop l, Product p
             WHERE l.model = p.model;

SELECT l.model, speed, ram, hd, screen, price, maker
FROM Laptop l, Product p
WHERE l.model = p.model
AND price <= 4000 
AND speed >= 1.2 
AND ram >= 512
AND hd >= 80
AND screen >= 15.3
;

SELECT l.model, speed, ram, hd, screen, price, maker
FROM Laptop l, Product p
WHERE l.model = p.model
AND p.model = p.model
;

SELECT pc.model, speed, ram, hd, price, maker
FROM pc pc, Product p
WHERE pc.model = p.model
AND maker = 'E'
AND p.model = p.model
AND price <= 4000 
AND speed >= 1.2 
AND ram >= 512
AND hd >= 80
;

select * from product where maker = 'G';
begin transaction;
INSERT INTO product (maker, model, type) VALUES ('G',2030,'pc');
select * from product where maker = 'G';
rollback;

SELECT * FROM PC WHERE model IN (SELECT model FROM Product WHERE type ='pc');

SELECT model FROM Product  WHERE type = 'pc';

SELECT * FROM Laptop WHERE model IN (SELECT model FROM Product WHERE type = 'laptop');
SELECT * FROM Printer WHERE model IN (SELECT model FROM Product WHERE type = 'printer');

SELECT model, price FROM PC  ORDER BY price;
SELECT model, price FROM Printer  ORDER BY price;
SELECT COUNT(*) FROM PC;
 INSERT INTO Product VALUES('F', 2011, 'laptop');
 INSERT INTO PC VALUES(2011, 2.08, 8096, 2048, 2000);
 SELECT *  FROM Product;SELECT *  FROM pc;

                            -- WHERE maker = ? AND

--             INTO :manf
             FROM Product
--             WHERE model = :modelOfClosest;

select tablename from pg_catalog.pg_tables
where tablename like 'p%';
show tables;