namespace DDDSW7.Demo.Model
{
    using System;
    using System.Collections.Generic;

    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order GetById(Guid id);
        Order CreateNewOrder(List<OrderItem> model);
        bool Delete(Guid id);
        IEnumerable<OrderItemViewModel> GetItemsForOrder(Guid id);
        bool AddItemsToOrder(Guid id, List<OrderItem> model);    
        bool ChangeStatus(Guid id, string status, string info);

    }
}