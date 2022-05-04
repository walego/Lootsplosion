using Lootsplosion.Models.Item;
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
    [Authorize]
    public class ItemController : Controller
    {
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
            var rarities = from Rarity r in Enum.GetValues(typeof(Rarity)) select new { Id = (int)r, Name = r.ToString() };
            var types = from ItemType t in Enum.GetValues(typeof(ItemType)) select new { Id = (int)t, Name = t.ToString() };
            ViewBag.Rarity = new SelectList(rarities, "Id", "Name", "Rarity");
            ViewBag.Type = new SelectList(types, "Id", "Name", "Type");
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
            if(service.CreateItem(model))
            {
                TempData["SaveResult"] = "Item successfully created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Item unable to be created");
            return View(model);
        }
    }
}