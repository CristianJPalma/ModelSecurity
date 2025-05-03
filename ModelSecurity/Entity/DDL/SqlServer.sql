CREATE TABLE Person (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(20) NOT NULL,
    LastName NVARCHAR(20) NOT NULL,
    Email NVARCHAR(50) NOT NULL
);

CREATE TABLE [User] (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(20) NOT NULL,
    Password NVARCHAR(255) NOT NULL, -- Contraseña agregada
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Active BIT NOT NULL DEFAULT 1,
    CreateAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    DeleteAt DATETIME2 NULL,
    PersonId INT NOT NULL,
    FOREIGN KEY (PersonId) REFERENCES Person(Id) ON DELETE CASCADE
);

CREATE TABLE Rol (
    Id INT PRIMARY KEY IDENTITY(1,1),
    [Description] NVARCHAR(100) NOT NULL,
    [Name] NVARCHAR(20) NOT NULL,
    Active BIT NOT NULL DEFAULT 1,
    CreateAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    DeleteAt DATETIME2 NULL
);

CREATE TABLE Permission (
    Id INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(20) NOT NULL,
    Active BIT NOT NULL DEFAULT 1,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    CreateAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    DeleteAt DATETIME2 NULL
);

CREATE TABLE Form (
    Id INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(20) NOT NULL,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Active BIT NOT NULL DEFAULT 1,
    CreateAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    DeleteAt DATETIME2 NULL
);

CREATE TABLE Module (
    Id INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(20) NOT NULL,
    Active BIT NOT NULL DEFAULT 1,
    CreateAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    DeleteAt DATETIME2 NULL
);

CREATE TABLE RolUser (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    RolId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES [User](Id) ON DELETE CASCADE,
    FOREIGN KEY (RolId) REFERENCES Rol(Id) ON DELETE CASCADE
);

CREATE TABLE RolFormPermission (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RolId INT NOT NULL,
    PermissionId INT NOT NULL,
    FormId INT NOT NULL,
    FOREIGN KEY (RolId) REFERENCES Rol(Id) ON DELETE CASCADE,
    FOREIGN KEY (PermissionId) REFERENCES Permission(Id) ON DELETE CASCADE,
    FOREIGN KEY (FormId) REFERENCES Form(Id) ON DELETE CASCADE
);

CREATE TABLE FormModule (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ModuleId INT NOT NULL,
    FormId INT NOT NULL,
    FOREIGN KEY (ModuleId) REFERENCES Module(Id) ON DELETE CASCADE,
    FOREIGN KEY (FormId) REFERENCES Form(Id) ON DELETE CASCADE
);

-- ROL ADMIN
INSERT INTO Rol ([Description], [Name], Active) 
VALUES ('Administrador del sistema con todos los privilegios', 'Admin', 1);

-- PERMISOS BÁSICOS
INSERT INTO Permission ([Name], Code, Active) VALUES ('Agregar', 'PERM_ADD', 1);
INSERT INTO Permission ([Name], Code, Active) VALUES ('Editar', 'PERM_EDIT', 1);
INSERT INTO Permission ([Name], Code, Active) VALUES ('Eliminar', 'PERM_DELETE', 1);
INSERT INTO Permission ([Name], Code, Active) VALUES ('Desactivar', 'PERM_DEACTIVATE', 1);

-- MÓDULOS Y FORMULARIOS POR ENTIDAD
INSERT INTO Module ([Name], Active) VALUES ('Seguridad', 1);
INSERT INTO Module ([Name], Active) VALUES ('Gestión de Usuarios', 1);

-- Formularios (uno por entidad)
INSERT INTO Form ([Name], Code, Active) VALUES ('Formulario Persona', 'FORM_PERSON', 1);
INSERT INTO Form ([Name], Code, Active) VALUES ('Formulario Usuario', 'FORM_USER', 1);
INSERT INTO Form ([Name], Code, Active) VALUES ('Formulario Rol', 'FORM_ROL', 1);
INSERT INTO Form ([Name], Code, Active) VALUES ('Formulario Permiso', 'FORM_PERMISSION', 1);
INSERT INTO Form ([Name], Code, Active) VALUES ('Formulario Módulo', 'FORM_MODULE', 1);
INSERT INTO Form ([Name], Code, Active) VALUES ('Formulario RolUsuario', 'FORM_ROLUSER', 1);
INSERT INTO Form ([Name], Code, Active) VALUES ('Formulario RolPermiso', 'FORM_ROLFORMPERM', 1);

-- ASOCIAR FORMULARIOS A MÓDULOS
-- Seguridad
INSERT INTO FormModule (ModuleId, FormId) VALUES (1, 1); -- Persona
INSERT INTO FormModule (ModuleId, FormId) VALUES (1, 2); -- Usuario
INSERT INTO FormModule (ModuleId, FormId) VALUES (1, 3); -- Rol
INSERT INTO FormModule (ModuleId, FormId) VALUES (1, 4); -- Permiso
INSERT INTO FormModule (ModuleId, FormId) VALUES (1, 5); -- Módulo
INSERT INTO FormModule (ModuleId, FormId) VALUES (1, 6); -- RolUser
INSERT INTO FormModule (ModuleId, FormId) VALUES (1, 7); -- RolFormPermission

-- ASOCIAR TODOS LOS PERMISOS A TODOS LOS FORMULARIOS PARA ADMIN
-- Admin tiene ID = 1
-- Permisos tienen ID = 1 a 4
-- Formularios tienen ID = 1 a 7

DECLARE @rolId INT = 1;

-- Loop para asignar permisos a cada formulario
DECLARE @formId INT = 1;
WHILE @formId <= 7
BEGIN
    -- Agregar
    INSERT INTO RolFormPermission (RolId, PermissionId, FormId) VALUES (@rolId, 1, @formId);
    -- Editar
    INSERT INTO RolFormPermission (RolId, PermissionId, FormId) VALUES (@rolId, 2, @formId);
    -- Eliminar
    INSERT INTO RolFormPermission (RolId, PermissionId, FormId) VALUES (@rolId, 3, @formId);
    -- Desactivar
    INSERT INTO RolFormPermission (RolId, PermissionId, FormId) VALUES (@rolId, 4, @formId);
    SET @formId += 1;
END;
