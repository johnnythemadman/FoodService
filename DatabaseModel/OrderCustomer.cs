using System;
using System.Collections.Generic;

namespace DatabaseModel
{
    public partial class OrderCustomer
    {
        public int OrderIdRef { get; set; }
        public int CustomerIdRef { get; set; }

        public Customer CustomerIdRefNavigation { get; set; }
        public Order OrderIdRefNavigation { get; set; }
    }
}
