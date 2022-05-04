using OganiShop.Data.Infrastructure;
using OganiShop.Data.Repositories;
using OganiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiShop.Service
{
    public interface IOrderService
    {
        Order Create(ref Order order, List<OrderDetail> orderDetails);
        void UpdateStatus(int orderId);
        void UpdatePayStatus(int orderId, string type);
        void Save();

        IEnumerable<Order> GetAll();

        IEnumerable<Order> GetAll(string type, string keyword);

        IEnumerable<Order> GetByUsername(string username);

        IEnumerable<OrderDetail> GetListdetailByOrderId(int id);

        Order GetById(int id);
        void Update(Order order);
    }
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IOrderDetailRepository _orderDetailRepository;
        IUnitOfWork _unitOfWork;


        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public Order Create(ref Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();

                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderID = order.ID;
                    _orderDetailRepository.Add(orderDetail);
                }
                return order;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateStatus(int orderId)
        {
            var order = _orderRepository.GetSingleById(orderId);
            order.Status = true;
            _orderRepository.Update(order);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAllnew();
        }

        public IEnumerable<Order> GetByUsername(string username)
        {
            return _orderRepository.GetByUsername(username);
        }

        public IEnumerable<Order> GetAll(string type, string keyword)
        {
            int keyWordAsInt = 0;
            try
            {
                keyWordAsInt = int.Parse(keyword);
            }
            catch (Exception)
            {

            }
            if (!string.IsNullOrEmpty(keyword))
            {
                if (type != "")
                {
                    return _orderRepository.GetMulti(x => x.OrderStatus == type && (x.CustomerName.Contains(keyword) || x.ID == keyWordAsInt));
                }
                else
                {
                    return _orderRepository.GetMulti(x => x.CustomerName.Contains(keyword) || x.ID == keyWordAsInt);
                }
            }
            else
            {
                if (type != "" && type != null)
                {
                    return _orderRepository.GetMulti(x => x.OrderStatus == type);
                }
                else
                {
                    return _orderRepository.GetAllnew();
                }
            }
        }

        public IEnumerable<OrderDetail> GetListdetailByOrderId(int id)
        {
            return _orderDetailRepository.GetMulti(x => x.OrderID == id);
        }

        public Order GetById(int id)
        {
            return _orderRepository.FindWithRelate(id);
        }

        public void UpdatePayStatus(int orderId, string type)
        {
            var order = _orderRepository.GetSingleById(orderId);
            if (type == "thanhtoan")
            {
                order.PaymentStatus = "yes";
            }
            else if (type == "xuly")
            {
                order.OrderStatus = "Đang xử lý";
            }
            else if (type == "giaohang")
            {
                order.OrderStatus = "Đang giao";
            }
            else if (type == "hoanthanh")
            {
                order.OrderStatus = "Hoàn tất";
            }
            else if (type == "huydonhang")
            {
                order.OrderStatus = "Hủy đơn hàng";
                _orderDetailRepository.CancelOrder(orderId);
            }
            _orderRepository.Update(order);
        }

        public void Update(Order order)
        {
            var oldOrder = _orderRepository.GetSingleById(order.ID);
            oldOrder.CustomerAddress = order.CustomerAddress;
            oldOrder.CustomerName = order.CustomerName;
            oldOrder.CustomerMobile = order.CustomerMobile;
            oldOrder.CustomerMessage = order.CustomerMessage;
            oldOrder.CustomerEmail = order.CustomerEmail;
            _orderRepository.Update(oldOrder);
        }
    }
}
