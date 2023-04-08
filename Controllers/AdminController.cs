using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RailwayDBKurs.Models;

namespace RailwayDBKurs.Controllers
{
    public class AdminController : Controller
    {
        ApplicationContext db;
        public AdminController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            if (!HttpContext.Session.Keys.Contains("user_role"))
            {
                HttpContext.Session.SetString("user_role", "admin");
            }
            return View();
        }


        public IActionResult RegionList()
        {
            return View(db.Regions.ToList());
        }

        public IActionResult AddRegion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRegion(Region region)
        {
            db.Regions.Add(region);
            db.SaveChanges();
            return RedirectToAction("RegionList");
        }

        public IActionResult EditRegion(int id)
        {
            return View(db.Regions.Find(id));
        }

        [HttpPost]
        public IActionResult EditRegion(Region region)
        {
            db.Regions.Update(region);
            db.SaveChanges();
            return RedirectToAction("RegionList");
        }

        public IActionResult DeleteRegion(int id)
        {
            Region region = db.Regions.Find(id);
            if (region != null)
            {
                db.Regions.Remove(region);
                db.SaveChanges();
            }
            return RedirectToAction("RegionList");
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

        public IActionResult AddDepot()
        {
            ViewBag.regions = new SelectList(db.Regions.Select(x => x.ID).ToArray());
            return View();
        }

        [HttpPost]
        public IActionResult AddDepot(Depot depot)
        {
            db.Depot.Add(depot);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError("", "Участок или название уже используются. Введите другие значения.");
                ViewBag.regions = new SelectList(db.Regions.Select(x => x.ID).ToArray(), depot.RegionID);
                return View(depot);
            }
            return RedirectToAction("DepotList");
        }

        public IActionResult EditDepot(int id)
        {
            ViewBag.regions = new SelectList(db.Regions.Select(x => x.ID).ToArray());
            return View(db.Depot.Find(id));
        }

        [HttpPost]
        public IActionResult EditDepot(Depot depot)
        {
            db.Depot.Update(depot);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError("", "Участок или название уже используются. Введите другие значения.");
                ViewBag.regions = new SelectList(db.Regions.Select(x => x.ID).ToArray(), depot.RegionID);
                return View(depot);
            }
            return RedirectToAction("DepotList");
        }

        public IActionResult DeleteDepot(int id)
        {
            Depot depot = db.Depot.Find(id);
            if (depot != null)
            {
                db.Depot.Remove(depot);
                db.SaveChanges();
            }
            return RedirectToAction("DepotList");
        }


        public IActionResult VanList()
        {
            return View(db.Vans.ToList());
        }

        public IActionResult AddVan()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddVan(Van van)
        {
            db.Vans.Add(van);
            db.SaveChanges();
            return RedirectToAction("VanList");
        }

        public IActionResult EditVan(int id)
        {
            return View(db.Vans.Find(id));
        }

        [HttpPost]
        public IActionResult EditVan(Van van)
        {
            db.Vans.Update(van);
            db.SaveChanges();
            return RedirectToAction("VanList");
        }

        public IActionResult DeleteVan(int id)
        {
            Van van = db.Vans.Find(id);
            if (van != null)
            {
                db.Vans.Remove(van);
                db.SaveChanges();
            }
            return RedirectToAction("VanList");
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

        public IActionResult AddRepairVanEvent()
        {
            ViewBag.vans = new SelectList(db.Vans.Select(x => x.ID).ToArray());
            ViewBag.depots = new SelectList(db.Depot.Select(x => new { x.ID, x.Name }).ToArray(), "ID", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddRepairVanEvent(RepairVanEvent repairVanEvent, CommonRepairEvent commonRepairEvent)
        {
            db.Database.ExecuteSql($"dbo.NewRepairVanEvent {commonRepairEvent.DepotID}, {commonRepairEvent.BeginDate}, {commonRepairEvent.EndDate}, {commonRepairEvent.Status}, {commonRepairEvent.Description}, {repairVanEvent.VanID}, {repairVanEvent.ListOfRepairedParts}, {repairVanEvent.Type}");
            return RedirectToAction("RepairVanEventList");
        }

        public IActionResult EditRepairVanEvent(int id)
        {
            RepairVanEvent repairVanEvent = db.RepairVanEvents.Find(id);
            CommonRepairEvent commonRepairEvent = db.RepairEvents.Find(id);
            ViewBag.vanEventDetails = repairVanEvent;
            ViewBag.commonEventDetails = db.RepairEvents.Find(id);
            ViewBag.vans = new SelectList(db.Vans.Select(x => x.ID).ToArray(), repairVanEvent.VanID);
            ViewBag.depots = new SelectList(db.Depot.Select(x => new { x.ID, x.Name }).ToArray(), "ID", "Name", commonRepairEvent.DepotID);
            return View();
        }

        [HttpPost]
        public IActionResult EditRepairVanEvent(RepairVanEvent repairVanEvent, CommonRepairEvent commonRepairEvent)
        {
            db.RepairEvents.Update(commonRepairEvent);
            db.RepairVanEvents.Update(repairVanEvent);
            db.SaveChanges();
            return RedirectToAction("RepairVanEventList");
        }

        public IActionResult DeleteRepairVanEvent(int id)
        {
            CommonRepairEvent commonRepairEvent = db.RepairEvents.Find(id);
            if (commonRepairEvent != null)
            {
                db.RepairEvents.Remove(commonRepairEvent);
                db.SaveChanges();
            }

            return RedirectToAction("RepairVanEventList");
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

        public IActionResult AddRepairRailwayEvent()
        {
            ViewBag.regions = new SelectList(db.Regions.Select(x => x.ID).ToArray());
            ViewBag.depots = new SelectList(db.Depot.Select(x => new { x.ID, x.Name }).ToArray(), "ID", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddRepairRailwayEvent(RepairRailwayEvent repairRailwayEvent, CommonRepairEvent commonRepairEvent)
        {
            db.Database.ExecuteSql($"dbo.NewRepairRailwayEvent {commonRepairEvent.DepotID}, {commonRepairEvent.BeginDate}, {commonRepairEvent.EndDate}, {commonRepairEvent.Status}, {commonRepairEvent.Description}, {repairRailwayEvent.RegionID}, {repairRailwayEvent.BeginPoint}, {repairRailwayEvent.EndPoint}");
            return RedirectToAction("RepairRailwayEventList");
        }

        public IActionResult EditRepairRailwayEvent(int id)
        {
            RepairRailwayEvent repairRailwayEvent = db.RepairRailwayEvents.Find(id);
            CommonRepairEvent commonRepairEvent = db.RepairEvents.Find(id);
            ViewBag.railwayEventDetails = repairRailwayEvent;
            ViewBag.commonEventDetails = commonRepairEvent;
            ViewBag.regions = new SelectList(db.Regions.Select(x => x.ID).ToArray(), repairRailwayEvent.RegionID);
            ViewBag.depots = new SelectList(db.Depot.Select(x => new { x.ID, x.Name }).ToArray(), "ID", "Name", commonRepairEvent.DepotID);
            return View();
        }

        [HttpPost]
        public IActionResult EditRepairRailwayEvent(RepairRailwayEvent repairRailwayEvent, CommonRepairEvent commonRepairEvent)
        {
            db.RepairEvents.Update(commonRepairEvent);
            db.RepairRailwayEvents.Update(repairRailwayEvent);
            db.SaveChanges();
            return RedirectToAction("RepairRailwayEventList");
        }

        public IActionResult DeleteRepairRailwayEvent(int id)
        {
            CommonRepairEvent commonRepairEvent = db.RepairEvents.Find(id);
            if (commonRepairEvent != null)
            {
                db.RepairEvents.Remove(commonRepairEvent);
                db.SaveChanges();
            }

            return RedirectToAction("RepairRailwayEventList");
        }
    }
}