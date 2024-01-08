using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FP.Models
{
    public class BeneficiaryModel
    {
        public BeneficiaryModel()
        {
            Beneficiary_Id_pk = Guid.Empty;
        }
        public System.Guid Beneficiary_Id_pk { get; set; }
        public Nullable<int> HindiEng { get; set; }
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
        public Nullable<int> Q4 { get; set; }
        public string Q5 { get; set; }
        public Nullable<System.DateTime> Q6 { get; set; }
        public string Q7 { get; set; }
        public Nullable<int> Q8 { get; set; }
        public string Q9 { get; set; }
        public string Q10 { get; set; }
        public string Q11 { get; set; }
        public string Q12_1 { get; set; }
        public string Q12_2 { get; set; }
        public string Q13 { get; set; }
        public string Q14 { get; set; }
        public Nullable<int> Q15 { get; set; }
        public Nullable<int> Q16 { get; set; }
        public Nullable<int> Q17 { get; set; }
        public string Q18 { get; set; }
        public Nullable<int> Q19 { get; set; }
        public string Q20 { get; set; }
        public Nullable<int> Q21 { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}