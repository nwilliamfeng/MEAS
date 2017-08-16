using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MEAS.Data.MySql
{
    public class AccountRepository :DbRepository, IAccountRepository
    {
        private const string TABLE = "Users";
        private const string USR_NAME = "UserName";
        private const string USR_PASSWORD = "Password";
        private const string ROLE_STRING = "Roles";
        private const string LOGIN_NAME = "LoginName";

        public async Task<UserInfoDao> Find(string loginName, string password)
        {
            var query = string.Format("select * from {0} where {1}=@{1} and {2}=@{2}", TABLE, LOGIN_NAME, USR_PASSWORD);
            var results = (await this.CreateConnection().QueryAsync<UserInfoDao>(query, new { loginName,  password }));
            return results.FirstOrDefault();
        }

        public async Task<IEnumerable<UserInfoDao>> LoadAll()
        {
            return await CreateConnection().QueryAsync<UserInfoDao>(string.Format("select * from {0} ", TABLE)); 
        }

        public async Task<bool> AppendUser(UserInfoDao user)
        {
            var insert = string.Format("Insert into {0} Values (@{1},@{2},@{3},@{4},@{5})", TABLE, ID, LOGIN_NAME, USR_NAME, USR_PASSWORD, ROLE_STRING);
            var result = await this.CreateConnection().ExecuteAsync(insert, new { user.Id, user.LoginName, user.UserName, user.Password, user.Roles }) > 0;
            if (result)
                user.Id = await this.CreateConnection().QueryFirstAsync<int>(string.Format("select max(id) from {0}", TABLE));
            return result;
        }

        public async Task<bool> RemoveUser(UserInfoDao user)
        {
            return await this.Delete(user.Id, TABLE);
        }

        public async Task<bool> UpdateUser(UserInfoDao user)
        {
            string sql =string.Format( "UPDATE {0} SET {2} = @{2}, {3}=@{3},{4}=@{4} WHERE {1} = @{1}",TABLE,ID, USR_NAME,USR_PASSWORD,ROLE_STRING);
            return await this.CreateConnection().ExecuteAsync(sql, new { user.Id, user.UserName, user.Password, user.Roles }) > 0;
        }
    }
}
