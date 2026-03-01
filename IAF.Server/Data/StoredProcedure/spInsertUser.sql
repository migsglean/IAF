CREATE PROCEDURE spInsertUser
    @UserName NVARCHAR(100),
    @EmailAddress NVARCHAR(255),
    @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO Credentials (UserName, EmailAddress, Password, LastLoginDate, CreatedAt, UpdatedAt, DeletedAt)
        VALUES (@UserName, @EmailAddress, @Password, NULL, GETDATE(), NULL, NULL);
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END