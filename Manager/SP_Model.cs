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
            sp.Command.AddParameter("@DistrictId",DistrictId,DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPPanchayat(int DistrictId,int BlockId)
        {
            StoredProcedure sp = new StoredProcedure("SP_Panchayat");
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.Int32);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPVillage(int DistrictId,int BlockId,int PanchayatId)
        {
            StoredProcedure sp = new StoredProcedure("SP_Village");
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.Int32);
            sp.Command.AddParameter("@BlockId", BlockId, DbType.Int32);
            sp.Command.AddParameter("@PanchayatId", PanchayatId, DbType.Int32);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SpUserDetails(string Roleid,string CutUser)
        {
            StoredProcedure sp = new StoredProcedure("Usp_UserDetails");
            sp.Command.AddParameter("@Roleid", Roleid, DbType.String);
            sp.Command.AddParameter("@CutUser", CutUser, DbType.String);
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

        #region Beneficiary
        public static DataTable SPBFYList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_BFYList");
           // sp.Command.AddParameter("@Roleid", Roleid, DbType.String);
          //  sp.Command.AddParameter("@CutUser", CutUser, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        #endregion

    }
}
