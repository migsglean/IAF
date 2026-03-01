CREATE PROCEDURE spUpdatePart
    @Parts_ID NVARCHAR(50),
    @Image VARBINARY(MAX) = NULL,
    @Quantity INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Parts
    SET Quantity = @Quantity,
        Image = @Image,
        UpdatedAt = GETDATE()
    WHERE Parts_ID = @Parts_ID;
END;