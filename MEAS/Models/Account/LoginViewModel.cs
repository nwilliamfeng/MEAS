using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MEAS.Models
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(12)]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Display(Name = "登录密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }
}