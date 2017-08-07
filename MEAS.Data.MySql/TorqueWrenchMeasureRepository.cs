using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data.MySql
{
    public class TorqueWrenchMeasureRepository : ITorqueWrenchMeasureRepository
    {
        private static List<TorqueWrenchMeasureDao> lst  ;

        public TorqueWrenchMeasureRepository()
        {
            if (lst == null)
            {
                lst = new List<TorqueWrenchMeasureDao>();
           
                for (int i = 0; i < 12; i++)
                    lst.Add(new TorqueWrenchMeasureDao { Id = 1+i, Code = (20151001+i).ToString(), TestDate = new DateTime(2016, i+1, 2+i, 7+i, 4+i, 0) });
                for (int i = 0; i < 12; i++)
                    lst.Add(new TorqueWrenchMeasureDao { Id = 1 + i, Code = (20160301 + i).ToString(), TestDate = new DateTime(2016, i+1, 8 + i, 8 + i, 10 + i, 0) });
                for (int i = 0; i < 12; i++)
                        lst.Add(new TorqueWrenchMeasureDao { Id = 1 + i, Code = (20170001 + i).ToString(), TestDate = new DateTime(2017, i+1, 15 + i, 9 + i, 20 + i, 0) });
            };
        }

        public Task<bool> Delete(int id)
        {
            return Task.Run(() =>
            {
                var old = lst.FirstOrDefault(x => x.Id == id);
                if (old == null)
                    return false;
                lst.Remove(old);
                return true;
            });
        }

        public Task<IEnumerable<TorqueWrenchMeasureDao>> FindWithCode(string code)
        {
            return Task.Run(() =>
            {
                return lst.Where(x => x.Code == code);
            });
        }

        public Task<TorqueWrenchMeasureDao> FindWithId(int productId)
        {
            return Task.Run(() =>
            {
                return lst.FirstOrDefault(x => x.Id == productId);
            });
        }

       public  Task<SearchResult<TorqueWrenchMeasureDao>> Find(DateTime start, DateTime end, int pagesize = 3, int pageIdx = 0)
        {
            return Task.Run(() =>
            {

                if (pageIdx < 0 || pagesize < 1)
                    return new SearchResult<TorqueWrenchMeasureDao>(new TorqueWrenchMeasureDao[] { },0);
                var daos = lst.Where(x => x.TestDate > start && x.TestDate < end).ToList();
                var data = daos.Skip(pagesize * (pageIdx)).Take(pagesize).ToList();
                foreach (var d in data)
                    System.Diagnostics.Debug.WriteLine(d);
                return new SearchResult<TorqueWrenchMeasureDao>(data, daos.Count);
            });
       
        }


    }
}
