using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Service
{
    public interface IAccountService
    {
        Task<UserInfo> Find(string loginName,string password);

        Task<IEnumerable<UserInfo>> All();

        Task<bool> UpdateLogin(UserInfo user);

        Task<string> GetUserName(string loginName);

        Task<bool> UpdateLogout(string loginName);
    }
}
