using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEAS.Data;
using MEAS.Data.SqlClient;
using System.Diagnostics;
using AutoMapper;

namespace MEAS.Tests.Data
{
    [TestClass]
    public class TorqueWrenchMeasureTableTest
    {
        private static IEnvironmentRepository EnvironmentRepository { get; set; }
        static TorqueWrenchMeasureTableTest()
        {
            EnvironmentRepository = new EnvironmentRepository();
           
        }

        [TestMethod]
        public  async Task TestAppendMeasure()
        {
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository(EnvironmentRepository);
            //    Environment ev = new Environment {Time=DateTime.Now , Address = "abc", Humidity = 23.5, Temperature = 20.4 };
            var ev = await EnvironmentRepository.Find(2);
            var measure = new TorqueWrenchMeasure { TestCode = DateTime.Now.ToShortDateString()+DateTime.Now.Millisecond .ToString(),  Tester = "rwerwewer",Environment=ev  };
            measure.Dump();

            var result = await rp.Add(measure);
            measure.Dump();
            Assert.IsTrue(result);

        }

        [TestMethod]
        public async Task TestFindWithId()
        {
            ITorqueWrenchMeasureRepository repository = new TorqueWrenchMeasureRepository(EnvironmentRepository);
            var result =await repository.FindWithId(5);
            result.Checker = "bbb";
            result.Environment  = new Environment { Time = DateTime.Now, Address = "vbv", Humidity = 23.5, Temperature = 20.4 }; ;
          await  repository.Update(result);
            if (result != null)
                result.Dump();
            Assert.IsTrue(result!=null);
        }


        [TestMethod]
        public async Task TestPage()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository(EnvironmentRepository);
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
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository(EnvironmentRepository);
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
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository(EnvironmentRepository);
            
            var result = await rp.Delete(5);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository(EnvironmentRepository);
            var test =await  rp.FindWithId(5);
         
            test.TestCode = "cvbnm";
         //      test.Environment = await EnvironmentRepository.Find(3);
             test.Environment.Address = "abcdef";
            var result =await rp.Update(test);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestUpdateChecker()
        {
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository(EnvironmentRepository);
            IAccountRepository accountRepository = new AccountRepository();
            var qr = await rp.FindWithCode("xyz");
            if (qr.Data.Count() == 0)
                return;
            TorqueWrenchMeasure test =await rp.FindWithId( qr.Data.FirstOrDefault().Id);
            var users = await accountRepository.LoadAll();
            
            //if (test == null)
            //{
            //    test = new TorqueWrenchMeasure { TestCode = "xyz", TestDate = DateTime.Now, Tester = users.First() };
            //    await rp.Add(test);
            //}
            test.Checker = "fvd";
          
            var result = await rp.Update(test);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestTimestamp()
        {
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository(EnvironmentRepository);
            var test = await rp.FindWithId(5);
            if (test != null)
                test.Dump();
            Assert.IsTrue(test != null);

            test.TestCode = "xxx";
            await rp.Update(test);
            var result = await rp.Update(test);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestUpdateEnvironment()
        {
          
          var et= await EnvironmentRepository.Find(1);
            et.Address = "dfdfs";
          await  EnvironmentRepository.Update(et);
            et.Dump();
        }

        [TestMethod]
        public async Task TestAddEnvironment()
        {
            Environment ev = new Environment { Address = "vbn", Temperature = 23.45, Humidity = 45.6 };
            var et = await EnvironmentRepository.Add(ev);
            ev.Dump();
        }
    }
}
