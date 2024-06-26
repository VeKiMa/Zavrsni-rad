﻿
--use master;
--go
--drop database if exists webtrgovina;
--go

--create database webtrgovina;
--go
--alter database webtrgovina collate Croatian_CI_AS;
--go
--use webtrgovina;
--go

-- Ovo za produkciju treba
SELECT name, collation_name FROM sys.databases;
GO
-- Doma primjeniti na ime svoje baze 3 puta
ALTER DATABASE db_aa599f_vekima_admin SET SINGLE_USER WITH
ROLLBACK IMMEDIATE;
GO
ALTER DATABASE db_aa599f_vekima_admin COLLATE Croatian_CI_AS;
GO
ALTER DATABASE db_aa599f_vekima_admin SET MULTI_USER;
GO
SELECT name, collation_name FROM sys.databases;
GO





create table kupci(
sifra int not null primary key identity,
ime varchar(50) not null,
prezime varchar(50) not null,
email varchar(100) not null,
adresa varchar(100) not null,
telefon varchar(20) 
);

create table proizvodi(
sifra int not null primary key identity,
naziv varchar(50) not null,
vrsta varchar(50) not null,
cijena decimal(18,2),

);
create table narudzbe(
sifra int not null primary key identity(1,1),
kupac int not null references kupci(sifra),
proizvod int not null references proizvodi(sifra),
datumnarudzbe char,
placanje varchar(50) not null,
ukupaniznos decimal(18,2),
);


insert into kupci(ime,prezime,email,adresa,telefon) values
('Marija','Mihelić','mmiheli14@gmail.com','Opatijska 26b','0989836373'),
('Božić','Petra','bozic.petra35@gmail.com','Vukovarska1','0911234567'),
('Farkaš','Dominik','sinisartf13@gmail.com','Vukovarska1','0911234568'),
('Glavaš','Natalija','natalija-glavas@hotmail.com','Vukovarska1','0911234569'),
('Janić','Miroslav','miroslav.janic@gmail.com','Vukovarska1','0911234560'),
('Janješić','Filip','filip.janjesic@gmail.com','Vukovarska1','0911234589'),
('Jović','Nataša','natasajovic238@gmail.com','Vukovarska1','0911234509');


insert into proizvodi(naziv,vrsta,cijena) values
('katana','elite',130.00),
('katana','tiger',145.00),
('katana','musashi',180.00),
('wakizashi','classic',115.00),
('tanto','light',95.00),
('umjetnina','ulje na platnu',120.00),
('umjetnina','ulje na platnu',115.00),
('umjetnina','pouring',100.00);


