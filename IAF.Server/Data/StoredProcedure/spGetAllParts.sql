CREATE PROCEDURE spGetAllParts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Parts_ID,
        Parts_Desc,
        Quantity,
        Image,
        Product_ID
    FROM Parts
	WHERE DeletedAt IS NULL;
END;