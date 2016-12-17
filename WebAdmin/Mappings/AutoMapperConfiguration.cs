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

        }
        
    }
}