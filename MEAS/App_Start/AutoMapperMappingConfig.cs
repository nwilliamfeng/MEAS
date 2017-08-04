using System;
using AutoMapper;
using MEAS.Service;

namespace MEAS
{
    /// <summary>
    /// AutoMapper映射配置
    /// </summary>
    public static class AutoMapperMappingConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<EntityToDaoMappingProfile>();
                x.AddProfile<DaoToEntityMappingProfile>();
            });
        }

       
    }
}