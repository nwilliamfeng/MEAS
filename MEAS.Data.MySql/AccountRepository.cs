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
     
        private const string USR_NAME = "UserName";
        private const string USR_PASSWORD = "Password";
        private const string ROLE_STRING = "Roles";
        private const string LOGIN_NAME = "LoginName";

        public async Task<UserInfo> Find(string loginName, string password)
        {
            var query = string.Format("select * from {0} where {1}=@{1} and {2}=@{2}", TableName, LOGIN_NAME, USR_PASSWORD);
            var result = await this.CreateConnection().QueryFirstOrDefaultAsync<UserInfoDao>(query, new { loginName,  password });
            return result?.ToEntity();
        }

        protected override string TableName
        {
            get
            {
                return "Users";
            }
        }

        public async Task<IEnumerable<UserInfo>> LoadAll()
        {
            var lst= await CreateConnection().QueryAsync<UserInfoDao>(string.Format("select * from {0} ", TableName));
            return lst.ToEntity();
        }

        public async Task<bool> AppendUser(UserInfo user)
        {
            //注意在mysql中不能在一条语句中执行插入和返回，sqlserver可以 ：https://stackoverflow.com/questions/8270205/how-do-i-perform-an-insert-and-return-inserted-identity-with-dapper/8270264#8270264
            //mysql 参考：https://stackoverflow.com/questions/8477398/invalid-cast-when-returning-mysql-last-insert-id-using-dapper-net

            var dao = user.ToDao();
            string sql = string.Format("insert into {0}  values(@a,@b,@c,@d,@e)", TableName);
            var result = await this.CreateConnection().ExecuteAsync(sql, new { a = dao.Id, b = dao.LoginName, c = dao.UserName, d = dao.Password, e = dao.Roles }) > 0;

            //var insert = string.Format("Insert into {0} Values (@{1},@{2},@{3},@{4},@{5})", TABLE, ID, LOGIN_NAME, USR_NAME, USR_PASSWORD, ROLE_STRING);
            //var result = await this.CreateConnection().ExecuteAsync(insert, new { user.Id, user.LoginName, user.UserName, user.Password, user.Roles }) > 0;
            if (result)
                //  user.Id = await this.CreateConnection().QueryFirstAsync<int>(string.Format("select max(id) from {0}", TableName));
                user.Id = this.GetInsertedId();
            return result;


        }

        public async Task<bool> RemoveUser(UserInfo user)
        {
            return await this.Delete(user.Id, TableName);
        }

        public async Task<bool> UpdateUser(UserInfo user)
        {
            var dao = user.ToDao();
            string sql =string.Format( "UPDATE {0} SET {2} = @{2}, {3}=@{3},{4}=@{4} WHERE {1} = @{1}",TableName,ID, USR_NAME,USR_PASSWORD,ROLE_STRING);
            return await this.CreateConnection().ExecuteAsync(sql, new { dao.Id, dao.UserName, dao.Password, dao.Roles }) > 0;
        }

        public async Task<UserInfo> Find(int id)
        {         
             var dao= await CreateConnection().QueryFirstOrDefaultAsync<UserInfoDao>(string.Format("select * from {0} where id={1}", TableName,id));
            return dao?.ToEntity();
        }
    }
}
