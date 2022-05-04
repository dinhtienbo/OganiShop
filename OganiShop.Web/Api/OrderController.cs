using AutoMapper;
using Newtonsoft.Json.Linq;
using OganiShop.Model.Models;
using OganiShop.Service;
using OganiShop.Web.Infrastructure.Core;
using OganiShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OganiShop.Web.Api
{
    [RoutePrefix("api/orderdetail")]
    // [Authorize]
    public class OrderController : ApiControllerBase
    {
        #region Initialize
        private IOrderService _orderDetailService;

        public OrderController(IErrorService errorService, IOrderService orderDetailService)
            : base(errorService)
        {
            this._orderDetailService = orderDetailService;
        }

        #endregion

        //[Route("getallparents")]
        //[HttpGet]
        //public HttpResponseMessage GetAll(HttpRequestMessage request)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        var model = _orderDetailService.GetAll();

        //        var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);

        //        var response = request.CreateResponse(HttpStatusCode.OK, responseData);
        //        return response;
        //    });
        //}

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _orderDetailService.GetById(id);

                var response = request.CreateResponse(HttpStatusCode.OK, model);

                return response;
            });
        }

        [Route("{id:int}")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, Order order, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _orderDetailService.Update(order);
                    _orderDetailService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, order);

                }
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string orderStatus, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _orderDetailService.GetAll(orderStatus, keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);


                var responseData = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(query.AsEnumerable());

                var paginationSet = new PaginationSet<OrderViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }



        [Route("updatebyid/{id:int}")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage UpdateById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                JObject json = JObject.Parse(request.Content.ReadAsStringAsync().Result);
                String type = json.GetValue("type").ToString();
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var model = _orderDetailService.GetById(id);

                    _orderDetailService.UpdatePayStatus(id, type);
                    _orderDetailService.Save();

                    //var responseData = Mapper.Map<Order, OrderViewModel>(model);

                    response = request.CreateResponse(HttpStatusCode.OK, response);

                }
                return response;
            });
        }



        //[Route("deletemulti")]
        //[HttpDelete]
        //[AllowAnonymous]
        //public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedProductCategories)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        if (!ModelState.IsValid)
        //        {
        //            response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            var listProductCategory = new JavaScriptSerializer().Deserialize<List<int>>(checkedProductCategories);
        //            foreach (var item in listProductCategory)
        //            {
        //                var model = _orderDetailService.GetById(item);
        //                model.PaymentStatus = false;
        //                _orderDetailService.Update(model);
        //            }

        //            _orderDetailService.Save();

        //            response = request.CreateResponse(HttpStatusCode.OK, listProductCategory.Count);
        //        }

        //        return response;
        //    });
        //}
    }
}
