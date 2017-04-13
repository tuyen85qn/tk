using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using TK.Data;
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
            situation.OccurenceDay = DateTime.ParseExact(situationVm.OccurenceDay,"yyyy-MM-dd", CultureInfo.InvariantCulture);           
            situation.Image = situationVm.Image;
            situation.MoreImages = situationVm.MoreImages;           
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
        public static void UpdateSituationListView(this SituationListViewModel situationLVm, Situation situation)
        {
            TKDbContext db = new TKDbContext();
            situationLVm.ID = situation.ID;
            situationLVm.Name = situation.Name;
            situationLVm.OccurenceDay = situation.OccurenceDay.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            situationLVm.TheInjured = situation.TheInjured;
            situationLVm.TheDead = situation.TheDead;          
            situationLVm.PropertyDamage = situation.PropertyDamage;
            situationLVm.Status = situation.Status;
            SituationCategory situationCategory = db.SituationCategories.FirstOrDefault(x => x.ID == situation.SituationCategoryID);
            PoliceOrganization policeOrganization = db.PoliceOrganizations.FirstOrDefault(x => x.ID == situation.PoliceOrganizationID);
            ResolvedSituation resolvedSituation = db.ResolvedSituations.FirstOrDefault(x => x.ID == situation.ResolvedSituationID);
            if( situationCategory != null)
            {
                situationLVm.SituationCategoryName = situationCategory.Name;
            }
            if (policeOrganization !=null)
            {
                situationLVm.PoliceOrganizationName = policeOrganization.Name;
            }
            if (resolvedSituation != null)
            {
                situationLVm.ResolvedSituationName = resolvedSituation.Name;
            }            
            
            District district = db.Districts.FirstOrDefault(x => x.ID == situation.DistrictID);
            Province province = db.Provinces.FirstOrDefault(x => x.ID == situation.ProvinceID);
            if(district == null)
            {
                situationLVm.Place = province.Type + " " + province.Name; 
            }
            else
            {
                situationLVm.Place = district.Type + " " + district.Name + ", " +
                    province.Type + " " + province.Name;
            }
              
        }
        public static void UpdateStatistic(this Statistic statistic, StatisticViewModel statisticVm)
        {
            statistic.ID = statisticVm.ID;
            statistic.Name = statisticVm.Name;
            statistic.Alias = statisticVm.Alias;
            statistic.StatisticCategoryID = statisticVm.StatisticCategoryID;
            statistic.ToDate = DateTime.ParseExact(statisticVm.ToDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            statistic.FromDate = DateTime.ParseExact(statisticVm.FromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
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
            statistic.TotalSituationCount = statisticVm.TotalSituationCount;
            statistic.PropertyDamage = statisticVm.PropertyDamage;           
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
        public static void UpdateDailySheetVM(this DailySheetShowViewModel dailySheetVm, DailySheet dailySheet)
        {
            TKDbContext db = new TKDbContext();
            dailySheetVm.ID = dailySheet.ID;
            dailySheetVm.DayReport = dailySheet.DayReport.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            PoliceOrganization policeOrganization = db.PoliceOrganizations.FirstOrDefault(x => x.ID == dailySheet.PoliceOrganizationID);
            if(dailySheet.TypeReportID != null)
            {
                TypeReport typeReport = db.TypeReports.FirstOrDefault(x => x.ID == dailySheet.TypeReportID);
                dailySheetVm.TypeReportName = typeReport.Name;
            }
           
            dailySheetVm.PoliceOrganizationName = policeOrganization.Name;
            dailySheetVm.OnDuty = dailySheet.OnDuty;
            dailySheetVm.DirectCommand = dailySheet.DirectCommand;
            dailySheetVm.Description = dailySheet.Description;
            dailySheetVm.CreatedDate = dailySheet.CreatedDate;
            dailySheetVm.CreatedBy = dailySheet.CreatedBy;
            dailySheetVm.UpdatedBy = dailySheet.UpdatedBy;
            dailySheetVm.UpdatedDate = dailySheet.UpdatedDate;
            dailySheetVm.MetaKeyword = dailySheet.MetaKeyword;
            dailySheetVm.MetaDescription = dailySheet.MetaDescription;
            dailySheetVm.Status = dailySheet.Status;

        }
        public static void UpdateDailySheet(this DailySheet dailySheet, DailySheetShowViewModel dailySheetVm)
        {
            TKDbContext db = new TKDbContext();
            dailySheet.ID = dailySheetVm.ID;
            dailySheet.DayReport = DateTime.ParseExact(dailySheetVm.DayReport, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            PoliceOrganization policeOrganization = db.PoliceOrganizations.FirstOrDefault(x => x.Name == dailySheetVm.PoliceOrganizationName);
            TypeReport typeReport = db.TypeReports.FirstOrDefault(x => x.Name == dailySheetVm.TypeReportName);
            dailySheet.PoliceOrganizationID = policeOrganization.ID;
            if(typeReport !=null)
            {
                dailySheet.TypeReportID = typeReport.ID;
            }            
            dailySheet.OnDuty = dailySheetVm.OnDuty;
            dailySheet.DirectCommand = dailySheetVm.DirectCommand;
            dailySheet.Description = dailySheetVm.Description;
            dailySheet.CreatedDate = dailySheetVm.CreatedDate;
            dailySheet.CreatedBy = dailySheetVm.CreatedBy;
            dailySheet.UpdatedBy = dailySheetVm.UpdatedBy;
            dailySheet.UpdatedDate = dailySheetVm.UpdatedDate;
            dailySheet.MetaKeyword = dailySheetVm.MetaKeyword;
            dailySheet.MetaDescription = dailySheetVm.MetaDescription;
            dailySheet.Status = dailySheetVm.Status;

        }
        public static void UpdateDailySheetDB(this DailySheet dailySheet, DailySheetViewModel dailySheetVm)
        {
            TKDbContext db = new TKDbContext();
            dailySheet.ID = dailySheetVm.ID;
            dailySheet.DayReport = DateTime.ParseExact(dailySheetVm.DayReport, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            dailySheet.PoliceOrganizationID = dailySheetVm.PoliceOrganizationID;
            if(dailySheetVm.TypeReportID > 0) {
                dailySheet.TypeReportID = dailySheetVm.TypeReportID;
            }            
            dailySheet.OnDuty = dailySheetVm.OnDuty;
            dailySheet.DirectCommand = dailySheetVm.DirectCommand;
            dailySheet.Description = dailySheetVm.Description;
            dailySheet.CreatedDate = dailySheetVm.CreatedDate;
            dailySheet.CreatedBy = dailySheetVm.CreatedBy;
            dailySheet.UpdatedBy = dailySheetVm.UpdatedBy;
            dailySheet.UpdatedDate = dailySheetVm.UpdatedDate;
            dailySheet.MetaKeyword = dailySheetVm.MetaKeyword;
            dailySheet.MetaDescription = dailySheetVm.MetaDescription;
            dailySheet.Status = dailySheetVm.Status;

        }
    }
}