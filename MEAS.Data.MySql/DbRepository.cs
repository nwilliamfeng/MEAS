using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MEAS
{
    public abstract class DbRepository
    {
        
        protected const string ID = "Id";
       
       

        protected DbConnection CreateConnection()
        {
            var connection = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            return new MySqlConnection(connection); 
        }

         


        protected void InitizeInsertCommand(DbCommand cmd, string table)
        {
            string columns = null;
            string values = null;
            foreach (DbParameter p in cmd.Parameters)
            {
                columns += "[" + p.ParameterName + "],";
                values += "@" + p.ParameterName + ",";
            }
            if (columns.Length > 0)
            {
                columns = columns.TrimEnd(',');
                values = values.TrimEnd(',');
            }
            cmd.CommandText = string.Format("Insert into {0}({1}) values({2})", table, columns, values);
        }

         
        protected void InitizeUpdateCommand(DbCommand cmd, string table, int id)
        {
            string columns = null;
            foreach (DbParameter p in cmd.Parameters)
                columns += string.Format("{0} = @{0},", p.ParameterName);
            if (columns.Length > 0)
                columns = columns.TrimEnd(',');
            cmd.CommandText = string.Format("Update  {0} Set {1} where {2}={3}", table, columns, ID, id);
        }

 

    }
}
