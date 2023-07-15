using Dapper;
using HospitalApi.Connection;
using HospitalApi.Models;
using HospitalApi.Scripts;

namespace HospitalApi.Repositories
{
    public class RoleRepository : RepositoryBase<Role>
    {
        public const string RoleNameParam = "@RoleName";

        public RoleRepository(HospitalConnection connection)
            : base(connection)
        {
        }

        protected override string AnyScript => RoleScripts.AnyRole;

        public async Task AddRole(Role role)
        {
            await ExecuteQuery(RoleScripts.InsertRole, new DynamicParameters(role));
        }

        public async Task AddRoles(IEnumerable<Role> roles)
        {
            foreach (var role in roles)
            {
                await AddRole(role);
            }
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            return await GetFirstOrDefault(RoleScripts.SelectRoleByName, new { RoleNameParam, roleName });
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await GetData(RoleScripts.SelectRoles);
        }
    }
}
