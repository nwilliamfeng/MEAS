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
            this.CreateMap<TorqueWrenchMeasureDao, TorqueWrenchMeasure>();
            this.CreateMap<TorqueWrenchMeasureSettingDao, TorqueWrenchMeasureSetting>().AfterMap((x,y) =>
            {
                y.NominalValues = JsonConvert.DeserializeObject<ICollection<double>>(x.NominalValuesString);
            });
     }

        public override string ProfileName
        {
            get { return "DaoToEntityMappingProfile"; }
        }

       
    }
}
