using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace MEAS.Data
{
    public class UserProfileRepository :DbRepository, IUserProfileRepository
    {
        private const string TABLE = "UserProfiles";     
        private const string AVATAR = "Avatar";
        private const string PHONE = "Phone";
        private const string MOBILE = "Mobile";
        private const string EMAIL_ADDRESS = "EmailAddress";

        public async Task<bool> Append(UserProfileDao user)
        {
            var insert = string.Format("Insert into {0} Values (@{1},@{2},@{3},@{4},@{5})", TABLE, ID, PHONE, MOBILE, EMAIL_ADDRESS, AVATAR);
            var result = await this.CreateConnection().ExecuteAsync(insert, new { user.Id, user.Phone, user.Mobile, user.EmailAddress, user.Avatar }) > 0;
            if (result)
                user.Id = await this.CreateConnection().QueryFirstAsync<int>(string.Format("select max(id) from {0}", TABLE));
            return result;
        }

        public async Task<UserProfileDao> Find(int userId)
        {
            var query = string.Format("select * from {0} where {1}=@{1}", TABLE, ID);
            var results = (await this.CreateConnection().QueryAsync<UserProfileDao>(query, new { userId }));
            return results.FirstOrDefault();
        }

        public async Task<bool> Remove(UserProfileDao user)
        {
            return await this.Delete(user.Id, TABLE);
        }

        public async Task<bool> Update(UserProfileDao user)
        {
            string sql = string.Format("UPDATE {0} SET {2} = @{2}, {3}=@{3},{4}=@{4},{5}=@{5} WHERE {1} = @{1}", TABLE, ID, PHONE, MOBILE, EMAIL_ADDRESS,AVATAR);
            return await this.CreateConnection().ExecuteAsync(sql, new { user.Id, user.Phone , user.Mobile , user.EmailAddress ,user.Avatar }) > 0;
        }

      
       

       
    }
}
