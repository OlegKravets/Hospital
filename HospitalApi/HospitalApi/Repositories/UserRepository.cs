using Dapper;
using HospitalApi.Connection;
using HospitalApi.Interfaces;
using HospitalApi.Models;
using HospitalApi.Scripts;

namespace HospitalApi.Repositories
{
    public sealed class UserRepository : RepositoryBase<User>
    {
        private const string UsernameParam = "@Username";
        private const string IdParam = "@Id";

        public UserRepository(HospitalConnection connection)
            : base(connection)
        {
        }

        public async Task AddUser(User newUser)
        {
            await ExecuteQuery(UserScripts.InsertUser, newUser);
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
            return await GetSingle(UserScripts.SelectByUsername, new Dictionary<string, object>() { { UsernameParam, userName } });
        }

        public async Task<bool> IsUserExist(string userName)
        {
            return await IsExist(UserScripts.SelectByUsername, new Dictionary<string, object>() { { UsernameParam, userName } });
        }
    }
}
