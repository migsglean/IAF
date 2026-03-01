CREATE PROCEDURE spGetPartsByProductId
    @Product_ID NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Parts_ID,
        Parts_Desc,
        Quantity
    FROM Parts
    WHERE Product_ID = @Product_ID;
END