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
    public class CustomerTableTest
    {
     
        

        [TestMethod]
        public  async Task TestAddCustomer()
        {
            ICustomerRepository rp = new CustomerRepository( );
            Customer customer = new Customer { Name = "abc", Address = "dddd" };
            customer.Contacts.Add(new CustomerContact {  Company =customer , LastName="三",FirstName ="张"  });
            customer.Contacts.Add(new CustomerContact { Company = customer, LastName = "二", FirstName = "王" });
           var result= await rp.Add(customer);
           customer.Dump();
 
            Assert.IsTrue(result);

        }

        [TestMethod]
        public async Task TestLoadCustomer()
        {
            ICustomerRepository rp = new CustomerRepository();
            Customer customer = await rp.Find(1);
           //  customer.Dump();
            Assert.IsTrue(customer!=null);
            foreach (var c in customer.Contacts)
                Console.WriteLine(c.FullName); 
            Assert.IsTrue(customer.Contacts.Count == 2);
         
        }

        [TestMethod]
        public async Task TestRemoveCustomerContact()
        {
            ICustomerRepository rp = new CustomerRepository();
            Customer customer = await rp.Find(3);

            customer.Contacts.Remove(customer.Contacts.Last());
            var result = await rp.Update(customer);
            customer.Dump();
            foreach (var d in customer.Contacts)
                d.Dump();
            Assert.IsTrue(result);

        }

        [TestMethod]
        public async Task TestAddCustomerContact()
        {
            ICustomerRepository rp = new CustomerRepository();
            Customer customer = await rp.Find(8);
            customer.Dump();
            Console.WriteLine("-----------------分割线----------------------------"); 
            customer.Address = "fdf";
            customer.Contacts.First().LastName = "sdf";
            customer.Contacts.ElementAt(1).FirstName = "abcd";
            customer.Contacts.Remove(customer.Contacts.Last());
            customer.Contacts.Add(new CustomerContact { Company = customer, LastName = "cv", FirstName = "45" });
        //    customer.Contacts.Add(new CustomerContact { Company = customer, LastName = "粑粑", FirstName = "周" });
           // customer.Contacts.Add(new CustomerContact { Company = customer, LastName = "三", FirstName = "王" });
           
          

            var result = await rp.Update(customer);
            customer.Dump();
          
            Assert.IsTrue(result);

        }

        [TestMethod]
        public async Task TestFindCustomerWithId()
        {
           
        }

       

        [TestMethod]
        public async Task TestFindCustomerWithPage()
        {
            
        }

       


        [TestMethod]
        public async Task TestDeleteCustomerWithId()
        {
            CustomerRepository rp = new CustomerRepository();            
            var result = await rp.Remove(1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestDeleteCustomer()
        {
            ICustomerRepository rp = new CustomerRepository();
            var customer = await rp.Find(2);
            var result = await rp.Remove(customer);
            Assert.IsTrue(result);
        }

      

    

   
    }
}
