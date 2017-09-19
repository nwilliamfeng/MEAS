using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace MEAS.Data
{
    public static class DaoExtensions
    {
        public static IEnumerable<TorqueWrenchMeasure> ToEntity(this IEnumerable<TorqueWrenchMeasureDao> daos)
        {
            return daos.Select(x => Mapper.Map<TorqueWrenchMeasure>(x));
        }

        public static IEnumerable<TorqueWrenchMeasureDao> ToDao(this IEnumerable<TorqueWrenchMeasure> entitys)
        {
            return entitys.Select(x => Mapper.Map<TorqueWrenchMeasureDao>(x));
        }

        public static TorqueWrenchMeasure ToEntity(this TorqueWrenchMeasureDao dao)
        {
            var result = Mapper.Map<TorqueWrenchMeasureDao, TorqueWrenchMeasure>(dao, x =>
            {
                x.AfterMap((a, b) =>
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<TorqueWrenchMeasure.TorqueWrenchMeasureData, TorqueWrenchMeasure.TorqueWrenchMeasureData>());
                    AutoMapper.Mapper.Map(Newtonsoft.Json.JsonConvert.DeserializeObject<TorqueWrenchMeasure.TorqueWrenchMeasureData>(a.Data), b.Data);
                });
            });
            return result;
        }

        public static  TorqueWrenchMeasureDao  ToDao(this  TorqueWrenchMeasure  entity )
        {
            
            return  Mapper.Map<TorqueWrenchMeasure, TorqueWrenchMeasureDao>(entity,x=>
            {
                x.AfterMap((a, b) =>
                {
                 
                      b.Data=   Newtonsoft.Json.JsonConvert.SerializeObject(a.Data);
                });
            }  );
        }

     
    
      
     
        
    }
}
