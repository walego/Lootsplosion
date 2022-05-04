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
            var rarities = from Rarity r in Enum.GetValues(typeof(Rarity)) select new { Id = (int)r, Name = r.ToString() };
            ViewBag.Rarity = new SelectList(rarities, "Id", "Name", "Rarity");
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