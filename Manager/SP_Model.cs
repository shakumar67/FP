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
        public static DataTable SPPanchayat(int DistrictId, int BlockId)
        {
            StoredProcedure sp = new StoredProcedure("SP_Panchayat");
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.Int32);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPVillage(int DistrictId, int BlockId, int PanchayatId)
        {
            StoredProcedure sp = new StoredProcedure("SP_Village");
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.Int32);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.Int32);
            sp.Command.AddParameter("@PanchayatId", PanchayatId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SpUserDetails(string Roleid, string CutUser)
        {
            StoredProcedure sp = new StoredProcedure("Usp_UserDetails");
            sp.Command.AddParameter("@DisId", MvcApplication.CUser.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", MvcApplication.CUser.BlockId, DbType.String);
            sp.Command.AddParameter("@PytId", MvcApplication.CUser.Panchayatid, DbType.String);
            sp.Command.AddParameter("@VoId", MvcApplication.CUser.Void, DbType.String);
            sp.Command.AddParameter("@Roleid", Roleid, DbType.String);
            sp.Command.AddParameter("@CutUser", CommonModel.GetUserRoleLogin(), DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPVillagelist(int DistrictId, int BlockId, int PanchayatId)
        {
            StoredProcedure sp = new StoredProcedure("SP_Villagelist");
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.Int32);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.Int32);
            sp.Command.AddParameter("@PanchayatId", PanchayatId, DbType.Int32);
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
            // sp.Command.AddParameter("@Roleid", Roleid, DbType.String);
            //  sp.Command.AddParameter("@CutUser", CutUser, DbType.String);
            sp.Command.AddParameter("@DisId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@BlkId", model.BlockId, DbType.String);
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", model.RoleId, DbType.String);
            sp.Command.AddParameter("@CutUser", model.CutUser, DbType.String);
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
            sp.Command.AddParameter("@PytId", model.PanchayatId, DbType.String);
            sp.Command.AddParameter("@VoId", model.VOId, DbType.String);
            sp.Command.AddParameter("@Month", model.Month, DbType.String);
            sp.Command.AddParameter("@Year", model.Year, DbType.String);
            sp.Command.AddParameter("@Role", model.RoleId, DbType.String);
            sp.Command.AddParameter("@CutUser", model.CutUser, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        #endregion

    }
}
