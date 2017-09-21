using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEAS.Data;
using MEAS.Data.SqlClient;
using System.Diagnostics;
using AutoMapper;
using Newtonsoft.Json;

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
            var wrench = await new TorqueWrenchRepository().Find(1);
          
            var measure = new TorqueWrenchMeasure { TestCode = DateTime.Now.ToShortDateString()+DateTime.Now.Millisecond .ToString(),  Tester = "fedf",Environment=ev ,Measurand=wrench };
            measure.Standard = new TorqueStandard { Name = "sname", CertificateName = "certname" };
            measure.Data.ZeroPoint = 0.03;
            measure.Data.GagingPoints.Add(new TorqueMeasurePoint { Nominal = 10, Values = new List<double>(new double[] { 12, 13,14 }) });
            measure.Data.GagingPoints.Add(new TorqueMeasurePoint { Nominal = 20, Values = new List<double>(new double[] { 22, 23,24 }) });
            measure.Data.GagingPoints.Add(new TorqueMeasurePoint { Nominal = 30, Values = new List<double>(new double[] { 32, 33,34 }) });
            
            measure.Dump();


            var result = await rp.Add(measure);
            measure.Dump();
            Assert.IsTrue(result);

        }

        [TestMethod]
        public async Task TestFindWithId()
        {
            ITorqueWrenchMeasureRepository repository = new TorqueWrenchMeasureRepository();
            var result =await repository.Find(18);
            
            if (result != null)
            {
               result.Dump();
                foreach (var g in result.Measurand.Owner.Contacts)
                    g.Dump();
                foreach (var g in result.Data .GagingPoints)
                Console.WriteLine(g.Nominal);
            }
            
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
            TorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository();
            var test =await  rp.Find(1);

            // test.Dump();

            //   test.Measurand = await new TorqueWrenchRepository().Find(5);
            TorqueWrenchProduct product = await new TorqueWrenchProductRepository().Find(7);
            Customer customer = await new CustomerRepository().Find(5);
            test.Measurand = new TorqueWrench { ManufactureDate = DateTime.Now, Product = product, Owner = customer, SerialNumber = "bnmb" };

            test.TestCode = "123";
            test.Data.ZeroPoint = 121 ;
            test.Data.GagingPoints[0].Nominal = 111;
            test.Data.GagingPoints[1].Values[1] = 111;
            test.Environment = await EnvironmentRepository.Find(2);
   
          //  test.Environment = new Environment { Time = DateTime.Now, Address = "abcded", Humidity = 12, Temperature =64 };
            


            var result =await rp.Update(test);
         //   test.Dump();
          
            Assert.IsTrue(result);
        }



        [TestMethod]
        public async Task TestUpdateChecker()
        {
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository();
            var sr = await rp.Find("sn223");
            foreach (var tst in sr.Data)
                tst.Dump();
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
            et.Dump();
            et.Address = "gggg";
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
