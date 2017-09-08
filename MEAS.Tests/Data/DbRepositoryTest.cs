using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEAS.Data;
using Dapper;
using MEAS.Data.SqlClient;

namespace MEAS.Tests.Data
{
    [TestClass]
    public class  DbRepositoryTest
    {
      

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
