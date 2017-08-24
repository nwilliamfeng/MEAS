using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Configuration;
using Dapper;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MEAS
{
    public abstract class DbRepository
    {
        
        protected const string ID = "Id";


        public bool IsTableExist()
        {
            //sqlserver:  string sql =string.Format( "SELECT * FROM dbo.SysObjects WHERE ID = object_id(N'[{0}]') AND OBJECTPROPERTY(ID, 'IsTable') = 1",TableName);

            var conn = this.CreateConnection();
            var sql = string.Format("select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA='{0}' and TABLE_NAME='{1}'", conn.Database, this.TableName);
            Console.WriteLine(sql); 
            return !string.IsNullOrEmpty( conn.QuerySingle<string>(sql));
        }
        

       

        protected IDbConnection CreateConnection()
        {
            return NewConnection();
        }

        public static IDbConnection NewConnection()
        {
            var connection = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            return new MySqlConnection(connection);
        }

       

        //protected void InitizeInsertCommand(DbCommand cmd, string table)
        //{

      
        //    string columns = null;
        //    string values = null;
        //    foreach (DbParameter p in cmd.Parameters)
        //    {
        //        columns += "[" + p.ParameterName + "],";
        //        values += "@" + p.ParameterName + ",";
        //    }
        //    if (columns.Length > 0)
        //    {
        //        columns = columns.TrimEnd(',');
        //        values = values.TrimEnd(',');
        //    }
        //    cmd.CommandText = string.Format("Insert into {0}({1}) values({2})", table, columns, values);
        //}


        //protected void InitizeUpdateCommand(DbCommand cmd, string table, int id)
        //{
        //    string columns = null;
        //    foreach (DbParameter p in cmd.Parameters)
        //        columns += string.Format("{0} = @{0},", p.ParameterName);
        //    if (columns.Length > 0)
        //        columns = columns.TrimEnd(',');
        //    cmd.CommandText = string.Format("Update  {0} Set {1} where {2}={3}", table, columns, ID, id);

        //}


        protected virtual string TableName
        {
            get
            {
                var tn = this.GetType().Name;
                var endwith = "Repository";
                return tn.EndsWith(endwith) ? tn.Replace(endwith, "s") : tn;
            }
        }


        protected async Task<bool> Delete(int id, string table)
        {
            var delete = string.Format("DELETE FROM {0} WHERE ({1}=@{1})", table, ID);
            return await this.CreateConnection().ExecuteAsync(delete, new { id }) > 0;
        }


    }
}
