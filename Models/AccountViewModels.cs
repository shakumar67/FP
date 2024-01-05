using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FP.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
       // public string Email { get; set; }

       // [Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        ////[DataType(DataType.Password)]
        /// <summary>
        /// [Display(Name = "Confirm password")]
        /// </summary>
        ////[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }

        public System.Guid EmpID_pk { get; set; }
        public string UserID_fk { get; set; }
        public string RoleID_fk { get; set; }
        [Display(Name = "Role")]
        public string Roles { get; set; }
        [Display(Name = "District")]
        public Nullable<int> DistrictId { get; set; }
        [Display(Name = "Block")]
        public Nullable<int> BlockId { get; set; }
        [Display(Name = "Panchayat")]
        public Nullable<int> PanchayatId { get; set; }
        [Display(Name = "Other Panchayat")]
        public string Panchayat_Other { get; set; }
        [Display(Name = "Village")]
        public Nullable<int> VillageId { get; set; }
        [Display(Name = "Other Village")]
        public string Other_Vo { get; set; }
        //[Display(Name = "Confirm password")]
        [Display(Name = "Name Of Village Organization")]
        public string NameOfTheVillageOrganization { get; set; }
        public string EmpName { get; set; }
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string DisplayName1
        {
            get
            {
                string CN = string.Empty;
                if (Roles == "B5421964-4F00-426A-8254-4297B6DB9204")
                {
                    CN = "Name of CNRP";
                }
                else if (Roles == "727C58E2-8A14-4804-AB53-4762EC323A76")
                {
                    CN = "Name of CM";
                }
                else if (Roles == "437554FA-C90B-49AF-9015-BBCB33F5CCE7")
                {
                    CN = "Name of BPIU";
                }
                else if (Roles == "168820B3-0F7D-43E2-997F-A90589C939A4")
                {
                    CN = "Name of BPMU";
                }
                else
                {
                    CN = "Name";
                }
                return CN;
            }
            //set
            //{
            //    string CN = string.Empty;
            //    if (Roles == "B5421964-4F00-426A-8254-4297B6DB9204")
            //    {
            //        CN = "Name of CNRP";
            //    }
            //    else if (Roles == "727C58E2-8A14-4804-AB53-4762EC323A76")
            //    {
            //        CN = "Name of CM";
            //    }
            //    else if (Roles == "437554FA-C90B-49AF-9015-BBCB33F5CCE7")
            //    {
            //        CN = "Name of BPIU";
            //    }
            //    else if (Roles == "168820B3-0F7D-43E2-997F-A90589C939A4")
            //    {
            //        CN = "Name of BPMU";
            //    }
            //    else
            //    {
            //        CN = "Name";
            //    }
            //}
        }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
