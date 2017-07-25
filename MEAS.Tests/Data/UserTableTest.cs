using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEAS.Data;
using MEAS.Data.MySql;

namespace MEAS.Tests.Data
{
    [TestClass]
    public class UserTableTest
    {
        [TestMethod]
        public  async Task TestAppend()
        {
            IAccountRepository rp = new AccountRepository();
            var user = new UserInfo { LoginName = "login", Password = "1111", RoleString = "1,2,3" ,UserName="user"};
            var result =await rp.AppendUser(user);
            Assert.IsTrue(result);
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
            foreach(var user in result)
            Console.WriteLine(result);
            Assert.IsTrue(result.Count()>0);
        }

        [TestMethod]
        public async Task TestDelete()
        {
            AccountRepository rp = new AccountRepository();
            var user = new UserInfo { LoginName = "login", Password = "1111", RoleString = "1,2,3", UserName = "user" };
            await rp.AppendUser(user);
           var result =await rp.RemoveUser(user);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            AccountRepository rp = new AccountRepository();
            var user = await rp.Find("test", "1111");
            user.RoleString = "1,2,3,4,5,6";
            user.Password = "1234";
            var result =await rp.UpdateUser(user);
            Assert.IsTrue(result);
        }
    }
}
