using System;
using System.Collections.Generic;
using AutoMapper;
using MEAS.Data;
using Newtonsoft.Json;


namespace MEAS.Data
{
    internal class DaoToEntityMappingProfile : Profile
    {
        public DaoToEntityMappingProfile()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TorqueWrenchMeasure.TorqueWrenchMeasureData, TorqueWrenchMeasure.TorqueWrenchMeasureData>());
            this.CreateMap<TorqueWrenchMeasureDao, TorqueWrenchMeasure>().AfterMap((x, y) => //注意在aftermap里打Console.writeline无效
            {
                var data = JsonConvert.DeserializeObject<TorqueWrenchMeasure.TorqueWrenchMeasureData>(x.Data);
       
                Mapper.Map( data,y.Data );

            });
          
     }

        public override string ProfileName
        {
            get { return "DaoToEntityMappingProfile"; }
        }

       
    }
}
