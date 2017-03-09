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
    [RoutePrefix("api/statistic")]
    [Authorize]
    public class StatisticController : ApiControllerBase
    {
        #region
        private IStatisticService _statisticService;
        public StatisticController(IErrorService errorService, IStatisticService statisticService)
            : base(errorService)
        {
            _statisticService = statisticService;
        }
        #endregion
        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string filter, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {

                int totalRow = 0;
                var model = _statisticService.GetAll(filter);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responeData = Mapper.Map<IEnumerable<Statistic>, IEnumerable<StatisticViewModel>>(query);
                var paginationSet = new PaginationSet<StatisticViewModel>()
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
                var model = _statisticService.GetAll();
                var responeData = Mapper.Map<IEnumerable<Statistic>, IEnumerable<StatisticViewModel>>(model);
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
                var model = _statisticService.GetById(id);
                var responeData = Mapper.Map<Statistic, StatisticViewModel>(model);
                var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                return respone;
            });
        }
        [Route("create")]
        [HttpPost]       
        public HttpResponseMessage Create(HttpRequestMessage request, StatisticViewModel statisticVm)
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
                    var newStatistic = new Statistic();
                    newStatistic.UpdateStatistic(statisticVm);
                    newStatistic.CreatedDate = DateTime.Now;
                    newStatistic.CreatedBy = User.Identity.Name;
                    _statisticService.Add(newStatistic);
                    _statisticService.Save();

                    var responeData = Mapper.Map<Statistic, StatisticViewModel>(newStatistic);
                    respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                }
                return respone;
            });
        }
        [Route("update")]
        [HttpPut]       
        public HttpResponseMessage Update(HttpRequestMessage request, StatisticViewModel statisticVm)
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
                    var oldStatistic = _statisticService.GetById(statisticVm.ID);
                    oldStatistic.UpdateStatistic(statisticVm);
                    oldStatistic.UpdatedDate = DateTime.Now;
                    oldStatistic.UpdatedBy = User.Identity.Name;
                    _statisticService.Update(oldStatistic);
                    _statisticService.Save();

                    var responeData = Mapper.Map<Statistic, StatisticViewModel>(oldStatistic);
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

                    var delStatistic = _statisticService.Delete(id);
                    _statisticService.Save();

                    var responeData = Mapper.Map<Statistic, StatisticViewModel>(delStatistic);
                    respone = request.CreateResponse(HttpStatusCode.OK, responeData);


                }
                return respone;
            });
        }

    }
}