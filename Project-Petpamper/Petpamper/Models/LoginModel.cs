using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetPamper.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc"), MaxLength(30)]
        [Display(Name = "Tên đăng nhập")]
        public string Tendangnhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc"), MaxLength(30)]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Matkhau { get; set; }
    }
}