using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetPamper.Areas.Admin.Models
{
    public class HoaDonModel
    {
        public HoaDonModel()
        {

            MaHD = string.Empty;
            MaLich = string.Empty;
            Tongtien = string.Empty;
           /* MaDV = string.Empty;
            Soluong = string.Empty;
            Chiphi = string.Empty;*/
        }
       
        
        public string MaHD { get; set; }
        [StringLength(maximumLength: 5, MinimumLength = 5, ErrorMessage = "Mã Lịch không hợp lệ")]
        [Required(ErrorMessage = "Mã Lịch là bắt buộc")]
        public string MaLich { get; set; }
        [Required(ErrorMessage = "Tổng tiền là bắt buộc")]
        public string Tongtien { get; set; }
       /* public string MaDV { get; set; }
        [Required(ErrorMessage = "Số lượng là bắt buộc")]
        public string Soluong { get; set; }
        [Required(ErrorMessage = "Chi phí là bắt buộc")]
        public string Chiphi { get; set; }*/
    }
}