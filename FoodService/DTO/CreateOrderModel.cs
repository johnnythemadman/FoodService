using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FoodService.FoodService.DataAccess.DAO.Database;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodService.DTO
{
    public class CreateOrderModel
    {
        public CreateOrderModel(List<SelectListItem> customers, List<SelectListItem> employees)
        {
            Customers = customers;
            Employees = employees;
        }

        public CreateOrderModel()
        {

        }
        public List<SelectListItem> Customers { get; }
        public List<SelectListItem> Employees { get; }

        [Display(Name = "Customer (client)")]
        public string CustomerSelected { get; set; }
        [Display(Name = "Employee (service)")]
        public string EmployeeSeleced { get; set; }

    }
}
