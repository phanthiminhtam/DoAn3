using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteCosmetic.Models
{
    public class LoginCustomer
    {

        [Key]


        [Required(ErrorMessage ="Bạn phải nhập tên tài khoản!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập mật khẩu!")]
        public string Password { get; set; }
    }
}