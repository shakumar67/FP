using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FP.Controllers
{
    public class AchievementController : Controller
    {
        // GET: Achievement
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddAchievePlan()
        {
            return View();
        }

    }
}