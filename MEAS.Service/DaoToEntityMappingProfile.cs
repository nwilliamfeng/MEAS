using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MEAS.Data;

namespace MEAS.Service
{
    public class DaoToEntityMappingProfile : Profile
    {
        public DaoToEntityMappingProfile()
        {

            this.CreateMap<TorqueWrenchMeasure, TorqueWrenchMeasureDao>();
            this.CreateMap<TorqueWrenchMeasureDao, TorqueWrenchMeasure>();
        }

        public override string ProfileName
        {
            get { return "DaoToEntityMappingProfile"; }
        }

       
    }
}
