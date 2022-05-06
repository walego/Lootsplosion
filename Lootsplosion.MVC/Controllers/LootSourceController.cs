﻿using Lootsplosion.Common;
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
        private LootSourceService CreateSourceService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LootSourceService(userId);
            return service;
        }
        // GET: Item
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
                SourceType = detail.SourceType,
                Pulls = detail.Pulls,
            };
            ViewBag.SourceType = _enum.GetLootSourceTypes();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LootSourceEdit model)
        {
            ViewBag.SourceType = _enum.GetLootSourceTypes();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.SourceType == SourceType.World)
            {
                ModelState.AddModelError("", "World Loot Source Creation is not available to users");
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
        public ActionResult DeleteEnemy(int id)
        {
            var service = CreateSourceService();
            service.DeleteSource(id);
            TempData["SaveResult"] = "Loot Source Deleted";
            return RedirectToAction("Index");
        }
    }
}