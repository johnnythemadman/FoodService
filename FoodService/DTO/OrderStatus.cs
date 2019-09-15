using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.DTO
{
    public enum OrderStatusEnum
    {
        Pending,
        Complete
    }

    public class OrderStatus
    {
        public OrderStatusEnum Status { get; set; }

        public override string ToString()
        {
            switch (Status)
            {
                case OrderStatusEnum.Pending:
                    return "Pending";
                case OrderStatusEnum.Complete:
                    return "Complete";
                default:
                    return "Unknown";
            }
        }
        

    }

}
