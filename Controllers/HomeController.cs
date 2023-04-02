using Microsoft.AspNetCore.Mvc;
using RailwayDBKurs.Models;
using System.Diagnostics;

namespace RailwayDBKurs.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return View("~/Views/" + ControllerContext.RouteData.Values["controller"] + "/Index.cshtml");
        }

        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user_role");
            return View("LogIn", "Home");
        }
    }
}