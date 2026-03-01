CREATE PROCEDURE spProduceProduct
    @TransactionNo NVARCHAR(50),
    @Product_ID NVARCHAR(50),
    @Parts_ID NVARCHAR(50),
    @Quantity INT,
    @UserName NVARCHAR(255),
    @OutputMessage NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
     SET @OutputMessage = NULL;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS (
            SELECT 1
            FROM Parts
            WHERE Parts_ID = @Parts_ID
              AND Quantity < @Quantity
        )
        BEGIN
            SET @OutputMessage = 'Insufficient parts to produce product.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        UPDATE Parts
        SET Quantity = Quantity - @Quantity
        WHERE Parts_ID = @Parts_ID;

        INSERT INTO Production_Summary (
            TransactionNo, Product_ID, Parts_ID, TransactedBy, Transaction_Date, CreatedAt
        )
        VALUES (
            @TransactionNo, @Product_ID, @Parts_ID, @UserName, GETDATE(), GETDATE()
        );

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
                ROLLBACK TRANSACTION;

        SET @OutputMessage = ERROR_MESSAGE();
    END CATCH
END;