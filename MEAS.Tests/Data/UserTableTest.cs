using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEAS.Data;
using MEAS.Data.SqlClient;
using AutoMapper;

namespace MEAS.Tests.Data
{
    [TestClass]
    public class UserTableTest
    {
        static UserTableTest()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<EntityToDaoMappingProfile>();
                x.AddProfile<DaoToEntityMappingProfile>();

                x.AddProfile<ViewModelToEntityMappingProfile>();
                x.AddProfile<EntityToViewModelMappingProfile>();
            });
        }

        [TestMethod]
        public  async Task TestAppendUser()
        {
            IAccountRepository rp = new AccountRepository();
            var user = new UserInfo { LoginName = "login4", Password = "1111", Roles = new string[] { "2", "4" }, UserName="user4"};
            user.Dump();
            var result =await rp.AppendUser(user);
            Assert.IsTrue(result);
            Console.WriteLine("after inserted..."); 
            user.Dump();
            Assert.IsTrue(user.Id > 0);
        }

        [TestMethod]
        public async Task TestFind()
        {
            AccountRepository rp = new AccountRepository();

            var result = await rp.Find("fw", "1111");
            Console.WriteLine(result);
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public async Task TestLoadAll()
        {
            AccountRepository rp = new AccountRepository();
            var result = await rp.LoadAll();
            foreach (var user in result)
                user.Dump();
            Assert.IsTrue(result.Count()>0);
        }

        [TestMethod]
        public async Task TestDelete()
        {
            AccountRepository rp = new AccountRepository();
            var user = new UserInfo { LoginName = "login", Password = "1111", Roles = new string[] { "2", "4" }, UserName = "user" };
            await rp.AppendUser(user);
           var result =await rp.RemoveUser(user);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            AccountRepository rp = new AccountRepository();
            var user = await rp.Find("test", "1111");
            user.Roles = new string[] { "2","4"};
            user.Password = "1234";
            var result =await rp.UpdateUser(user);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestModifyPassword()
        {
            AccountRepository rp = new AccountRepository();
            var user = await rp.Find("login2", "1111");
            user.Dump();


            var result = await rp.ModifyPassword(user.Id, "1234");
            Assert.IsTrue(result);
        }
    }
}
