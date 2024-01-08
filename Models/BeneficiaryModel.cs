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
        public Nullable<int> DistrictId_fk { get; set; }
        public Nullable<int> BlockId_fk { get; set; }
        public Nullable<int> PanchayatId_fk { get; set; }
        public Nullable<int> VillageId_fk { get; set; }
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
        public string Q1D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Beneficiary_Id";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी_आईडी";
                }
                return CN;
            }
        }
        public string Q2D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Name of SHG";
                }
                else if (HindiEng == 2)
                {
                    CN = "समूह का नाम";
                }
                return CN;
            }
        }
        public string Q3D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Name of Beneficiary";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी का नाम";
                }
                return CN;
            }
        }
        public string Q4D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Age of Beneficiary (In year)";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी का उम्र (वर्ष में)";
                }
                return CN;
            }
        }
        public string Q5D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Husband's name of Beneficiary";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी के पति का नाम";
                }
                return CN;
            }
        }
        public string Q6D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Date of marriage";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी के शादी की तिथि /वर्ष";
                }
                return CN;
            }
        }
        public string Q7D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Mobile no. of beneficiary";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी का मोबाईल नंबर";
                }
                return CN;
            }
        }
        public string Q8D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "SHG Affiliation";
                }
                else if (HindiEng == 2)
                {
                    CN = "समूह से जुड़ाव";
                }
                return CN;
            }
        }
        public string Q9D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Beneficiary's family member name who is associated with the SHG group";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी के परिवार का जो सदस्य समूह से जुड़ी हैं उस महिला का नाम लिखें";
                }
                return CN;
            }
        }
        public string Q10D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "No. of male child at present";
                }
                else if (HindiEng == 2)
                {
                    CN = "वर्तमान में लड़कों की संख्या";
                }
                return CN;
            }
        }
        public string Q11D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "No. of female child at present";
                }
                else if (HindiEng == 2)
                {
                    CN = "वर्तमान में लड़कियों की संख्या";
                }
                return CN;
            }
        }
        public string Q12D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Age of youngest child (Year/months)";
                }
                else if (HindiEng == 2)
                {
                    CN = "सबसे छोटे बच्चे की उम्र";
                }
                return CN;
            }
        }
        public string Q13D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Code no. of related AWC";
                }
                else if (HindiEng == 2)
                {
                    CN = "कोड संख्या। संबंधित आंगनवाड़ी केंद्र की";
                }
                return CN;
            }
        }
        public string Q14D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Name of related ASHA";
                }
                else if (HindiEng == 2)
                {
                    CN = "संबंधित आशा का नाम";
                }
                return CN;
            }
        }
        public string Q15D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Which contraceptive method are you currently using";
                }
                else if (HindiEng == 2)
                {
                    CN = "वर्त्तमान में कौन से गर्भनिरोधक साधन का उपयोग कर रहे हैं";
                }
                return CN;
            }
        }
        public string Q16D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Temporary Method";
                }
                else if (HindiEng == 2)
                {
                    CN = "अस्थायी विधि";
                }
                return CN;
            }
        }
        public string Q17D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Permanent Method";
                }
                else if (HindiEng == 2)
                {
                    CN = "अस्थायी विधि";
                }
                return CN;
            }
        }
        public string Q18D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Other Method";
                }
                else if (HindiEng == 2)
                {
                    CN = "अस्थायी विधि";
                }
                return CN;
            }
        }
        public string Q19D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Beneficiary followed-up in the reporting month";
                }
                else if (HindiEng == 2)
                {
                    CN = "रिपोर्टिंग माह लाभार्थी के यहाँ फॉलो अप किया गया";
                }
                return CN;
            }
        }
        public string Q20D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "SHGs where module was rolled out";
                }
                else if (HindiEng == 2)
                {
                    CN = "एसएचजी जहां मॉड्यूल शुरू किया गया था";
                }
                return CN;
            }
        }
        public string Q21D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Medium of module rollout";
                }
                else if (HindiEng == 2)
                {
                    CN = "मॉड्यूल रोलआउट का माध्यम";
                }
                return CN;
            }
        }





    }
}