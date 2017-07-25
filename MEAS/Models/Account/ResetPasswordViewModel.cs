using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace MEAS.Models
{
    public class ResetPasswordViewModel
    {

        [Display(Name = "登录名")]
        [Editable(false)]
        public string LoginName { get; set; }

        [Display(Name = "用户名")]
        [Editable(false)]
        public string UserName { get;  set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(12)]
        [MinLength(4)]
        [Display(Name = "旧密码")]    
        public string OldPassword { get; set; }

        [Required]
        [MaxLength(12)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [Required]
        [MaxLength(12)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "确认密码与新密码不一致！")]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }
    }
}