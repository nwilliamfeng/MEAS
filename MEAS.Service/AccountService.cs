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
        private IAccountRepository _repository;
        private IUserProfileRepository _profileRepository;
        private static ConcurrentDictionary<string, UserInfo> userInfoDictionary =new ConcurrentDictionary<string, UserInfo> ();

        public AccountService(IAccountRepository repository, IUserProfileRepository profileRepository)
        {
            this._repository = repository;
            this._profileRepository = profileRepository;
            this.InitizeUserInfos();
        }

        private UserInfo ToEntity(UserInfoDao dao)
        {
            return Mapper.Map<UserInfo>(dao);
        }

        private UserInfoDao ToDao(UserInfo entity)
        {
            return Mapper.Map<UserInfoDao>(entity);
        }

        private  void InitizeUserInfos()
        {
            if (userInfoDictionary == null)
            {
                userInfoDictionary = new ConcurrentDictionary<string, UserInfo>();
                var users =  this._repository.LoadAll().Result.Select(x=>this.ToEntity(x));
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
            var daos= await this._repository.LoadAll();
            return daos.Select(x => this.ToEntity(x));
        }

        public async Task<UserInfo> Find(string loginName, string password)
        {
            var user = userInfoDictionary.Values.FirstOrDefault(x => (x.LoginName.Equals(loginName, StringComparison.CurrentCultureIgnoreCase) && x.Password == password));
            if (user == null)
            {
                var dao=  await this._repository.Find(loginName, password);
                if (dao != null)
                {
                    user = this.ToEntity(dao);
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
            var result= await this._repository.RemoveUser(this.ToDao(user));
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
            return await this._repository.UpdateUser(this.ToDao(old));
        }

        public async Task<UserProfile> GetProfile(int id)
        {
            var dao =await this._profileRepository.Find(id);
        }
    }
}
