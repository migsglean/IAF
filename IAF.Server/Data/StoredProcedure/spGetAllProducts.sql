CREATE PROCEDURE spGetAllProducts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Product_ID, Product_Desc, Forecasted_Produced_Count, Image
    FROM Products
    WHERE DeletedAt IS NULL;
END;