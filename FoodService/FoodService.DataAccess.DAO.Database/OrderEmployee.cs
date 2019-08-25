using System;
using System.Collections.Generic;

namespace FoodService.FoodService.DataAccess.DAO.Database
{
    public partial class OrderEmployee
    {
        public int OrderIdRef { get; set; }
        public int EmployeeIdRef { get; set; }

        public Employee EmployeeIdRefNavigation { get; set; }
        public Order OrderIdRefNavigation { get; set; }
    }
}
