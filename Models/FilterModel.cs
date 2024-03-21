using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FP.Manager;

namespace FP.Models
{
    public class FilterModel
    {
        [Display(Name = "Beneficiary")]
        public string BFYId { get; set; }
        [Display(Name = CommonModel.DispLevel.District)]
        public string DistrictId { get; set; }
        [Display(Name = CommonModel.DispLevel.Block)]
        public string BlockId { get; set; }
        [Display(Name = CommonModel.DispLevel.Cluster)]
        public string CLFId { get; set; }
        [Display(Name = CommonModel.DispLevel.Panchayat)]
        public string PanchayatId { get; set; }
        [Display(Name = CommonModel.DispLevel.VOFull)]
        public string VOId { get; set; }
        [Display(Name = CommonModel.DispLevel.FromDate)]
        public string FromDt { get; set; }
        [Display(Name = CommonModel.DispLevel.ToDate)]
        public string ToDt { get; set; }
        [Display(Name = CommonModel.DispLevel.Name)]
        public string Name { get; set; }
        public string DOB { get; set; }
        [Display(Name = CommonModel.DispLevel.Role)]
        public string RoleId { get; set; }
        [Display(Name = CommonModel.DispLevel.Role)]
        public string Roles { get; set; }
        public string CutUser { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        [Display(Name = "Achieved")]
        public string IsPlanAchved { get; set; }
        public int Type { get; set; }
    }
   
}