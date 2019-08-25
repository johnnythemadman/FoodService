using System;
using System.Collections.Generic;

namespace FoodService.FoodService.DataAccess.DAO.Database
{
    public partial class OrderCustomer
    {
        public int OrderIdRef { get; set; }
        public int CustomerIdRef { get; set; }

        public Customer CustomerIdRefNavigation { get; set; }
        public Order OrderIdRefNavigation { get; set; }
    }
}
