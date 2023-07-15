using HospitalApi.Connection;
using HospitalApi.Models;
using HospitalApi.Scripts;

namespace HospitalApi.Repositories
{
    public sealed class UserRepository : RepositoryBase<User>
    {
        private const string UsernameParam = "@Username";
        private const string RoleParam = "@Role";
        private const string IdParam = "@Id";

        public UserRepository(HospitalConnection connection)
            : base(connection)
        {
        }

        protected override string AnyScript => UserScripts.AnyUser;

        public async Task<int> AddUser(User newUser)
        {
            return await InsertQuery(UserScripts.InsertUser, newUser);
        }

        public async Task AddUsers(IEnumerable<User> newUsers)
        {
            foreach (var user in newUsers)
            {
                await AddUser(user);
            }
        }

        public async Task<bool> RemoveUser(int userId)
        {
            try
            {
                await ExecuteQuery(UserScripts.DeleteUser, new Dictionary<string, object>() { { IdParam, userId } });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<User>> GetUsers() => await GetData(UserScripts.SelectAllUser);

        public async Task<User> GetUserByUsername(string userName)
        {
            return await GetFirstOrDefault(UserScripts.SelectByUsername, new { UsernameParam, userName });
        }

        public async Task<bool> IsUserExist(string userName)
        {
            return await IsExist(UserScripts.SelectByUsername, new { UsernameParam, userName });
        }

        public async Task<IEnumerable<User>> GetUserByRole(string role)
        {
            return await GetData(UserScripts.SelectByRole, new { RoleParam, role });
        }
    }
}
