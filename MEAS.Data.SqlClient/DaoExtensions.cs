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

        public static  TorqueWrenchMeasure  ToEntity(this  TorqueWrenchMeasureDao  dao)
        {
            return  Mapper.Map<TorqueWrenchMeasure>(dao);
        }

        public static  TorqueWrenchMeasureDao  ToDao(this  TorqueWrenchMeasure  entity )
        {
            return  Mapper.Map<TorqueWrenchMeasureDao>(entity);
        }

     
    
      
     
        
    }
}
