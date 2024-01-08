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
        public ActionResult Beneficiary(int Id = 0,int HindiEng=1)
        {
            BeneficiaryModel model = new BeneficiaryModel();
            model.HindiEng = HindiEng;
            if (Id != 0)
            {
                var tbl = db.TBL_Beneficiary.Find();
                if (tbl != null)
                {
                    model.Beneficiary_Id_pk = tbl.Beneficiary_Id_pk;
                    model.HindiEng = tbl.HindiEng;
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
                    model.Q12_1 = tbl.Q12_1;
                    model.Q12_2 = tbl.Q12_2;
                    model.Q13 = tbl.Q13;
                    model.Q14 = tbl.Q14;
                    model.Q15 = tbl.Q15;
                    model.Q16 = tbl.Q16;
                    model.Q17 = tbl.Q17;
                    model.Q18 = tbl.Q18;
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
            try
            {
                JsonResponseData response = new JsonResponseData();
                List<TBL_Beneficiary> tbllist = new List<TBL_Beneficiary>();
                TBL_Beneficiary tbl;
                if (model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        tbl = new TBL_Beneficiary();
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
                        tbl.Q12_1 = item.Q12_1;
                        tbl.Q12_2 = item.Q12_2;
                        tbl.Q13 = item.Q13;
                        tbl.Q14 = item.Q14;
                        tbl.Q15 = item.Q15;
                        tbl.Q16 = item.Q16;
                        tbl.Q17 = item.Q17;
                        tbl.Q18 = item.Q18;
                        tbl.IsActive = true;
                        if (item.Beneficiary_Id_pk == Guid.Empty)
                        {
                            tbl.Beneficiary_Id_pk = Guid.NewGuid();
                            tbl.CreatedBy = User.Identity.Name;
                            tbl.CreatedOn = DateTime.Now;
                            tbllist.Add(tbl);
                        }
                        else
                        {
                            tbl.UpdatedBy = User.Identity.Name;
                            tbl.UpdatedOn = DateTime.Now;
                        }
                    }
                }
                else
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = CommonModel.GetEnumDisplayName(Enums.eReturnReg.AllFieldsRequired), Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }
                int res = db.SaveChanges();
                if (res > 0)
                {
                    response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = CommonModel.GetEnumDisplayName(Enums.eReturnReg.Insert), Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = CommonModel.GetEnumDisplayName(Enums.eReturnReg.Error), Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse3.MaxJsonLength = int.MaxValue;
                return resResponse3;
            }

            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = CommonModel.GetEnumDisplayName(Enums.eReturnReg.AllFieldsRequired), Data = null };
            var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
            resResponse4.MaxJsonLength = int.MaxValue;
            return resResponse4;
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