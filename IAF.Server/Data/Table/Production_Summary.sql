CREATE TABLE Production_Summary (
    TransactionNo NVARCHAR(50) NOT NULL PRIMARY KEY,
    Product_ID NVARCHAR(50) NOT NULL,
    Parts_ID NVARCHAR(50) NOT NULL,
    TransactedBy NVARCHAR(255) NOT NULL,
    Transaction_Date DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedAt DATETIME NULL,
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL
    CONSTRAINT FK_ProductionSummary_Products FOREIGN KEY (Product_ID)
        REFERENCES Products(Product_ID),
    CONSTRAINT FK_ProductionSummary_Parts FOREIGN KEY (Parts_ID)
        REFERENCES Parts(Parts_ID)
);