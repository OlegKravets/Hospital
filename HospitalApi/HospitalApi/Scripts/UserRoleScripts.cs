namespace HospitalApi.Scripts
{
    public static class UserRoleScripts
    {
        public const string InsertUserRole = "INSERT INTO UserRoles(UserId, RoleId) OUTPUT INSERTED.UserRoleId VALUES(@UserId, @RoleId);";
    }
}
