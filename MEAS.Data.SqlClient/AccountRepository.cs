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
                    return dc.Users.Where(x => x.LoginName == loginName && x.Password == password)
                    .Select(x => new UserInfo { Id = x.Id, UserName = x.UserName, Password = x.Password,LoginName=x.LoginName })
                    .FirstOrDefault();
                }
            });
          
         
        }
 

        public   Task<IEnumerable<UserInfo>> LoadAll()
        {
            return Task.Run<IEnumerable<UserInfo>>(() =>
            {
                using (var dc = new SqlServerDbContext())
                {
                    return dc.Users
                    .Select(x => new UserInfo { Id = x.Id, LoginName = x.LoginName, Password = x.Password,UserName=x.UserName })
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
                if (result == 1)
                {
                    user.Id = dao.Id;
                    user.Timestamp = dao.Timestamp;
                }
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
                try
                {

                    var dao = user.ToDao();
                   var et= dc.Users.Attach(dao);
                    DbEntityEntry<UserInfoDao> entry = dc.Entry(dao);

                    // entry.Property(e => e.LoginName).IsModified = true;
                    //     entry.Property(e => e.Password).IsModified = true;
                    //  entry.Property(e => e.Roles).IsModified = true;

                    var result = await dc.SaveChangesAsync();
                    if (result > 0)
                        user.Timestamp = et.Timestamp;
                    return result == 1;
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException)
                {
                    throw new InvalidOperationException("无法提交修改的内容，此对象已被修改："+user.UserName);
                }
            }
        }

       public async Task<bool> ModifyPassword(int id, string newPassword)
        {
            using (var dc = new SqlServerDbContext())
            {
                try
                {
                    var user = dc.Users.Where(x=>x.Id==id)
                        .Select(x => new UserInfo { Id = x.Id, Password = x.Password, Timestamp = x.Timestamp })
                        .FirstOrDefault()?.ToDao();
                    dc.Configuration.ValidateOnSaveEnabled = false; //关闭全局校验
                    user.Password = newPassword;
                   dc.Users.Attach(user);
                    DbEntityEntry<UserInfoDao> entry = dc.Entry(user);  //部分修改，只对需要修改的字段的ismodify置true
                 //   entry.Property(e => e.LoginName).IsModified = false;
                    entry.Property(e => e.Password).IsModified = true;
                    //entry.Property(e => e.Roles).IsModified = false;                  
                    var result = await dc.SaveChangesAsync();
                    return result == 1;
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException)
                {
                    throw new InvalidOperationException("无法提交修改的内容，此对象已被修改，用户id：" + id);
                }
            }
        }

        public   Task<UserInfo> Find(int id,bool fullLoad=false)
        {
            return Task.Run(() =>
            {
                using (var dc = new SqlServerDbContext())
                {
                    if (!fullLoad)
                        return dc.Users.Select(x => new UserInfoDao{ Id = x.Id, LoginName = x.LoginName, Password = x.Password ,UserName=x.UserName})                                            
                                                .FirstOrDefault(x => x.Id == id)
                                                ?.ToEntity();
                    else
                        return dc.Users.FirstOrDefault(x => x.Id == id)?.ToEntity();
                }
            });
        }
    }
}
