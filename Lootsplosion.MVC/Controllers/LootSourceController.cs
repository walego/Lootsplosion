using Lootsplosion.Common;
using Lootsplosion.Models.LootPool;
using Lootsplosion.Models.LootSource;
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
    public class LootSourceController : Controller
    {
        private readonly EnumCollection _enum = new EnumCollection();

        //Service Methods
        private LootSourceService CreateSourceService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LootSourceService(userId);
            return service;
        }
        private LootService CreateLootService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LootService(userId);
            return service;
        }
        // Index/Create/Details/Edit/Delete
        public ActionResult Index()
        {
            var service = CreateSourceService();
            var model = service.GetSources();
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.SourceType = _enum.GetLootSourceTypes();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LootSourceCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.SourceType == SourceType.World)
            {
                ModelState.AddModelError("", "World Loot Source Creation is not available to users");
                return View(model);
            }
            var service = CreateSourceService();
            if (service.CreateSource(model))
            {
                TempData["SaveResult"] = "Loot Source successfully created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Loot Source unable to be created");
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var service = CreateSourceService();
            var model = service.GetSourceById(id);
            if (model.LootSourceId == -1)
                return HttpNotFound();

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateSourceService();
            var detail = service.GetSourceById(id);
            if (detail.LootSourceId == -1)
                return HttpNotFound();
            var model = new LootSourceEdit
            {
                LootSourceId = detail.LootSourceId,
                SourceName = detail.SourceName,
                SourceDescription = detail.SourceDescription,
                NoLootWeight = detail.NoLootWeight,
                CommonWeight = detail.CommonWeight,
                UncommonWeight = detail.CommonWeight,
                RareWeight = detail.RareWeight,
                EpicWeight = detail.EpicWeight,
                LegendaryWeight = detail.LegendaryWeight,
                Pulls = detail.Pulls,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LootSourceEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.LootSourceId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateSourceService();
            if (service.UpdateSource(model))
            {
                TempData["Save Result"] = "Loot Source updated";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Loot Source was unable to be updated");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateSourceService();
            var model = service.GetSourceById(id);
            if (model.LootSourceId == -1)
                return HttpNotFound();

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSource(int id)
        {
            var service = CreateSourceService();
            service.DeleteSource(id);
            TempData["SaveResult"] = "Loot Source Deleted";
            return RedirectToAction("Index");
        }
        //***EXTRA METHODS***//
        //Details--Gets a list of all loot pools in a loot source
        public ActionResult LootPools(int id)
        {
            var service = CreateSourceService();
            var model = service.GetLootPoolsInSource(id);
            return View(model);
        }
        //Details--Creates a new loot pool automatically attached to the loot source
        public ActionResult AddLoot(int id)
        {
            ViewBag.LootId = GetLootList();
            ViewBag.SecretRarity = _enum.GetRarities();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLoot(int id, LootPoolCreateFromSource model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateSourceService();
            if (service.AddLootToSource(model, id))
            {
                TempData["SaveResult"] = "Loot added to Source.";
                return RedirectToAction($"LootPools/{id}");
            }
            ModelState.AddModelError("", "Loot was unable to be added");
            return View(model);
        }
        // Creates SelectList for Loot Pool Create dropdown menu
        private SelectList GetLootList()
        {
            var lootService = CreateLootService();
            var lootList = lootService.GetLootDescriptions();
            return new SelectList(lootList, "LootId", "Loot");
        }
    }
}