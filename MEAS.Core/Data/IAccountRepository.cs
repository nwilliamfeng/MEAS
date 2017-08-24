using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public interface IAccountRepository
    {
        Task<UserInfoDao> Find(string loginName,string password);

        Task<UserInfoDao> Find(int id);

        Task<bool> AppendUser(UserInfoDao user);

        Task<bool> RemoveUser(UserInfoDao user);

        Task<bool> UpdateUser(UserInfoDao user);

        Task<IEnumerable<UserInfoDao>> LoadAll();
    }
}
