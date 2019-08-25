using System;
using System.Collections.Generic;

namespace FoodService.FoodService.DataAccess.DAO.Database
{
    public partial class Payment
    {
        public Payment()
        {
            Order = new HashSet<Order>();
        }

        public int PaymentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
