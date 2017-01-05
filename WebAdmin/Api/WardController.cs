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
    [RoutePrefix("api/ward")]
    [Authorize]
    public class WardController : ApiControllerBase
    {
        #region
        private IWardService _wardService;
        public WardController(IErrorService errorService, IWardService wardService) : base(errorService)
        {
            this._wardService = wardService;
        }
        #endregion
        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _wardService.GetAll(keyword);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);
                var responeData = Mapper.Map<IEnumerable<Ward>, IEnumerable<WardViewModel>>(query);
                var paginationSet = new PaginationSet<WardViewModel>()
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
                var model = _wardService.GetAll();
                var responeData = Mapper.Map<IEnumerable<Ward>, IEnumerable<WardViewModel>>(model);
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
                var model = _wardService.GetById(id);
                var responeData = Mapper.Map<Ward, WardViewModel>(model);
                var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                return respone;
            });
        }
        [Route("getbyprovinceid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetByProvinceId(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _wardService.GetByProvinceId(id);
                var responeData = Mapper.Map<IEnumerable<Ward>, IEnumerable<WardViewModel>>(model);
                var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                return respone;
            });
        }
        [Route("getbydistrictid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetByDistrictId(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _wardService.GetByDistrictId(id);
                var responeData = Mapper.Map<IEnumerable<Ward>, IEnumerable<WardViewModel>>(model);
                var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                return respone;
            });
        }
    }
}