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
    public class KhachHangSQL
    {
        public static List<KhachHangModel> GetAll()
        {
            DataTable datas = MSSQL.GetData(@"SELECT MaKH, TenKH, SDT, SoCMND, Email, Diachi FROM KHACHHANG Where TrangThai = 1", null, null);
            List<KhachHangModel> models = new List<KhachHangModel>();
            foreach (DataRow row in datas.Rows)
            {
                models.Add(new KhachHangModel
                {
                    MaKH = row["MaKH"] + string.Empty,
                    Name = row["TenKH"] + string.Empty,
                    Phone = row["SDT"] + string.Empty,
                    IdentifyNumber = row["SoCMND"] + string.Empty,
                    Email = row["Email"] + string.Empty,
                    Address = row["Diachi"] + string.Empty

                });
            }
            return models;
        }

        public static KhachHangModel GetById(string maKH)
        {
            var userRow = MSSQL.GetRow(@"
SELECT MaKH, TenKH, SDT, SoCMND, Email, Diachi, TrangThai
FROM KHACHHANG WHERE MaKH = @MaKH", new string[] { "MaKH" }, new object[] { maKH });
            
            if (userRow != null)
            {
                return new KhachHangModel
                {
                    MaKH = userRow["MaKH"] + string.Empty,
                    Name = userRow["TenKH"] + string.Empty,
                    Phone = userRow["SDT"] + string.Empty,
                    IdentifyNumber = userRow["SoCMND"] + string.Empty,
                    Email = userRow["Email"] + string.Empty,
                    Address = userRow["Diachi"] + string.Empty,
                    TrangThai = int.Parse(userRow["TrangThai"] + string.Empty)
                };
            }
            return null;
        }
        public static void Insert(KhachHangModel model)
        {
            var status = MSSQL.Execute(@"
Insert into KHACHHANG(MaKH, TenKH, SoCMND, SDT, Email, DiaChi) values(@MaKH, @TenKH, @SoCMND, @SDT, @Email, @DiaChi)", new string[] { "MaKH", "TenKH", "SoCMND", "SDT", "Email", "DiaChi", }, new object[] { model.MaKH, model.Name, model.IdentifyNumber, model.Phone, model.Email, model.Address });
        }

        public static void Update(KhachHangModel profile)
        {
            var status = MSSQL.Execute(@"
UPDATE KHACHHANG 
SET TenKH = @TenKH, 
	SoCMND = @SoCMND,
	SDT = @SDT,
	Email = @Email,
	Diachi = @DiaChi,
TrangThai = @TrangThai
FROM KHACHHANG 
WHERE MaKH = @MaKH", new string[] {"MaKH", "TenKH", "SoCMND", "SDT", "Email", "DiaChi", "TrangThai"}, new object[] { profile.MaKH, profile.Name, profile.IdentifyNumber, profile.Phone, profile.Email, profile.Address, profile.TrangThai});

        }
    }
}