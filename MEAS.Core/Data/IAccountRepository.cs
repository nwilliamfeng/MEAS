using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public interface IAccountRepository
    {
        Task<UserInfo> Find(string loginName,string password);

        Task<UserInfo> Find(int id,bool fullLoad=false);

        Task<bool> AppendUser(UserInfo user);

        Task<bool> Remove(int id);

        Task<bool> ModifyPassword(int id, string newPassword);

        Task<bool> Update(UserInfo user);

        Task<IEnumerable<UserInfo>> LoadAll();
    }
}
