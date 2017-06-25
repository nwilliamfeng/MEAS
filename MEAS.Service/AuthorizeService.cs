using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Service
{
    public class AuthorizeService : IAuthorizeService
    {
        private static Dictionary<string, string> userDict;

        public AuthorizeService()
        {
            if (userDict == null)
                userDict = new Dictionary<string, string>();
        }

        public void AddUser(string userId, string password)
        {
            if (!userDict.ContainsKey(userId))
                userDict[userId] = password;
        }

        public bool Authorize(string userId, string password)
        {
            return userDict.ContainsKey(userId) && userDict[userId] == password;
        }

        public void RemoveUser(string userId)
        {
            
        }
    }
}
