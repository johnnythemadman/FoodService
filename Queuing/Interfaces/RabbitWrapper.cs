using FoodService.DAO.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodService.Queuing.Interfaces
{
    public interface RabbitWrapper
    {
        void AddToQueue(OrderItem orderItem);

    }
}
