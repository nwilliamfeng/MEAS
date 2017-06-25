using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    public interface IAuthorizeService
    {
        void AddUser(string userId, string password);

        void RemoveUser(string userId);

        bool Authorize(string userId,string password);
    }
}
