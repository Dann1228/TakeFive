using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TabkeFiveWebApplication.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "電子郵件")]
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
        [Display(Name = "代碼")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "記住此瀏覽器?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "電子郵件")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Display(Name = "驗證碼")]
        public string CaptchaCode { get; set; }

        [Display(Name = "記住我?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("Password", ErrorMessage = "密碼和確認密碼不相符。")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "{0}不得為空")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} 長度必需在{2}和{1}個之間")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "{0}不得為空")]
        //[StringLength(4, MinimumLength = 1, ErrorMessage = "{0} 長度必需在{2}和{1}個之間")]
        //[Display(Name = "性別")]
        //public string Gender { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "{0}不得為空")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "{0} 長度必需在{2}和{1}個之間")]
        [Display(Name = "電話")]
        public string phNumber { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0}不得為空")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "{0} 最多 {1} 個字元")]
        [Display(Name = "生日")]
        public string Birthday { get; set; }

        //[Required(ErrorMessage = "{0}不得為空")]
        //[StringLength(3, ErrorMessage = "{0} 最多 {1} 個字元")]
        //[Display(Name = "國別")]
        //public string CountryCode { get; set; }

        //[Required(ErrorMessage = "{0}不得為空")]
        [StringLength(100, ErrorMessage = "{0} 最多 {1} 個字元")]
        [Display(Name = "個人簡介")]
        public string Introductions { get; set; }

        [Display(Name = "圖片")]
        public byte[] Pic { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("Password", ErrorMessage = "密碼和確認密碼不相符。")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
    }

 
}
