using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.OrderRepo
{
    public interface IOrderRepo
    {
        Task<int> Add(OrderVM order);

        Task<int> Edit(OrderVM order);

       Task< OrderVM> GetById(int orderId);

       Task<List<OrderVM>> GetAll();

        Task<OrderVM> IncludeLists();
       Task< List<OrderVM>> GetOrdersByDateRange(DateTime fromDate, DateTime toDate);
        Task<int> Delete(int id);
        public Task<OrderStatusVM> GetStatus(int orderId);
        public  Task<int> updateStatus(OrderStatusVM orderStatusVM);

    }
}
