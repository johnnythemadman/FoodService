using System;
using System.Collections.Generic;

namespace DatabaseModel
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int FoodItemIdRef { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        public FoodItem FoodItemIdRefNavigation { get; set; }
        public Order Order { get; set; }
    }
}
