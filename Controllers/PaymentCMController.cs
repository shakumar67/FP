using FP.Manager;
using FP.Models;
using Newtonsoft.Json;
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
    public class PaymentCMController : Controller
    {
        // GET: PaymentCM
        FP_DBEntities db = new FP_DBEntities();
        JsonResponseData response = new JsonResponseData();
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
        public ActionResult PostDataCMMIPay(CMMIPModel model)
        {
            var results = 0;
            FP_DBEntities db_ = new FP_DBEntities();
            JsonResponseData response = new JsonResponseData();
            var cdt = DateTime.Now;
            try
            {
                var reslist = this.Request.Unvalidated.Form["CMMIPModel"];
                if (reslist != null)
                {
                    var mlist = JsonConvert.DeserializeObject<List<CMMIncentivePayModel>>(reslist);
                    if (mlist != null)
                    {
                        if (mlist.Count() > 0)
                        {
                            if (model.Month == null || model.Month == 0 || model.Year == null || model.Year == 0)
                            {
                                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.AllFieldsRequired), Data = null };
                                var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
                                resResponse4.MaxJsonLength = int.MaxValue;
                                return resResponse4;
                            }
                            if (db_.tbl_CMMIncentivePayment.Any(x => x.DistrictId_fk == model.DistrictId && x.BlockId_fk == model.BlockId && x.ClusterId_fk == model.ClusterId && x.MIMonth == model.Month && x.MIYear == model.Year))
                            {
                                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.Already), Data = null };
                                var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
                                resResponse4.MaxJsonLength = int.MaxValue;
                                return resResponse4;
                            }

                            tbl_CMMIncentivePayment tbl;
                            List<tbl_CMMIncentivePayment> tbl_list = new List<tbl_CMMIncentivePayment>();
                            if (model.DistrictId != null && model.BlockId != null && model.ClusterId != null
                               && model.Month != null && model.Year != null)
                            {
                                foreach (var m in mlist)
                                {
                                    if (m.BFYId_fk != Guid.Empty &&
                                        m.DistrictId_fk != null && m.BlockId_fk != null && m.ClusterId_fk != null &&
                                        m.PanchayatId_fk != null && m.VoId_fk != null)
                                    {
                                        //MRP Approve 1
                                        tbl = m.CMMIPId_pk != Guid.Empty ? db_.tbl_CMMIncentivePayment.Find(m.CMMIPId_pk) : new tbl_CMMIncentivePayment();
                                        if (tbl != null)
                                        {
                                            if (m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve))
                                            {
                                                if (m.CMMIPId_pk == Guid.Empty)
                                                {
                                                    tbl.CMMIPId_pk = Guid.NewGuid();
                                                    if (m.DistrictId_fk == model.DistrictId && m.BlockId_fk == model.BlockId && m.ClusterId_fk == model.ClusterId)
                                                    {
                                                        tbl.DistrictId_fk = m.DistrictId_fk;
                                                        tbl.BlockId_fk = m.BlockId_fk;
                                                        tbl.ClusterId_fk = m.ClusterId_fk;

                                                        tbl.PanchayatId_fk = m.PanchayatId_fk;
                                                        tbl.VoId_fk = m.VoId_fk;
                                                        tbl.BFYId_fk = m.BFYId_fk;
                                                        tbl.FollowupId_fk = m.FollowupId_fk;
                                                        tbl.MIMonth = model.Month;
                                                        tbl.MIYear = model.Year;
                                                        tbl.Approved1Status = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? true : false;
                                                        tbl.Approved1Date = cdt;
                                                        tbl.Approved1Remarks = model.ApprovedRemarks;
                                                        tbl.Approved1By = MvcApplication.CUser.Id;
                                                        tbl.IsActive = true;
                                                        tbl_list.Add(tbl);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (tbl_list.Count > 0)
                                {
                                    db_.tbl_CMMIncentivePayment.AddRange(tbl_list);
                                    results += db_.SaveChanges();
                                }
                                // results += db_.SaveChanges();
                                var groups = mlist.GroupBy(x => x.ReportedByUserId);
                                if (groups != null && results > 0)
                                {
                                    foreach (var group in groups)
                                    {
                                        var appovlist = group.Where(x => x.PlanApprove == Convert.ToInt16(Enums.eTypeApprove.Approve)).ToList();
                                        tbl_PaymentHistory tblpay = new tbl_PaymentHistory();
                                        tblpay.PaymentHistoryId_pk = Guid.NewGuid();
                                        tblpay.ApprovedAchvId = string.Join(",", appovlist);
                                        tblpay.NoofApproved = appovlist.Count;
                                        tblpay.VerifyUserTypeId = Guid.Parse(MvcApplication.CUser.RoleId);
                                        tblpay.TargetUserTypeId = Guid.Parse(db_.AspNetRoles.First(x => x.Name == CommonModel.RoleNameCont.CM).Id);
                                        tblpay.TargetUserId = group.Key;
                                        tblpay.ClaimAmount = CommonModel.GetClaimApprove(appovlist.Count, CommonModel.RoleNameCont.CM);
                                        tblpay.ApprovedAmount = CommonModel.GetClaimApprove(appovlist.Count, CommonModel.RoleNameCont.CM);
                                        tblpay.PayMonth = model.Month;
                                        tblpay.PayYear = model.Year;
                                        tblpay.IsActive = true;
                                        tblpay.CreatedBy = MvcApplication.CUser.Id;
                                        tblpay.UpdatedBy = MvcApplication.CUser.Id;
                                        tblpay.CreatedOn = cdt;
                                        tblpay.UpdatedOn = cdt;
                                        db_.tbl_PaymentHistory.Add(tblpay);
                                        results += db_.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) + "\r\n", Data = null };
                                var resResponseerr = Json(response, JsonRequestBehavior.AllowGet);
                                resResponseerr.MaxJsonLength = int.MaxValue;
                                return resResponseerr;
                            }
                        }
                    }
                }
                response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Congratulations,Monthly Incentive" + GetEnumDescription(Enums.eReturnReg.Insert) + "Successfully! \r\n", Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse3.MaxJsonLength = int.MaxValue;
                return resResponse3;
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.ExceptionError), Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse3.MaxJsonLength = int.MaxValue;
                return resResponse3;
            }
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