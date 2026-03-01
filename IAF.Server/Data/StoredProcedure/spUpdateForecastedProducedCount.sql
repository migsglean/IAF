CREATE PROCEDURE spUpdateForecastedProducedCount
    @Product_ID NVARCHAR(50),
    @ForecastedCount INT
AS
BEGIN
    UPDATE Products
    SET Forecasted_Produced_Count = @ForecastedCount
    WHERE Product_ID = @Product_ID;
END