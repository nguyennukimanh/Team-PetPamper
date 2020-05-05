using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetPamper.Models
{
    public class ForgotModel
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc"), MaxLength(30)]
        [Display(Name = "Tên đăng nhập")]
        public string Tendangnhap { get; set; }

        [EmailAddress(ErrorMessage = "Đây không phải là email")]
        [Required(ErrorMessage = "Email là bắt buộc")]
        public string Email { get; set; }
    }
}