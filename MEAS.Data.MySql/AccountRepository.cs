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
        private const string ROLE_STRING = "RoleString";
        private const string LOGIN_NAME = "LoginName";

        public async Task<UserInfo> Find(string loginName, string password)
        {
            var query = string.Format("select * from {0} where {1}=@{1} and  {2}=@{2} ", TABLE, LOGIN_NAME, USR_PASSWORD);
            var results = (await this.CreateConnection().QueryAsync<UserInfo>(query, new { LOGIN_NAME = loginName, USR_PASSWORD = password }));
            return results.FirstOrDefault();
        }

        public async Task<IEnumerable<UserInfo>> LoadAll()
        {
            return await CreateConnection().QueryAsync<UserInfo>(string.Format("select * from {0} ", TABLE)); 
        }

        public async Task<bool> AppendUser(UserInfo user)
        {

            //  var id = connection.Query<int>(sql, new { Stuff = mystuff }).Single();
            try
            {


                var insert = string.Format("Insert into {0} Values (@{1},@{2},@{3},@{4},@{5})", TABLE, ID, LOGIN_NAME, USR_NAME, USR_PASSWORD, ROLE_STRING);
                var result = await this.CreateConnection().ExecuteAsync(insert, new { user.Id, user.LoginName, user.UserName,  user.Password, user.RoleString }) > 0;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            //  var insert = string.Format("Insert {0}({1},{2},{3},{4},{5}) Values (@{1},@{2},@{3},@{4},@{5}); select SCOPE_IDENTITY as bigint",  TABLE,ID, LOGIN_NAME, USR_NAME, USR_PASSWORD, ROLE_STRING);
            //var id = (await this.CreateConnection().QueryAsync<long>(insert, new {ID=user.Id, LOGIN_NAME = user.LoginName, USR_NAME = user.UserName, USR_PASSWORD = user.Password, ROLE_STRING = user.RoleString }))
            //    .Single() ;
            //if (id > 0)
            //    user.Id = id;
            //return id > 0;
        }
    }
}
