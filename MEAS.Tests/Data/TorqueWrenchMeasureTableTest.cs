using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEAS.Data;
using MEAS.Data.SqlClient;
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
           
            var measure = new TorqueWrenchMeasure { TestCode = DateTime.Now.ToShortDateString(), TestDate = DateTime.Now, Tester = users.First() };
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
        public async Task TestPage()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository();
            var result = await rp.Find(new DateTime(2017, 8, 25), new DateTime(2017, 8, 27), 2, 0);
            sw.Stop();
            Console.WriteLine("cost "+sw.ElapsedMilliseconds);
            foreach (var d in result.Data)
                d.Dump();
            Console.WriteLine("count "+ result.TotalCount);
        }

        [TestMethod]
        public async Task TestQueryCode()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository();
            var result = await rp.FindWithCode("017/8/25");
            sw.Stop();
            Console.WriteLine("cost " + sw.ElapsedMilliseconds);
            foreach (var d in result.Data)
                d.Dump();
            Console.WriteLine("count " + result.TotalCount);
        }



        [TestMethod]
        public async Task TestDelete()
        {
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository();

            var result = await rp.Delete(11);
            Assert.IsTrue(result);
        }

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
