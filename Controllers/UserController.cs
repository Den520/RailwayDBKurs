using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RailwayDBKurs.Models;
using System.Collections;
using System.Diagnostics;

namespace RailwayDBKurs.Controllers
{
    public class UserController : Controller
    {
        ApplicationContext db;
        public UserController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            if (!HttpContext.Session.Keys.Contains("user_role"))
            {
                HttpContext.Session.SetString("user_role", "user");
            }
            return View();
        }

        public IActionResult RegionList()
        {
            return View(db.Regions.ToList());
        }


        public IActionResult DepotList()
        {
            return View(db.Depot.ToList());
        }


        public IActionResult VanList()
        {
            return View(db.Vans.ToList());
        }


        public IActionResult RepairVanEventsList()
        {
            RepairVanEvent[] vanEvents = db.RepairVanEvents.ToArray();
            CommonRepairEvent[] commonEvents = db.RepairEvents.ToArray();

            var fullVanEvents = vanEvents
                .Join(
                    commonEvents,
                    o => o.ID,
                    i => i.ID,
                    (o, i) => new
                    {
                        mainValues = o,
                        extraValues = i
                    });
            ViewBag.VanEvents = fullVanEvents;
            return View();
        }


        public IActionResult RepairRailwayEventsList()
        {
            RepairRailwayEvent[] railwayEvents = db.RepairRailwayEvents.ToArray();
            CommonRepairEvent[] commonEvents = db.RepairEvents.ToArray();

            var fullRailwayEvents = railwayEvents
                .Join(
                    commonEvents,
                    o => o.ID,
                    i => i.ID,
                    (o, i) => new
                    {
                        mainValues = o,
                        extraValues = i
                    });
            ViewBag.RailwayEvents = fullRailwayEvents;
            return View();
        }
    }
}
