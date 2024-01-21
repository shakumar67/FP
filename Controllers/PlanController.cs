using FP.Manager;
using FP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static FP.Manager.Enums;

namespace FP.Controllers
{
    public class PlanController : Controller
    {
        FP_DBEntities db = new FP_DBEntities();
        JsonResponseData response = new JsonResponseData();
        int result = 0; bool CheckStatus = false;
        string MSG = string.Empty;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreatePlan() 
        {
            PlanModel model = new PlanModel();  
            return View(model);
        }
        public ActionResult CreatePlan(PlanModel model)
        {
            int res = 0;
            try
            {
                FP_DBEntities _db = new FP_DBEntities();
                JsonResponseData response = new JsonResponseData();
                if (!ModelState.IsValid)
                {
                    var d = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired);
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired), Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }
                if (model != null)
                {
                    var tbl = model.PlanID_pk != Guid.Empty ? _db.tbl_Plan.Find(model.PlanID_pk) : new tbl_Plan();
                    if (tbl != null)
                    {
                        tbl.PlanDt = model.PlanDt;
                        tbl.HVDt = model.HVDt;
                        tbl.DOMDt = model.DOMDt;
                        tbl.DOMHVDt = model.DOMHVDt;
                        tbl.SubjectId = model.SubjectId;
                        tbl.IsActive = true;
                        if (model.PlanID_pk == Guid.Empty)
                        {
                            tbl.PlanID_pk = Guid.NewGuid();
                            tbl.DistrictId_fk = model.DistrictId_fk;
                            tbl.BlockId_fk = model.BlockId_fk;
                            tbl.PanchayatId_fk = model.PanchayatId_fk;
                            tbl.VoId_fk = model.VoId_fk;
                            tbl.CreatedBy = User.Identity.Name;
                            tbl.CreatedOn = DateTime.Now;
                            db.tbl_Plan.Add(tbl);
                        }
                        else
                        {
                            tbl.UpdatedBy = User.Identity.Name;
                            tbl.UpdatedOn = DateTime.Now;
                            res += _db.SaveChanges();
                        }
                    }
                    if (res > 0)
                    {
                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.Insert), Data = null };
                        var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse3.MaxJsonLength = int.MaxValue;
                        return resResponse3;
                    }
                }
                else
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired), Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.Error), Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse3.MaxJsonLength = int.MaxValue;
                return resResponse3;
            }

            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired), Data = null };
            var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
            resResponse4.MaxJsonLength = int.MaxValue;
            return resResponse4;
        }
        public ActionResult Details() 
        {
           return RedirectToAction("Index");
        }
    }
}