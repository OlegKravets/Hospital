﻿namespace HospitalApi.Scripts
{
    public static class UserScripts
    {
        public const string InsertUser = "INSERT INTO Users(Name, Age, PasswordHash, PasswordSalt, Salary, HospitalId) VALUES(@Name, @Age, @PasswordHash, @PasswordSalt, @Salary, @HospitalId);";
        public const string AnyUser = "SELECT COUNT(1) FROM Users;";
        public const string DeleteUser = "DELETE FROM Users WHERE Id = @Id;";
        public const string SelectAllUser = "SELECT * FROM Users;";
        public const string SelectByUsername = "SELECT * FROM Users u WHERE u.Name = @Username;";
    }
}
