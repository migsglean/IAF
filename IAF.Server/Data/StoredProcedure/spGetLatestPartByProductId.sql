CREATE PROCEDURE spGetLatestPartByProductId
    @Product_ID NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1 *
    FROM Parts
    WHERE Product_ID = @Product_ID
    ORDER BY CreatedAt DESC; 
END
