using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TK.Service;
using WebAdmin.Infrastructure.Core;

namespace WebAdmin.Api
{
    [RoutePrefix("api/dailySheet")]
    [Authorize]
    public class DailySheetController : ApiControllerBase
    {
        #region
        private IDailySheetService _dailySheetService;
        public DailySheetController(IErrorService errorService, IDailySheetService dailySheetService) : base(errorService)
        {
            this._dailySheetService = dailySheetService;
        }
        #endregion
        //[Route("getlistpaging")]
        //[HttpGet]
        //public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        int totalRow = 0;
        //        var model = _dailySheetService.GetAll(keyword);
        //        totalRow = model.Count();
        //        var query = model.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);
        //        var responeData = Mapper.Map<IEnumerable<DailySheet>, IEnumerable<DailySheetViewModel>>(query);
        //        var paginationSet = new PaginationSet<DailySheetViewModel>()
        //        {
        //            Items = responeData,
        //            Page = page,
        //            TotalCount = totalRow,
        //            TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
        //        };
        //        var respone = request.CreateResponse(HttpStatusCode.OK, paginationSet);
        //        return respone;
        //    });
        //}
        //[Route("getall")]
        //[HttpGet]
        //public HttpResponseMessage GetAll(HttpRequestMessage request)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        var model = _dailySheetService.GetAll();
        //        var responeData = Mapper.Map<IEnumerable<DailySheet>, IEnumerable<DailySheetViewModel>>(model);
        //        var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
        //        return respone;
        //    });
        //}
        //[Route("getbyid/{id:int}")]
        //[HttpGet]
        //public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        var model = _dailySheetService.GetById(id);
        //        var responeData = Mapper.Map<DailySheet, DailySheetViewModel>(model);
        //        var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
        //        return respone;
        //    });
        //}
       
    }
}