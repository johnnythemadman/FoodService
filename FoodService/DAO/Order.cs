using System;
using System.Collections.Generic;

namespace FoodService.DAO
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int CustomerIdRef { get; set; }
        public int EmployeeRef { get; set; }
        public int PaymentIdRef { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }

        public Customer CustomerIdRefNavigation { get; set; }
        public Employee EmployeeRefNavigation { get; set; }
        public Payment PaymentIdRefNavigation { get; set; }
    }
}
