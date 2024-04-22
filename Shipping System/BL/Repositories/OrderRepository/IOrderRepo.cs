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
       Task<OrderStatusVM> GetStatus(int orderId);
       Task<int> updateStatus(OrderStatusVM orderStatusVM);
        Task<List<OrderVM>> GetRepresntiveOrders(string Representive_ID);
        Task<List<OrderVM>> GetTraderOrders(string Trader_ID);

    }
}
