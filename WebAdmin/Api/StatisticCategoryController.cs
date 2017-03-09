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
using WebAdmin.Infrastructure.Extensions;
using WebAdmin.Models;

namespace WebAdmin.Api
{
    [RoutePrefix("api/statisticCategory")]
    [Authorize]
    public class StatisticCategoryController : ApiControllerBase
    {
        #region
        private IStatisticCategoryService _statisticCategoryService;
        public StatisticCategoryController(IErrorService errorService, IStatisticCategoryService statisticCategoryService)
            : base(errorService)
        {
            _statisticCategoryService = statisticCategoryService;
        }
        #endregion
        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string filter, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _statisticCategoryService.GetAll(filter);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responeData = Mapper.Map<IEnumerable<StatisticCategory>, IEnumerable<StatisticCategoryViewModel>>(query);
                foreach(var item in responeData)
                {
                    string st= String.Format("{0:MM/dd/yyyy}", item.CreatedDate);
                    item.CreatedDate = DateTime.ParseExact(st, "MM/dd/yyyy",null);
                }
                var paginationSet = new PaginationSet<StatisticCategoryViewModel>()
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
                var model = _statisticCategoryService.GetAll();
                var responeData = Mapper.Map<IEnumerable<StatisticCategory>, IEnumerable<StatisticCategoryViewModel>>(model);
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
                var model = _statisticCategoryService.GetById(id);
                var responeData = Mapper.Map<StatisticCategory, StatisticCategoryViewModel>(model);
                var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                return respone;
            });
        }
        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, StatisticCategoryViewModel statisticCategoryVm)
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
                    var newStatisticCategory = new StatisticCategory();
                    newStatisticCategory.UpdateStatisticCategory(statisticCategoryVm);
                    newStatisticCategory.CreatedDate = DateTime.Now;
                    _statisticCategoryService.Add(newStatisticCategory);
                    _statisticCategoryService.Save();

                    var responeData = Mapper.Map<StatisticCategory, StatisticCategoryViewModel>(newStatisticCategory);
                    respone = request.CreateResponse(HttpStatusCode.OK, responeData);

                }
                return respone;
            });
        }
        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, StatisticCategoryViewModel statisticCategoryVm)
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
                    var oldStatisticCategory = _statisticCategoryService.GetById(statisticCategoryVm.ID);
                    oldStatisticCategory.UpdateStatisticCategory(statisticCategoryVm);
                    oldStatisticCategory.UpdatedDate = DateTime.Now;
                    _statisticCategoryService.Update(oldStatisticCategory);
                    _statisticCategoryService.Save();

                    var responeData = Mapper.Map<StatisticCategory, StatisticCategoryViewModel>(oldStatisticCategory);
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
                    var dbStatisticCategory = _statisticCategoryService.GetById(id);
                    if (dbStatisticCategory.ParentID == null)
                    {
                        dbStatisticCategory = _statisticCategoryService.Delete(id);
                        _statisticCategoryService.Save();

                        var responeData = Mapper.Map<StatisticCategory, StatisticCategoryViewModel>(dbStatisticCategory);
                        respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                    }
                    else
                    {
                        respone = request.CreateErrorResponse(HttpStatusCode.Forbidden, "Vui lòng xóa hết các danh mục con.");
                    }

                }
                return respone;
            });
        }
    }
}