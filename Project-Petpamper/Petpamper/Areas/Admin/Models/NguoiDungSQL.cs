using PetPamper.Areas.Admin.Models;
using PetPamper.Lib.SQL;
using PetPamper.Models.Abstractions;
using System.Web.Mvc;
using System.Web.Security;
using System;
using System.Collections.Generic;
using System.Data;

namespace PetPamper.Areas.Admin.Models
{
    public class NguoiDungSQL
    {
        public static List<NguoiDungModel> GetAll()
        {
            DataTable datas = MSSQL.GetData(@"SELECT MaND, Tendangnhap FROM NGUOIDUNG Where DaXoa = 0", null, null);
            List<NguoiDungModel> models = new List<NguoiDungModel>();
            foreach (DataRow row in datas.Rows)
            {
                models.Add(new NguoiDungModel
                {
                    MaND = row["MaND"] + string.Empty,
                    Tendangnhap = row["Tendangnhap"] + string.Empty
                });
            }
            return models;
        }

        public static NguoiDungModel GetDetail(string MaND)
        {
            //            var userRow = MSSQL.GetRow(@"
            //SELECT MaKH, TenKH, SDT, SoCMND, Email, Diachi  
            //FROM NGUOIDUNG INNER JOIN KHACHHANG ON NGUOIDUNG.MaKH = KHACHHANG.MaKH
            //Where (DaXoa = 0) AND (Trangthai = 1)");

            //trước a có dặn e đặt tên biến sao cho nó dễ hiểu và đúng ngữ cảnh nhất, có copy paste cũng phải sửa chứ :(
            var ndRow = MSSQL.GetRow(@"
SELECT MaND, Tendangnhap, MaKH, Trangthai, Quanly
FROM NGUOIDUNG
WHERE MaND = @MaND", new string[] { "MaND" }, new object[] { MaND });

            if (ndRow != null)
            {
                return new NguoiDungModel
                {
                    // Mấy column e select ra đâu có dấu đâu, sao mấy cái này lại có dấu?
                    //MaKH = khRow["Mã KH"] + string.Empty,
                    //TenKH = khRow["Họ và tên"] + string.Empty,
                    //SDT = khRow["Số điện thoại"] + string.Empty,
                    //SoCMND = khRow["Số CMND"] + string.Empty,
                    //Email = khRow["Email"] + string.Empty,
                    //Diachi = khRow["Địa chỉ"] + string.Empty
                    MaND = ndRow["MaND"] + string.Empty,
                    Tendangnhap = ndRow["Tendangnhap"] + string.Empty,
                    MaKH = ndRow["MaKH"] + string.Empty,
                    Trangthai = ndRow["Trangthai"] + string.Empty,
                    Quanly = ndRow["Quanly"] + string.Empty
                };
            }
            return null;
        }
        public static void Update(NguoiDungModel profile)
        {
            var status = MSSQL.Execute(@"
UPDATE NGUOIDUNG 
SET Tendangnhap = @Tendangnhap, 
	Trangthai = @Trangthai,
	Quanly = @Quanly,
DaXoa = @DaXoa
FROM NGUOIDUNG 
WHERE MaND = @MaND", new string[] { "MaND","Tendangnhap", "Trangthai", "Quanly", "DaXoa"}, new object[] { profile.MaND, profile.Tendangnhap, profile.Trangthai, profile.Quanly, profile.DaXoa});
        }

        
    }
}