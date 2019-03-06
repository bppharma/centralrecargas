using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace waCentralRecargas.Controllers
{
    public class PortalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Policies()
        {
            return View();
        }
        public IActionResult Terms()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}