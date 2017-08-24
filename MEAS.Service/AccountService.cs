using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using AutoMapper;
using MEAS.Data;

namespace MEAS.Service
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _userRepository;
        private IUserProfileRepository _profileRepository;
        private static ConcurrentDictionary<string, UserInfo> userInfoDictionary =new ConcurrentDictionary<string, UserInfo> ();

        public AccountService(IAccountRepository repository, IUserProfileRepository profileRepository)
        {
            this._userRepository = repository;
            this._profileRepository = profileRepository;
            this.InitizeUserInfos();
        }

        private UserInfo ToUserEntity(UserInfoDao dao)
        {
            return Mapper.Map<UserInfo>(dao);
        }

        private UserInfoDao ToUserDao(UserInfo entity)
        {
            return Mapper.Map<UserInfoDao>(entity);
        }
 

        private  void InitizeUserInfos()
        {
            if (userInfoDictionary == null)
            {
                userInfoDictionary = new ConcurrentDictionary<string, UserInfo>();
                var users =  this._userRepository.LoadAll().Result.Select(x=>this.ToUserEntity(x));
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
            var daos= await this._userRepository.LoadAll();
            return daos.Select(x => this.ToUserEntity(x));
        }

        public async Task<UserInfo> Find(string loginName, string password)
        {
            var user = userInfoDictionary.Values.FirstOrDefault(x => (x.LoginName.Equals(loginName, StringComparison.CurrentCultureIgnoreCase) && x.Password == password));
            if (user == null)
            {
                var dao=  await this._userRepository.Find(loginName, password);
                if (dao != null)
                {
                    user = this.ToUserEntity(dao);
                    userInfoDictionary[loginName] = user;
                }
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
            var result= await this._userRepository.RemoveUser(this.ToUserDao(user));
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
            return await this._userRepository.UpdateUser(this.ToUserDao(old));
        }

        public async Task<UserProfile> GetProfile(int id)
        {
            var user = await this._userRepository.Find(id);
             var pd =await this._profileRepository.Find(id);
             var profile =Mapper.Map<UserProfile>(pd);
            Mapper.Map(this.ToUserEntity(user), profile);
            return profile;
        }

        public async Task<bool> UpdateAvatar(int userId, byte[] avatar)
        {
            var profile = await this._profileRepository.Find(userId);
            profile.Avatar = avatar;
            return await this._profileRepository.Update(profile);
        }
    }
}
