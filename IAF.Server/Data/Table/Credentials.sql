CREATE TABLE Credentials (
    Credential_ID INT PRIMARY KEY IDENTITY(1,1),   
    UserName NVARCHAR(50) NOT NULL UNIQUE,     
    EmailAddress NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,               
    LastLoginDate DATETIME NULL,                   
    CreatedAt DATETIME NULL,
	UpdatedAt DATETIME NULL,
	DeletedAt DATETIME NULL
);