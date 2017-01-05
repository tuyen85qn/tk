﻿using AutoMapper;
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
    [RoutePrefix("api/resolvedSituation")]
    [Authorize]
    public class ResolvedSituationController : ApiControllerBase
    {
        #region
        private IResolvedSituationService _resolvedSituationService;
        public ResolvedSituationController(IErrorService errorService, IResolvedSituationService resolvedSituationService) : base(errorService)
        {
            this._resolvedSituationService = resolvedSituationService;
        }
        #endregion
        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _resolvedSituationService.GetAll(keyword);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);
                var responeData = Mapper.Map<IEnumerable<ResolvedSituation>, IEnumerable<ResolvedSituationViewModel>>(query);
                var paginationSet = new PaginationSet<ResolvedSituationViewModel>()
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
                var model = _resolvedSituationService.GetAll();
                var responeData = Mapper.Map<IEnumerable<ResolvedSituation>, IEnumerable<ResolvedSituationViewModel>>(model);
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
                var model = _resolvedSituationService.GetById(id);
                var responeData = Mapper.Map<ResolvedSituation, ResolvedSituationViewModel>(model);
                var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                return respone;
            });
        }
    }
}