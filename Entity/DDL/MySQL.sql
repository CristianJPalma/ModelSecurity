CREATE TABLE Person (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(20) NOT NULL,
    LastName VARCHAR(20) NOT NULL,
    Email VARCHAR(29) NOT NULL
);

CREATE TABLE `User` (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserName VARCHAR(20) NOT NULL,
    Code VARCHAR(50) NOT NULL UNIQUE,
    Active BOOLEAN NOT NULL DEFAULT TRUE,
    CreateAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DeleteAt DATETIME DEFAULT NULL,
    PersonId INT NOT NULL,
    FOREIGN KEY (PersonId) REFERENCES Person(Id)
);

CREATE TABLE Rol (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    `Description` VARCHAR(100) NOT NULL,
    `Name` VARCHAR(20) NOT NULL,
    Active BOOLEAN NOT NULL DEFAULT TRUE,
    CreateAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DeleteAt DATETIME DEFAULT NULL
);

CREATE TABLE Permission (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    `Name` VARCHAR(20) NOT NULL,
    Active BOOLEAN NOT NULL DEFAULT TRUE,
    Code VARCHAR(50) NOT NULL UNIQUE,
    CreateAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DeleteAt DATETIME DEFAULT NULL
);

CREATE TABLE Form (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    `Name` VARCHAR(20) NOT NULL,
    Code VARCHAR(50) NOT NULL UNIQUE,
    Active BOOLEAN NOT NULL DEFAULT TRUE,
    CreateAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DeleteAt DATETIME DEFAULT NULL
);

CREATE TABLE Module (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    `Name` VARCHAR(20) NOT NULL,
    Active BOOLEAN NOT NULL DEFAULT TRUE,
    CreateAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DeleteAt DATETIME DEFAULT NULL
);

CREATE TABLE RoleUser (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    RolId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES `User`(Id),
    FOREIGN KEY (RolId) REFERENCES Rol(Id)
);

CREATE TABLE RoleFormPermission (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    RolId INT NOT NULL,
    PermissionId INT NOT NULL,
    FormId INT NOT NULL,
    FOREIGN KEY (RolId) REFERENCES Rol(Id),
    FOREIGN KEY (PermissionId) REFERENCES Permission(Id),
    FOREIGN KEY (FormId) REFERENCES Form(Id)
);

CREATE TABLE FormModule (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ModuleId INT NOT NULL,
    FormId INT NOT NULL,
    FOREIGN KEY (ModuleId) REFERENCES Module(Id),
    FOREIGN KEY (FormId) REFERENCES Form(Id)
);
