using OganiShop.Service;
using OganiShop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OganiShop.Web.Api
{
    [RoutePrefix("api/statistic")]
    public class StatisticController : ApiControllerBase
    {
        IStatisticService _statisticService;
        public StatisticController(IErrorService errorService, IStatisticService statisticService) : base(errorService)
        {
            _statisticService = statisticService;
        }

        [Route("getrevenue")]
        [HttpGet]
        public HttpResponseMessage GetRevenueStatistic(HttpRequestMessage request, string fromDate, string toDate)
        {

            return CreateHttpResponse(request, () =>
            {
                var model = _statisticService.GetRevenueStatistic(fromDate, toDate);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
        [Route("getstock")]
        [HttpGet]
        public HttpResponseMessage GetStock(HttpRequestMessage request)
        {

            return CreateHttpResponse(request, () =>
            {
                var model = _statisticService.GetStockStatistic();
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("getbestseller")]
        [HttpGet]
        public HttpResponseMessage GetBestSeller(HttpRequestMessage request, string fromDate, string toDate)
        {

            return CreateHttpResponse(request, () =>
            {
                var model = _statisticService.GetBestSellerStatistic(fromDate, toDate).ToList();
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
    }
}