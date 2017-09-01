using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEAS.Data;
using MEAS.Data.MySql;

namespace MEAS.Tests.Data
{
    [TestClass]
    public class UserProfileTableTest
    {
        [TestMethod]
        public  async Task TestAppendProfile()
        {
            IUserProfileRepository rp = new UserProfileRepository();
            var user = new UserProfile { Id=32, EmailAddress="ff@163.com", Mobile="12323423435",Phone="43654454" };
            using (var stream = File.Open(@"e:\photos\avatar1.png", FileMode.Open))
            {
               user.Avatar= stream.ToBytes();
            };
          
            var result =await rp.Append(user);
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
            user.Roles = new string[] { "1", "2", "3", "7" };
            user.Password = "1234";
            var result =await rp.UpdateUser(user);
            Assert.IsTrue(result);
        }

       
    }
}
