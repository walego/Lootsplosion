using Lootsplosion.Common;
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
    }
}