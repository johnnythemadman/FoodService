using System;
using System.Collections.Generic;

namespace FoodService.FoodService.DataAccess.DAO.Database
{
    public partial class Order
    {
        public Order()
        {
            OrderCustomer = new HashSet<OrderCustomer>();
            OrderEmployee = new HashSet<OrderEmployee>();
        }

        public int OrderId { get; set; }
        public int CustomerIdRef { get; set; }
        public int EmployeeRef { get; set; }
        public int PaymentIdRef { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public Customer CustomerIdRefNavigation { get; set; }
        public Employee EmployeeRefNavigation { get; set; }
        public Payment PaymentIdRefNavigation { get; set; }
        public ICollection<OrderCustomer> OrderCustomer { get; set; }
        public ICollection<OrderEmployee> OrderEmployee { get; set; }
    }
}
