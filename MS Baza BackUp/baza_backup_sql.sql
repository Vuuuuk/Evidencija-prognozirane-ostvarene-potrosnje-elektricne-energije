CREATE TABLE EvidencijaNevalidnihFajlova(
    vreme varchar(255),
    imeFajla varchar(255),
    lokacija varchar(255),
    brojRedova int
);

CREATE TABLE EvidencijaOstvarenePotrosnje(
    vremeUcitavanja varchar(10),
    imeFajla varchar(255),
    lokacija varchar(255),
    sat int,
    load int,
    oblast varchar(10),
    datum date
);

CREATE TABLE EvidencijaPrognoziranePotrosnje(
    vremeUcitavanja varchar(10),
    imeFajla varchar(255),
    lokacija varchar(255),
    sat int,
    load int,
    oblast varchar(10),
    datum date
);

CREATE TABLE EvidencijaGeoPodrucja(
    naziv varchar(10),
    sifra varchar(10)
)