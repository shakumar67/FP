using FP.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FP.Models
{
    public class CLFModel
    {
        public int CLF_ID_pk { get; set; }
        [Required]
        [Display(Name = CommonModel.DispLevel.District)]
        public Nullable<int> DistrictId_fk { get; set; }
        [Required]
        [Display(Name = CommonModel.DispLevel.Block)]
        public Nullable<int> BlockId_fk { get; set; }
        [Required]
        [Display(Name = CommonModel.DispLevel.Cluster)]
        public string CLFName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}