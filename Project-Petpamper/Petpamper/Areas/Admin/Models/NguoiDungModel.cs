using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetPamper.Areas.Admin.Models
{
    // phan tach ra dum a, KHACHANG hay NGUOIDUNG?
    public class NguoiDungModel
    {
        public NguoiDungModel()
        {
            Tendangnhap = string.Empty;
            Trangthai = string.Empty;
            Quanly = string.Empty;
        }
        public string MaND { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc"), MaxLength(30)]
        public string Tendangnhap { get; set; }
        
        public string MaKH { get; set; }

        [Required(ErrorMessage = "Trạng thái là True (1) hoặc False (0)")]
        public string Trangthai { get; set; }

        [Required(ErrorMessage = "Quản lý là True (1) hoặc False (0)")]
        public string Quanly { get; set; }

        public bool DaXoa { get; set; }

    }
}