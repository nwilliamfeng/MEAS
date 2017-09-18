using System;
using System.Linq;
using System.Collections.Generic;
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
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository();
          //   Environment ev = new Environment {Time=DateTime.Now , Address = "bnmb", Humidity = 12, Temperature = 56 };
        var ev = await EnvironmentRepository.Find(2);
            TorqueWrenchMeasureSetting setting = new TorqueWrenchMeasureSetting { NominalValues = new List<double>(new double[] { 10, 20, 30 }), TestCount = 3  };
            var measure = new TorqueWrenchMeasure { TestCode = DateTime.Now.ToShortDateString()+DateTime.Now.Millisecond .ToString(),  Tester = "fedf",Environment=ev ,Setting=setting };
            measure.Dump();

            var result = await rp.Add(measure);
            measure.Dump();
            Assert.IsTrue(result);

        }

        [TestMethod]
        public async Task TestFindWithId()
        {
            ITorqueWrenchMeasureRepository repository = new TorqueWrenchMeasureRepository();
            var result =await repository.Find(15);
           
            if (result != null)
                result.Dump();
            foreach (var nv in result.Setting.NominalValues)
                Console.WriteLine("nominal: "+nv); 
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
            
            var result = await rp.Remove(5);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository();
            var test =await  rp.Find(15);
         
            test.TestCode = "vbvbvb";
             test.Setting = new TorqueWrenchMeasureSetting { NominalValues = new List<double>(new double[] { 20, 40, 60 }), TestCount = 4 };
            //     test.Environment = await EnvironmentRepository.Find(3);

            var result =await rp.Update(test);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestUpdateChecker()
        {
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository();
            IAccountRepository accountRepository = new AccountRepository();
            var qr = await rp.FindWithCode("xyz");
            if (qr.Data.Count() == 0)
                return;
            TorqueWrenchMeasure test =await rp.Find( qr.Data.FirstOrDefault().Id);
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
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository();
            var test = await rp.Find(5);
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
