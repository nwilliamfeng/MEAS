using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MEAS.Data.MySql
{
    public static class MySqlExtensions
    {
        /// <summary>
        /// 添加分页sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static string Page(this string sql, int pageSize, int pageIndex)
        {
            if (string.IsNullOrEmpty(sql) || pageSize == 0)
                return sql;
            sql = sql.TrimEnd(';');
            return sql + string.Format(" LIMIT {0},{1}", pageSize * pageIndex, pageSize);
        }

        /// <summary>
        /// 查找1对多实体
        /// </summary>
        /// <typeparam name="P">主实体</typeparam>
        /// <typeparam name="C">主实体所包含的实体集合的实体类型</typeparam>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        public static IEnumerable<P> QueryOneToMany<P, C>(this System.Data.IDbConnection conn, string sql, Action<P, C> process)
         where P : Entity, new()
           where C : Entity, new()
        {
            var lookup = new Dictionary<int, P>();
            conn.Query<P, C, P>(sql, (a, b) =>
            {
                P parent;
                if (!lookup.TryGetValue(a.Id, out parent))
                    lookup.Add(a.Id, parent = a);
                process(parent, b);
                return parent;
            });
            var resultList = lookup.Values;
            return resultList;
        }

        /// <summary>
        /// 将多条sql语句关联起来
        /// </summary>
        /// <param name="sql1"></param>
        /// <param name="sql2"></param>
        /// <returns></returns>
        public static string JoinSql(this string sql1,params string[] otherSqls)
        {
            if (string.IsNullOrEmpty(sql1)|| otherSqls==null)
                return sql1;
            foreach (var sql in otherSqls)
                sql1 += ";" + sql;
            return sql1;
        }
        
    }
}
