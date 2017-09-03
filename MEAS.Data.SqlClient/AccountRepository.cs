using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace MEAS.Data.SqlClient
{
    public class AccountRepository : IAccountRepository
    {
     
        private const string USR_NAME = "UserName";
        private const string USR_PASSWORD = "Password";
        private const string ROLE_STRING = "Roles";
        private const string LOGIN_NAME = "LoginName";

        public  Task<UserInfo> Find(string loginName, string password)
        {
            return Task.Run(() =>
            {
                using (var dc = new SqlServerDbContext())
                {
                    return dc.Users.FirstOrDefault(x => x.LoginName == loginName && x.Password == password)?.ToEntity();
                }
            });
          
         
        }
 

        public   Task<IEnumerable<UserInfo>> LoadAll()
        {
            return Task.Run(() =>
            {
                using (var dc = new SqlServerDbContext())
                {
                    return dc.Users.ToList().ToEntity();
                }
            });
        }

        public async Task<bool> AppendUser(UserInfo user)
        {
            using (var dc = new SqlServerDbContext())
            {
                dc.Users.Add(user.ToDao());
                var result = await dc.SaveChangesAsync();
                return result == 1;
            }
        }

        public async Task<bool> RemoveUser(UserInfo user)
        {
            using (var dc = new SqlServerDbContext())
            {
                dc.Users.Remove(user.ToDao());
                var result = await dc.SaveChangesAsync();
                return result == 1;
            }
        }

        public async Task<bool> UpdateUser(UserInfo user)
        {
            using (var dc = new SqlServerDbContext())
            {
                dc.Users.Remove(user.ToDao());
                dc.Users.Add(user.ToDao());
                var result = await dc.SaveChangesAsync();
                return result == 1;
            }
        }

        public   Task<UserInfo> Find(int id)
        {
            return Task.Run(() =>
            {
                using (var dc = new SqlServerDbContext())
                {
                    return dc.Users.FirstOrDefault(x=>x.Id ==id)?.ToEntity();
                }
            });
        }
    }
}
