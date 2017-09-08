using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MEAS.Models;

namespace MEAS
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {

            this.CreateMap<UserProfileViewModel, UserInfo>();
        }

        public override string ProfileName
        {
            get { return "ViewModelToEntityMappingProfile"; }
        }


    }
}