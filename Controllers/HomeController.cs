using FP.Manager;
using FP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FP.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GetIndex(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                DataSet ds = new DataSet();
                ds = SP_Model.SPContraceptive(model);
                if (ds.Tables.Count > 0)
                {
                    IsCheck = true;
                }
                var dslist = JsonConvert.SerializeObject(ds);
                //var html = ConvertViewToString("_BFYData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = dslist }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetChildData(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                DataTable dt = new DataTable();
                dt = SP_Model.SP_GetTotalChild(model);
                if (dt.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var dslist = JsonConvert.SerializeObject(dt);
                var res = Json(new { IsSuccess = IsCheck, Data = dslist }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetModuleData(FilterModel model)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = SP_Model.SP_GetModulerollout(model);
                if (dt.Rows.Count > 0)
                {
                    var dslist = JsonConvert.SerializeObject(dt);
                    var res = Json(new { IsSuccess = true, Data = dslist }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
                else
                {
                    var res = Json(new { IsSuccess = false, Data = Enums.GetEnumDescription(Enums.eReturnReg.RecordNotFound) }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetServiceContraceptionChart(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds = SP_Model.SP_ServiceContraceptionChart(model);
                if (ds.Tables.Count > 0)
                {
                    IsCheck = true;
                    dt = ds.Tables[0];
                }
                var dslist = JsonConvert.SerializeObject(dt);
                //var html = ConvertViewToString("_BFYData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data1 = dslist }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet);
            }
        }
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