using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FP.Controllers
{
    public class PlanController : Controller
    {
        // GET: Plan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreatePlan() 
        {
            return View();
        }
        public ActionResult Details() 
        {
           return RedirectToAction("Index");
        }
    }
}