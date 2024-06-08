using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FP.Models
{
    public class AchvPlanModel
    {
        public Nullable<int> DistrictId_fk { get; set; }
        public Nullable<int> BlockId_fk { get; set; }
        public Nullable<int> ClusterId_fk { get; set; }
        public Nullable<int> PanchayatId_fk { get; set; }
        public Nullable<int> PlanMonth { get; set; }
        public Nullable<int> PlanYear { get; set; }
        public List<AVPlanModel> AVPlanModel { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> PlanStatus { get; set; }
        public Nullable<System.DateTime> PlanStatusDate { get; set; }

        public Nullable<bool> CLFValidation { get; set; }
        public Nullable<bool> CLFLeadersPresident { get; set; }
        public Nullable<bool> CLFLeadersSecretary { get; set; }
        public Nullable<bool> CLFLeadersTreasurer { get; set; }

    }
    public class AVPlanModel
    {
        public AVPlanModel()
        {
            AchieveId_pk = Guid.Empty;
        }
        public System.Guid AchieveId_pk { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> BlockId { get; set; }
        public Nullable<int> ClusterId { get; set; }
        public Nullable<int> PanchayatId { get; set; }
        public Nullable<int> VoId_fk { get; set; }
        public string VoIds_fk { get; set; }
        public string BfyIds { get; set; }
        public Nullable<int> ActivityId_fk { get; set; }
        public Nullable<Guid> UserId { get; set; }
        public Nullable<Guid> empId { get; set; }
        public DateTime? Meetingheld { get; set; }
        public Nullable<int> Noofparticipant { get; set; }
        public Nullable<int> PlanApprove { get; set; }
        public Nullable<int> Level1Reject { get; set; }
        public Nullable<System.DateTime> Level1RejectDt { get; set; }
        public string Remark1 { get; set; }
        public Nullable<int> Level2Reject { get; set; }
        public Nullable<System.DateTime> Level2RejectDt { get; set; }
        public string Remark2 { get; set; }

        public Nullable<bool> CLFValidation { get; set; }
        public Nullable<bool> CLFLeadersPresident { get; set; }
        public Nullable<bool> CLFLeadersSecretary { get; set; }
        public Nullable<bool> CLFLeadersTreasurer { get; set; }

    }

}