using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetPamper.Areas.Admin.Models
{
    public class ThuCungModel
    {
        public ThuCungModel()
        {

            MaKH = string.Empty;
            TenTC = string.Empty;
            Loai = string.Empty;
            Gioitinh = string.Empty;
            Cannang = string.Empty;
            Tuoi = string.Empty;
        }

        public string MaTC { get; set; }

        [StringLength(maximumLength: 5, MinimumLength = 5, ErrorMessage = "Mã KH không hợp lệ")]
        [Required(ErrorMessage = "Mã KH là bắt buộc")]
        public string MaKH { get; set; }

        [Required(ErrorMessage = "Tên thú cưng là bắt buộc"), MaxLength(30)]
        public string TenTC { get; set; }

        [Required(ErrorMessage = "Loài là True (1) hoặc False (0)")]
        public string Loai { get; set; }

        [Required(ErrorMessage = "Giới tính là True (1) hoặc False (0)")]
        public string Gioitinh { get; set; }

        [Required(ErrorMessage = "Cân nặng là bắt buộc"), MaxLength(30)]
        public string Cannang { get; set; }

        [Required(ErrorMessage = "Tuổi là bắt buộc"), MaxLength(30)]
        public string Tuoi { get; set; }

        public int Trangthai { get; set; }
    }
}
