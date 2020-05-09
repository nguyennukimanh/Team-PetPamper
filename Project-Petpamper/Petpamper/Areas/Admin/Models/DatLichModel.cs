using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PetPamper.Areas.Admin.Models
{
    public class DatLichModel
    {
        public DatLichModel()
        {
            //MaLich = string.Empty;
            MaKH = string.Empty;
            MaTC = string.Empty;
            Thoigiandat = string.Empty;
            Thoigianden = string.Empty;
            Thoigiantra = string.Empty;


        }
        public string MaLich { get; set; }
        public string MaKH { get; set; }
        public string MaTC { get; set; }

        [Required(ErrorMessage = "Thời gian đặt là bắt buộc"), MaxLength(30)]
        public string Thoigiandat { get; set; }

        [Required(ErrorMessage = "Thời gian đến là bắt buộc"), MaxLength(30)]
        public string Thoigianden { get; set; }

        [Required(ErrorMessage = "Thời gian trả là bắt buộc"), MaxLength(30)]
        public string Thoigiantra { get; set; }

        [Required(ErrorMessage = "Hình thức là True (1) hoặc False (0)")]
        public int Hinhthuc { get; set; }

        public int Trangthai { get; set; }

    }
}