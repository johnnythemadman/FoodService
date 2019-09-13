using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.FoodService.DataAccess.DAO.Database;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodService.DTO
{
    public class DashboardModel
    {
        public DashboardModel()
        {
            using (var context = new FoodServiceContext())
            {
                Orders = context.Order.ToList();
                OrderItems = context.OrderItem.ToList();
                Customers = context.Customer.ToList();
                Employees = context.Employee.ToList();
                FoodItems = context.FoodItem.ToList();
                Payments = context.Payment.ToList();
            }

            var modelData = GetCreateModelData();
            CreateOrderModel = new CreateOrderModel(modelData["Customers"], modelData["Employees"], modelData["Payments"], modelData["FoodItems"]);
        }


        public CreateOrderModel CreateOrderModel { get; set; }

        public IList<Order> Orders { get; }
        public IList<OrderItem> OrderItems { get; }
        public IList<Customer> Customers { get;  }
        public IList<Employee> Employees { get;  }
        public IList<FoodItem> FoodItems { get; }
        public IList<Payment> Payments { get; }

        private IDictionary<string, List<SelectListItem>> GetCreateModelData()
        {
            using (var context = new FoodServiceContext())
            {
                var customers = (from customer in context.Customer
                    select customer).ToList();
                var employees = (from employee in context.Employee select employee).ToList();
                var payments = (from payment in context.Payment select payment).ToList();
                var foodItems = (from foodItem in context.FoodItem select foodItem).ToList();

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
                    },
                    {
                        "Payments", payments.Select(a => new SelectListItem
                        {
                            Value = a.PaymentId.ToString(),
                            Text = $"{a.Code}, {a.Name}"
                        }).ToList()
                    },
                    {
                        "FoodItems", foodItems.Select(a => new SelectListItem
                        {
                            Value = a.FoodItemId.ToString(),
                            Text = $"{a.Name}, {a.UnitPrice}$"
                        }).ToList()
                    }
                };

                return dictionary;
            }

        }

    }
}
