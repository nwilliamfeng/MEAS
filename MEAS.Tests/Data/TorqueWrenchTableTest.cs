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
    public class TorqueWrenchTableTest
    {
      
  

        [TestMethod]
        public  async Task TestAppendTorqueWrench()
        {
            ITorqueWrenchRepository rp = new TorqueWrenchRepository();
            TorqueWrenchProduct product = new TorqueWrenchProduct { Manufacturer = "manfucv",Model="model3",Name="tname2", MinRange =200, MaxRange = 2000, WorkDirection = WorkDirection.Bidirectional };
            Customer customer = new Customer { Address = "address3", Name = "cmpname2", ContactPhone = "123213" };
            customer.Contacts.Add(new CustomerContact { Company = customer, FirstName = "tom2", LastName = "cluse2", Mobile = "654255" });

            TorqueWrench wrench = new TorqueWrench { ManufactureDate = DateTime.Now, Product = product, Owner = customer ,SerialNumber="vbn-098"};
       
            var result = await rp.Add(wrench );
             wrench .Dump();
            Assert.IsTrue(result);

        }

        [TestMethod]
        public async Task TestAppendTorqueWrenchWithExistProduct()
        {
            ITorqueWrenchRepository rp = new TorqueWrenchRepository();
           
             TorqueWrenchProduct product = await new TorqueWrenchProductRepository().Find(1);
            Customer customer = await new CustomerRepository().Find(1016);
             
            TorqueWrench wrench = new TorqueWrench { ManufactureDate = DateTime.Now, Product = product, Owner = customer, SerialNumber = "1234567" };

            var result = await rp.Add(wrench);
            wrench.Dump();
            Assert.IsTrue(result);

        }

        [TestMethod]
        public async Task TestFindWrenchWithId()
        {
            ITorqueWrenchRepository rp = new TorqueWrenchRepository();
            var result =await rp.Find(2);
            result.Dump();
             Assert.IsTrue(result!=null);
        }


        

   


        [TestMethod]
        public async Task TestDeleteWrench()
        {
            ITorqueWrenchRepository rp = new TorqueWrenchRepository();

            var result = await rp.Remove(1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestUpdateWrench()
        {
            ITorqueWrenchRepository rp = new TorqueWrenchRepository();
            var wrench = await rp.Find(5);
            wrench.Dump();
 
            wrench.SerialNumber = "dgh";
             var customer =await new CustomerRepository().Find(6);
              var product = await new TorqueWrenchProductRepository().Find(10);
           // var product = new TorqueWrenchProduct { Manufacturer = "nmb", MaxRange = 100, MinRange = 10, WorkDirection = WorkDirection.Clockwise, Model = "modelx" , Name="tnxx"};
            wrench.Owner = customer;
            wrench.Product = product;
            var result =await rp.Update(wrench);
            wrench.Dump();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestFindWrenchWithRange()
        {
            ITorqueWrenchRepository rp = new TorqueWrenchRepository();
            var sr =await rp.FindWithRange(100, 1000);
            //foreach(var wrench in sr.Data)
            //    wrench.Dump();
            Assert.IsTrue(sr.Data.Count()>0);
        }
       
       
        
    }
}
