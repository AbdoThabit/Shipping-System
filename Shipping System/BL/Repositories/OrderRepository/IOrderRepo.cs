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
       Task< List<OrderVM>> GetOrdersByDateRange(DateTime fromDate, DateTime toDate , string UserName);
        Task<int> Delete(int id);
       Task<OrderStatusVM> GetStatus(int orderId);
       Task<int> updateStatus(OrderStatusVM orderStatusVM);
        Task<List<OrderVM>> GetRepresntiveOrders(string Representive_UserName);
        Task<List<OrderVM>> GetTraderOrders(string Trader_UserName);
        Task<StatusCountVM> GetAllStatusCount();
        Task<StatusCountVM> GetTraderStatusCount(string User_Id);
        Task<StatusCountVM> GetRepresentiveStatusCount(string User_Id);

    }
}
