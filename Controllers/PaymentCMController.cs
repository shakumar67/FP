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

        #region CM Level Monthly Incentive One Monthly Payment Level Approved Planning (MRP Level First)
        public ActionResult CMBFYFollowMIPay(int TypeLayer = 1)
        {
            FilterModel model = new FilterModel();
            model.TypeLayer = TypeLayer;
            model.BtnType = TypeLayer == 1 ? "Validate" : TypeLayer == 2 ? "Checked" : TypeLayer == 3 ? "Recommend" : "Submit";
            return View(model);
        }
        public ActionResult GetCMMRPBFYFollowList(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SP_MIPayBFYApprovedGroupBy(model);
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
                return Json(new { IsSuccess = false, Data = Enums.GetEnumDescription(Enums.eReturnReg.ExceptionError) }, JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult BFYDetailFollowData(Guid BFYId)
        //{
        //    FilterModel model = new FilterModel();
        //    if (BFYId != null && BFYId != Guid.Empty)
        //    {
        //        model.BFYId = Convert.ToString(BFYId);
        //    }
        //    return View(model);
        //}
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
                            var isvaliddb = false;
                            if (model.Month == null || model.Month == 0 || model.Year == null || model.Year == 0)
                            {
                                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.AllFieldsRequired), Data = null };
                                var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
                                resResponse4.MaxJsonLength = int.MaxValue;
                                return resResponse4;
                            }
                            if (CommonModel.RoleNameCont.MRP == MvcApplication.CUser.Role)
                            {
                                if (db_.tbl_CMMIncentivePayment.Any(x => x.DistrictId_fk == model.DistrictId && x.BlockId_fk == model.BlockId && x.ClusterId_fk == model.ClusterId && x.MIMonth == model.Month && x.MIYear == model.Year && model.TypeLayer == 1 && !(x.Approved1By == null || x.Approved1By.Trim() == string.Empty)))
                                {
                                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.Already), Data = null };
                                    var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
                                    resResponse4.MaxJsonLength = int.MaxValue;
                                    return resResponse4;
                                }
                            }
                            else if (CommonModel.RoleNameCont.CC == MvcApplication.CUser.Role )
                            {
                                if (db_.tbl_CMMIncentivePayment.Any(x => x.DistrictId_fk == model.DistrictId && x.BlockId_fk == model.BlockId && x.ClusterId_fk == model.ClusterId && x.MIMonth == model.Month && x.MIYear == model.Year && model.TypeLayer == 2 && !(x.Approved2By == null || x.Approved2By.Trim() == string.Empty)))
                                {
                                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.Already), Data = null };
                                    var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
                                    resResponse4.MaxJsonLength = int.MaxValue;
                                    return resResponse4;
                                }
                            }
                            else if (CommonModel.RoleNameCont.BPM == MvcApplication.CUser.Role)
                            {
                                if (db_.tbl_CMMIncentivePayment.Any(x => x.DistrictId_fk == model.DistrictId && x.BlockId_fk == model.BlockId && x.ClusterId_fk == model.ClusterId && x.MIMonth == model.Month && x.MIYear == model.Year && model.TypeLayer == 3 && !(x.Approved3By == null || x.Approved3By.Trim() == string.Empty)))
                                {
                                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.Already), Data = null };
                                    var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
                                    resResponse4.MaxJsonLength = int.MaxValue;
                                    return resResponse4;
                                }
                            }
                            else 
                            {
                                if (db_.tbl_CMMIncentivePayment.Any(x => x.DistrictId_fk == model.DistrictId && x.BlockId_fk == model.BlockId && x.ClusterId_fk == model.ClusterId && x.MIMonth == model.Month && x.MIYear == model.Year && ((model.TypeLayer == 1 && !(x.Approved1By == null || x.Approved1By.Trim() == string.Empty)) || (model.TypeLayer == 2 && !(x.Approved2By == null || x.Approved2By.Trim() == string.Empty)) || (model.TypeLayer == 3 && !(x.Approved3By == null || x.Approved3By.Trim() == string.Empty)))))
                                {
                                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.Already), Data = null };
                                    var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
                                    resResponse4.MaxJsonLength = int.MaxValue;
                                    return resResponse4;
                                }
                            }
                            //if (CommonModel.RoleNameCont.BPM == MvcApplication.CUser.Role || CommonModel.RoleNameCont.Admin == MvcApplication.CUser.Role)
                            //{
                            //    if (db_.tbl_CMMIncentivePayment.Any(x => x.DistrictId_fk == model.DistrictId && x.BlockId_fk == model.BlockId && x.MIMonth == model.Month && x.MIYear == model.Year))
                            //    {
                            //        response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.Already), Data = null };
                            //        var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
                            //        resResponse4.MaxJsonLength = int.MaxValue;
                            //        return resResponse4;
                            //    }
                            //}
                            if (CommonModel.RoleNameCont.MRP == MvcApplication.CUser.Role || CommonModel.RoleNameCont.CC == MvcApplication.CUser.Role || CommonModel.RoleNameCont.Admin == MvcApplication.CUser.Role)
                            {
                                if (mlist.Count() > 0 && model.DistrictId != null && model.BlockId != null && model.ClusterId != null
                                   && model.Month != null && model.Year != null)
                                {
                                    isvaliddb = true;
                                }
                            }
                            if (CommonModel.RoleNameCont.BPM == MvcApplication.CUser.Role || CommonModel.RoleNameCont.Admin == MvcApplication.CUser.Role)
                            {
                                if (mlist.Count() > 0 && model.DistrictId != null && model.BlockId != null
                                  && model.Month != null && model.Year != null)
                                {
                                    isvaliddb = true;
                                }
                            }
                            tbl_CMMIncentivePayment tbl;
                            List<tbl_CMMIncentivePayment> tbl_list = new List<tbl_CMMIncentivePayment>();
                            if (isvaliddb)
                            {
                                var getFollowdata = db_.tbl_BFYFollowup.Where(x => x.FMonth == model.Month && x.FYear == model.Year)
                                                           .OrderByDescending(x => x.CreatedOn).ToList();
                                foreach (var m in mlist)
                                {
                                    // m.BFYId_fk != Guid.Empty &&
                                    if (
                                        m.DistrictId_fk != null && m.BlockId_fk != null && m.ClusterId_fk != null &&
                                        m.PanchayatId_fk != null && m.VoId_fk != null)
                                    {
                                        //MRP Approve 1
                                        tbl = m.CMMIPId_pk != Guid.Empty ? db_.tbl_CMMIncentivePayment.Find(m.CMMIPId_pk) : new tbl_CMMIncentivePayment();
                                        if (tbl != null)
                                        {
                                            if (m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve))
                                            {
                                                //
                                                if (m.DistrictId_fk == model.DistrictId && m.BlockId_fk == model.BlockId)
                                                {
                                                    if (m.CMMIPId_pk == Guid.Empty && m.ClusterId_fk == model.ClusterId)
                                                    {
                                                        if (tbl.Approved1Status != true && (CommonModel.RoleNameCont.MRP == MvcApplication.CUser.Role || model.TypeLayer == 1))
                                                        {
                                                            var FollowId = getFollowdata.Where(x => x.BFYID_fk == m.BFYId_fk && x.FMonth == model.Month && x.FYear == model.Year)
                                                             .OrderByDescending(x => x.CreatedOn).Take(1)?.FirstOrDefault()?.FollowupID_pk;

                                                            tbl.CMId = m.CMId;
                                                            tbl.DistrictId_fk = m.DistrictId_fk;
                                                            tbl.BlockId_fk = m.BlockId_fk;
                                                            tbl.ClusterId_fk = m.ClusterId_fk;

                                                            tbl.PanchayatId_fk = m.PanchayatId_fk;
                                                            tbl.VoId_fk = m.VoId_fk;
                                                            tbl.BFYId_fk = m.BFYId_fk;
                                                            tbl.FollowupId_fk = FollowId;
                                                            tbl.MIMonth = model.Month;
                                                            tbl.MIYear = model.Year;
                                                            tbl.CMMIPId_pk = Guid.NewGuid();

                                                            tbl.ClaimedAmount = CommonModel.GetClaimApprove(1, CommonModel.RoleNameCont.CM);
                                                            tbl.Approved1Status = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? true : false;
                                                            tbl.Approved1Date = cdt;
                                                            tbl.Approved1Remarks = model.ApprovedRemarks;
                                                            tbl.Approved1By = MvcApplication.CUser.Id;

                                                        }

                                                        tbl.IsActive = true;
                                                        tbl.CreatedUpdatedOn = cdt;
                                                        tbl_list.Add(tbl);
                                                    }
                                                    else if (m.CMMIPId_pk != Guid.Empty)
                                                    {
                                                        //CC
                                                        if (tbl.CMMIPId_pk != Guid.Empty && m.ClusterId_fk == model.ClusterId && tbl.Approved2Status != true && (CommonModel.RoleNameCont.CC == MvcApplication.CUser.Role || model.TypeLayer == 2))
                                                        {
                                                            tbl.Approved2Status = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? true : false;
                                                            tbl.Approved2Date = cdt;
                                                            tbl.Approved2Remarks = model.ApprovedRemarks;
                                                            tbl.Approved2By = MvcApplication.CUser.Id;
                                                            results += db_.SaveChanges();
                                                        }
                                                        //BPM
                                                        else if (tbl.CMMIPId_pk != Guid.Empty && tbl.Approved3Status != true && (CommonModel.RoleNameCont.BPM == MvcApplication.CUser.Role || model.TypeLayer == 3))
                                                        {
                                                            tbl.Approved3Status = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? true : false;
                                                            tbl.Approved3Date = cdt;
                                                            tbl.Approved3Remarks = model.ApprovedRemarks;
                                                            tbl.Approved3By = MvcApplication.CUser.Id;
                                                            results += db_.SaveChanges();
                                                        }
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
                                var strjoin = "";

                                if (CommonModel.RoleNameCont.MRP == MvcApplication.CUser.Role || CommonModel.RoleNameCont.Admin == MvcApplication.CUser.Role)
                                {
                                    if (tbl_list.Count > 0)
                                    {
                                        var grplist = tbl_list.Select(x => x.CMMIPId_pk);
                                        strjoin = string.Join(",", grplist).ToUpper();
                                    }
                                    else
                                    {
                                        var grplist = mlist.Select(x => x.CMMIPId_pk);
                                        strjoin = string.Join(",", grplist).ToUpper();
                                    }
                                }
                                else
                                {
                                    var grplist = mlist.Select(x => x.CMMIPId_pk);
                                    strjoin = string.Join(",", grplist).ToUpper();
                                }

                                var groups = mlist.GroupBy(x => x.CMId);
                                if (groups != null && results > 0)
                                {
                                    foreach (var group in groups)
                                    {
                                        var appovlist = group.Where(x => x.PlanApprove == Convert.ToInt16(Enums.eTypeApprove.Approve)).ToList();
                                        tbl_PaymentHistory tblpay = new tbl_PaymentHistory();
                                        tblpay.PaymentHistoryId_pk = Guid.NewGuid();
                                        tblpay.ApprovedAchvId = strjoin;
                                        tblpay.NoofApproved = appovlist.Count;
                                        tblpay.VerifyUserTypeId = Guid.Parse(MvcApplication.CUser.RoleId);
                                        tblpay.TargetUserTypeId = Guid.Parse(db_.AspNetRoles.First(x => x.Name == CommonModel.RoleNameCont.CM).Id);
                                        tblpay.TargetUserId = group.Key;
                                        tblpay.TypeofPayment = Enums.GetEnumDescription(eTypeOfPayment.MonthlyCM);
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

                                if (results > 0)
                                {
                                    response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Congratulations,Monthly Incentive" + GetEnumDescription(Enums.eReturnReg.Insert) + "Successfully! \r\n", Data = null };
                                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                                    resResponse3.MaxJsonLength = int.MaxValue;
                                    return resResponse3;
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
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) + "\r\n", Data = null };
                var resResponseerr1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponseerr1.MaxJsonLength = int.MaxValue;
                return resResponseerr1;
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