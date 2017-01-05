using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TK.Model.Models;
using WebAdmin.Models;

namespace WebAdmin.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<SituationCategory,SituationCategoryViewModel>();
            Mapper.CreateMap<Situation, SituationViewModel>();
            Mapper.CreateMap<StatisticCategory, StatisticCategoryViewModel>();
            Mapper.CreateMap<Statistic, StatisticViewModel>();
            Mapper.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
            Mapper.CreateMap<ApplicationUser, ApplicationUserViewModel>();
            Mapper.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
            Mapper.CreateMap<Province, ProvinceViewModel>();
            Mapper.CreateMap<District, DistrictViewModel>();
            Mapper.CreateMap<Ward, WardViewModel>();
            Mapper.CreateMap<PoliceOrganization, PoliceOrganizationViewModel>();
            Mapper.CreateMap<ResolvedSituation, ResolvedSituationViewModel>();
        }
        
    }
}