using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using MEAS.Data;
using AutoMapper;
 
namespace MEAS.Service
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _userRepository;
       
        private static ConcurrentDictionary<string, UserInfo> userInfoDictionary =new ConcurrentDictionary<string, UserInfo> ();

        public AccountService(IAccountRepository repository )
        {
            this._userRepository = repository;
        
            this.InitizeUserInfos();
        }

       

        private  void InitizeUserInfos()
        {
            if (userInfoDictionary == null)
            {
                userInfoDictionary = new ConcurrentDictionary<string, UserInfo>();
                var users =  this._userRepository.LoadAll().Result;
                users.ToList().ForEach(x =>
                {
                    userInfoDictionary.TryAdd(x.LoginName.ToUpper(), x);
                });
            }
        }

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

       

        public async Task<IEnumerable<UserInfo>> All()
        {
            return await this._userRepository.LoadAll();
        }

        public async Task<UserInfo> Find(string loginName, string password)
        {
            var user = userInfoDictionary.Values.FirstOrDefault(x => (x.LoginName.Equals(loginName, StringComparison.CurrentCultureIgnoreCase) && x.Password == password));
            if (user == null)
            {
                 user=  await this._userRepository.Find(loginName, password);
                if (user != null)
                    userInfoDictionary[loginName] = user;
            }
            return user;
        }

        public Task<UserInfo> GetCurrentUser()
        {
            var principal = System.Threading.Thread.CurrentPrincipal;
            //有一种情况是调试过程中创建了userinfo，但是在token未过期时候再一次进行调试这时候userInfoDictionary必须重新赋值
            if(principal.Identity.IsAuthenticated && userInfoDictionary == null)
                this.InitizeUserInfos();

            string loginName = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            return Task.Run(() =>
            {
                if (string.IsNullOrEmpty(loginName))
                    return null;
                var key = loginName.ToUpper();
                if (!userInfoDictionary.ContainsKey(key))
                    return null;
                return userInfoDictionary[key];
            });
        }

        public async Task<bool> RemoveUser(UserInfo user)
        {
            var result= await this._userRepository.Remove(user.Id);
            if (result)
                userInfoDictionary.TryRemove(user.LoginName, out user);
            return result;
        }

        public async Task<bool> ModifyPassword(string loginName, string password, string newPassword)
        {
            var old =  userInfoDictionary[loginName];
            if (old == null)
                return false;
            old.Password = newPassword;
            return await this._userRepository.Update(old);
        }

        public async Task<UserInfo> GetDetail(int id)
        {
            return await this._userRepository.Find(id,true);       
        }

        

        public async Task<bool> UpdateAvatar(int userId, byte[] avatar)
        {
            var user = await this._userRepository.Find(userId,true);
            user.Avatar = avatar;
            return await this._userRepository.Update(user);
        }

     
    }
}
