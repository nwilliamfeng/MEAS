using System;
using System.ComponentModel.DataAnnotations;

namespace MEAS.Models
{
    public class UserInfoViewModel
    {      
        public byte[] Logo { get; set; }

        [Required]
        [Display(Name ="登录名")]
        public string LoginName { get; set; }

        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Phone]
        [Display(Name ="固定电话")]
        public string Phone { get; set; }

       [Required]
        [RegularExpression(@"^1[3458][0-9]{9}$", ErrorMessage = "手机号格式不正确")]
        [Display(Name = "移动电话")]
        public string Mobile { get; set; }

        [Display(Name ="邮箱地址")]
        [EmailAddress]
        public string EmailAddress { get; set; }
         
    }
}