CREATE PROCEDURE dbo.spUpdateProductForecast
(
    @Product_ID NVARCHAR(50),
    @Quantity INT,
    @OutputMessage NVARCHAR(4000) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @OutputMessage = NULL;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Products
        SET Forecasted_Produced_Count = Forecasted_Produced_Count - @Quantity
        WHERE Product_ID = @Product_ID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @OutputMessage = ERROR_MESSAGE();
    END CATCH
END