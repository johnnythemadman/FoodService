using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FoodService.Controllers
{
    public class HomeController : Controller
    {
        [Route("/Home")]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}