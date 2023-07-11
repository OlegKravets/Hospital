using HospitalApi.Connection;
using HospitalApi.Models;
using HospitalApi.Scripts;

namespace HospitalApi.Repositories
{
    public class UserRoleRepository : RepositoryBase<UserRole>
    {
        public UserRoleRepository(HospitalConnection connection)
            : base(connection)
        {
        }

        public async Task AddUserRoles(IEnumerable<UserRole> userRoles)
        {
            foreach (var userRole in userRoles)
            {
                await ExecuteQuery(UserRoleScripts.InsertUserRole, userRole);
            }
        }
    }
}
