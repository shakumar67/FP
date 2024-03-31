using FP.Manager;
using FP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FP.Controllers
{
    public class PaymentCMController : Controller
    {
        // GET: PaymentCM
        public ActionResult Index()
        {
            return View();
        }

        #region CM Level Monthly Incentive 2nd Monthly Payment Level Approved Planning (MRP Level First)
        public ActionResult CMBFYFollowMIPay()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetCMMRPBFYFollowList(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SPMIPayBFYApproved(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_MIncPayBFYData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BFYDetailFollowData(Guid BFYId)
        {
            FilterModel model = new FilterModel();
            if (BFYId != null && BFYId != Guid.Empty)
            {
                model.BFYId = Convert.ToString(BFYId);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult PostDataCMMIPay(List<CMMIncentivePayModel> modellist)
        {
            return View();
        }

        #endregion

        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }
    }
}