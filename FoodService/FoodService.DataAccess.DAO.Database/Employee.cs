using System;
using System.Collections.Generic;

namespace FoodService.FoodService.DataAccess.DAO.Database
{
    public partial class Employee
    {
        public Employee()
        {
            Order = new HashSet<Order>();
            OrderEmployee = new HashSet<OrderEmployee>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<Order> Order { get; set; }
        public ICollection<OrderEmployee> OrderEmployee { get; set; }
    }
}
