﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;

namespace MEAS.Data
{
    public static class DaoExtensions
    {

        
      

        public static TorqueWrenchMeasure ToEntity(this TorqueWrenchMeasureDao dao)
        {
        
            return Mapper.Map<TorqueWrenchMeasureDao, TorqueWrenchMeasure>(dao, x =>
            {
                x.AfterMap((a, b) =>
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<TorqueWrenchMeasure.TorqueWrenchMeasureData, TorqueWrenchMeasure.TorqueWrenchMeasureData>());
                    AutoMapper.Mapper.Map(Newtonsoft.Json.JsonConvert.DeserializeObject<TorqueWrenchMeasure.TorqueWrenchMeasureData>(a.Data), b.Data);
                });
            });
             
        }

        public static TorqueWrenchMeasure ToEntity2(this TorqueWrenchMeasureDao dao)
        {
            //Mapper.Initialize(cfg => cfg.CreateMap<string, TorqueWrenchMeasure.TorqueWrenchMeasureData>().AfterMap((a,b)=>
            //{
            //    b = Newtonsoft.Json.JsonConvert.DeserializeObject<TorqueWrenchMeasure.TorqueWrenchMeasureData>(a);
            //}));
            //Mapper.Initialize(x => x.CreateMap<TorqueWrenchMeasureDao, TorqueWrenchMeasure>().AfterMap((a, b) =>
            //{

            //    Mapper.Initialize(cfg => cfg.CreateMap<TorqueWrenchMeasure.TorqueWrenchMeasureData, TorqueWrenchMeasure.TorqueWrenchMeasureData>());
            //    AutoMapper.Mapper.Map(Newtonsoft.Json.JsonConvert.DeserializeObject<TorqueWrenchMeasure.TorqueWrenchMeasureData>(a.Data), b.Data);
            //}));

            Mapper.Initialize(x => x.CreateMap<TorqueWrenchMeasure, TorqueWrenchMeasureDao>().AfterMap((a, b) =>
            {
                b.Data = JsonConvert.SerializeObject(a.Data);
            }));
            

            return Mapper.Map<TorqueWrenchMeasure>(dao);

        }

      

        public static  TorqueWrenchMeasureDao  ToDao(this  TorqueWrenchMeasure  entity )
        {

            Mapper.Initialize(x => x.CreateMap<TorqueWrenchMeasure, TorqueWrenchMeasureDao>().AfterMap((a, b) =>
            {
                b.Data = JsonConvert.SerializeObject(a.Data);
            }));
            return Mapper.Map<TorqueWrenchMeasureDao>(entity);
         
        }

     
    
      
     
        
    }
}
