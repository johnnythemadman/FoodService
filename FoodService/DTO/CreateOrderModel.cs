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

        public CreateOrderModel() 
        {
            
            
        }

        public CreateOrderModel(List<SelectListItem> customers, List<SelectListItem> employees,
            List<SelectListItem> payments, List<SelectListItem> foodItems)
        {
            Customers = customers;
            Employees = employees;
            Payments = payments;
            FoodItems = foodItems;
        }

        public List<SelectListItem> Customers { get; }
        public List<SelectListItem> Employees { get;  }
        public List<SelectListItem> Payments { get;  }
        public List<SelectListItem> FoodItems { get; }
        //public List<TypeOfFood> TypeOfFood { get; }

        [Display(Name = "Customer (client)")]
        public string CustomerSelected { get; set; }
        [Display(Name = "Employee (service)")]
        public string EmployeeSelected { get; set; }
        [Display(Name = "Payment method")]
        public string PaymentSelected { get; set; }
        [Display(Name = "Food items")]
        public List<string> FoodItemsSelected { get; set; }

        
    }
}
