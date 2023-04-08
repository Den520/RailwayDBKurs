using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RailwayDBKurs.Models;

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

        [HttpPost]
        public IActionResult DepotList(string NameSearch)
        {
            Depot[] depots = db.Depot.ToArray();
            if (NameSearch != null)
            {
                depots = depots.Where(x => x.Name.ToLower().Contains(NameSearch.ToLower())).ToArray();
            }
            return View(depots.ToList());
        }


        public IActionResult VanList()
        {
            return View(db.Vans.ToList());
        }


        public IActionResult RepairVanEventList()
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
                    })
                .Join(
                    db.Depot.ToArray(),
                    o => o.extraValues.DepotID,
                    i => i.ID,
                    (o, i) => new {
                        mainValues = o.mainValues,
                        extraValues = o.extraValues,
                        DepotName = i.Name
                    });
            ViewBag.VanEvents = fullVanEvents;
            return View();
        }

        [HttpPost]
        public IActionResult RepairVanEventList(string StatusFilter)
        {
            RepairVanEvent[] vanEvents = db.RepairVanEvents.ToArray();
            CommonRepairEvent[] commonEvents = db.RepairEvents.ToArray();

            if (StatusFilter != "Выбрать все")
            {
                commonEvents = commonEvents.Where(x => x.Status == StatusFilter).ToArray();
            }

            var fullVanEvents = vanEvents
                .Join(
                    commonEvents,
                    o => o.ID,
                    i => i.ID,
                    (o, i) => new
                    {
                        mainValues = o,
                        extraValues = i
                    })
                .Join(
                    db.Depot.ToArray(),
                    o => o.extraValues.DepotID,
                    i => i.ID,
                    (o, i) => new {
                        mainValues = o.mainValues,
                        extraValues = o.extraValues,
                        DepotName = i.Name
                    });
            ViewBag.VanEvents = fullVanEvents;
            return View();
        }


        public IActionResult RepairRailwayEventList()
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
                    })
                .Join(
                    db.Depot.ToArray(),
                    o => o.extraValues.DepotID,
                    i => i.ID,
                    (o, i) => new {
                        mainValues = o.mainValues,
                        extraValues = o.extraValues,
                        DepotName = i.Name
                    });
            ViewBag.RailwayEvents = fullRailwayEvents;
            return View();
        }

        [HttpPost]
        public IActionResult RepairRailwayEventList(string StatusFilter)
        {
            RepairRailwayEvent[] railwayEvents = db.RepairRailwayEvents.ToArray();
            CommonRepairEvent[] commonEvents = db.RepairEvents.ToArray();

            if (StatusFilter != "Выбрать все")
            {
                commonEvents = commonEvents.Where(x => x.Status == StatusFilter).ToArray();
            }

            var fullRailwayEvents = railwayEvents
                .Join(
                    commonEvents,
                    o => o.ID,
                    i => i.ID,
                    (o, i) => new
                    {
                        mainValues = o,
                        extraValues = i
                    })
                .Join(
                    db.Depot.ToArray(),
                    o => o.extraValues.DepotID,
                    i => i.ID,
                    (o, i) => new {
                        mainValues = o.mainValues,
                        extraValues = o.extraValues,
                        DepotName = i.Name
                    });
            ViewBag.RailwayEvents = fullRailwayEvents;
            return View();
        }
    }
}