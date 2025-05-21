create database Hospital_Veterinario
go

create table Usuarios (
    id INT PRIMARY KEY IDENTITY (1,1),
    dni NVARCHAR(50) NOT NULL,
    nombre NVARCHAR(50) NOT NULL,
    apellido NVARCHAR(50) NOT NULL,
    password NVARCHAR(255) NOT NULL,
    email NVARCHAR(50) NOT NULL,
    isBlock BIT DEFAULT 0,
    intentos INT NOT NULL,
    perfil_Id INT NOT NULL,
);
go

create table Perfiles (
    id INT PRIMARY KEY IDENTITY (1,1),
    nombre NVARCHAR(50) NOT NULL,
    tipo NVARCHAR(255) NOT NULL
);
go

create table Eventos (
    id INT PRIMARY KEY IDENTITY (1,1),
    modulo NVARCHAR(50) NOT NULL,
    descripcion NVARCHAR(255) NOT NULL,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    usuario_Id INT NOT NULL,
    criticidad INT NOT NULL,
    FOREIGN KEY (usuario_Id) REFERENCES Usuarios(id)
);

go
insert into Usuarios (Dni, Nombre, Apellido, Password, Email, IsBlock, Intentos, Perfil_Id)
values ('12345678', 'Tomas', 'Almada', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 'tomas@gmail.com', 0, 0, 1),
       ('87654321', 'Lujan', 'Mansilla', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 'lujan@gmail.com', 0, 0, 2),
       ('11223344', 'Lautaro', 'Puchol', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 'lautaro@gmail.com', 0, 0, 3);

go
insert into Perfiles (Nombre, Tipo)
values ('Admin', 'Patente'),
       ('Cliente', 'Patente'),
       ('WebMaster', 'Patente');
