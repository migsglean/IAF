CREATE PROCEDURE spAddPart
    @Parts_ID NVARCHAR(50),
    @Parts_Desc NVARCHAR(255),
    @Quantity INT,
    @Image VARBINARY(MAX) = NULL,
    @Product_ID NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

     BEGIN TRY
        IF EXISTS (
            SELECT 1
            FROM Parts
            WHERE Parts_Desc = @Parts_Desc
              AND Product_ID = @Product_ID
              AND DeletedAt IS NULL
        )
        BEGIN
            RAISERROR('Part description already exists for this product.', 16, 1);
            RETURN;
        END

        INSERT INTO Parts (Parts_ID, Parts_Desc, Quantity, Image, Product_ID, CreatedAt, UpdatedAt, DeletedAt)
        VALUES (@Parts_ID, @Parts_Desc, @Quantity, @Image, @Product_ID, GETDATE(), NULL, NULL);
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;