using FoodService.DAO.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodService.Queuing.Interfaces
{
    public interface IRabbitWrapper
    {
        void AddToQueue(OrderItem orderItem);
    }
}
