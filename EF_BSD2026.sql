Create Database  Examen2026
go
use Examen2026
go

Create table tb_Supervisor (
idsup int primary key identity(1,100),
nomsup varchar(40) not null,
dirsup varchar(155) not null,
emailsup varchar(250) not null,
idpais int not null,
dnisup varchar(8) not null UNIQUE
constraint FK_TB_SUPERVISOR_TB_PAIS foreign key (idpais) references tb_Pais(idpais)
)
Create table tb_Pais(
idpais int primary key identity(1,100),
nompais varchar(40) not null
)

INSERT INTO tb_Pais (nompais) VALUES
('Perú'),
('Chile'),
('Argentina'),
('Colombia');


CREATE PROCEDURE usp_agrega_supervisor
    @nomsup VARCHAR(40),
    @dirsup VARCHAR(155),
    @emailsup VARCHAR(250),
    @idpais INT,
    @dnisup CHAR(8)
AS
BEGIN
    INSERT INTO tb_Supervisor(nomsup, dirsup, emailsup, idpais, dnisup)
    VALUES (@nomsup, @dirsup, @emailsup, @idpais, @dnisup)
END
GO

CREATE PROCEDURE usp_eliminar_supervisor
    @idsup INT
AS
BEGIN
    DELETE FROM tb_Supervisor WHERE idsup = @idsup
END
GO


CREATE PROCEDURE usp_listar_supervisor
AS
BEGIN
    SELECT s.idsup, s.nomsup, s.dirsup, s.emailsup, p.nompais, s.dnisup
    FROM tb_Supervisor s
    INNER JOIN tb_Pais p ON s.idpais = p.idpais
END
go


CREATE PROCEDURE usp_listar_paises
AS
BEGIN
    SELECT idpais, nompais FROM tb_Pais
END
go


---pregunta 2-----

CREATE PROCEDURE usp_paises_cantidad_supervisores
AS
BEGIN
    SELECT 
        p.nompais,
        COUNT(s.idsup) AS cantidad_supervisores
    FROM tb_Pais p
    LEFT JOIN tb_Supervisor s 
        ON p.idpais = s.idpais
    GROUP BY p.nompais
END
GO

CREATE PROCEDURE usp_pais_x_dni_supervisor
    @dni CHAR(8)
AS
BEGIN
    SELECT 
        p.nompais
    FROM tb_Supervisor s
    INNER JOIN tb_Pais p 
        ON s.idpais = p.idpais
    WHERE s.dnisup = @dni
END
GO