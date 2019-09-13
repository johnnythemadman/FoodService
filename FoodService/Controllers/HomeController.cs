using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FoodService.DTO;
using FoodService.FoodService.DataAccess.DAO.Database;
using FoodService.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> AddNewOrder(CreateOrderModel createOrderModel)
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


            using (var context = new FoodServiceContext())
            {
                await context.Order.AddAsync(newOrder);
                await context.OrderCustomer.AddAsync(newOrderCustomer);
                await context.OrderEmployee.AddAsync(newOrderEmployee);
                await context.OrderItem.AddRangeAsync(newOrderItems);

                await context.SaveChangesAsync();
                await context.Entry(newOrder).Reference(x => x.EmployeeRefNavigation).LoadAsync();
                await context.Entry(newOrder).Reference(x => x.CustomerIdRefNavigation).LoadAsync();
                await context.Entry(newOrder).Reference(x => x.PaymentIdRefNavigation).LoadAsync();
                try
                {
                    decimal totalPrice = 0;
                    foreach (var orderItem in newOrderItems)
                    {
                        await context.Entry(orderItem).Reference(x => x.FoodItemIdRefNavigation).LoadAsync();
                        totalPrice += orderItem.FoodItemIdRefNavigation.UnitPrice * orderItem.Quantity;
                    }

                    var toAppend = $"<tr><td>{newOrder.OrderId}</td>" +
                                   $"<td>{newOrder.CustomerIdRefNavigation.FirstName}, {newOrder.CustomerIdRefNavigation.LastName}</td>" +
                                   $"<td>{newOrder.EmployeeRefNavigation.FirstName}, {newOrder.EmployeeRefNavigation.LastName}</td>" +
                                   $"<td>{newOrder.PaymentIdRefNavigation.Code}</td>" +
                                   $"<td>{newOrder.Status}</td>" +
                                   $"<td>{newOrder.Timestamp.ToString()}</td>"+
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

        [HttpPost]
        public async Task<IActionResult> TestMethod([FromForm]string input)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(15000);
            });

            return Json(new
            {
                status = input
            });
        }

    }
}