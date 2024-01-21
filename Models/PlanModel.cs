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
        }
        [Key]
        public System.Guid PlanID_pk { get; set; }
        public Nullable<int> DistrictId_fk { get; set; }
        public Nullable<int> BlockId_fk { get; set; }
        public Nullable<int> PanchayatId_fk { get; set; }
        public Nullable<int> VoId_fk { get; set; }
        public Nullable<System.DateTime> PlanDt { get; set; }
        public Nullable<System.DateTime> HVDt { get; set; }
        public Nullable<int> IsBFY { get; set; }
        public Nullable<System.DateTime> DOMDt { get; set; }
        public Nullable<System.DateTime> DOMHVDt { get; set; }
        public Nullable<int> SubjectId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> IsCount { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}