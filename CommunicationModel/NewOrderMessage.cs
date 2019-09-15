using DatabaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationModel
{
    public class NewOrderMessage
    {
        public Order NewOrder;
        public OrderCustomer NewOrderCustomer;
        public OrderEmployee NewOrderEmployee;
        public IList<OrderItem> NewOrderItems;

        public NewOrderMessage(Order order, OrderCustomer orderCustomer, OrderEmployee orderEmployee, IList<OrderItem> orderItems)
        {
            NewOrder = order;
            NewOrderCustomer = orderCustomer;
            NewOrderEmployee = orderEmployee;
            NewOrderItems = orderItems;
        }
    }
}
