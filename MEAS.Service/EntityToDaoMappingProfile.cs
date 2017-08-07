using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MEAS.Data;

namespace MEAS.Service
{
    public class EntityToDaoMappingProfile : Profile
    {
        public EntityToDaoMappingProfile()
        {

            this.CreateMap<TorqueWrenchMeasure, TorqueWrenchMeasureDao>();
        }

        public override string ProfileName
        {
            get { return "EntityToDaoMappingProfile"; }
        }

       
    }
}
