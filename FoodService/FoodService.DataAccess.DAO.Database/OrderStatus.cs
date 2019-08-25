using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.FoodService.DataAccess.DAO.Database
{
    public enum OrderStatus
    {
        Unknown = -1,
        Pending = 1,
        Completed = 2
    }
}
