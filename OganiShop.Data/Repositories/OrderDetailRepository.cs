using OganiShop.Data.Infrastructure;
using OganiShop.Model.Models;
using System.Linq;

namespace OganiShop.Data.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void CancelOrder(int orderId);
    }

    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public void CancelOrder(int orderId)
        {
            foreach (var item in DbContext.OrderDetails.Where(x => x.OrderID == orderId).ToList())
            {
                if (item.ProductID != 0)
                {
                    Product product = DbContext.Products.Find(item.ProductID);
                    product.Quantity += item.Quantity;
                }
            }
            DbContext.SaveChanges();
        }
    }
}