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
   [RoutePrefix("api/province")]
    [Authorize]
    public class ProvinceController : ApiControllerBase
    {
        #region
        private IProvinceService _provinceService;
        public ProvinceController(IErrorService errorService, IProvinceService provinceService) : base(errorService)
        {
            this._provinceService = provinceService;
        }
        #endregion
        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _provinceService.GetAll(keyword);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);
                var responeData = Mapper.Map<IEnumerable<Province>, IEnumerable<ProvinceViewModel>>(query);
                var paginationSet = new PaginationSet<ProvinceViewModel>()
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
                var model = _provinceService.GetAll();
                var responeData = Mapper.Map<IEnumerable<Province>, IEnumerable<ProvinceViewModel>>(model);
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
                var model = _provinceService.GetById(id);
                var responeData = Mapper.Map<Province, ProvinceViewModel>(model);
                var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                return respone;
            });
        }
    }
}