using OganiShop.Common.ViewModels;
using OganiShop.Data.Repositories;
using OganiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiShop.Service
{
    public interface IStatisticService
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);
        IEnumerable<Product> GetStockStatistic();
        IEnumerable<object> GetBestSellerStatistic(string fromDate, string toDate);
    }
    public class StatisticService : IStatisticService
    {
        IOrderRepository _orderRepository;
        public StatisticService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<object> GetBestSellerStatistic(string fromDate, string toDate)
        {
            return _orderRepository.GetBestSellerStatistic(fromDate, toDate);
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate)
        {
            return _orderRepository.GetRevenueStatistic(fromDate, toDate);
        }

        public IEnumerable<Product> GetStockStatistic()
        {
            return _orderRepository.GetStockStatistic();
        }
    }
}