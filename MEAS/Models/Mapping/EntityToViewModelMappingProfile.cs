using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MEAS.Models;

namespace MEAS
{
    public class EntityToViewModelMappingProfile : Profile
    {
        public EntityToViewModelMappingProfile()
        {

            this.CreateMap<UserInfo, UserProfileViewModel>();
        }

        public override string ProfileName
        {
            get { return "EntityToViewModelMappingProfile"; }
        }


    }
}