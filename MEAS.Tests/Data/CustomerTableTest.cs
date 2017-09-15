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
        private static IEnvironmentRepository EnvironmentRepository { get; set; }
        static CustomerTableTest()
        {
            EnvironmentRepository = new EnvironmentRepository();
            Mapper.Initialize(x =>
            {
                x.AddProfile<EntityToDaoMappingProfile>();
                x.AddProfile<DaoToEntityMappingProfile>();

                x.AddProfile<ViewModelToEntityMappingProfile>();
                x.AddProfile<EntityToViewModelMappingProfile>();
            });
        }

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
            Customer customer = await rp.Find(4);
            customer.Address = "keen";
            customer.Contacts.First().LastName = "aaa";
            customer.Contacts.ElementAt(1).FirstName = "bbb";
            customer.Contacts.Remove(customer.Contacts.Last());
            customer.Contacts.Add(new CustomerContact { Company = customer, LastName = "五", FirstName = "赵" });
        //    customer.Contacts.Add(new CustomerContact { Company = customer, LastName = "粑粑", FirstName = "周" });
           // customer.Contacts.Add(new CustomerContact { Company = customer, LastName = "三", FirstName = "王" });
           
          

            var result = await rp.Update(customer);
            //customer.Dump();
            //foreach (var d in customer.Contacts)
            //    d.Dump();
            Assert.IsTrue(result);

        }

        [TestMethod]
        public async Task TestFindCustomerWithId()
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
        public async Task TestFindCustomerWithPage()
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

        [TestMethod]
        public async Task TestUpdateCustomer()
        {
            ITorqueWrenchMeasureRepository rp = new TorqueWrenchMeasureRepository(EnvironmentRepository);
            var test =await  rp.FindWithId(5);
         
            test.TestCode = "cvbnm";
         //      test.Environment = await EnvironmentRepository.Find(3);
             test.Environment.Address = "abcdef";
            var result =await rp.Update(test);
            Assert.IsTrue(result);
        }

    

   
    }
}
