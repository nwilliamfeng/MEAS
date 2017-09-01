using System;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> Find(int userId);

        Task<bool> Append(UserProfile user);

        Task<bool> Remove(UserProfile user);

        Task<bool> Update(UserProfile user);

       
    }
}
