using System;
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
            this.CreateMap<UserInfoDao, UserInfo>().AfterMap((a, b) => b.Roles = a.Roles?.Split(',')); 
        }

        public override string ProfileName
        {
            get { return "DaoToEntityMappingProfile"; }
        }

       
    }
}
