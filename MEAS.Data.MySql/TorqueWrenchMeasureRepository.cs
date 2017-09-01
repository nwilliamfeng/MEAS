using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace MEAS.Data.MySql
{
    public class TorqueWrenchMeasureRepository :DbRepository, ITorqueWrenchMeasureRepository
    {   
        private string Col_tstcode = "TestCode";
        private string Col_tsterId = "TesterId";
        private string Col_tstDate = "TestDate";

        public async Task<bool> Delete(int id)
        {
            var result = await this.CreateConnection().ExecuteAsync(string.Format("delete from {0} where id=@a",TableName), new { a = id });
            return result== 1;
        }

        public Task<SearchResult<TorqueWrenchMeasure>> FindWithCode(string code, int pagesize = 3, int pageIdx = 0)
        {
            return Task.Run(() =>
            {
                if (string.IsNullOrEmpty(code))
                    return SearchResult<TorqueWrenchMeasure>.Empty;
                var connection = this.CreateConnection();
                var sql1 = string.Format("select * from {0} t  join users u on t.testerid = u.id where {1} like @a", TableName, Col_tstcode).Page(pagesize, pageIdx);
                var sql2 = string.Format("select Count(*) from {0}  where {1} like @a", TableName, Col_tstcode);
                var sql = sql1.JoinSql(sql2);
                using (var reader = connection.QueryMultiple(sql, new { a = "%" + code + "%" }))
                {
                    var daos = reader.Read<TorqueWrenchMeasureDao, UserInfoDao, TorqueWrenchMeasureDao>((a, b) =>
                    {
                        a.Tester = b;
                        return a;
                    });
                    var total = reader.ReadFirstOrDefault<int>(); //读取总的记录数
                    return new SearchResult<TorqueWrenchMeasure>(daos.ToEntity(), total);
                }
            });          
        }

        public async Task<TorqueWrenchMeasure> FindWithId(int id)
        {
            //这里注意inner join和left join区别，前者需要满足两者条件，后者仅满足左表的条件    
            //query带的一个参数是splitOn，如果参数带上字段a，则从第一个字段开始比较，直到这个字段结束，之后字段信息会忽略，不返回。
            var sql = string.Format("select * from {0} t  join users u on t.testerid = u.id where t.id ={1}",TableName, id);
            var result = await this.CreateConnection().QueryAsync<TorqueWrenchMeasureDao, UserInfoDao, TorqueWrenchMeasureDao>(sql, (a, b) =>
            {
                a.Tester = b;
                return a;
            });       
            return result.FirstOrDefault()?.ToEntity();
        }

        public Task<SearchResult<TorqueWrenchMeasure>> Find(DateTime start, DateTime end, int pagesize = 3, int pageIdx = 0)
        {
            return Task.Run(() =>
            {
                var connection = this.CreateConnection();
                var sql1 = string.Format("select * from {0} t  join users u on t.testerid = u.id where {1}>=@a and {1}<=@b ", TableName, Col_tstDate).Page(pagesize, pageIdx);
                var sql2 = string.Format("select Count(*) from {0} where {1}>=@a and {1}<=@b", TableName, Col_tstDate);
                var sql = sql1.JoinSql(sql2);              
                using (var reader = connection.QueryMultiple(sql, new { a = start, b = end }))
                {
                    var daos = reader.Read<TorqueWrenchMeasureDao, UserInfoDao, TorqueWrenchMeasureDao>((a, b) =>
                    {
                        a.Tester = b;
                        return a;
                    });
                    var total = reader.ReadFirstOrDefault<int>(); //读取总的记录数
                    return new SearchResult<TorqueWrenchMeasure>(daos.ToEntity(), total);
                }               
            });
        }

        public async Task<bool> Add(TorqueWrenchMeasure measure)
        {
            var sql = string.Format("insert into {0} values(@a,@b,@c,@d)", this.TableName);
            var result = await this.CreateConnection().ExecuteAsync(sql, new { a = measure.Id, b = measure.TestCode, c = measure.Tester.Id, d = measure.TestDate })==1;
            if (result)
                measure.Id = this.GetInsertedId();
            return result;
        }

        public async Task<bool> Update(TorqueWrenchMeasure measure)
        {
            string sql = string.Format("UPDATE {0} SET {2} = @a, {3}=@b WHERE {1} = @{1}", TableName, ID, Col_tstcode, Col_tstDate);
            return await this.CreateConnection().ExecuteAsync(sql, new { a=measure.TestCode ,b=measure.TestDate }) > 0;
        }
    }
}
