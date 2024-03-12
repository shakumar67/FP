using FP.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FP.Models
{
    public class PanchayatModel
    {
        public PanchayatModel()
        {
            Panchayatid_pk = 0;
        }
        public int Panchayatid_pk { get; set; }
        [Required]
        [Display(Name = CommonModel.DispLevel.District)]
        public Nullable<int> DistrictId_fk { get; set; }
        [Required]
        [Display(Name = CommonModel.DispLevel.Block)]
        public Nullable<int> Blockid_fk { get; set; }
        //public string Block { get; set; }
        [Required]
        [Display(Name = CommonModel.DispLevel.Cluster)]
        public Nullable<int> CLF_Id_fk { get; set; }
        [Required]
        [Display(Name = CommonModel.DispLevel.Panchayat)]
        public string Panchayat { get; set; }
        public string FPCP_Panchayat { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}