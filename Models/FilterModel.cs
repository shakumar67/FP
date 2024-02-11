using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FP.Models
{
    public class FilterModel
    {
        [Display(Name ="District")]
        public string DistrictId { get; set; }
        [Display(Name = "Block")]
        public string BlockId { get; set; }
        [Display(Name = "Panchayat")]
        public string PanchayatId { get; set; }
        [Display(Name = "Village Organization")]
        public string VOId { get; set; }
        [Display(Name = "From Date")]
        public string FromDt { get; set; }
        [Display(Name = "To Date")]
        public string ToDt { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        public string DOB { get; set; }
        [Display(Name = "Role")]
        public string RoleId { get; set; }
        [Display(Name = "Role")]
        public string Roles { get; set; }
        public string CutUser { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}