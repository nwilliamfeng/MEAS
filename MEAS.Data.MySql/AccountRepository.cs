using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data.MySql
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<UserInfo> Find(string loginName, string password)
        {
            if (loginName == "admin")
                return new UserInfo {Id=1, UserName = "冯伟", LoginName = loginName, Password = password, Roles = new string[] { "1", "2", "3", "4" } };
            else
                return new UserInfo {Id=2, UserName = "匿名", LoginName = loginName, Password = password, Roles = new string[] {  "4" ,"5"} };
        }

        public async Task<IEnumerable<UserInfo>> LoadAll()
        {
            return new UserInfo[] { };
        }
    }
}
