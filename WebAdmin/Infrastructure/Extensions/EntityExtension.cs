using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TK.Model.Models;
using WebAdmin.Models;

namespace WebAdmin.Infrastructure.Extensions
{
    public static class EntityExtension
    {
        public static void UpdateSituation(this Situation situation, SituationViewModel situationVm)
        {
            situation.ID = situationVm.ID;
            situation.Name = situationVm.Name;
            situation.Alias = situationVm.Alias;
            situation.SituationCategoryID = situationVm.SituationCategoryID;
            situation.Image = situationVm.Image;
            situation.MoreImages = situationVm.MoreImages;
            situation.Description = situationVm.Description;
            situation.Content = situationVm.Content;
            situation.HomeFlag = situationVm.HomeFlag;
            situation.HotFlag = situationVm.HotFlag;
            situation.ViewCount = situationVm.ViewCount;
            situation.Tags = situationVm.Tags;
            situation.ProvinceID = situationVm.ProvinceID;
            situation.DistrictID = situationVm.DistrictID;
            situation.WardID = situationVm.WardID;
            situation.Hamlet = situationVm.Hamlet;
            situation.TheInjured = situationVm.TheInjured;
            situation.TheDead = situationVm.TheDead;
            situation.PropertyDamage = situationVm.PropertyDamage;
            situation.PoliceOrganizationID = situationVm.PoliceOrganizationID;
            situation.ResolvedSituationID = situationVm.ResolvedSituationID;
            situation.CreatedDate = situationVm.CreatedDate;
            situation.CreatedBy = situationVm.CreatedBy;
            situation.UpdatedBy = situationVm.UpdatedBy;
            situation.UpdatedDate = situationVm.UpdatedDate;
            situation.MetaKeyword = situationVm.MetaKeyword;
            situation.MetaDescription = situationVm.MetaDescription;
            situation.Status = situationVm.Status;

        }
        public static void UpdateStatistic(this Statistic statistic, StatisticViewModel statisticVm)
        {
            statistic.ID = statisticVm.ID;
            statistic.Name = statisticVm.Name;
            statistic.Alias = statisticVm.Alias;
            statistic.StatisticCategoryID = statisticVm.StatisticCategoryID;
            statistic.ToDate = statisticVm.ToDate;
            statistic.FromDate = statisticVm.FromDate;
            statistic.Description = statisticVm.Description;
            statistic.Content = statisticVm.Content;
            statistic.HomeFlag = statisticVm.HomeFlag;
            statistic.HotFlag = statisticVm.HotFlag;
            statistic.ViewCount = statisticVm.ViewCount;
            statistic.Tags = statisticVm.Tags;
            statistic.ProvinceID = statisticVm.ProvinceID;
            statistic.DistrictID = statisticVm.DistrictID;
            statistic.WardID = statisticVm.WardID;
            statistic.TheInjured = statisticVm.TheInjured;
            statistic.TheDead = statisticVm.TheDead;
            statistic.PropertyDamage = statisticVm.PropertyDamage;
            statistic.PoliceOrganizationID = statisticVm.PoliceOrganizationID;
            statistic.ResolvedSituationID = statisticVm.ResolvedSituationID;
            statistic.CreatedDate = statisticVm.CreatedDate;
            statistic.CreatedBy = statisticVm.CreatedBy;
            statistic.UpdatedBy = statisticVm.UpdatedBy;
            statistic.UpdatedDate = statisticVm.UpdatedDate;
            statistic.MetaKeyword = statisticVm.MetaKeyword;
            statistic.MetaDescription = statisticVm.MetaDescription;
            statistic.Status = statisticVm.Status;

        }
        public static void UpdateStatisticCategory(this StatisticCategory statisticCategory, StatisticCategoryViewModel statisticCategoryVm)
        {
            statisticCategory.ID = statisticCategoryVm.ID;
            statisticCategory.Name = statisticCategoryVm.Name;
            statisticCategory.Alias = statisticCategoryVm.Alias;
            statisticCategory.Description = statisticCategoryVm.Description;
            statisticCategory.ParentID = statisticCategoryVm.ParentID;
            statisticCategory.HomeFlag = statisticCategoryVm.HomeFlag;
            statisticCategory.DisplayOrder = statisticCategoryVm.DisplayOrder;
            statisticCategory.CreatedDate = statisticCategoryVm.CreatedDate;
            statisticCategory.CreatedBy = statisticCategoryVm.CreatedBy;
            statisticCategory.UpdatedBy = statisticCategoryVm.UpdatedBy;
            statisticCategory.UpdatedDate = statisticCategoryVm.UpdatedDate;
            statisticCategory.MetaKeyword = statisticCategoryVm.MetaKeyword;
            statisticCategory.MetaDescription = statisticCategoryVm.MetaDescription;
            statisticCategory.Status = statisticCategoryVm.Status;

        }
        public static void UpdateSituationCategory(this SituationCategory situationCategory, SituationCategoryViewModel situationCategoryVm)
        {
            situationCategory.ID = situationCategoryVm.ID;
            situationCategory.Name = situationCategoryVm.Name;
            situationCategory.Alias = situationCategoryVm.Alias;
            situationCategory.Description = situationCategoryVm.Description;
            situationCategory.ParentID = situationCategoryVm.ParentID;
            situationCategory.HomeFlag = situationCategoryVm.HomeFlag;
            situationCategory.DisplayOrder = situationCategoryVm.DisplayOrder;
            situationCategory.CreatedDate = situationCategoryVm.CreatedDate;
            situationCategory.CreatedBy = situationCategoryVm.CreatedBy;
            situationCategory.UpdatedBy = situationCategoryVm.UpdatedBy;
            situationCategory.UpdatedDate = situationCategoryVm.UpdatedDate;
            situationCategory.MetaKeyword = situationCategoryVm.MetaKeyword;
            situationCategory.MetaDescription = situationCategoryVm.MetaDescription;
            situationCategory.Status = situationCategoryVm.Status;

        }
        public static void UpdateApplicationGroup(this ApplicationGroup appGroup, ApplicationGroupViewModel appGroupViewModel)
        {
            appGroup.ID = appGroupViewModel.ID;
            appGroup.Name = appGroupViewModel.Name;
        }

        public static void UpdateApplicationRole(this ApplicationRole appRole, ApplicationRoleViewModel appRoleViewModel, string action = "add")
        {
            if (action == "update")
                appRole.Id = appRoleViewModel.Id;
            else
                appRole.Id = Guid.NewGuid().ToString();
            appRole.Name = appRoleViewModel.Name;
            appRole.Description = appRoleViewModel.Description;
        }
        public static void UpdateUser(this ApplicationUser appUser, ApplicationUserViewModel appUserViewModel, string action = "add")
        {

            appUser.Id = appUserViewModel.Id;
            appUser.FullName = appUserViewModel.FullName;
            appUser.BirthDay = appUserViewModel.BirthDay;
            appUser.Email = appUserViewModel.Email;
            appUser.UserName = appUserViewModel.UserName;
            appUser.PhoneNumber = appUserViewModel.PhoneNumber;
        }
    }
}