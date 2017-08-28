using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace MEAS.Tests.Data
{
    public class TestDb
    {
        private const string connstr = @"Data Source=DESKTOP-07EN549\LVB;Initial Catalog=testdb;MultipleActiveResultSets=true;User ID=sa;Password=2004v704";

        public void DoTest()
        {
            SqlConnection connection = new SqlConnection(connstr);
            //var sql = string.Format("select t.*,u.*,x.* from (orders as t  inner join customers as u on t.customerid = u.id) inner join orderdetails as x on t.detailId= x.id where t.id ={0}",2);
            var sql = "select t.*,u.* from (orders as t  inner join customers as u on t.customerid = u.id)  ";
            //var result = connection.Query<Order, Customer, Order>(sql, (a, b) =>
            // {
            //     a.Customer = b;

            //     return a;
            // }, splitOn: "CustomerId,DetailId");



            var result = connection.Query<Order, Customer, Order>(sql, (a, b) =>
            {
                a.Customer = b;

                return a;
            });
            if (result != null)
                foreach(var d in result)
                Console.WriteLine(d.ToString()); 
        }


        public void DoTest2()
        {
            SqlConnection connection = new SqlConnection(connstr);
             var sql = string.Format("select t.*,u.*,x.* from (orders as t  inner join customers as u on t.customerid = u.id) inner join orderdetails as x on t.detailId= x.id where t.id ={0}",2);
         
            var result = connection.Query<Order, Customer,OrderDetail, Order>(sql, (a, b,c) =>
             {
                 a.Customer = b;
                 a.Detail = c;
                 return a;
             } );

         
            if (result != null)
                foreach (var d in result)
                    Console.WriteLine(d.ToString());
        }

        public void DoTest3()
        {
            SqlConnection connection = new SqlConnection(connstr);
            var sql =  @"select *  from orders 
                                select count(*) from orders";
            using (var reader = connection.QueryMultiple(sql))
            {
                var orders = reader.Read<Order>();
                foreach (var order in orders)
                    Console.WriteLine(order);
                var count = reader.ReadSingle<int>();
                Console.WriteLine("the count " + count);             
            }            
        }


        public void DoTest4()
        {
            SqlConnection connection = new SqlConnection(connstr);
      
            var sql = @"select  count(*)  from orders";
          var count =  connection.ExecuteScalar<int>(sql);
            Console.WriteLine("count "+count); 
        }


        public void DoTest5()
        {
            CategoryRepository cr = new Tests.CategoryRepository();
            cr.LoadAll();
        }

    }


   

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("id:{0},name{1},age{2}", Id, Name, Age);
        }
    }

    public class Order
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public Customer Customer { get; set; }

       public OrderDetail Detail { get; set; }

        public DateTime OrderDate { get; set; }

        public int Count { get; set; }

        public override string ToString()
        {
           return string.Format("id:{0},code:{1},orderdate:{2},customer:{3},details:{4},count:{5}", Id, Code, OrderDate,Customer?.ToString(),Detail,Count);
          //  return string.Format("id:{0},code:{1},orderdate:{2},customer:{3}", Id, Code, OrderDate, Customer?.ToString());
        }
    }

    public class OrderDetail
    {
        public int Id { get; set; }
        public string Detail { get; set; }

        public override string ToString()
        {
            return    string.Format("id:{0},detail:{1} ", Id, Detail);
        }
    }
}
