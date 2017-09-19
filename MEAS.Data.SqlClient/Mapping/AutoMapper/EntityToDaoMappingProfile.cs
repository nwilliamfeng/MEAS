using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MEAS.Data;
using Newtonsoft.Json;

namespace MEAS.Data
{
    internal class EntityToDaoMappingProfile : Profile
    {
        public EntityToDaoMappingProfile()
        {

            this.CreateMap<TorqueWrenchMeasure, TorqueWrenchMeasureDao>().AfterMap((a, b) =>
            {
                 b.Data = JsonConvert.SerializeObject(a.Data);
            });
            //this.CreateMap<TorqueWrenchMeasureSetting , TorqueWrenchMeasureSettingDao>().AfterMap((x, y) =>
            //{
            //    y.NominalValuesString = JsonConvert.SerializeObject(x.NominalValues);
            //});

        }

        public override string ProfileName
        {
            get { return "EntityToDaoMappingProfile"; }
        }

      
       
    }
}
