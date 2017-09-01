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

        Task<UserInfo> Find(int id);

        Task<bool> AppendUser(UserInfo user);

        Task<bool> RemoveUser(UserInfo user);

        Task<bool> UpdateUser(UserInfo user);

        Task<IEnumerable<UserInfo>> LoadAll();
    }
}
