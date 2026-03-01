CREATE PROCEDURE spLoginUser
    @UserName NVARCHAR(255),
    @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DECLARE @UserExists BIT = 0;

        -- Check if user exists
        IF EXISTS (
            SELECT 1
            FROM Credentials
            WHERE UserName = @UserName
              AND Password = @Password
              AND DeletedAt IS NULL
        )
        BEGIN
            SET @UserExists = 1;

            SELECT UserName
            FROM Credentials
            WHERE UserName = @UserName;

            UPDATE dbo.Credentials
            SET LastLoginDate = GETDATE()
            WHERE UserName = @UserName;
        END

        IF @UserExists = 0
        BEGIN
            THROW 50001, 'Invalid username or password.', 1;
        END
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END