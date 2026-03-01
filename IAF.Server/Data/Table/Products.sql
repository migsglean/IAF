CREATE TABLE Products (
    Product_ID NVARCHAR(50) NOT NULL PRIMARY KEY,
    Product_Desc NVARCHAR(255) NOT NULL,
    Forecasted_Produced_Count INT NOT NULL,
    Image VARBINARY(MAX) NULL,
    CreatedAt DATETIME NULL,
	UpdatedAt DATETIME NULL,
	DeletedAt DATETIME NULL
);