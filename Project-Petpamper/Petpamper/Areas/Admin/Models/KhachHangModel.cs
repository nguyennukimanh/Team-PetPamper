using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetPamper.Areas.Admin.Models
{
    public class KhachHangModel
    {
        
        public KhachHangModel()
        {

            Name = string.Empty;
            Phone = string.Empty;
            IdentifyNumber = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
        }
        public string MaKH { get; set; }

        [Required(ErrorMessage="Tên là bắt buộc"), MaxLength(30)]
        public string Name { get; set; }

        [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "Số điện thoại không hợp lệ")]
        [Phone(ErrorMessage= "Đây không phải là số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        public string Phone { get; set; }

        [StringLength(maximumLength: 9, MinimumLength = 9, ErrorMessage = "Số CMND không hợp lệ")]
        [Required(ErrorMessage="Số CMND là bắt buộc")]
        public string IdentifyNumber { get; set; }

        [EmailAddress(ErrorMessage = "Đây không phải là email")]
        [Required(ErrorMessage = "Email là bắt buộc")]
        public string Email { get; set; }

        public string Address { get; set; }

        public int TrangThai { get; set; }
    }
}