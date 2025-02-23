CREATE DATABASE DbClubAccessSystem;
USE DbClubAccessSystem;


CREATE TABLE Roles (
	RolId INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE TipoClientes (
	TipoClienteId INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);


CREATE TABLE Usuarios (
    UsuarioId INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    RolId INT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (RolId) REFERENCES Roles(RolId)
);

CREATE TABLE Clientes (
    ClienteId INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Contacto VARCHAR(100) NOT NULL,
    TipoCliente INT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (TipoCliente) REFERENCES TipoClientes(TipoClienteId)
);

CREATE TABLE RegistrosAcceso (
    RegistroId INT PRIMARY KEY AUTO_INCREMENT,
    FechaEntrada DATETIME NOT NULL,
    FechaSalida DATETIME,
    ClienteId INT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, 
    FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId)
    
);


-- Insertar Roles
INSERT INTO Roles (Nombre) VALUES ('Administrador');
INSERT INTO Roles (Nombre) VALUES ('Personal');

-- Insertar TipoClientes
INSERT INTO TipoClientes (Nombre) VALUES ('Visitante');
INSERT INTO TipoClientes (Nombre) VALUES ('Miembro');

-- Insertar Usuarios
INSERT INTO Usuarios (Nombre, Email, Password, RolId) 
VALUES ('Admin', 'admin@club.com', 'admin123', 1);

INSERT INTO Usuarios (Nombre, Email, Password, RolId) 
VALUES ('Personal1', 'personal1@club.com', 'personal123', 2);

-- Insertar Clientes
INSERT INTO Clientes (Nombre, Contacto, TipoCliente) 
VALUES ('Juan Perez', 'juan@example.com', 1);

INSERT INTO Clientes (Nombre, Contacto, TipoCliente) 
VALUES ('Maria Gomez', 'maria@example.com', 2);

-- Insertar RegistrosAcceso
INSERT INTO RegistrosAcceso (FechaEntrada, ClienteId) 
VALUES ('2023-10-01 10:00:00', 1);

UPDATE RegistrosAcceso 
SET FechaSalida = '2023-10-01 12:00:00' 
WHERE RegistroId = 1;

INSERT INTO RegistrosAcceso (FechaEntrada, ClienteId) 
VALUES ('2023-10-01 11:00:00', 2);

