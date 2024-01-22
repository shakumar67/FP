using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FP.Models
{
    public class FilterModel
    {
        public string DistrictId { get; set; }
        public string BlockId { get; set; }
        public string PanchayatId { get; set; }
        public string VOId { get; set; }
        public string FromDt { get; set; }
        public string ToDt { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public string RoleId { get; set; }
        public string Roles { get; set; }
        public string CutUser { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}