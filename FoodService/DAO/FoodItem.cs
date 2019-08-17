using System;
using System.Collections.Generic;

namespace FoodService.DAO
{
    public partial class FoodItem
    {
        public FoodItem()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int FoodItemId { get; set; }
        public int TypeOfFoodIdRef { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public TypeOfFood TypeOfFoodIdRefNavigation { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
