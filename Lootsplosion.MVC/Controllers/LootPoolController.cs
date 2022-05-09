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
    public class LootPoolController : Controller
    {
        private readonly EnumCollection _enum = new EnumCollection();
        private LootPoolService CreatePoolService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LootPoolService(userId);
            return service;
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
    }
}