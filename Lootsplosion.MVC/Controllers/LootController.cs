using Lootsplosion.Common;
using Lootsplosion.Models.Loot;
using Lootsplosion.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.MVC.Controllers
{
    public class LootController : Controller
    {
        private readonly EnumCollection _enum = new EnumCollection();
        private LootService CreateItemService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LootService(userId);
            return service;
        }
        // GET: Item
        public ActionResult Index()
        {
            var service = CreateItemService();
            var model = service.GetAllLoot();
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.Rarity = _enum.GetRarities();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LootCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateItemService();
            if (service.CreateLoot(model))
            {
                TempData["SaveResult"] = "Loot successfully created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Loot unable to be created");
            return View(model);
        }
    }
}