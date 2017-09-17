using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MEAS.Data;

namespace MEAS.Data
{
    internal class EntityToDaoMappingProfile : Profile
    {
        public EntityToDaoMappingProfile()
        {

            this.CreateMap<TorqueWrenchMeasure, TorqueWrenchMeasureDao>();
       
            this.CreateMap<UserInfo, UserInfoDao>().AfterMap((a, b) =>
             {
                 if (a.Roles != null)
                     b.Roles = string.Join(",", a.Roles);
             });
          
        }

        public override string ProfileName
        {
            get { return "EntityToDaoMappingProfile"; }
        }

      
       
    }
}
