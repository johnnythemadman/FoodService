using System;
using System.Collections.Generic;

namespace FoodService.DAO.Database
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int FoodItemIdRef { get; set; }

        public FoodItem FoodItemIdRefNavigation { get; set; }
    }
}
