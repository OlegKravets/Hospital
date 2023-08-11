namespace HospitalApi.Scripts
{
    public static class UserScripts
    {
        public const string InsertUser = "INSERT INTO Users(Name, Age, PasswordHash, PasswordSalt, Salary, HospitalId) OUTPUT INSERTED.Id VALUES(@Name, @Age, @PasswordHash, @PasswordSalt, @Salary, @HospitalId)";
        public const string AnyUser = "SELECT COUNT(1) FROM Users;";
        public const string DeleteUser = "DELETE FROM Users WHERE Id = @Id;";
        public const string SelectAllUser = "SELECT * FROM Users";
        public const string SelectAllUserWithPhoto = $"{SelectAllUser} u LEFT JOIN Photos p ON u.PhotoId = p.Id";
        public const string SelectByUsername = $"{SelectAllUser} u WHERE u.Name = @Username";
        public const string SelectByUsernameWithPhoto = $"{SelectAllUserWithPhoto} WHERE u.Name = @Username";
        public const string SelectByRole = "SELECT u.*, p.* FROM Users u LEFT JOIN Photos p ON u.PhotoId = p.Id JOIN UserRoles ur ON u.Id = ur.UserId JOIN Roles r ON ur.RoleId = r.Id WHERE r.RoleName = @Role";
    }
}
