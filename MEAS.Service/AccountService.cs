using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using MEAS.Data;

namespace MEAS.Service
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _repository;
        private static ConcurrentDictionary<string, UserInfo> userInfoDictionary =new ConcurrentDictionary<string, UserInfo> ();



        public async Task<bool> UpdateLogin(UserInfo user)
        {
            return await Task.Run(() =>
            {
                if (user == null || user.LoginName ==null)
                    return false;
                string key = user.LoginName.ToUpper();
                if (userInfoDictionary.ContainsKey(key))
                    return userInfoDictionary.TryUpdate(key, user, user);
                else
                    return userInfoDictionary.TryAdd(key, user);
            });
       
        }


        public async Task<bool> UpdateLogout(string loginName)
        {
            return await Task.Run(() =>
            {

                var exist = userInfoDictionary.Values.FirstOrDefault(x => x.LoginName == loginName);
                if (exist == null)
                    return false;
                return userInfoDictionary.TryRemove(loginName, out exist);
            });
        }

        public AccountService(IAccountRepository repository)
        {
            this._repository = repository;
            UserInfo ui = new UserInfo { LoginName = "loginname", Password = "1111", RoleString = "1,2,3", UserName = "name" };
            this._repository.AppendUser(ui);
        }

        public async Task<IEnumerable<UserInfo>> All()
        {
            return await this._repository.LoadAll();
        }

        public async Task<UserInfo> Find(string loginName, string password)
        {
            var user = userInfoDictionary.Values.FirstOrDefault(x => (x.LoginName.Equals(loginName, StringComparison.CurrentCultureIgnoreCase) && x.Password == password));
            if (user == null)
                return await this._repository.Find(loginName, password);
            return user;
        }

        public Task<string> GetUserName(string loginName)
        {
            return Task.Run(() =>
            {
                var key = loginName.ToUpper();
                if (!userInfoDictionary.ContainsKey(key))
                    return null;
                return userInfoDictionary[key].UserName;
            });
        }
    }
}
