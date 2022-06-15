using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using BeautyCosmetic.Common.ViewModels;
using OganiShop.Common.ViewModels;
using OganiShop.Data.Infrastructure;
using OganiShop.Model.Models;

namespace OganiShop.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);

        IEnumerable<Product> GetStockStatistic();


        IEnumerable<Order> GetAllnew();

        IEnumerable<Order> GetByUsername(string username);
        IEnumerable<object> GetBestSellerStatistic(string fromDate, string toDate);

        Order FindWithRelate(int id);

    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Order> GetAllnew()
        {
            var query = DbContext.Orders;
            return DbContext.Orders;
        }

        public Order FindWithRelate(int id)
        {
            Order result = DbContext.Orders.Include("OrderDetails").Include("OrderDetails.Product").SingleOrDefault(x => x.ID == id);
            //result.OrderDetails = DbContext.OrderDetails.Where(x => x.OrderID == id).ToList();
            return result;
        }

        public Order GetSingleById(int id)
        {
            return DbContext.Orders.Find(id);
        }
        public IEnumerable<object> GetBestSellerStatistic(string fromDate, string toDate)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate)
            };
            return DbContext.Database.SqlQuery<BestSellerStatisticViewModel>("GetBestSellerStatistic @fromDate,@toDate", parameters);

        }

        public IEnumerable<Order> GetByUsername(string username)
        {
            ApplicationUser user = DbContext.Users.Single(x => x.UserName == username);
            return DbContext.Orders.Where(x => x.User.UserName == username).ToList();
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate)
            };
            return DbContext.Database.SqlQuery<RevenueStatisticViewModel>("GetRevenueStatistic @fromDate,@toDate", parameters);
        }

        public IEnumerable<Product> GetStockStatistic()
        {
            return DbContext.Products.Where(x => x.Status).ToList();
        }

    }
}