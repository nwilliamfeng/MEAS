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
            TorqueWrenchProduct product = new TorqueWrenchProduct { Manufacturer = "manufactor",Model="model",Name="tname", MinRange = 100, MaxRange = 1000, WorkDirection = WorkDirection.Bidirectional };
            Customer customer = new Customer { Address = "address", Name = "cmpname", ContactPhone = "234234" };
            customer.Contacts.Add(new CustomerContact { Company = customer, FirstName = "tom", LastName = "cluse", Mobile = "1232325345" });

            TorqueWrench wrench = new TorqueWrench { ManufactureDate = DateTime.Now, Product = product, Owner = customer ,SerialNumber="sn223"};
       
            var result = await rp.Add(wrench );
             wrench .Dump();
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
            var wrench = await rp.Find(4);
            wrench.Dump();

            wrench.SerialNumber = "897";
            var newCustomer =await new CustomerRepository().Find(6);
            var newProduct = await new TorqueWrenchProductRepository().Find(7);
            wrench.Owner = newCustomer;
            wrench.Product = newProduct;
            var result =await rp.Update(wrench);
            wrench.Dump();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestFindWrenchWithRange()
        {
            ITorqueWrenchRepository rp = new TorqueWrenchRepository();
            var sr =await rp.FindWithRange(20, 50);
            foreach(var wrench in sr.Data)
                wrench.Dump();
            Assert.IsTrue(sr.Data.Count()>0);
        }
       
       
        
    }
}
