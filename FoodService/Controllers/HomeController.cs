using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunicationModel;
using DatabaseModel;
using FoodService.DTO;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

namespace FoodService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            var model = new DashboardModel();
            return View("Dashboard", new DashboardModel());
        }

        [HttpPost]
        public IActionResult AddNewOrder(CreateOrderModel createOrderModel)
        {
            var timestamp = DateTime.UtcNow;

            var newOrder = new Order
            {
                CustomerIdRef = Int32.Parse(createOrderModel?.CustomerSelected),
                EmployeeRef = Int32.Parse(createOrderModel?.EmployeeSelected),
                PaymentIdRef = Int32.Parse(createOrderModel?.PaymentSelected),
                Description =
                    $"Ordered at {timestamp.ToShortDateString()} by {Int32.Parse(createOrderModel?.CustomerSelected)}. Employee: {Int32.Parse(createOrderModel?.EmployeeSelected)}",
                Status = (new OrderStatus { Status = OrderStatusEnum.Pending }).ToString(),
                Timestamp = timestamp,
            };

            var newOrderCustomer = new OrderCustomer
            {
                CustomerIdRef = newOrder.CustomerIdRef,
                OrderIdRefNavigation = newOrder
            };

            var newOrderEmployee = new OrderEmployee
            {
                EmployeeIdRef = newOrder.EmployeeRef,
                OrderIdRefNavigation = newOrder
            };

            var newOrderItems = (from foodItem in createOrderModel?.FoodItemsSelected
                                 select new OrderItem
                                 {
                                     FoodItemIdRef = Int32.Parse(foodItem),
                                     Quantity = 1,
                                     Order = newOrder
                                 }).ToList();

            var publisher = new Publisher.Publisher();
            try
            {
                var newOrderMessage = new NewOrderMessage(newOrder, newOrderCustomer, newOrderEmployee, newOrderItems);
                publisher.Publish(newOrderMessage);

                string customerFullName = null;
                string employeeFullName = null;
                string paymentCode = null;
                decimal totalPrice = 0;
                using (var context = new FoodServiceContext())
                {
                    var customer = context.Customer.FirstOrDefault(x => x.CustomerId == newOrder.CustomerIdRef);
                    customerFullName = $"{customer.FirstName}, {customer.LastName}";

                    var employee = context.Employee.FirstOrDefault(x => x.EmployeeId == newOrder.EmployeeRef);
                    employeeFullName = $"{employee.FirstName}, {employee.LastName}";

                    var payment = context.Payment.FirstOrDefault(x => x.PaymentId == newOrder.PaymentIdRef);
                    paymentCode = payment.Code;

                    foreach (var orderItem in newOrderItems)
                    {
                        var foodItem = context.FoodItem.FirstOrDefault(x => x.FoodItemId == orderItem.FoodItemIdRef);
                        totalPrice += foodItem.UnitPrice * orderItem.Quantity;
                    }

                }
                var toAppend = $"<tr>"+
                                   $"<td>{customerFullName}</td>" +
                                   $"<td>{employeeFullName}</td>" +
                                   $"<td>{paymentCode}</td>" +
                                   $"<td>{newOrder.Status}</td>" +
                                   $"<td>{newOrder.Timestamp.ToString()}</td>" +
                                   $"<td>{totalPrice}</tr>";
                return Json(new
                {
                    status = "OK",
                    toAppend = toAppend
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = "ERROR"
                });
            }

        }

    }
}