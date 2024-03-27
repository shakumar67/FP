using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
//using ExpressiveAnnotations.Attributes;

namespace FP.Models
{
    public class ServiceBFYModel
    {
        public ServiceBFYModel()
        {
            ServiceBFYId_pk = Guid.Empty;
        }
        [Key]
        public System.Guid ServiceBFYId_pk { get; set; }
        [Required]
        [Display(Name = DisplayAchBFY.ServiceYearId)]
        public Nullable<int> ServiceYearId { get; set; }
        [Required]
        [Display(Name = DisplayAchBFY.ServiceMonthId)]
        public Nullable<int> ServiceMonthId { get; set; }
        //[Required]
        public Nullable<System.Guid> FollowId_fk { get; set; }
       // public Nullable<System.Guid> PlanId_fk { get; set; }
        [Required]
        public Nullable<System.Guid> BFYId_fk { get; set; }

        [Required]
        [Display(Name = DisplayAchBFY.IsPPresent)]
        public Nullable<bool> IsPeerPresent { get; set; }

        [Required]
        [Display(Name = DisplayAchBFY.IsFUpHV)]
        public Nullable<bool> IsFollowUpHV { get; set; }
        //[Required]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsPeerPresent==true || IsFollowUpHV == true)")]
        [Display(Name = DisplayAchBFY.IsPPrIsCt)]
        public Nullable<bool> IsContraception { get; set; }
        [RequiredIf("IsContraception", true)]
        [Display(Name = DisplayAchBFY.Ct)]
        public Nullable<int> ContraceptionId_fk { get; set; }
        [Display(Name = DisplayAchBFY.CtusemethodOther)]
        //[RequiredIf("ContraceptionId_fk", 4)]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && ContraceptionId_fk == 4)")]
        public string ContraceptionOther { get; set; }
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && (ContraceptionId_fk == 1 || ContraceptionId_fk == 2))")]
        [Display(Name = DisplayAchBFY.Ctusemethod)]
        public Nullable<int> UseMethodId_fk { get; set; }
        [Display(Name = DisplayAchBFY.Isservice)]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && (ContraceptionId_fk == 1 || ContraceptionId_fk == 2))")]
        public Nullable<bool> Isservice { get; set; }
        [Display(Name = DisplayAchBFY.ServiceRevcDt)]
        //[RequiredIf("Isservice", true)]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && Isservice==true && (ContraceptionId_fk == 1 || ContraceptionId_fk == 2))")]
        public Nullable<System.DateTime> ServiceRevcDt { get; set; }
        [Display(Name = DisplayAchBFY.ServiceProvider)]
        //[RequiredIf("Isservice", true)]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && Isservice==true && (ContraceptionId_fk == 1 || ContraceptionId_fk == 2))")]
        public Nullable<int> ServiceProvider { get; set; }
        //[RequiredIf("Isservice", true)]
        [Display(Name = DisplayAchBFY.Location)]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && Isservice==true && (ContraceptionId_fk == 1 || ContraceptionId_fk == 2))")]
        public string Location { get; set; }
        //[Display(Name = DisplayAchBFY.CMEligible)]
        public Nullable<decimal> CMEligible { get; set; }
        public Nullable<decimal> CNRPEligible { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

    }
    public static class DisplayAchBFY
    {
        public const string ServiceYearId = "Year";
        public const string ServiceMonthId = "Month";
        public const string IsPPresent= "Present in Peer Group Meeting";
        public const string IsFUpHV = "FollowUp/HV in Current Month";
        public const string IsPPrIsCt = "Want to use contraception after meeting/HV";
        public const string Ct= "Method of contraception";
        public const string Ctusemethod= "Use method";
        public const string CtusemethodOther= "Other use method ";
        public const string Isservice = "Linked to ASHA for service";
        public const string ServiceRevcDt = "Service Received Date";
        public const string ServiceProvider = "Service Provider";
        public const string Location = "Facility Name (Location)";
        public const string CMEligible = "CM Eligible for Incentive";//If Copper T/Antara inj/Permanent 20
        public const string CNRPEligible = "CNRP Eligible for Incentive";// 80
    }
}