using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TK.Common;
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
        public async Task<HttpResponseMessage> Create(HttpRequestMessage request)
        {

            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Định dạng không được server hỗ trợ");
            }

            var root = HttpContext.Current.Server.MapPath(CommonConstants.pathDailySheet);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            var provider = new CustomMultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                DailySheetViewModel dailySheetVm = Helper.AsObject<DailySheetViewModel>(provider.FormData.GetValues("dailySheet").FirstOrDefault());
                DailySheet dailySheet = new DailySheet();               
                if (provider.FileData.Count > 0)
                {
                    string fileName = provider.FileData[0].Headers.ContentDisposition.FileName;
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    var tmpFileName = Helper.RandomString(20) + '_' + fileName;
                    var fullPath = Path.Combine(root, tmpFileName);
                    File.Move(provider.FileData[0].LocalFileName, fullPath);
                    dailySheetVm.FileDailySheet = CommonConstants.pathDailySheet.Substring(CommonConstants.pathDailySheet.IndexOf("/") + 1) + "/" + tmpFileName;
                    
                }

                dailySheet.UpdateDailySheetDB(dailySheetVm);
                dailySheet.CreatedDate = DateTime.Now;
                dailySheet.CreatedBy = User.Identity.Name;
                _dailySheetService.Add(dailySheet);
                _dailySheetService.Save();
                return Request.CreateResponse(HttpStatusCode.OK, dailySheetVm);

            }
            catch (System.Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Invalid file type or other error.");
            }
             
        }
        [Route("statistic")]
        [HttpGet]
        public HttpResponseMessage DailySheetStatistic(HttpRequestMessage request, string fromDate, string toDate, int policeOrganizationID = 0)
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
                    if (policeOrganizationID > 0)
                    {
                        lstDailySheetStatistic.Add(AddDailySheetStatistic(fromDate, toDate, policeOrganizationID));
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
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request)
        {
            if(!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Định dạng không được server hỗ trợ");
            }

            var root = HttpContext.Current.Server.MapPath(CommonConstants.pathDailySheet);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            var provider = new CustomMultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                DailySheetViewModel dailySheetVm = Helper.AsObject<DailySheetViewModel>(provider.FormData.GetValues("dailySheet").FirstOrDefault());
                DailySheet dailySheet = _dailySheetService.GetById(dailySheetVm.ID);
                if (provider.FileData.Count > 0)
                {
                    string fileName = provider.FileData[0].Headers.ContentDisposition.FileName;
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    var tmpFileName = Helper.RandomString(20) + '_' + fileName;
                    var fullPath = Path.Combine(root, tmpFileName);                    
                    if (!String.IsNullOrEmpty(dailySheet.FileDailySheet))
                    {
                        var oldFileName = dailySheet.FileDailySheet.Split('/').Last();
                        if (!String.IsNullOrEmpty(Directory.GetFiles(root, oldFileName).FirstOrDefault()))
                        {
                            File.Delete(Path.Combine(root, oldFileName));
                        }
                    }
                    File.Move(provider.FileData[0].LocalFileName, fullPath);
                    dailySheetVm.FileDailySheet = CommonConstants.pathDailySheet.Substring(CommonConstants.pathDailySheet.IndexOf("/")+1) + "/" + tmpFileName;
                }

                dailySheet.UpdateDailySheetDB(dailySheetVm);
                dailySheet.UpdatedDate = DateTime.Now;
                dailySheet.UpdatedBy = User.Identity.Name;
                _dailySheetService.Update(dailySheet);
                _dailySheetService.Save();
                return Request.CreateResponse(HttpStatusCode.OK, dailySheetVm);

            }
            catch (System.Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Invalid file type or other error.");
            }            
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
                    var root = HttpContext.Current.Server.MapPath(CommonConstants.pathDailySheet);
                    if(!string.IsNullOrEmpty(delDailySheet.FileDailySheet))
                    {
                        var delFileName = delDailySheet.FileDailySheet.Split('/').Last();
                        if (!String.IsNullOrEmpty(System.IO.Directory.GetFiles(root, delFileName).FirstOrDefault()))
                        {
                            File.Delete(Path.Combine(root, delFileName));
                        }
                    }
                    var responeData = Mapper.Map<DailySheet, DailySheetViewModel>(delDailySheet);
                    respone = request.CreateResponse(HttpStatusCode.OK, responeData);

                }
                return respone;
            });
        }

    }
}