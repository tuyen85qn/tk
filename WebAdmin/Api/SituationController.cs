using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TK.Data;
using TK.Model.Models;
using TK.Service;
using WebAdmin.Infrastructure.Core;
using WebAdmin.Infrastructure.Extensions;
using WebAdmin.Models;

namespace WebAdmin.Api
{
    [RoutePrefix("api/situation")]
    [Authorize]
    public class SituationController : ApiControllerBase
    {
        #region
        private ISituationService _situationService;
        public SituationController(IErrorService errorService, ISituationService situationService)
            : base(errorService)
        {
            _situationService = situationService;           
        }
        #endregion
        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string filter, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {

                int totalRow = 0;
                var model = _situationService.GetAll(filter);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                List<SituationListViewModel> responeData = new List<SituationListViewModel>();
                foreach (var item in query)
                {
                    var newSituationVm = new SituationListViewModel();
                    newSituationVm.UpdateSituationListView(item);
                    responeData.Add(newSituationVm);                 
                }
                               
                var paginationSet = new PaginationSet<SituationListViewModel>()
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
        [Route("getlistbycategory")]
        [HttpGet]
        public HttpResponseMessage GetListByCategory(HttpRequestMessage request, string filter, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    TKDbContext dbContex = new TKDbContext();
                    List<SituationCategory> lstCategory = new List<SituationCategory>();
                    List<Situation> lstSituation = new List<Situation>();
                    SituationCategory situationCategory = dbContex.SituationCategories.SingleOrDefault(x => x.Name == filter);
                    if(situationCategory == null)
                    {
                        return null;
                    }
                    lstCategory = SituationCategoryController.GetSubCascading(situationCategory.ID, dbContex);
                    for (int i = 0; i < lstCategory.Count(); i++)
                    {
                        lstSituation.AddRange(_situationService.GetListByCategory(lstCategory[i].ID));
                    }
                    int totalRow = 0;

                    totalRow = lstSituation.Count();
                    var query = lstSituation.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);
                    List<SituationListViewModel> responeData = new List<SituationListViewModel>();
                    foreach (var item in query)
                    {
                        var newSituationVm = new SituationListViewModel();
                        newSituationVm.UpdateSituationListView(item);
                        responeData.Add(newSituationVm);
                    }

                    var paginationSet = new PaginationSet<SituationListViewModel>()
                    {
                        Items = responeData,
                        Page = page,
                        TotalCount = totalRow,
                        TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                    };
                    var respone = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                    return respone;

                }
                else
                {
                    return null;
                }                

            });
        }
        [Route("getlistpagingbydate")]
        [HttpGet]
        public HttpResponseMessage GetListByDate(HttpRequestMessage request, string fromDate, string toDate,int provinceID, int? districtID = null,
            int? resolvedSituationID= null, int page = 0, int pageSize =20, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                DateTime FromDate = DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                var model = _situationService.GetListByDate(FromDate, ToDate, provinceID);
                if (districtID != null)
                {
                    model = model.Where(x => x.DistrictID == districtID);
                }
                if (resolvedSituationID != null)
                {
                    model = model.Where(x => x.ResolvedSituationID == resolvedSituationID);
                }

                if (!string.IsNullOrEmpty(filter))
                {
                    TKDbContext dbContex = new TKDbContext();
                    List<SituationCategory> lstCategory = new List<SituationCategory>();
                    List<Situation> lstSituation = new List<Situation>();
                    SituationCategory situationCategory = dbContex.SituationCategories.SingleOrDefault(x => x.Name == filter);
                    lstCategory = SituationCategoryController.GetSubCascading(situationCategory.ID, dbContex);
                    for( int i = 0; i < lstCategory.Count(); i++)
                    {
                        lstSituation.AddRange(model.Where(x => x.SituationCategoryID == lstCategory[i].ID).ToList());
                    }
                    model = lstSituation;
                }
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.OccurenceDay).Skip(page * pageSize).Take(pageSize);
                List<SituationListViewModel> responeData = new List<SituationListViewModel>();
                foreach (var item in query)
                {
                    var newSituationVm = new SituationListViewModel();
                    newSituationVm.UpdateSituationListView(item);
                    responeData.Add(newSituationVm);
                }
                var paginationSet = new PaginationSet<SituationListViewModel>()
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
                var model = _situationService.GetAll();
                var responeData = Mapper.Map<IEnumerable<Situation>, IEnumerable<SituationViewModel>>(model);
                var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                return respone;
            });
        }
      
        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _situationService.GetById(id);
                var responeData = Mapper.Map<Situation, SituationViewModel>(model);
                var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                return respone;
            });
        }
        [Route("create")]
        [HttpPost]
        
        public HttpResponseMessage Create(HttpRequestMessage request, SituationViewModel situationVm)
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
                    var newSituation = new Situation();
                    newSituation.UpdateSituation(situationVm);
                    newSituation.CreatedDate = DateTime.Now;
                    newSituation.CreatedBy = User.Identity.Name;
                    _situationService.Add(newSituation);
                    _situationService.Save();
                    var responeData = Mapper.Map<Situation, SituationViewModel>(newSituation);
                    respone = request.CreateResponse(HttpStatusCode.OK, responeData);

                }
                return respone;
            });
        }
        [Route("update")]
        [HttpPut]
        
        public HttpResponseMessage Update(HttpRequestMessage request, SituationViewModel situationVm)
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
                    var oldSituation = _situationService.GetById(situationVm.ID);
                    oldSituation.UpdateSituation(situationVm);                   
                    oldSituation.UpdatedDate = DateTime.Now;
                    oldSituation.UpdatedBy = User.Identity.Name;
                    _situationService.Update(oldSituation);
                    _situationService.Save();

                    var responeData = Mapper.Map<Situation, SituationViewModel>(oldSituation);
                    respone = request.CreateResponse(HttpStatusCode.OK, responeData);

                }
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

                    var delSituation = _situationService.Delete(id);
                    _situationService.Save();

                    var responeData = Mapper.Map<Situation, SituationViewModel>(delSituation);
                    respone = request.CreateResponse(HttpStatusCode.OK, responeData);


                }
                return respone;
            });
        }

    }
}