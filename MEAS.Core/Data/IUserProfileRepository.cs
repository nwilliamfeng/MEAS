using System;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public interface IUserProfileRepository
    {
        Task<UserProfileDao> Find(int userId);

        Task<bool> Append(UserProfileDao user);

        Task<bool> Remove(UserProfileDao user);

        Task<bool> Update(UserProfileDao user);

       
    }
}
