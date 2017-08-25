using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEAS.Data;
using MEAS.Data.MySql;
using System.Diagnostics;

namespace MEAS.Tests.Data
{
    [TestClass]
    public class TorqueWrenchMeasureTableTest
    {
        [TestMethod]
        public  async Task TestAppendMeasure()
        {
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository();
            IAccountRepository accountRepository = new AccountRepository();
            var users = await accountRepository.LoadAll();
            Assert.IsTrue(users.Any());
           
            var measure = new TorqueWrenchMeasureDao { TestCode = DateTime.Now.ToShortDateString(), TestDate = DateTime.Now, Tester = users.First() };
            var result = await rp.Add(measure);
           
            Assert.IsTrue(result);

        }

        [TestMethod]
        public async Task TestFindWithId()
        {
            ITorqueWrenchMeasureRepository repository = new TorqueWrenchMeasureRepository();
            var result =await repository.FindWithId(8);
            if (result != null)
                result.Dump();
            Assert.IsTrue(result!=null);
        }

        [TestMethod]
        public void TestFindWithId2()
        {
            TestDb td = new Data.TestDb();
            td.DoTest3();
        }

        //[TestMethod]
        //public async Task TestLoadAll()
        //{
        //    AccountRepository rp = new AccountRepository();
        //    var result = await rp.LoadAll();
        //    foreach(var user in result)
        //    Console.WriteLine(result);
        //    Assert.IsTrue(result.Count()>0);
        //}

        //[TestMethod]
        //public async Task TestDelete()
        //{
        //    AccountRepository rp = new AccountRepository();
        //    var user = new UserInfoDao { LoginName = "login", Password = "1111", Roles = "1,2,3", UserName = "user" };
        //    await rp.AppendUser(user);
        //   var result =await rp.RemoveUser(user);
        //    Assert.IsTrue(result);
        //}

        //[TestMethod]
        //public async Task TestUpdate()
        //{
        //    AccountRepository rp = new AccountRepository();
        //    var user = await rp.Find("test", "1111");
        //    user.Roles = "1,2,3,4,5,6";
        //    user.Password = "1234";
        //    var result =await rp.UpdateUser(user);
        //    Assert.IsTrue(result);
        //}
    }
}
