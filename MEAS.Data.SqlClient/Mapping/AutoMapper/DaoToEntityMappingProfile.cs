using System;
using AutoMapper;
using MEAS.Data;

namespace MEAS.Data
{
    internal class DaoToEntityMappingProfile : Profile
    {
        public DaoToEntityMappingProfile()
        {
            this.CreateMap<TorqueWrenchMeasureDao, TorqueWrenchMeasure>();
            this.CreateMap<UserInfoDao, UserInfo>().AfterMap((a, b) => b.Roles = a.Roles?.Split(','));
   
        }

        public override string ProfileName
        {
            get { return "DaoToEntityMappingProfile"; }
        }

       
    }
}
