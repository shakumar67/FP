using SubSonic.Schema;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP.Models;
using System.Security.Cryptography.X509Certificates;

namespace FP.Manager
{
    public class SP_Model
    {
        public static DataTable SPDistrict()
        {
            StoredProcedure sp = new StoredProcedure("SP_District");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPBlock(int DistrictId)
        {
            StoredProcedure sp = new StoredProcedure("SP_Block");
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPCLF(int DistrictId, int BlockId)
        {
            StoredProcedure sp = new StoredProcedure("SP_CLF");
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.Int32);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPPanchayat(int DistrictId, int BlockId, int CLFId)
        {
            StoredProcedure sp = new StoredProcedure("SP_Panchayat");
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.Int32);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.Int32);
            sp.Command.AddParameter("@CLFId", CLFId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPVillage(int DistrictId, int BlockId, int CLFId, int PanchayatId)
        {
            StoredProcedure sp = new StoredProcedure("SP_Village");
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.Int32);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.Int32);
            sp.Command.AddParameter("@CLFId", CLFId, DbType.Int32);
            sp.Command.AddParameter("@PanchayatId", PanchayatId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SpUserDetails(string Roleid, string CutUser)
        {
            FilterModel model = new FilterModel();
            StoredProcedure sp = new StoredProcedure("Usp_UserDetails");
            if (MvcApplication.CUser.Role == CommonModel.RoleNameCont.CNRP)
            {
                model.DistrictId = MvcApplication.CUser.DistrictId;
                model.BlockId = MvcApplication.CUser.BlockId;
                model.CLFId = MvcApplication.CUser.CLFId;
                model.PanchayatId = MvcApplication.CUser.Panchayatid;
            }
            //sp.Command.AddParameter("@DisId", MvcApplication.CUser.DistrictId, DbType.String);
            //sp.Command.AddParameter("@BlkId", MvcApplication.CUser.BlockId, DbType.String);
            //sp.Command.AddParameter("@PytId", MvcApplication.CUser.Panchayatid, DbType.String);
            //sp.Command.AddParameter("@VoId", MvcApplication.CUser.Void, DbType.String);
            //sp.Command.AddParameter("@Roleid", Roleid, DbType.String);
            //sp.Command.AddParameter("@CutUser", CommonModel.GetUserRoleLogin(), DbType.String);
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@CLFId", model.CLFId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", Roleid, DbType.String);
            sp.Command.AddParameter("@CutUser", MvcApplication.CUser.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPVillagelist(int DistrictId, int BlockId, int CLFId, int PanchayatId)
        {
            StoredProcedure sp = new StoredProcedure("SP_Villagelist");
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.Int32);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.Int32);
            sp.Command.AddParameter("@CLFId", CLFId, DbType.Int32);
            sp.Command.AddParameter("@PanchayatId", PanchayatId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPPanchayatList(int DistrictId, int BlockId, int CLFId)
        {
            StoredProcedure sp = new StoredProcedure("SP_PanchayatList");
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.Int32);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.Int32);
            sp.Command.AddParameter("@CLFId", CLFId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;

        }

        public static DataTable SPCLFMasterlist(int DistrictId, int BlockId)
        {
            StoredProcedure sp = new StoredProcedure("SP_CLFMasterlist");
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.Int32);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet SPContraceptive(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("USP_Contraceptive");
            sp.Command.AddParameter("@DistrictId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlockId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@PanchayatId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Role", "", DbType.String);
            sp.Command.AddParameter("@CutUser", "", DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }

        #region Beneficiary
        public static DataTable SPBFYList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_BFYList");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@CLFId", model.CLFId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", MvcApplication.CUser.Role, DbType.String);
            sp.Command.AddParameter("@CutUser", MvcApplication.CUser.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPCMFollowupView(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_CMFollowupView");
            sp.Command.AddParameter("@BFYID_fk", model.BFYId, DbType.String);
            sp.Command.AddParameter("@Type", model.Type, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPBFYDetailView(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SPBFYDetailView");
            sp.Command.AddParameter("@BFYPKID", model.BFYId, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPBFYFUpMonthList(CMFollowupModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_BFYFUpMonthList");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@CLFId", model.CLFId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", MvcApplication.CUser.Role, DbType.String);
            sp.Command.AddParameter("@CutUser", MvcApplication.CUser.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPFollowUpDataList(CMFollowupModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_BFYFollowUpDataList");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@CLFId", model.CLFId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", MvcApplication.CUser.Role, DbType.String);
            sp.Command.AddParameter("@CutUser", MvcApplication.CUser.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }

        #endregion

        #region Plan
        public static DataTable SPPlanBFYList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_PlanBFYList");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@CLFId", model.CLFId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", model.RoleId, DbType.String);
            sp.Command.AddParameter("@CutUser", model.CutUser, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_PlanList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_PlanList");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@CLFId", model.CLFId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@IsPlanAchved", model.IsPlanAchved, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", MvcApplication.CUser.Role, DbType.String);
            sp.Command.AddParameter("@CutUser", MvcApplication.CUser.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_AppPlanList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_AppPlanList");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@CLFId", model.CLFId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@IsPlanAchved", model.IsPlanAchved, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", MvcApplication.CUser.Role, DbType.String);
            sp.Command.AddParameter("@CutUser", MvcApplication.CUser.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_PlanAchBFYList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_PlanAchBFYList");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@CLFId", model.CLFId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", MvcApplication.CUser.Role, DbType.String);
            sp.Command.AddParameter("@CutUser", MvcApplication.CUser.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        #endregion
        #region Achievement Plan 
        public static DataTable SP_AchvPlanList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_AchvPlanList");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@CLFId", model.CLFId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", MvcApplication.CUser.Role, DbType.String);
            sp.Command.AddParameter("@CutUser", MvcApplication.CUser.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        /* MRP */
        public static DataTable SP_AchvPlanApprove(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_AchvPlanApprove");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@CLFId", model.CLFId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", MvcApplication.CUser.Role, DbType.String);
            sp.Command.AddParameter("@CutUser", MvcApplication.CUser.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetAchvPlanApproveChild(string UserId, string EmpId, int MonthId, int YearId)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetAchvPlanApproveChild");
            sp.Command.AddParameter("@UserId", UserId, DbType.String);
            sp.Command.AddParameter("@EmpId", EmpId, DbType.String);
            sp.Command.AddParameter("@MonthId", MonthId, DbType.Int32);
            sp.Command.AddParameter("@YearId", YearId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }

        /* CC */
        public static DataTable SP_AchvPlanApv2ndlevel(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_AchvPlanApv2ndlevel");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@CLFId", model.CLFId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", MvcApplication.CUser.Role, DbType.String);
            sp.Command.AddParameter("@CutUser", MvcApplication.CUser.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }

        #endregion


        #region Report Letter and Dashboard Home

        public static DataTable SP_BFYPrapatra_One(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_BFYPrapatra_One");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@CLFId", model.CLFId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", MvcApplication.CUser.Role, DbType.String);
            sp.Command.AddParameter("@CutUser", MvcApplication.CUser.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetModulerollout(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetModulerollout");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", MvcApplication.CUser.Role, DbType.String);
            sp.Command.AddParameter("@CutUser", MvcApplication.CUser.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetTotalChild(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetTotalChild");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", MvcApplication.CUser.Role, DbType.String);
            sp.Command.AddParameter("@CutUser", MvcApplication.CUser.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SpLetterTwo(FilterModel model)
        {
            //model.DistrictId=model.DistrictId == null ? "" : model.DistrictId;
            //model.BlockId=model.BlockId == null ? "" : model.BlockId;
            //model.PanchayatId=model.PanchayatId == null ? "" : model.PanchayatId;
            //model.VOId=model.VOId == null ? "" : model.VOId;
            //model.Year=model.Year == null ? "" : model.Year;
            //model.Month=model.Month == null ? "" : model.Month;
            StoredProcedure sp = new StoredProcedure("Usp_LetterTwo");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@CLFId", model.CLFId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", MvcApplication.CUser.Role, DbType.String);
            sp.Command.AddParameter("@CutUser", MvcApplication.CUser.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet SpDistrictGraph(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("Usp_DistrictGraph");
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", model.RoleId, DbType.String);
            sp.Command.AddParameter("@CutUser", model.CutUser, DbType.String);
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        #endregion

    }
}
