using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TK.Model.Models;
using TK.Service;
using WebAdmin.Infrastructure.Core;
using WebAdmin.Models;

namespace WebAdmin.Api
{
    [RoutePrefix("api/policeOrganization")]
    [AllowAnonymous]
    public class PoliceOrganizationController : ApiControllerBase
    {
        #region
        private IPoliceOrganizationService _policeOrganizationService;
        public PoliceOrganizationController(IErrorService errorService, IPoliceOrganizationService policeOrganizationService) : base(errorService)
        {
            this._policeOrganizationService = policeOrganizationService;
        }
        #endregion
        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _policeOrganizationService.GetAll(keyword);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);
                var responeData = Mapper.Map<IEnumerable<PoliceOrganization>, IEnumerable<PoliceOrganizationViewModel>>(query);
                var paginationSet = new PaginationSet<PoliceOrganizationViewModel>()
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
                var model = _policeOrganizationService.GetAll();
                var responeData = Mapper.Map<IEnumerable<PoliceOrganization>, IEnumerable<PoliceOrganizationViewModel>>(model);
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
                var model = _policeOrganizationService.GetById(id);
                var responeData = Mapper.Map<PoliceOrganization, PoliceOrganizationViewModel>(model);
                var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                return respone;
            });
        }
    }
}