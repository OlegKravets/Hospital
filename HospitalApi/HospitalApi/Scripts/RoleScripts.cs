namespace HospitalApi.Scripts
{
    public static class RoleScripts
    {
        public const string InsertRole = "INSERT INTO Roles(RoleName) VALUES(@RoleName);";
        public const string AnyRole = "SELECT COUNT(1) FROM Roles;";
        public const string SelectRoleByName = "SELECT * FROM Roles r WHERE r.RoleName = @RoleName;";

    }
}
