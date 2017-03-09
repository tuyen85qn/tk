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
    [RoutePrefix("api/typeReport")]
    [Authorize]
    public class TypeReportController : ApiControllerBase
    {
        #region
        private ITypeReportService _typeReportService;
        public TypeReportController(IErrorService errorService, ITypeReportService typeReportService) : base(errorService)
        {
            this._typeReportService = typeReportService;
        }
        #endregion       
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _typeReportService.GetAll();
                var responeData = Mapper.Map<IEnumerable<TypeReport>, IEnumerable<TypeReportViewModel>>(model);
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
                var model = _typeReportService.GetById(id);
                var responeData = Mapper.Map<TypeReport, TypeReportViewModel>(model);
                var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                return respone;
            });
        }
        
    }
 
}