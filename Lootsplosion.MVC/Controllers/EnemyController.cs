using Lootsplosion.Common;
using Lootsplosion.Models.Enemy;
using Lootsplosion.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lootsplosion.MVC.Controllers
{
    public class EnemyController : Controller
    {
        private readonly EnumCollection _enum = new EnumCollection();
        private EnemyService CreateEnemyService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new EnemyService(userId);
            return service;
        }
        // GET: Item
        public ActionResult Index()
        {
            var service = CreateEnemyService();
            var model = service.GetEnemies();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EnemyCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateEnemyService();
            if (service.CreateEnemy(model))
            {
                TempData["SaveResult"] = "Enemy successfully created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Enemy unable to be created");
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var service = CreateEnemyService();
            var model = service.GetEnemyById(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateEnemyService();
            var detail = service.GetEnemyById(id);
            var model = new EnemyEdit
            {
                EnemyId = detail.EnemyId,
                EnemyName = detail.EnemyName,
                EnemyDescription = detail.EnemyDescription,
                IsBoss = detail.IsBoss,
                IsElite = detail.IsElite,
                Level = detail.Level,
                Strength = detail.Strength,
                Intelligence = detail.Intelligence,
                Vitality = detail.Vitality,
                Mobility = detail.Mobility,
                CritChance = detail.CritChance,
                Skills = detail.Skills,
                WorldPulls = detail.WorldPulls
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EnemyEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.EnemyId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateEnemyService();
            if (service.UpdateEnemy(model))
            {
                TempData["Save Result"] = "Enemy updated";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Enemy was unable to be updated");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateEnemyService();
            var model = service.GetEnemyById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEnemy(int id)
        {
            var service = CreateEnemyService();
            service.DeleteEnemy(id);
            TempData["SaveResult"] = "Enemy Deleted";
            return RedirectToAction("Index");
        }
    }
}