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

        Task<IEnumerable<UserInfo>> LoadAll();
    }
}
