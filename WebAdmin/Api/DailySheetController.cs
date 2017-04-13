using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using TK.Model.Models;
using TK.Service;
using WebAdmin.Infrastructure.Core;
using WebAdmin.Infrastructure.Extensions;
using WebAdmin.Models;

namespace WebAdmin.Api
{
    [RoutePrefix("api/dailySheet")]
    [Authorize]
    public class DailySheetController : ApiControllerBase
    {
        #region
        private IDailySheetService _dailySheetService;
        private IPoliceOrganizationService _policeOrganizationService;
        public DailySheetController(IErrorService errorService, IDailySheetService dailySheetService, IPoliceOrganizationService policeOrganizationService)
            : base(errorService)
        {
            this._dailySheetService = dailySheetService;
            this._policeOrganizationService = policeOrganizationService;
        }
        #endregion
        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, string filterDate, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;                
                var model = _dailySheetService.GetAll(filterDate);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.DayReport).Skip(page * pageSize).Take(pageSize);
                List<DailySheetShowViewModel> responeData = new List<DailySheetShowViewModel>();
                foreach (var item in query)
                {
                    var newDailySheetVm = new DailySheetShowViewModel();
                    newDailySheetVm.UpdateDailySheetVM(item);
                    responeData.Add(newDailySheetVm);
                }
                var paginationSet = new PaginationSet<DailySheetShowViewModel>()
                {
                    Items = responeData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var respone = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return respone;
            });
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {                
                var model = _dailySheetService.GetAll();                                
                List<DailySheetShowViewModel> responeData = new List<DailySheetShowViewModel>();
                foreach (var item in model)
                {
                    var newDailySheetVm = new DailySheetShowViewModel();
                    newDailySheetVm.UpdateDailySheetVM(item);
                    responeData.Add(newDailySheetVm);
                }
                
                var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                return respone;
            });
        }
        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, DailySheetViewModel dailySheetVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage respone = null;
                if (!ModelState.IsValid)
                {
                    respone = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    DailySheet dailySheet = new DailySheet();
                    dailySheet.UpdateDailySheetDB(dailySheetVm);
                    dailySheet.CreatedDate = DateTime.Now;
                    dailySheet.CreatedBy = User.Identity.Name;
                    _dailySheetService.Add(dailySheet);
                    _dailySheetService.Save();                 
                    respone = request.CreateResponse(HttpStatusCode.OK, dailySheetVm);

                }
                return respone;
            });
        }
        [Route("statistic")]
        [HttpGet]
        public HttpResponseMessage DailySheetStatistic(HttpRequestMessage request, string fromDate, string toDate, int policeOrganizationID=0)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage respone = null;
                List<DailySheetStatistic> lstDailySheetStatistic = new List<DailySheetStatistic>();
                if (!ModelState.IsValid)
                {
                    respone = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                   if(policeOrganizationID>0)
                    {                        
                        lstDailySheetStatistic.Add(AddDailySheetStatistic(fromDate,toDate,policeOrganizationID));                       
                    }
                   else
                    {
                        var lstPoliceOrganization = _policeOrganizationService.GetListByType("Huyện, Thành phố");                      
                        foreach (var item in lstPoliceOrganization)
                        {
                            lstDailySheetStatistic.Add(AddDailySheetStatistic(fromDate, toDate, item.ID));
                            
                        }
                       
                    }
                    
                }
                respone = request.CreateResponse(HttpStatusCode.OK, lstDailySheetStatistic);
                return respone;
            });
        }
        public DailySheetStatistic AddDailySheetStatistic(string fromDate, string toDate, int policeOrganizationID)
        {
            DailySheetStatistic dailySheetStatistic = new DailySheetStatistic();
            var lstDailySheet = _dailySheetService.GetListDoNotDailySheet(fromDate, toDate, policeOrganizationID);
            dailySheetStatistic.ToDate = toDate;
            dailySheetStatistic.FromDate = fromDate;
            dailySheetStatistic.TotalDay = (DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture) - DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)).Days;
            dailySheetStatistic.PoliceOrganizationName = _policeOrganizationService.GetById(policeOrganizationID).Name;
            if (lstDailySheet.Count() == 0)
            {
                dailySheetStatistic.TotalDailySheet = dailySheetStatistic.TotalDay;
                dailySheetStatistic.Content = "Báo cáo ngày đầy đủ";
            }
            else
            {
                dailySheetStatistic.TotalDailySheet = dailySheetStatistic.TotalDay - lstDailySheet.Count();
                dailySheetStatistic.Content = "Các ngày không báo cáo:";
                foreach (var item in lstDailySheet)
                {                    
                    dailySheetStatistic.Content += "\n" + "-" + item.DayReport.ToString("yyyy-mm-dd");
                }
            }
            return dailySheetStatistic;
        }
        
        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, DailySheetViewModel dailySheetVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage respone = null;
                if (!ModelState.IsValid)
                {
                    respone = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    DailySheet dailySheet = _dailySheetService.GetById(dailySheetVm.ID);
                    dailySheet.UpdateDailySheetDB(dailySheetVm);
                    dailySheet.UpdatedDate = DateTime.Now;
                    dailySheet.UpdatedBy = User.Identity.Name;
                    _dailySheetService.Update(dailySheet);
                    _dailySheetService.Save();
                    respone = request.CreateResponse(HttpStatusCode.OK, dailySheetVm);

                }
                return respone;
            });
        }        
        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dailySheetService.GetById(id);
                var responeData = Mapper.Map<DailySheet, DailySheetViewModel>(model);
                var respone = request.CreateResponse(HttpStatusCode.OK, model);
                return respone;
            });
        }
        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage respone = null;
                if (!ModelState.IsValid)
                {
                    respone = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {

                    var delDailySheet = _dailySheetService.Delete(id);
                    _dailySheetService.Save();

                    var responeData = Mapper.Map<DailySheet, DailySheetViewModel>(delDailySheet);
                    respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                    
                }
                return respone;
            });
        }

    }
}