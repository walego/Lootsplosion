using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lootsplosion.MVC.Controllers
{
    public class LootController : Controller
    {
        // GET: Loot
        public ActionResult Index()
        {
            return View();
        }
    }
}