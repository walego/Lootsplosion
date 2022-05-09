using Lootsplosion.Common;
using Lootsplosion.Models.LootPool;
using Lootsplosion.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lootsplosion.MVC.Controllers
{
    public class LootPoolController : Controller
    {
        private readonly EnumCollection _enum = new EnumCollection();
        private LootPoolService CreatePoolService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LootPoolService(userId);
            return service;
        }
        private LootService CreateLootService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LootService(userId);
            return service;
        }
        private LootSourceService CreateSourceService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LootSourceService(userId);
            return service;
        }
        public ActionResult Index()
        {
            var service = CreatePoolService();
            var model = service.GetAllLootPools();

            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.LootId = GetLootList();
            ViewBag.LootSourceId = GetSourceList();
            ViewBag.SecretRarity = _enum.GetRarities();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LootPoolCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreatePoolService();
            if(service.CreateLootPool(model))
            {
                TempData["SaveResult"] = "Pool Created";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Pool unable to be created");
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var service = CreatePoolService();
            var model = service.GetPoolById(id);
            if (model.LootPoolId == -1)
                return HttpNotFound();

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreatePoolService();
            var detail = service.GetPoolById(id);
            if (detail.LootPoolId == -1)
                return HttpNotFound();
            var model = new LootPoolEdit
            {
                LootPoolId = detail.LootPoolId,
                LootId = detail.LootId,
                LootSourceId =detail.LootSourceId,
                CurrentRarity = detail.SecretRarity,
                SecretRarity = detail.SecretRarity
            };
            ViewBag.LootId = GetLootList();
            ViewBag.SourceId = GetSourceList();
            ViewBag.SecretRarity = _enum.GetRarities();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LootPoolEdit model)
        {
            ViewBag.LootId = GetLootList();
            ViewBag.SourceId = GetSourceList();
            ViewBag.SecretRarity = _enum.GetRarities();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.LootPoolId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreatePoolService();
            if (service.UpdateLootPool(model))
            {
                TempData["Save Result"] = "Pool updated";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Pool was unable to be updated");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreatePoolService();
            var model = service.GetPoolById(id);
            if (model.LootSourceId == -1)
                return HttpNotFound();

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePool(int id)
        {
            var service = CreatePoolService();
            int sourceId = service.GetPoolById(id).LootSourceId;
            service.DeleteLootPool(id);
            TempData["SaveResult"] = "";
            return RedirectToAction($"LootPools/{sourceId}","LootSource");
        }
        private SelectList GetLootList()
        {
            var lootService = CreateLootService();
            var lootList = lootService.GetLootDescriptions();
            return new SelectList(lootList, "LootId", "Loot");
        }
        private SelectList GetSourceList()
        {
            var sourceService = CreateSourceService();
            var sourceList = sourceService.GetSources();
            return new SelectList(sourceList, "LootSourceId", "SourceName");
        }
    }
}