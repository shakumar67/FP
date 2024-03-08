using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FP.Models
{
    public class PlanModel
    {
        public PlanModel()
        {
            PlanID_pk = Guid.Empty;
            AchBFYModel =new AchBFYModel();
        }
        //[Key]
        public System.Guid PlanID_pk { get; set; }
        [Required]
        [Display(Name ="District")]
        public Nullable<int> DistrictId_fk { get; set; }
        [Required]
        [Display(Name = "Block")]
        public Nullable<int> BlockId_fk { get; set; }
        [Required]
        [Display(Name = "CLF")]
        public Nullable<int> CLFId_fk { get; set; }
        [Required]
        [Display(Name = "Panchayat")]
        public Nullable<int> PanchayatId_fk { get; set; }
        [Required]
        [Display(Name = "Village Organization")]
        public Nullable<int> VoId_fk { get; set; }
        [Required]
        [Display(Name = "Planning Month")] 
        public Nullable<int> PlanMonth { get; set; }
        [Required]
        [Display(Name = "Planning Year")]
        public Nullable<int> PlanYear { get; set; }
        [Required]
        [Display(Name = "Planning date for  Peer Group Meeting")]
       // [ExpressiveAnnotations.Attributes.AssertThat("DOMDt")]
        public Nullable<System.DateTime> PlanDt { get; set; }
        [Required]
        [Display(Name = "Planning date for Home Visit")]
        public Nullable<System.DateTime> HVDt { get; set; }
        [Display(Name = "Planning Achievement")]
        public bool IsPlanAchv { get; set; }
        //[Required]
        [Display(Name = "Select Peer Group Meeting & Home Visit")]
        public Nullable<int> IsBFY { get; set; }
        public bool IsCheckBFY { get; set; }
        //[Required]
       // [ExpressiveAnnotations.Attributes.AssertThat("PlanDt")]
        [Display(Name = "Date of meeting held for Peer Group meeting")]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsBFY==1 || IsBFY == 3)")]
        public Nullable<System.DateTime> DOMDt { get; set; }
        [Display(Name = "Date of Home Visit")]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsBFY==2 || IsBFY == 3)")]
        public Nullable<System.DateTime> DOMHVDt { get; set; }
        [Required]
        [Display(Name = "Subject")]
        public Nullable<int> SubjectId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> IsCount { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public  AchBFYModel AchBFYModel { get; set; }
    }
}