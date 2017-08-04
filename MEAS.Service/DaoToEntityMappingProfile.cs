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

            this.CreateMap<TorqueWrenchMeasureTest, TorqueWrenchMeasureTestDao>();
            this.CreateMap<TorqueWrenchMeasureTestDao, TorqueWrenchMeasureTest>();
        }

        public override string ProfileName
        {
            get { return "DaoToEntityMappingProfile"; }
        }

       
    }
}
