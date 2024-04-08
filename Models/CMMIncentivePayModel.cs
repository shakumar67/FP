using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FP.Models
{
    public class CMMIPModel
    {
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> BlockId { get; set; }
        public Nullable<int> ClusterId { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public virtual CMMIncentivePayModel CMMIncentivePayModel { get; set; }
        public string ApprovedRemarks { get; set; }
        public int TypeLayer { get; set; }
    }
    public class CMMIncentivePayModel
    {
        public CMMIncentivePayModel() { CMMIPId_pk = Guid.Empty; }
        public System.Guid CMMIPId_pk { get; set; }
        public Nullable<int> DistrictId_fk { get; set; }
        public Nullable<int> BlockId_fk { get; set; }
        public Nullable<int> ClusterId_fk { get; set; }
        public Nullable<int> PanchayatId_fk { get; set; }
        public Nullable<int> VoId_fk { get; set; }
        public Nullable<System.Guid> BFYId_fk { get; set; }
        public Nullable<System.Guid> FollowupId_fk { get; set; }
        public Nullable<int> MIMonth { get; set; }
        public Nullable<int> MIYear { get; set; }
        public Nullable<bool> Approved1Status { get; set; }
        public Nullable<System.DateTime> Approved1Date { get; set; }
        public string Approved1Remarks { get; set; }
        public string Approved1By { get; set; }
        public Nullable<bool> Approved2Status { get; set; }
        public Nullable<System.DateTime> Approved2Date { get; set; }
        public string Approved2Remarks { get; set; }
        public string Approved2By { get; set; }
        public Nullable<bool> Approved3Status { get; set; }
        public Nullable<System.DateTime> Approved3Date { get; set; }
        public string Approved3Remarks { get; set; }
        public string Approved3By { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedUpdatedOn { get; set; }
        public int PlanApprove { get; set; }
        public Guid ReportedByUserId { get; set; }
        
    }
}