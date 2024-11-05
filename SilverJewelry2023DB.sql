USE master
GO

CREATE DATABASE SilverJewelry2023DB
GO

USE SilverJewelry2023DB
GO

CREATE TABLE BranchAccount(
  AccountID int primary key,
  AccountPassword nvarchar(40) not null,
  FullName nvarchar(60) not null,
  EmailAddress nvarchar(60) unique, 
  Role int
)
GO

INSERT INTO BranchAccount VALUES(55 ,N'@5', N'Branch Administrator', 'admin@SilverJewelry.com.uk', 1);
INSERT INTO BranchAccount VALUES(56 ,N'@5', N'Branch Staff', 'staff@SilverJewelry.com.uk', 2);
INSERT INTO BranchAccount VALUES(57 ,N'@5', N'Branch Manager', 'manager@SilverJewelry.com.uk', 3);
INSERT INTO BranchAccount VALUES(58 ,N'@5', N'Branch Customer', 'customer@SilverJewelry.com.uk', 4);
GO


CREATE TABLE Category(
  CategoryId nvarchar(30) primary key,
  CategoryName nvarchar(100) not null,
  CategoryDescription nvarchar(250) not null, 
  FromCountry nvarchar(160)
)
GO
INSERT INTO Category VALUES(N'CC0046', N'Earrings', N'An earring is a piece of jewelry attached to the ear via a piercing in the earlobe or another external part of the ear (except in the case of clip earrings, which clip onto the lobe). ', N'United States')
GO
INSERT INTO Category VALUES(N'CC0047', N'Pendant', N'A pendant is a loose-hanging piece of jewellery, generally attached by a small loop to a necklace, which may be known as a "pendant necklace".', N'Japan')
GO
INSERT INTO Category VALUES(N'CC0048', N'Bracelet', N'A bracelet is an article of jewellery that is worn around the wrist. Bracelets may serve different uses, such as being worn as an ornament. ', N'United States')
GO
INSERT INTO Category VALUES(N'CC0049', N'Necklace', N'A necklace is an article of jewellery that is worn around the neck. Necklaces may have been one of the earliest types of adornment worn by humans.', N'Viet Nam')
GO



CREATE TABLE SilverJewelry(
 SilverJewelryId nvarchar(200) primary key,
 SilverJewelryName nvarchar(100) not null,
 SilverJewelryDescription nvarchar(250),
 MetalWeight decimal,
 Price decimal,
 ProductionYear int, 
 CreatedDate Datetime,
 CategoryId nvarchar(30) references Category(CategoryId) on delete cascade on update cascade
)
GO

INSERT INTO SilverJewelry VALUES(N'SBD2HE92287.600', N'Silver earrings with smoky quartz', N'Stones Content: Smoky quartz 3*6mmx2, Cz 1.0mmx22', 1.85, 189, 2023, CAST(N'2023-10-16' AS DateTime), 'CC0046')
GO
INSERT INTO SilverJewelry VALUES(N'SMD2HE92286.600', N'Silver pendant with Smoky quartz', N'Stones Content: Smoky quartz 4*8mmx1, Cz 1.25mmx12', 1.85, 934, 2022, CAST(N'2023-10-16' AS DateTime), 'CC0047')
GO
INSERT INTO SilverJewelry VALUES(N'SBD2HE92292.600', N'Silver earrings with Smoky quartz', N'Smoky quartz 3*6mmx2, cz 1.0mmx52', 2.07, 223, 2022, CAST(N'2023-10-16' AS DateTime), 'CC0046')
GO
INSERT INTO SilverJewelry VALUES(N'SND2KE13990.600', N'Silver ring made with Swarovski scarabaeus green crystal pearl', N'Stone content: Swarovski 1.25x14; Swarovski crystal pearl scarabaeus green 8.0mmx1', 2.51, 654, 2021, CAST(N'2023-10-16' AS DateTime), 'CC0049')
GO
INSERT INTO SilverJewelry VALUES(N'SMD2XE14003.600', N'Rose tone silver pendant with crystal pearls', N'Stone: Swarovski 1.25x14; Crystal pearl bordeaux 4.0x1, rosaline 3.0x1, white 3.0x1', 1.39, 140, 2022, CAST(N'2023-10-16' AS DateTime), 'CC0046')
GO
INSERT INTO SilverJewelry VALUES(N'SBD2KE11364.100', N'Silver Earrings with CZ', N'Stone Breakdown: Cz 1.0x98', 7.07, 140, 2023, CAST(N'2023-10-16' AS DateTime), 'CC0046')
GO
INSERT INTO SilverJewelry VALUES(N'SMD2KE11369.100', N'Silver Pendant with CZ', N'Stone Breakdown: Cz 1.0x76', 6.04, 180, 2020, CAST(N'2023-10-16' AS DateTime), 'CC0047')
GO
INSERT INTO SilverJewelry VALUES(N'SBD2CE13896.000', N'Two-tone silver earrings', N'Color: White and Yellow', 4.43, 167, 2021, CAST(N'2023-10-16' AS DateTime), 'CC0046')
GO
INSERT INTO SilverJewelry VALUES(N'SND2KE11375.100', N'Silver Ring with CZ', N'Stone Breakdown: Cz 1.5x4', 5.80, 112, 2022, CAST(N'2023-10-16' AS DateTime), 'CC0049')
GO

