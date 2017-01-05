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
    [RoutePrefix("api/district")]
    [Authorize]
    public class DistrictController : ApiControllerBase
    {
        #region
        private IDistrictService _districtService;
        public DistrictController(IErrorService errorService, IDistrictService districtService) : base(errorService)
        {
            this._districtService = districtService;
        }
        #endregion
        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _districtService.GetAll(keyword);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);
                var responeData = Mapper.Map<IEnumerable<District>, IEnumerable<DistrictViewModel>>(query);
                var paginationSet = new PaginationSet<DistrictViewModel>()
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
                var model = _districtService.GetAll();
                var responeData = Mapper.Map<IEnumerable<District>, IEnumerable<DistrictViewModel>>(model);
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
                var model = _districtService.GetById(id);
                var responeData = Mapper.Map<District, DistrictViewModel>(model);
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
                var model = _districtService.GetByProvinceId(id);
                var responeData = Mapper.Map<IEnumerable<District>, IEnumerable<DistrictViewModel>>(model);
                var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                return respone;
            });
        }
    }
}