using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.DTO;
using FoodService.FoodService.DataAccess.DAO.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Controllers
{
    public class HomeController : Controller
    {
        [Route("/Home")]
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Dashboard()
        {
            IList<Order> orders = null;
            using (var context = new FoodServiceContext())
            {
                orders = context.Order.ToList();
            }
            var model = new DashboardModel { Orders = orders };

            return View("Dashboard", model);
        }

        public ActionResult CreateOrderItem(OrderItem orderItem)
        {
            return null;
        }
    }

}