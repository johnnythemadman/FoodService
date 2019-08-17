using System;
using System.Collections.Generic;

namespace FoodService.DAO.Database
{
    public partial class Employee
    {
        public Employee()
        {
            Order = new HashSet<Order>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
