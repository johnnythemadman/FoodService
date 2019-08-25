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
            var model = new DashboardModel();
            return View("Dashboard", model);
        }

        public ActionResult CreateOrder(Order order)
        {
            return View("CreateOrder");
        }

        public ActionResult AddNewOrder(Order order)
        {
            using (var context = new FoodServiceContext())
            {
                context.Order.Add(order);
                context.SaveChanges();

                return View("Dashboard", new DashboardModel { Orders = context.Order.ToList() });
            }
        }
    }

}