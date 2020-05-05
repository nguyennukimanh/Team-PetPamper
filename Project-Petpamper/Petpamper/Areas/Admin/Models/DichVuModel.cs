using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetPamper.Areas.Admin.Models
{
    public class DichVuModel
    {
        public DichVuModel()
        {
            MaDV = string.Empty;
            TenDV = string.Empty;
            Thoigian = string.Empty;
            Chiphi = string.Empty;
        }
        [StringLength(maximumLength: 5, MinimumLength = 5, ErrorMessage = "Mã DV không hợp lệ")]
        [Required(ErrorMessage = "Mã DV là bắt buộc")]
        public string MaDV { get; set; }

        [Required(ErrorMessage = "Tên Dịch là bắt buộc"), MaxLength(30)]
        public string TenDV { get;set; }

        [Required(ErrorMessage = "Thời gian là bắt buộc"), MaxLength(30)]
        public string Thoigian { get;set; }

        public string Chiphi { get;  set; }

        public int TrangThai { get; set; }
    }
}