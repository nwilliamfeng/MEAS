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

        Task<UserProfile> GetProfile(int id);
    
        Task<IEnumerable<UserInfo>> All();

        Task<bool> UpdateLogin(UserInfo user);

        Task<bool> RemoveUser(UserInfo user);

        Task<bool> ModifyPassword(string loginName,string password,string newPassword);

        Task<UserInfo> GetCurrentUser();

        Task<bool> UpdateLogout(string loginName);
    }
}
