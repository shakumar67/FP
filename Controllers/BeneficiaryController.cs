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
        public ActionResult Beneficiary(Guid ?Id,int HindiEng=1)
        {
            BeneficiaryModel model = new BeneficiaryModel();
            model.HindiEng = HindiEng;
            if (Id !=Guid.Empty && Id !=null)
            {
                var tbl = db.TBL_Beneficiary.Find(Id);
                if (tbl != null)
                {
                    model.Beneficiary_Id_pk = tbl.Beneficiary_Id_pk;
                   // model.HindiEng = tbl.HindiEng;
                    model.DistrictId_fk=tbl.DistrictId_fk;
                    model.BlockId_fk=tbl.BlockId_fk;
                    model.PanchayatId_fk=tbl.PanchayatId_fk;
                    model.VillageOId_fk=tbl.VillageOId_fk;
                    model.ReportingMonth=tbl.ReportingMonth;
                    model.ReportingYear=tbl.ReportingYear;
                    model.HealthCenter=tbl.HealthCenter;
                    model.Q1 = tbl.Q1;
                    model.Q2 = tbl.Q2;
                    model.Q3 = tbl.Q3;
                    model.Q4 = tbl.Q4;
                    model.Q5 = tbl.Q5;
                    model.Q6 = tbl.Q6;
                    model.Q7 = tbl.Q7;
                    model.Q8 = tbl.Q8;
                    model.Q9 = tbl.Q9;
                    model.Q10 = tbl.Q10;
                    model.Q11 = tbl.Q11;
                    model.Q12 = tbl.Q12;
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
                        tbl= item.Beneficiary_Id_pk != Guid.Empty? _db.TBL_Beneficiary.Find(item.Beneficiary_Id_pk): new TBL_Beneficiary();
                        
                        tbl.HealthCenter= item.HealthCenter;
                        tbl.ReportingMonth = item.ReportingMonth;
                        tbl.ReportingYear = item.ReportingYear;
                        tbl.BFYVillageName = item.BFYVillageName;
                        tbl.Q2 = item.Q2;
                        tbl.Q3 = item.Q3;
                        tbl.Q4 = item.Q4;
                        tbl.Q5 = item.Q5;
                        tbl.Q6 = item.Q6;
                        tbl.Q7 = item.Q7;
                        tbl.Q8 = item.Q8;
                        tbl.Q9 = item.Q9;
                        tbl.Q10 = item.Q10;
                        tbl.Q11 = item.Q11;
                        tbl.Q12 = item.Q12;
                        tbl.Q13 = item.Q13;
                        tbl.Q14 = item.Q14;
                        tbl.Q15 = item.Q15;
                        tbl.Q16 =item.Q15==1? item.Q16:null;
                        tbl.Q17 = item.Q15 == 2 ? item.Q17 : null;
                        tbl.Q18 = item.Q15 == 4 ? item.Q18:null;
                        tbl.Q20 = item.Q20;
                        tbl.Q21 = item.Q21;
                        tbl.IsActive = true;
                        if (item.Beneficiary_Id_pk == Guid.Empty)
                        {
                            tbl.Beneficiary_Id_pk = Guid.NewGuid();
                            tbl.HindiEng = item.HindiEng;
                            tbl.DistrictId_fk=item.DistrictId_fk;
                            tbl.BlockId_fk=item.BlockId_fk;
                            tbl.PanchayatId_fk=item.PanchayatId_fk;
                            tbl.VillageOId_fk=item.VillageOId_fk;
                            tbl.CreatedBy = User.Identity.Name;
                            tbl.CreatedOn = DateTime.Now;
                            tbllist.Add(tbl);
                        }
                        else
                        {
                            tbl.UpdatedBy = User.Identity.Name;
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