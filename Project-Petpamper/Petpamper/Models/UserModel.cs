using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetPamper.Models
{
    public class UserModel
    {
        public UserModel()
        {
            TenKH = string.Empty;
            SDT = string.Empty;
            SoCMND = string.Empty;
            Email = string.Empty;
            Diachi = string.Empty;
            Tendangnhap = string.Empty;
            Matkhau = string.Empty;
        }

        public string MaKH { get; set; }

        [Required(ErrorMessage = "Họ và tên là bắt buộc"), MaxLength(30)]
        public string TenKH { get; set; }

        [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "Số điện thoại không hợp lệ")]
        [Phone(ErrorMessage = "Đây không phải là số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        public string SDT { get; set; }

        [StringLength(maximumLength: 9, MinimumLength = 9, ErrorMessage = "Số CMND không hợp lệ")]
        [Required(ErrorMessage = "Số CMND là bắt buộc")]
        public string SoCMND { get; set; }

        [EmailAddress(ErrorMessage = "Đây không phải là email")]
        [Required(ErrorMessage = "Email là bắt buộc")]
        public string Email { get; set; }

        public string Diachi { get; set; }

        public string MaND { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc"), MaxLength(30)]
        public string Tendangnhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc"), MaxLength(30)]
        [DataType(DataType.Password)]
        public string Matkhau{ get; set; }

        public string DecryptedPassword
        {
            get
            {
                return
                    Helper.Decrypt(Matkhau);
            }
            set
            {
                if (value != null)
                    Matkhau = Helper.Encrypt(value);
            }
        }

        public string Trangthai { get; set; }

        public string Quanly { get; set; }

        public string DaXoa { get; set; }
        
    }
}