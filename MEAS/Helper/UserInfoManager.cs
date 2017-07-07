using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace MEAS
{
    internal static class UserInfoManager
    {
        private static ConcurrentDictionary<long, UserInfo> userInfoDictionary;

        static UserInfoManager()
        {
            userInfoDictionary = new ConcurrentDictionary<long, UserInfo>();
        }

        public static bool Add(UserInfo user)
        {
            if (user == null)
                return false;
            if (userInfoDictionary.ContainsKey(user.Id))
                return false;
           return  userInfoDictionary.TryAdd(user.Id, user);
        }
 

        public static bool Remove(string loginName)
        {
           var exist=  userInfoDictionary.Values.FirstOrDefault(x => x.LoginName == loginName);
            if (exist == null)
                return false;
            return userInfoDictionary.TryRemove(exist.Id,out exist);
        }
    }
}
