using AutoMapper;
using System;
using System.Collections.Generic;
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
                var responeData = Mapper.Map<IEnumerable<Situation>, IEnumerable<SituationViewModel>>(query);
                var paginationSet = new PaginationSet<SituationViewModel>()
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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