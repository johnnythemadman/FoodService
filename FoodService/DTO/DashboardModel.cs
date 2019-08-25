using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.FoodService.DataAccess.DAO.Database;

namespace FoodService.DTO
{
    public class DashboardModel
    {
        public IList<Order> Orders { get; set; }
    }
}
