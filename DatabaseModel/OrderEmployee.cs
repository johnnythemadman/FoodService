using System;
using System.Collections.Generic;

namespace DatabaseModel
{
    public partial class OrderEmployee
    {
        public int OrderIdRef { get; set; }
        public int EmployeeIdRef { get; set; }

        public Employee EmployeeIdRefNavigation { get; set; }
        public Order OrderIdRefNavigation { get; set; }
    }
}
