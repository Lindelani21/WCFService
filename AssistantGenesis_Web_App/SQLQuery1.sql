Create PROC Sp_login
@UserId INT,
@UserPassword VARCHAR(50),
@IsValid BIT OUT
As 
BEGIN
SET @IsValid = (SELECT COUNT(1) FROM [User] WHERE Id = @UserId AND Password = @UserPassword)
end