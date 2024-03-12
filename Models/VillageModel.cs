using FP.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FP.Models
{
    public class VillageModel
    {
        public VillageModel()
        {
            Void_pk = 0;
        }
        public int Void_pk { get; set; }
        [Required]
        [Display(Name = CommonModel.DispLevel.District)]
        public Nullable<int> DistrictId_fk { get; set; }
        [Required]
        [Display(Name = CommonModel.DispLevel.Block)]
        public Nullable<int> BlockId_fk { get; set; }
        [Required]
        [Display(Name = CommonModel.DispLevel.Cluster)]
        public Nullable<int> CLF_Id_fk { get; set; }
        [Required]
        [Display(Name = CommonModel.DispLevel.Panchayat)]
        public Nullable<int> Panchayatid_fk { get; set; }
        [Display(Name = CommonModel.DispLevel.Panchayat)]
        public string Panchayat { get; set; }
        [Required]
        [Display(Name = CommonModel.DispLevel.VOFull)]
        public string Village_Organization { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string F5 { get; set; }
        public string CRUD { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}