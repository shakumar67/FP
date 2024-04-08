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
    public class MobilizationPaymentController : Controller
    {
        // GET: MobilizationPayment
        public ActionResult Index()
        {
            return View();
        }
        #region  Mobilization Incentive Payment Approved Adopted Service Beneficiary (MRP Level First)
        public ActionResult MobilizationIPayment(int TypeLayer = 1)
        {
            FilterModel model = new FilterModel();
            model.TypeLayer = TypeLayer;
            model.BtnType = TypeLayer == 1 ? "Validate" : TypeLayer == 2 ? "Checked" : TypeLayer == 3 ? "Approved" : "Submit";
            return View(model);
        }
        public ActionResult GetMobilizationIPayList(FilterModel model)
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SP_Model.SPMobilizationIPaymentBFY(model);
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_MobilizationIPBFYData", tbllist);
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
        public ActionResult PostDataPayLevel1(ServiceBFYMainModel model)
        {
            var results = 0;
            FP_DBEntities db_ = new FP_DBEntities();
            JsonResponseData response = new JsonResponseData();
            var cdt = DateTime.Now;
            try
            {
                var reslist = this.Request.Unvalidated.Form["PModel"];
                if (reslist != null)
                {
                    var mlist = JsonConvert.DeserializeObject<List<ServiceBFYModel>>(reslist);
                    if (mlist != null)
                    {
                        tbl_BFYService tbl;
                        var isvaliddb = false;
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
                        if (isvaliddb)
                        {
                            if (model.Month == null || model.Month == 0 || model.Year == null || model.Year == 0)
                            {
                                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.AllFieldsRequired), Data = null };
                                var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
                                resResponse4.MaxJsonLength = int.MaxValue;
                                return resResponse4;
                            }
                            foreach (var m in mlist)
                            {
                                if (m.BFYId_fk != Guid.Empty &&
                                    m.ServiceBFYId_pk != Guid.Empty)
                                {
                                    //MRP Approve 1
                                    tbl = db_.tbl_BFYService.Find(m.ServiceBFYId_pk);
                                    if (tbl != null && tbl.BFYId_fk == m.BFYId_fk && tbl.ServiceBFYId_pk == m.ServiceBFYId_pk)
                                    {
                                        if (m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve))
                                        {
                                            if (m.ServiceBFYId_pk != Guid.Empty && tbl.Approved1Status != true && (CommonModel.RoleNameCont.MRP == MvcApplication.CUser.Role || model.TypeLayer == 1))
                                            {
                                                tbl.Approved1Status = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? true : false;
                                                tbl.Approved1Date = cdt;
                                                tbl.Approved1Remarks = model.ApprovedRemarks;
                                                tbl.Approved1By = MvcApplication.CUser.Id;
                                                results += db_.SaveChanges();
                                            }
                                            else if (m.ServiceBFYId_pk != Guid.Empty && tbl.Approved2Status != true && (CommonModel.RoleNameCont.CC == MvcApplication.CUser.Role || model.TypeLayer == 2))
                                            {
                                                tbl.Approved2Status = m.PlanApprove == Convert.ToInt16(eTypeApprove.Approve) ? true : false;
                                                tbl.Approved2Date = cdt;
                                                tbl.Approved2Remarks = model.ApprovedRemarks;
                                                tbl.Approved2By = MvcApplication.CUser.Id;
                                                results += db_.SaveChanges();
                                            }
                                            else if (m.ServiceBFYId_pk != Guid.Empty && tbl.Approved3Status != true && (CommonModel.RoleNameCont.BPM == MvcApplication.CUser.Role || model.TypeLayer == 3))
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
                            var groups = mlist.GroupBy(x => x.ReportedByUserId);
                            var grplist = mlist.Select(x => x.ServiceBFYId_pk);
                            if (groups != null && results > 0)
                            {
                                foreach (var group in groups)
                                {
                                    var appovlist = group.Where(x => x.PlanApprove == Convert.ToInt16(Enums.eTypeApprove.Approve)).ToList();
                                    tbl_PaymentHistory tblpay = new tbl_PaymentHistory();
                                    tblpay.PaymentHistoryId_pk = Guid.NewGuid();
                                    tblpay.ApprovedAchvId = string.Join(",", grplist).ToUpper();
                                    tblpay.NoofApproved = appovlist.Count;
                                    tblpay.VerifyUserTypeId = Guid.Parse(MvcApplication.CUser.RoleId);
                                    tblpay.TargetUserTypeId = Guid.Parse(db_.AspNetRoles.First(x => x.Name == CommonModel.RoleNameCont.CM).Id);
                                    tblpay.TargetUserId = group.Key;
                                    tblpay.TypeofPayment = Enums.GetEnumDescription(eTypeOfPayment.MobilizationCMCNRP);
                                    tblpay.ClaimAmount = (CommonModel.GetClaimMobilization().CNRP * appovlist.Count);
                                    tblpay.ApprovedAmount = (CommonModel.GetClaimMobilization().CM * appovlist.Count);
                                    tblpay.MobilizationCM = (CommonModel.GetClaimMobilization().CM * appovlist.Count);
                                    tblpay.MobilizationCNRP = (CommonModel.GetClaimMobilization().CNRP * appovlist.Count);
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
                                response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Congratulations,Mobilization Incentive" + GetEnumDescription(Enums.eReturnReg.Insert) + "Successfully! \r\n", Data = null };
                                var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
                                resResponse4.MaxJsonLength = int.MaxValue;
                                return resResponse4;
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
                else
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.AllFieldsRequired), Data = null };
                    var resResponse = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse.MaxJsonLength = int.MaxValue;
                    return resResponse;
                }
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.ExceptionError), Data = null };
                var resResponse = Json(response, JsonRequestBehavior.AllowGet);
                resResponse.MaxJsonLength = int.MaxValue;
                return resResponse;
            }
            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = GetEnumDescription(Enums.eReturnReg.NotSubmitData), Data = null };
            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
            resResponse3.MaxJsonLength = int.MaxValue;
            return resResponse3;
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