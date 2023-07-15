using HospitalApi.Connection;
using HospitalApi.Models;
using HospitalApi.Scripts;

namespace HospitalApi.Repositories
{
    public class UserRoleRepository : RepositoryBase<UserRole>
    {
        private const string UserIdParam = "@UserId";
        private const string RoleIdParam = "@RoleId";
        public UserRoleRepository(HospitalConnection connection)
            : base(connection)
        {
        }

        public async Task InsertUserRoles(IEnumerable<UserRole> userRoles)
        {
            foreach (var userRole in userRoles)
            {
                await InsertUserRole(userRole.UserId, userRole.RoleId);
            }
        }

        public async Task<int> InsertUserRole(int userId, int roleId)
        {
            return await InsertQuery(
                UserRoleScripts.InsertUserRole,
                new Dictionary<string, object>
                {
                    { UserIdParam, userId },
                    { RoleIdParam, roleId }
                });
        }
    }
}
