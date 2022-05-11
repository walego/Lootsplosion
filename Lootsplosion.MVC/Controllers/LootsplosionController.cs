using Lootsplosion.Common;
using Lootsplosion.Models.LootPull;
using Lootsplosion.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lootsplosion.MVC.Controllers
{
    public class LootsplosionController : Controller
    {
        private readonly EnumCollection _enum = new EnumCollection();
        //Service Methods
        private LootsplosionService CreateLootsplosionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LootsplosionService(userId);
            return service;
        }
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
        private EnemyService CreateEnemyService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new EnemyService(userId);
            return service;
        }
        //Index View
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LootSource()
        {
            ViewBag.SelectedId = GetSourceList();
            return View();
        }
        [HttpPost]
        public ActionResult LootSource(DropdownId model)
        {
            var sourceService = CreateSourceService();
            var weightCalc = sourceService.RarityWeightCalculationsForRandom(model.SelectedId);
            if (weightCalc.WeightMultiplier == 0)
                ModelState.AddModelError("", "Mathematically unable to pull loot from source");
            var pullService = CreateLootsplosionService();
            var loot = pullService.LootPull(weightCalc);
            return RedirectToAction("Index");
        }
        public ActionResult Enemy()
        {
            ViewBag.SelectedId = GetEnemyList();
            return View();
        }
        [HttpPost]
        public ActionResult Enemy(DropdownId model)
        {
            return RedirectToAction("Index");
        }
        private SelectList GetSourceList()
        {
            var sourceService = CreateSourceService();
            var sourceList = sourceService.GetSources();
            return new SelectList(sourceList, "LootSourceId", "SourceName");
        }
        private SelectList GetEnemyList()
        {
            var enemyService = CreateEnemyService();
            var enemyList = enemyService.GetEnemies();
            return new SelectList(enemyList, "EnemyId", "EnemyName");
        }
    }
}