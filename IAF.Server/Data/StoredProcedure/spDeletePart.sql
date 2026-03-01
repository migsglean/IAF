CREATE PROCEDURE spDeletePart
    @Parts_ID NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Product_ID NVARCHAR(50);

    SELECT @Product_ID = Product_ID
    FROM Parts
    WHERE Parts_ID = @Parts_ID;

    DELETE FROM Parts
    WHERE Parts_ID = @Parts_ID;

    SELECT @Product_ID AS Product_ID;
END;