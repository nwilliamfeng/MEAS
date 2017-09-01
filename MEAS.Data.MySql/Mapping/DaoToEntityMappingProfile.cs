using System;
using AutoMapper;
using MEAS.Data;

namespace MEAS.Data
{
    public class DaoToEntityMappingProfile : Profile
    {
        public DaoToEntityMappingProfile()
        {
            this.CreateMap<TorqueWrenchMeasureDao, TorqueWrenchMeasure>();
            this.CreateMap<UserInfoDao, UserInfo>().AfterMap((a, b) => b.Roles = a.Roles?.Split(','));
            this.CreateMap<UserProfileDao, UserProfile>();
        }

        public override string ProfileName
        {
            get { return "DaoToEntityMappingProfile"; }
        }

       
    }
}
