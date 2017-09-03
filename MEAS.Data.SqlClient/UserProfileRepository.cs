using System;
using System.Linq;
using System.Threading.Tasks;
 

namespace MEAS.Data
{
    public class UserProfileRepository : IUserProfileRepository
    {
       
        public async Task<bool> Append(UserProfile user)
        {
            return false;
        }

        public  Task<UserProfile> Find(int userId)
        {
            return Task.Run(() =>
            {
                return new UserProfile();
            });
        }

        public async Task<bool> Remove(UserProfile user)
        {
            return false;
        }

        public async Task<bool> Update(UserProfile user)
        {
            return false;
        }

    }
}
