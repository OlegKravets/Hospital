﻿using API.Entities;
using Dapper;
using HospitalApi.Connection;
using HospitalApi.Models;
using HospitalApi.Scripts;
using Microsoft.Data.SqlClient;
using System.Data;

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

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await GetData(UserScripts.SelectAllUser);
        }

        public async Task<IEnumerable<User>> GetUsersWithPhoto()
        {
            return await GetUsersWithPhoto(UserScripts.SelectAllUserWithPhoto);
        }

        public async Task<User> GetUserByUsername(string userName)
        {
            return await GetFirstOrDefault(UserScripts.SelectByUsername, new { UsernameParam, userName });
        }

        public async Task<User> GetUserByUsernameWithPhoto(string userName)
        {
            var result = await GetUsersWithPhoto(UserScripts.SelectByUsernameWithPhoto, new { UsernameParam, userName });
            return result.FirstOrDefault();
        }

        public async Task<bool> IsUserExist(string userName)
        {
            return await IsExist(UserScripts.SelectByUsername, new { UsernameParam, userName });
        }

        public async Task<IEnumerable<User>> GetUserByRole(string role)
        {
            return await GetUsersWithPhoto(UserScripts.SelectByRole, new { RoleParam, role });
        }

        private async Task<IEnumerable<User>> GetUsersWithPhoto(string sql, object param = null)
        {
            using (IDbConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                return await connection.QueryAsync<User, Photo, User>(
                    sql: sql,
                    map: (user, photo) =>
                    {
                        user.Photo = photo;
                        return user;
                    },
                    param: param,
                    splitOn: nameof(User.PhotoId));
            }
        }
    }
}
