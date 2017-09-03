using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace MEAS.Data
{
    public static class DaoExtensions
    {
        public static IEnumerable<TorqueWrenchMeasure> ToEntity(this IEnumerable<TorqueWrenchMeasureDao> daos)
        {
            return daos.Select(x => Mapper.Map<TorqueWrenchMeasure>(x));
        }

        public static IEnumerable<TorqueWrenchMeasureDao> ToDao(this IEnumerable<TorqueWrenchMeasure> entitys)
        {
            return entitys.Select(x => Mapper.Map<TorqueWrenchMeasureDao>(x));
        }

        public static  TorqueWrenchMeasure  ToEntity(this  TorqueWrenchMeasureDao  dao)
        {
            return  Mapper.Map<TorqueWrenchMeasure>(dao);
        }

        public static  TorqueWrenchMeasureDao  ToDao(this  TorqueWrenchMeasure  entity )
        {
            return  Mapper.Map<TorqueWrenchMeasureDao>(entity);
        }

        public static IEnumerable<UserInfo> ToEntity(this IEnumerable<UserInfoDao> daos)
        {
            return daos.Select(x => Mapper.Map<UserInfo>(x));
        }

        public static IEnumerable<UserInfoDao> ToDao(this IEnumerable<UserInfo> entitys)
        {
            return entitys.Select(x => Mapper.Map<UserInfoDao>(x));
        }

        public static UserInfo ToEntity(this UserInfoDao dao)
        {
            return Mapper.Map<UserInfo>(dao);
        }

        public static UserInfoDao ToDao(this UserInfo entity)
        {
            return Mapper.Map<UserInfoDao>(entity);
        }

        public static IEnumerable<UserProfile> ToEntity(this IEnumerable<UserProfileDao> daos)
        {
            return daos.Select(x => Mapper.Map<UserProfile>(x));
        }

        public static IEnumerable<UserProfileDao> ToDao(this IEnumerable<UserProfile> entitys)
        {
            return entitys.Select(x => Mapper.Map<UserProfileDao>(x));
        }

        public static UserProfile ToEntity(this UserProfileDao dao)
        {
            return Mapper.Map<UserProfile>(dao);
        }

        public static UserProfileDao ToDao(this UserProfile entity)
        {
            return Mapper.Map<UserProfileDao>(entity);
        }
    }
}
