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
        private LootService CreateLootService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LootService(userId);
            return service;
        }
        // GET: Item
        public ActionResult Index()
        {
            var service = CreateLootService();
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
            var service = CreateLootService();
            if (service.CreateLoot(model))
            {
                TempData["SaveResult"] = "Loot successfully created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Loot unable to be created");
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var service = CreateLootService();
            var model = service.GetLootById(id);
            if (model.LootId == -1)
                return HttpNotFound();

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateLootService();
            var detail = service.GetLootById(id);
            if (detail.LootId == -1)
                return HttpNotFound();
            var model = new LootEdit
            {
                LootId = detail.LootId,
                LootName = detail.LootName,
                LootDescription = detail.LootDescription,
                Rarity = detail.Rarity,
                WorldDrop = detail.WorldDrop
            };
            ViewBag.Rarity = _enum.GetRarities();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LootEdit model)
        {
            ViewBag.Rarity = _enum.GetRarities();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.LootId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateLootService();
            if (service.UpdateLoot(model))
            {
                TempData["Save Result"] = "Loot updated";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Loot was unable to be updated");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateLootService();
            var model = service.GetLootById(id);
            if (model.LootId == -1)
                return HttpNotFound();

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItem(int id)
        {
            var service = CreateLootService();
            service.DeleteLoot(id);
            TempData["SaveResult"] = "Loot Deleted";
            return RedirectToAction("Index");
        }
    }
}