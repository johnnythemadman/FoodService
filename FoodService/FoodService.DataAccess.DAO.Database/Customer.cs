using System;
using System.Collections.Generic;

namespace FoodService.FoodService.DataAccess.DAO.Database
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
            OrderCustomer = new HashSet<OrderCustomer>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<Order> Order { get; set; }
        public ICollection<OrderCustomer> OrderCustomer { get; set; }
    }
}
