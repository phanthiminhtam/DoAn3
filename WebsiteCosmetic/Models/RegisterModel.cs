using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteCosmetic.Models
{
    public class RegisterModel
    {
        [Key]
        public long CusId { get; set; }

        [Required(ErrorMessage ="Bạn chưa nhập họ tên!")]
        public string CusName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Email!")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nhập tên đăng nhập của bạn!")]
        public string UserName { get; set; }

        [StringLength(20,MinimumLength = 6, ErrorMessage ="Mật khẩu ít nhất 6 ký tự!")]
        [Required(ErrorMessage = "Nhập mật khẩu của bạn!")]
        public string PassWord { get; set; }

        [Compare("PassWord", ErrorMessage ="Xác nhận mật khẩu không đúng!")]
        public string ConfirmPassword { get; set; }
    }
}