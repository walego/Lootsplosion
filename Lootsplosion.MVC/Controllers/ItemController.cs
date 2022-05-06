using Lootsplosion.Common;
using Lootsplosion.Models.Item;
using Lootsplosion.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.MVC.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly EnumCollection _enum = new EnumCollection();
        private ItemService CreateItemService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ItemService(userId);
            return service;
        }
        // GET: Item
        public ActionResult Index()
        {
            var service = CreateItemService();
            var model = service.GetItems();
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.Rarity = _enum.GetRarities();
            ViewBag.ItemType = _enum.GetItemTypes();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateItemService();
            if (service.CreateItem(model))
            {
                TempData["SaveResult"] = "Item successfully created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Item unable to be created");
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var service = CreateItemService();
            var model = service.GetItemById(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateItemService();
            var detail = service.GetItemById(id);
            var model = new ItemEdit
            {
                ItemId = detail.ItemId,
                ItemName = detail.ItemName,
                ItemDescription = detail.ItemDescription,
                Rarity = detail.Rarity,
                ItemType = detail.ItemType,
                Strength = detail.Strength,
                Intelligence = detail.Intelligence,
                Vitality = detail.Vitality,
                Mobility = detail.Mobility,
                CritChance = detail.CritChance,
                OtherEffects = detail.OtherEffects,
                WorldDrop = detail.WorldDrop
            };
            ViewBag.Rarity = _enum.GetRarities();
            ViewBag.ItemType = _enum.GetItemTypes();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ItemEdit model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Rarity = _enum.GetRarities();
                ViewBag.ItemType = _enum.GetItemTypes();
                return View(model);
            }
            if (model.ItemId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateItemService();
            if (service.UpdateItem(model))
            {
                TempData["Save Result"] = "Item updated";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Item was unable to be updated");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateItemService();
            var model = service.GetItemById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItem(int id)
        {
            var service = CreateItemService();
            service.DeleteItem(id);
            TempData["SaveResult"] = "Item Deleted";
            return RedirectToAction("Index");
        }
    }
}