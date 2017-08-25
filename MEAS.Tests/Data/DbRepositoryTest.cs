using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEAS.Data;
using Dapper;
using MEAS.Data.MySql;

namespace MEAS.Tests.Data
{
    [TestClass]
    public class  DbRepositoryTest
    {
        [TestMethod]
        public  void TestAppendProfile()
        {
            UserProfileRepository rp = new UserProfileRepository();
           
            Assert.IsTrue(rp.IsTableExist());

     
        }

        [TestMethod]
       public void TestSum()
        {
            var count = DbRepository.NewConnection().QuerySingle<int>("select count(*) from users");
            Console.WriteLine(count);
            Assert.IsTrue(count>0);
        }

        [TestMethod]
        public void Test()
        {
            Console.WriteLine("a: "+default(DBNull));
            Console.WriteLine("b: " + DBNull.Value);
            Assert.AreEqual(default(DBNull), DBNull.Value);
            //var connection = DbRepository.NewConnection();
            //var str = await connection.QueryFirstAsync<string>(new CommandDefinition("select 'abc' as [Value] union all select @txt", new { txt = "def" })).ConfigureAwait(false);
            //Assert.Equal("abc", str);
        }


    }
}
