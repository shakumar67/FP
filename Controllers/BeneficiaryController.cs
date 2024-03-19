using FP.Manager;
using FP.Models;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static FP.Manager.Enums;

namespace FP.Controllers
{
    [Authorize]
    public class BeneficiaryController : BaseController
    {
        FP_DBEntities db = new FP_DBEntities();
        JsonResponseData response = new JsonResponseData();
        int result = 0; bool CheckStatus = false;
        string MSG = string.Empty;
        // GET: Beneficiary
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Beneficiary(Guid? Id, int HindiEng = 1)
        {
            BeneficiaryModel model = new BeneficiaryModel();
            model.HindiEng = HindiEng;
            if (Id != Guid.Empty && Id != null)
            {
                var tbl = db.TBL_Beneficiary.Find(Id);
                if (tbl != null)
                {
                    model.Beneficiary_Id_pk = tbl.Beneficiary_Id_pk;
                    // model.HindiEng = tbl.HindiEng;
                    model.DistrictId_fk = tbl.DistrictId_fk;
                    model.BlockId_fk = tbl.BlockId_fk;
                    model.CLFId_fk = tbl.CLFId_fk;
                    model.PanchayatId_fk = tbl.PanchayatId_fk;
                    model.VillageOId_fk = tbl.VillageOId_fk;
                    model.ReportingMonth = tbl.ReportingMonth;
                    model.ReportingYear = tbl.ReportingYear;
                    model.HealthCenter = tbl.HealthCenter;
                    model.Q1 = tbl.Q1;
                    model.Q2 = tbl.Q2;
                    model.Q3 = tbl.Q3;
                    //model.Q4 = tbl.Q4;
                    model.BFYDOB=tbl.BFYDOB;
                    model.Q5 = tbl.Q5;
                    model.Q6DOMYear = tbl.Q6DOMYear;
                    model.Q6 = tbl.Q6;
                    model.Q6_Year = tbl.Q6_Year;
                    model.Q7 = tbl.Q7;
                    model.Q8 = tbl.Q8;
                    model.Q9 = tbl.Q9;
                    model.Q10 = tbl.Q10;
                    model.Q11 = tbl.Q11;
                    model.YoungestDOB = tbl.YoungestDOB;
                    //model.Q12 = tbl.Q12;
                    model.Q12_1 = tbl.Q12_1;
                    model.Q13 = tbl.Q13;
                    model.Q14 = tbl.Q14;
                    model.Q15 = tbl.Q15;
                    model.Q16 = tbl.Q16;
                    model.Q17 = tbl.Q17;
                    model.Q18 = tbl.Q18;
                    model.Q20 = tbl.Q20;
                    model.Q21 = tbl.Q21;
                    model.BFYVillageName = tbl.BFYVillageName;
                }
            }
            return View(model);
        }
        //[AllowAnonymous]
        [HttpPost]
        [EnableCors("*")]
        public ActionResult PostBeneficiary(List<BeneficiaryModel> model)
        {
            // if (ModelState.IsValid) { }
            int res = 0;
            try
            {
                FP_DBEntities _db = new FP_DBEntities();
                JsonResponseData response = new JsonResponseData();
                List<TBL_Beneficiary> tbllist = new List<TBL_Beneficiary>();
                TBL_Beneficiary tbl;
                if (!ModelState.IsValid)
                {
                    var d = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired);
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired), Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;

                }
                if (model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        string leftBFYName = item.Q3.Substring(0, 3);
                        string leftBFYmobile = leftBFYName + item.Q7;
                        if (_db.TBL_Beneficiary.Any(x => x.Q1 == leftBFYmobile && (x.Beneficiary_Id_pk != item.Beneficiary_Id_pk || x.Beneficiary_Id_pk == Guid.Empty)))
                        {
                            var data1 = _db.TBL_Beneficiary.Where(x => x.Q1 == leftBFYmobile)?.FirstOrDefault();

                            var d = Enums.GetEnumDescription(Enums.eReturnReg.Already);
                            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.Already) + "Beneficiary ID" + data1.Q1, Data = null };
                            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse3.MaxJsonLength = int.MaxValue;
                            return resResponse3;
                        }

                        tbl = item.Beneficiary_Id_pk != Guid.Empty ? _db.TBL_Beneficiary.Find(item.Beneficiary_Id_pk) : new TBL_Beneficiary();
                        tbl.HealthCenter = item.HealthCenter;
                        tbl.ReportingMonth = item.ReportingMonth;
                        tbl.ReportingYear = item.ReportingYear;
                        tbl.BFYVillageName = item.BFYVillageName;
                        tbl.Q2 = item.Q2;
                        tbl.Q3 = item.Q3;
                        //tbl.Q4 = item.Q4;
                        tbl.BFYDOB = item.BFYDOB;
                        tbl.Q5 = item.Q5;
                        tbl.Q6DOMYear = item.Q6DOMYear;
                        tbl.Q6 = item.Q6DOMYear == 1 ? item.Q6 : null;
                        tbl.Q6_Year = item.Q6DOMYear == 2 ? item.Q6_Year : null;
                        tbl.Q7 = item.Q7;
                        tbl.Q8 = item.Q8;
                        tbl.Q9 = item.Q9;
                        tbl.Q10 = item.Q10;
                        tbl.Q11 = item.Q11;
                        tbl.Q12_1 = item.Q12_1;
                        tbl.YoungestDOB = (item.Q12_1 == "Boy" || item.Q12_1 == "Girl") ? item.YoungestDOB : null;
                        //tbl.Q12_1 = item.Q12 != 0 ? item.Q12_1 : null;
                        tbl.Q13 = item.Q13;
                        tbl.Q14 = item.Q14;
                        tbl.Q15 = item.Q15;
                        tbl.Q16 = item.Q15 == 1 ? item.Q16 : null;
                        tbl.Q17 = item.Q15 == 2 ? item.Q17 : null;
                        tbl.Q18 = item.Q15 == 4 ? item.Q18 : null;
                        tbl.Q20 = item.Q20;
                        tbl.Q21 = item.Q21;
                        tbl.IsActive = true;
                        if (item.Beneficiary_Id_pk == Guid.Empty)
                        {
                            tbl.Beneficiary_Id_pk = Guid.NewGuid();
                            tbl.HindiEng = item.HindiEng;
                            tbl.DistrictId_fk = item.DistrictId_fk;
                            tbl.BlockId_fk = item.BlockId_fk;
                            tbl.CLFId_fk = item.CLFId_fk;
                            tbl.PanchayatId_fk = item.PanchayatId_fk;
                            tbl.VillageOId_fk = item.VillageOId_fk;
                            tbl.CreatedBy = MvcApplication.CUser.Id;
                            tbl.CreatedOn = DateTime.Now;
                            tbllist.Add(tbl);
                        }
                        else
                        {
                            tbl.BlockId_fk = item.BlockId_fk;
                            tbl.CLFId_fk = item.CLFId_fk;
                            tbl.PanchayatId_fk = item.PanchayatId_fk;
                            tbl.VillageOId_fk = item.VillageOId_fk;
                            tbl.UpdatedBy = MvcApplication.CUser.Id;
                            tbl.UpdatedOn = DateTime.Now;
                            res += _db.SaveChanges();
                        }
                    }

                    if (tbllist.Count > 0)
                    {
                        db.TBL_Beneficiary.AddRange(tbllist);
                        res = db.SaveChanges();
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
        public ActionResult BFYList()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetBFYList(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SPBFYList(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_BFYData", tbllist);
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
        public ActionResult FollowupList()
        {
            CMFollowupModel model = new CMFollowupModel();
            return View(model);
        }
        public ActionResult GetFollowupList(CMFollowupModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SPFollowUpDataList(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_BFYFollowDataList", tbllist);
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

        public ActionResult BFYFollow()
        {
            CMFollowupModel model = new CMFollowupModel();
            return View(model);
        }
        public ActionResult GetBFYFollowList(CMFollowupModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SPBFYFUpMonthList(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_BFYFollow", tbllist);
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
        public ActionResult AddFollowup(CMFollowupModel model)
        {
            int res = 0;
            FP_DBEntities _db = new FP_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                if (!ModelState.IsValid)
                {
                    var d = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired);
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired), Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }
                if (_db.tbl_BFYFollowup.Any(x => x.FMonth == model.FMonth && x.FYear == model.FYear && (x.FollowupID_pk != model.FollowupID_pk && model.FollowupID_pk == Guid.Empty)
                   && x.BFYID_fk == model.BFYID_fk))
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.Already), Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }
                var tblf = model.FollowupID_pk != Guid.Empty ? _db.tbl_BFYFollowup.Find(model.FollowupID_pk) : new tbl_BFYFollowup();

                tblf.FMonth = model.FMonth;
                tblf.FYear = model.FYear;
                tblf.IsFollowUp = model.IsFollowUp;
                tblf.IsContraception = model.IsContraception;
                tblf.ContraceptionId_fk = model.ContraceptionId_fk;
                tblf.ContraceptionOther = model.ContraceptionOther;
                tblf.UseMethodId_fk = model.UseMethodId_fk;
                tblf.ModuleRollout = model.ModuleRollout;
                tblf.ModuleRolloutId_fk = model.ModuleRolloutId_fk;
                if (model.FollowupID_pk == Guid.Empty)
                {
                    tblf.FollowupID_pk = Guid.NewGuid();
                    tblf.BFYID_fk = model.BFYID_fk;
                    tblf.CreatedBy = MvcApplication.CUser.Id;
                    tblf.CreatedOn = DateTime.Now;
                    _db.tbl_BFYFollowup.Add(tblf);
                }
                else
                {
                    tblf.UpdatedBy = MvcApplication.CUser.Id;
                    tblf.UpdatedOn = DateTime.Now;
                }
                res = _db.SaveChanges();
                if (res > 0)
                {
                    response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.Insert), Data = null };
                    var resResponse = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse.MaxJsonLength = int.MaxValue;
                    return resResponse;
                }
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.Error), Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;

            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.Error), Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse3.MaxJsonLength = int.MaxValue;
                return resResponse3;
            }
        }
        public ActionResult GetBFYFollowView(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SPCMFollowupView(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_BFYFollowView", tbllist);
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