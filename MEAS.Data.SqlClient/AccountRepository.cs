using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MEAS.Data.SqlClient
{
    public class AccountRepository : IAccountRepository
    {
      
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

        private UserInfo ToUserPartial(UserInfoDao x)
        {

            return new UserInfo { Id = x.Id, LoginName = x.LoginName, Password = x.Password };

        }
 

        public   Task<IEnumerable<UserInfo>> LoadAll()
        {
            return Task.Run<IEnumerable<UserInfo>>(() =>
            {
                using (var dc = new SqlServerDbContext())
                {
                    return dc.Users
                    .Select(x=>ToUserPartial(x))
                    .ToList();          
                }
            });
        }

        public async Task<bool> AppendUser(UserInfo user)
        {
            using (var dc = new SqlServerDbContext())
            {
                var dao = user.ToDao();
                dc.Users.Add(dao);
                var result = await dc.SaveChangesAsync();
                if (result==1)
                    user.Id = dao.Id;
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
                var dao = user.ToDao();
                dc.Users.Attach(dao);
                DbEntityEntry<UserInfoDao> entry = dc.Entry(dao);
               // entry.Property(e => e.LoginName).IsModified = true;
                entry.Property(e => e.Password).IsModified = true;
                entry.Property(e => e.Roles).IsModified = true;

                var result = await dc.SaveChangesAsync();
                return result == 1;
            }
        }

        public   Task<UserInfo> Find(int id,bool fullLoad=false)
        {
            return Task.Run(() =>
            {
                using (var dc = new SqlServerDbContext())
                {
                    if (!fullLoad)
                        return dc.Users.Select(x => new { Id = x.Id, LoginName = x.LoginName, Password = x.Password })
                                                .OfType<UserInfoDao>()
                                                .FirstOrDefault(x => x.Id == id)
                                                ?.ToEntity();
                    else
                        return dc.Users.FirstOrDefault(x => x.Id == id)?.ToEntity();
                }
            });
        }
    }
}
