using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.DTO;
using FoodService.FoodService.DataAccess.DAO.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public ActionResult CreateOrder()
        {
            var modelData = GetCreateModelData();
            var model = new CreateOrderModel(modelData["Customers"], modelData["Employees"]);

            return View("AddNewOrder", model);

        }

        [HttpPost]

        public ActionResult AddNewOrder([Bind("CustomerSelected, EmployeeSelected")] CreateOrderModel model)
        {
            //if (model.CustomerSelected.IsNullOrEmpty())
            //{
            //    RedirectToAction("CreateOrder", "Home");
            //}
            var newOrder = new Order
            {
                CustomerIdRef = Int32.Parse(model?.CustomerSelected),
                EmployeeRef = Int32.Parse(model?.EmployeeSeleced)
            };

            using (var context = new FoodServiceContext())
            {
                //context.Order.Add(newOrder);
                //context.SaveChanges();

                return View("Dashboard", new DashboardModel { Orders = context.Order.ToList() });
            }
        }

        private IDictionary<string, List<SelectListItem>> GetCreateModelData()
        {
            using (var context = new FoodServiceContext())
            {
                var customers = (from lol in context.Customer
                                 select lol).ToList();
                var employees = (from lol in context.Employee select lol).ToList();

                var dictionary = new Dictionary<string, List<SelectListItem>>()
                {
                    {
                        "Customers", customers.Select(a => new SelectListItem
                        {
                            Value = a.CustomerId.ToString(),
                            Text = a.Email
                        }).ToList()
                    },

                    {
                        "Employees", employees.Select(a => new SelectListItem
                        {
                            Value = a.EmployeeId.ToString(),
                            Text = a.Email
                        }).ToList()
                    }
                };

                return dictionary;
            }

        }
    }

}