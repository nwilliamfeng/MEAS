using System;
using AutoMapper;
using MEAS.Data;

namespace MEAS.Service
{
    public class OtherMappingProfile : Profile
    {
        public OtherMappingProfile()
        {
            this.CreateMap<UserInfo, UserProfile>();
            
        }

        public override string ProfileName
        {
            get { return "OtherMappingProfile"; }
        }

       
    }
}
