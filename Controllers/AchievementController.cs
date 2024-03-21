using FP.Manager;
using FP.Models;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static FP.Manager.Enums;

namespace FP.Controllers
{
    public class AchievementController : Controller
    {
        // GET: Achievement
        public ActionResult Index()
        {
            // SanitizePath("");
            return View();
        }
        #region Achievement Plan (CNRP Level)
        public ActionResult AddAchievePlan()
        {
            AchvPlanModel model = new AchvPlanModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddAchievePlan(AchvPlanModel model)
        {
            var results = 0;
            FP_DBEntities db_ = new FP_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                var resAchvPlanlist = this.Request.Unvalidated.Form["AVPlanModel"];

                if (resAchvPlanlist != null)
                {
                    var mlist = JsonConvert.DeserializeObject<List<AVPlanModel>>(resAchvPlanlist);
                    if (mlist != null)
                    {
                        if (mlist.Count() > 0)
                        {
                            tbl_AchievementPlan tbl;
                            List<tbl_AchievementPlan> tbl_list = new List<tbl_AchievementPlan>();
                            if (model.DistrictId_fk != null && model.BlockId_fk != null && model.ClusterId_fk != null
                                && model.PanchayatId_fk != null && model.PlanYear != null && model.PlanMonth != null)
                            {
                                foreach (var m in mlist)
                                {
                                    if (m.AchieveId_pk == Guid.Empty && m.Meetingheld != null && m.Noofparticipant != null)
                                    {
                                        tbl = new tbl_AchievementPlan()
                                        {
                                            AchieveId_pk = Guid.NewGuid(),
                                            DistrictId_fk = model.DistrictId_fk,
                                            BlockId_fk = model.BlockId_fk,
                                            PanchayatId_fk = model.PanchayatId_fk,
                                            ClusterId_fk = model.ClusterId_fk,
                                            PlanYear = model.PlanYear,
                                            PlanMonth = model.PlanMonth,
                                            VoId_fk = m.VoId_fk,
                                            Meetingheld = m.Meetingheld,
                                            Noofparticipant = m.Noofparticipant,
                                            CreatedBy = MvcApplication.CUser.Id,
                                            CreatedOn = DateTime.Now,
                                            IsActive = true
                                        };
                                        tbl_list.Add(tbl);
                                    }
                                    else if (m.AchieveId_pk != Guid.Empty && m.Meetingheld != null && m.Noofparticipant != null)
                                    {
                                        var tblu = db_.tbl_AchievementPlan.Find(m.AchieveId_pk);
                                        tblu.VoId_fk = m.VoId_fk;
                                        tblu.Meetingheld = m.Meetingheld;
                                        tblu.Noofparticipant = m.Noofparticipant;
                                        tblu.UpdatedBy = MvcApplication.CUser.Id;
                                        tblu.UpdatedOn = DateTime.Now;
                                        tblu.IsActive = true;
                                        results += db_.SaveChanges();
                                    }
                                }
                                if (tbl_list.Count > 0)
                                {
                                    db_.tbl_AchievementPlan.AddRange(tbl_list);
                                }
                                results += db_.SaveChanges();
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
                response = results > 0 ? new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, Achievement Plan Submitted Successfully ! \r\n", Data = null }
                : new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.NotSubmitData), Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse3.MaxJsonLength = int.MaxValue;
                return resResponse3;
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
            }
        }
        public ActionResult GetEditAchPlanList(AchvPlanModel model)
        {
            FP_DBEntities db_ = new FP_DBEntities();
            try
            {
                var items = db_.tbl_AchievementPlan.Where(x => x.DistrictId_fk == model.DistrictId_fk
                && x.BlockId_fk == model.BlockId_fk && x.ClusterId_fk == model.ClusterId_fk && x.PanchayatId_fk == model.PanchayatId_fk
                && x.PlanYear == model.PlanYear && x.PlanMonth == model.PlanMonth).ToList();
                if (items != null && items.Count > 0)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = Enums.GetEnumDescription(eReturnReg.RecordNotFound) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error!." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAchPlanList(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SP_AchvPlanList(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_AchvPlanData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        #endregion

        #region 1st Level Approved Planning (MRP Level)
        public ActionResult AddAchieveApprove()
        {
            AchvPlanModel model = new AchvPlanModel();
            return View(model);
        }
        public ActionResult GetAchApprovePlanList(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SP_AchvPlanApprove(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_AchvPlanApprove", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        [HttpPost]
        public ActionResult AchievePlanApprove(AchvPlanModel model)
        {
            var results = 0; var results_Reject = 0;
            FP_DBEntities db_ = new FP_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                var resAchvPlanlist = this.Request.Unvalidated.Form["AVPlanModel"];

                if (resAchvPlanlist != null)
                {
                    var mlist = JsonConvert.DeserializeObject<List<AVPlanModel>>(resAchvPlanlist);
                    if (mlist != null)
                    {
                        if (mlist.Count() > 0)
                        {
                            tbl_AchievementPlan tbl;
                            List<tbl_AchievementPlan> tbl_list = new List<tbl_AchievementPlan>();
                            if (model.DistrictId_fk != null && model.BlockId_fk != null && model.ClusterId_fk != null
                               && model.PlanYear != null && model.PlanMonth != null)
                            {
                                foreach (var m in mlist)
                                {
                                    if (m.AchieveId_pk != Guid.Empty &&
                                        m.DistrictId != null && m.BlockId != null && m.ClusterId != null &&
                                        m.PanchayatId != null && m.VoId_fk != null && string.IsNullOrWhiteSpace(m.Remark1))
                                    {
                                        //MRP Approve
                                        var tblu = db_.tbl_AchievementPlan.Find(m.AchieveId_pk);
                                        if (tblu != null && m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve))
                                        {
                                            var cdt = DateTime.Now;
                                            tbl_Achievement_Log tblLog = new tbl_Achievement_Log();
                                            tblLog.LogId_pk = Guid.NewGuid();
                                            tblLog.AchieveId_fk = m.AchieveId_pk;
                                            tblLog.PlanStatus = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? Convert.ToInt16(eTypeApprove.Approve) : 0;
                                            tblLog.PlanStatusDate = cdt;
                                            tblLog.CreatedBy = MvcApplication.CUser.Id;
                                            tblLog.CreatedOn = DateTime.Now;
                                            db_.tbl_Achievement_Log.Add(tblLog);
                                            db_.SaveChanges();

                                            tblu.FinalApproved = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? Convert.ToInt16(eTypeApprove.Approve) : 0;
                                            tblu.FinalApprovedDate = cdt;
                                            tblu.FinalApprovedBy = MvcApplication.CUser.Id;

                                            tblu.IsLevel1Approve = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? true : false;
                                            tblu.Level1ApproveDt = cdt;
                                            tblu.Level1ApproveBy = MvcApplication.CUser.Id;
                                            results += db_.SaveChanges();
                                        }
                                    }
                                    else if (m.AchieveId_pk != Guid.Empty && m.PanchayatId != null && m.VoId_fk != null && !string.IsNullOrWhiteSpace(m.Remark1))
                                    {
                                        //MRP Reject
                                        var cdt = DateTime.Now;
                                        var tblu = db_.tbl_AchievementPlan.Find(m.AchieveId_pk);
                                        if (tblu != null && m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject))
                                        {
                                            tbl_Achievement_Log tblLog = new tbl_Achievement_Log();
                                            tblLog.LogId_pk = Guid.NewGuid();
                                            tblLog.AchieveId_fk = m.AchieveId_pk;
                                            tblLog.PlanStatus = m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) ? Convert.ToInt16(eTypeApprove.Reject) : 0;
                                            tblLog.PlanStatusDate = cdt;
                                            tblLog.CreatedBy = MvcApplication.CUser.Id;
                                            tblLog.CreatedOn = DateTime.Now;
                                            db_.tbl_Achievement_Log.Add(tblLog);
                                            db_.SaveChanges();

                                            tblu.FinalApproved = m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) ? Convert.ToInt16(eTypeApprove.Reject) : 0;
                                            tblu.FinalApprovedDate = cdt;
                                            tblu.FinalApprovedBy = MvcApplication.CUser.Id;

                                            tblu.Remark1 = m.Remark1.Trim();
                                            tblu.IsLevel1Reject = true;
                                            tblu.Level1RejectDt = cdt;
                                            tblu.Level1RejectBy = MvcApplication.CUser.Id;
                                            results_Reject += db_.SaveChanges();
                                        }
                                    }
                                }
                                // results += db_.SaveChanges();
                                var groups = mlist.GroupBy(x => x.UserId);
                                foreach (var group in groups)
                                {
                                    var appovlist = group.Where(x => x.PlanApprove == Convert.ToInt16(Enums.eTypeApprove.Approve)).ToList();
                                    var rejectlist = group.Where(x => x.PlanApprove == Convert.ToInt16(Enums.eTypeApprove.Reject)).ToList();
                                    tbl_PaymentHistory tblpay = new tbl_PaymentHistory();
                                    tblpay.PaymentHistoryId_pk = Guid.NewGuid();
                                    tblpay.ApprovedAchvId = string.Join(",", appovlist);
                                    tblpay.RejectedAchvId = string.Join(",", rejectlist);
                                    tblpay.NoofApproved = appovlist.Count;
                                    tblpay.NoofRejected = rejectlist.Count;
                                    tblpay.VerifyUserTypeId = Guid.Parse(MvcApplication.CUser.RoleId);
                                    tblpay.TargetUserTypeId = Guid.Parse(db_.AspNetRoles.First(x => x.Name == CommonModel.RoleNameCont.CNRP).Id);
                                    tblpay.TargetUserId = group.Key;
                                    tblpay.ClaimAmount = CommonModel.GetClaimApprove(appovlist.Count, CommonModel.RoleNameCont.CNRP);
                                    tblpay.ApprovedAmount = CommonModel.GetClaimApprove(rejectlist.Count, CommonModel.RoleNameCont.CNRP);
                                    tblpay.PayMonth = model.PlanMonth;
                                    tblpay.PayYear = model.PlanYear;
                                    tblpay.IsActive = true;
                                    tblpay.CreatedBy = MvcApplication.CUser.Id;
                                    tblpay.UpdatedBy = MvcApplication.CUser.Id;
                                    tblpay.CreatedOn = tblpay.UpdatedOn = DateTime.Now;
                                    db_.tbl_PaymentHistory.Add(tblpay);
                                }
                                db_.SaveChanges();
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
                response = results > 0 && results_Reject == 0 ? new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, Achievement Planning Approved Successfully ! \r\n", Data = null }
                : results == 0 && results_Reject > 0 ? new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = " Congratulations, Achievement Planning Rejected Successfully ! \r\n", Data = null }
                : new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.NotSubmitData), Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse3.MaxJsonLength = int.MaxValue;
                return resResponse3;
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
            }
        }
        #endregion

        #region 2nd Level Approved Planning (CC Level)
        public ActionResult LevelSecAchvApprove()
        {
            AchvPlanModel model = new AchvPlanModel();
            return View(model);
        }
        public ActionResult GetAchApvSecPlanList(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SP_AchvPlanApv2ndlevel(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_AchvPlanApvLevelSec", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        [HttpPost]
        public ActionResult AchvPlanApvLevSec(AchvPlanModel model)
        {
            var results = 0; var results_Reject = 0;
            FP_DBEntities db_ = new FP_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                var resAchvPlanlist = this.Request.Unvalidated.Form["AVPlanModel"];

                if (resAchvPlanlist != null)
                {
                    var mlist = JsonConvert.DeserializeObject<List<AVPlanModel>>(resAchvPlanlist);
                    if (mlist != null)
                    {
                        if (mlist.Count() > 0)
                        {
                            tbl_AchievementPlan tbl;
                            List<tbl_AchievementPlan> tbl_list = new List<tbl_AchievementPlan>();
                            if (model.DistrictId_fk != null && model.BlockId_fk != null && model.ClusterId_fk != null
                               && model.PlanYear != null && model.PlanMonth != null)
                            {
                                foreach (var m in mlist)
                                {
                                    if (m.AchieveId_pk != Guid.Empty &&
                                        m.DistrictId != null && m.BlockId != null && m.ClusterId != null &&
                                        m.PanchayatId != null && m.VoId_fk != null && string.IsNullOrWhiteSpace(m.Remark2))
                                    {
                                        //MRP Approve
                                        var tblu = db_.tbl_AchievementPlan.Find(m.AchieveId_pk);
                                        if (tblu != null && m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve))
                                        {
                                            var cdt = DateTime.Now;
                                            tbl_Achievement_Log tblLog = new tbl_Achievement_Log();
                                            tblLog.LogId_pk = Guid.NewGuid();
                                            tblLog.AchieveId_fk = m.AchieveId_pk;
                                            tblLog.PlanStatus = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? Convert.ToInt16(eTypeApprove.Approve) : 0;
                                            tblLog.PlanStatusDate = cdt;
                                            tblLog.CreatedBy = MvcApplication.CUser.Id;
                                            tblLog.CreatedOn = DateTime.Now;
                                            db_.tbl_Achievement_Log.Add(tblLog);
                                            db_.SaveChanges();

                                            tblu.FinalApproved = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? Convert.ToInt16(eTypeApprove.Approve) : 0;
                                            tblu.FinalApprovedDate = cdt;
                                            tblu.FinalApprovedBy = MvcApplication.CUser.Id;

                                            tblu.IsLevel2Approve = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? true : false;
                                            tblu.Level2ApproveDt = cdt;
                                            tblu.Level2ApproveBy = MvcApplication.CUser.Id;
                                            results += db_.SaveChanges();
                                        }
                                    }
                                    else if (m.AchieveId_pk != Guid.Empty && m.PanchayatId != null && m.VoId_fk != null && !string.IsNullOrWhiteSpace(m.Remark2))
                                    {
                                        //MRP Reject
                                        var tblu = db_.tbl_AchievementPlan.Find(m.AchieveId_pk);
                                        if (tblu != null && m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject))
                                        {
                                            var cdt = DateTime.Now;
                                            tbl_Achievement_Log tblLog = new tbl_Achievement_Log();
                                            tblLog.LogId_pk = Guid.NewGuid();
                                            tblLog.AchieveId_fk = m.AchieveId_pk;
                                            tblLog.PlanStatus = m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) ? Convert.ToInt16(eTypeApprove.Reject) : 0;
                                            tblLog.PlanStatusDate = cdt;
                                            tblLog.CreatedBy = MvcApplication.CUser.Id;
                                            tblLog.CreatedOn = DateTime.Now;
                                            db_.tbl_Achievement_Log.Add(tblLog);
                                            db_.SaveChanges();

                                            tblu.FinalApproved = m.PlanApprove == Convert.ToInt16(eTypeApprove.Reject) ? Convert.ToInt16(eTypeApprove.Reject) : 0;
                                            tblu.FinalApprovedDate = cdt;
                                            tblu.FinalApprovedBy = MvcApplication.CUser.Id;

                                            tblu.Remark2 = m.Remark2.Trim();
                                            tblu.IsLevel2Reject = true;
                                            tblu.Level2RejectDt = cdt;
                                            tblu.Level2RejectBy = MvcApplication.CUser.Id;
                                            results_Reject += db_.SaveChanges();
                                        }
                                    }
                                }
                                // results += db_.SaveChanges();
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
                response = results > 0 && results_Reject == 0 ? new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, Achievement Planning Approved Successfully ! \r\n", Data = null }
                : results == 0 && results_Reject > 0 ? new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = " Congratulations, Achievement Planning Rejected Successfully ! \r\n", Data = null }
                : new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.NotSubmitData), Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse3.MaxJsonLength = int.MaxValue;
                return resResponse3;
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
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