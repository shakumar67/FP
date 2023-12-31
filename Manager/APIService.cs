using FP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace FP.Manager
{
    public class APIServices
    {
        public static FP_DBEntities db = new FP_DBEntities();

        #region AUTORIZATION
        //CARE INDIA //https://careindia.surveycto.com/index.html
        public static string GetUserName()
        {
            return "sunkumar@careindia.org";
        }
        public static string GETPassword()
        {
            return "Sun@Kumar";
        }
        //https://carebtsp.surveycto.com/
        public static string GetBTSPUserName()
        {
            //return "";
            return "";
        }
        public static string GETBTSPPassword()
        {
            // return "";
            return "";
        }
        private static long ConvertToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000000;
            return epoch;
        }
        public static string GetMAXDate(string para)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand comd = new SqlCommand("sp_GetMAXDate", connection);
            comd.CommandType = CommandType.StoredProcedure;
            comd.Parameters.AddWithValue("@para", para);
            string date = comd.ExecuteScalar().ToString();
            return date;
        }
        

        #endregion

    }
}