using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.FoodService.DataAccess.DAO.Database;

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
        }

        public IList<Order> Orders { get; set; }
            
        public Order NewOrder { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
        
        public IList<Customer> Customers { get; set; }
        public IList<Employee> Employees { get; set; }
        public IList<FoodItem> FoodItems { get; set; }
        public IList<Payment> Payments { get; set; }
    }
}
