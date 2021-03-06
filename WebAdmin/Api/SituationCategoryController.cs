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
using WebAdmin.Infrastructure.Extensions;
using WebAdmin.Models;

namespace WebAdmin.Api
{
    [RoutePrefix("api/situationcategory")]
    public class SituationCategoryController : ApiControllerBase
    {
        #region
        private ISituationCategoryService _situationCategoryService;
        public SituationCategoryController(IErrorService errorService, ISituationCategoryService situationCategoryService)
            : base(errorService)
        {
            _situationCategoryService = situationCategoryService;
        }
        #endregion
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _situationCategoryService.GetAll(keyword);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responeData = Mapper.Map<IEnumerable<SituationCategory>, IEnumerable<SituationCategoryViewModel>>(query);
                var paginationSet = new PaginationSet<SituationCategoryViewModel>()
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
        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _situationCategoryService.GetAll();
                var responeData = Mapper.Map<IEnumerable<SituationCategory>, IEnumerable<SituationCategoryViewModel>>(model);
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
                var model = _situationCategoryService.GetById(id);
                var responeData = Mapper.Map<SituationCategory, SituationCategoryViewModel>(model);
                var respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                return respone;
            });
        }
        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, SituationCategoryViewModel situationCategoryVm)
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
                    var newSituationCategory = new SituationCategory();
                    newSituationCategory.UpdateSituationCategory(situationCategoryVm);
                    newSituationCategory.CreatedDate = DateTime.Now;
                    _situationCategoryService.Add(newSituationCategory);
                    _situationCategoryService.Save();

                    var responeData = Mapper.Map<SituationCategory, SituationCategoryViewModel>(newSituationCategory);
                    respone = request.CreateResponse(HttpStatusCode.OK, responeData);

                }
                return respone;
            });
        }
        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, SituationCategoryViewModel situationCategoryVm)
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
                    var oldSituationCategory = _situationCategoryService.GetById(situationCategoryVm.ID);
                    oldSituationCategory.UpdateSituationCategory(situationCategoryVm);
                    oldSituationCategory.UpdatedDate = DateTime.Now;
                    _situationCategoryService.Update(oldSituationCategory);
                    _situationCategoryService.Save();

                    var responeData = Mapper.Map<SituationCategory, SituationCategoryViewModel>(oldSituationCategory);
                    respone = request.CreateResponse(HttpStatusCode.OK, responeData);

                }
                return respone;
            });
        }
        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request,int id)
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
                    var dbSituationCategory = _situationCategoryService.GetById(id);
                    if(dbSituationCategory.ParentID ==null)
                    {
                        dbSituationCategory = _situationCategoryService.Delete(id);
                        _situationCategoryService.Save();

                        var responeData = Mapper.Map<SituationCategory, SituationCategoryViewModel>(dbSituationCategory);
                        respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                    }
                    else
                    {
                        respone = request.CreateErrorResponse(HttpStatusCode.Forbidden,"Vui lòng xóa hết các danh mục con.");
                    }

                }
                return respone;
            });
        }
    }
}