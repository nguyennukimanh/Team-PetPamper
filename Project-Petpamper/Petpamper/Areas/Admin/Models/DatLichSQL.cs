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
    public class DatLichSQL
    {
        //Xemdanhsach
        public static List<DatLichModel> GetAll()
        {
            DataTable datas = MSSQL.GetData(@"SELECT MaLich, MaKH, MaTC, Thoigiandat, Thoigianden, Thoigiantra FROM DATLICH ", null, null);
            List<DatLichModel> models = new List<DatLichModel>();
            foreach (DataRow row in datas.Rows)
            {
                models.Add(new DatLichModel
                {
                    MaLich = row["MaLich"] + string.Empty,
                    MaKH = row["MaKH"] + string.Empty,
                    MaTC = row["MaTC"] + string.Empty,
                    Thoigiandat = row["thoigiandat"] + string.Empty,
                    Thoigianden = row["Thoigianden"] + string.Empty,
                    Thoigiantra = row["Thoigiantra"] + string.Empty


                });
            }
            return models;
        }

        public static DatLichModel GetById(string MaLich)
        {
            var userRow = MSSQL.GetRow(@"SELECT MaLich, MaKH, MaTC, Thoigiandat, Thoigianden, Thoigiantra, Hinhthuc, Trangthai FROM DATLICH where MaLich = @MaLich", new string[] { "MaLich" }, new object[] { MaLich });

            if (userRow != null)
            {
                return new DatLichModel
                {
                    MaLich = userRow["MaLich"] + string.Empty,
                    MaKH = userRow["MaKH"] + string.Empty,
                    MaTC = userRow["MaTC"] + string.Empty,
                    Thoigiandat = convert.toDatetime(userRow["thoigiandat"] + string.Empty),
                    Thoigianden = userRow["Thoigianden"] + string.Empty,
                    Thoigiantra = userRow["Thoigiantra"] + string.Empty,
                    Hinhthuc = int.Parse(userRow["Hinhthuc"] + string.Empty),
                    Trangthai = int.Parse(userRow["TrangThai"] + string.Empty)
                };
            }
            return null;
        }
        public static void Insert(DatLichModel model)
        {
            var status = MSSQL.Execute(@"
Insert into DATLICH(MaLich, MaKH, MaTC, Thoigiandat, Thoigianden, Thoigiantra, Hinhthuc, Trangthai) values(@MaLich, @MaKH, @MaTC, @Thoigiandat, @Thoigianden, @Thoigiantra, @Hinhthuc, @Trangthai)", new string[] { "MaLich", "MaKH", "MaTC", "Thoigiandat", "Thoigianden", "Thoigiantra", "Hinhthuc", "Trangthai" }, new object[] { model.MaLich, model.MaKH, });
        }

        public static void Update(DatLichModel profile)
        {
            var status = MSSQL.Execute(@"
UPDATE KHACHHANG 
SET MaKH = @MaKH,
    MaTC = @MaTC,
    Thoigiandat = @Thoigiandat,
    Thoigianden = @Thoigianden,
    Thoigiantra = @Thoigiantra,
    Hinhthuc = @Hinhthuc,
    Trangthai = @Trangthai
FROM KHACHHANG 
WHERE MaLich = @MaLich", new string[] { "MaLich", "MaKH", "MaTC", "Thoigiandat", "Thoigianden", "Thoigiantra", "Hinhthuc", "Trangthai" }, new object[] { profile.MaLich, profile.MaKH, profile.MaTC, profile.Thoigiandat, profile.Thoigianden, profile.Thoigiantra, profile.Hinhthuc, profile.Trangthai });
        }
        public static DatLichModel GetDetail(string MaLich)
        {

            var ndRow = MSSQL.GetRow(@"
SELECT MaLich, MaKH, MaTC, Thoigiandat, Thoigianden, Thoigiantra, Hinhthuc, Trangthai FROM DATLICH where MaLich = @MaLich", new string[] { "MaLich" }, new object[] {MaLich});

            if (ndRow != null)
            {
                return new DatLichModel
                {

                    MaLich = ndRow["MaLich"] + string.Empty,
                    MaKH = ndRow["MaKH"] + string.Empty,
                    MaTC = ndRow["MaTC"] + string.Empty,
                    Thoigiandat = ndRow["thoigiandat"] + string.Empty,
                    Thoigianden = ndRow["Thoigianden"] + string.Empty,
                    Thoigiantra = ndRow["Thoigiantra"] + string.Empty,
                    Hinhthuc = int.Parse(ndRow["Hinhthuc"] + string.Empty),
                    Trangthai = int.Parse(ndRow["TrangThai"] + string.Empty)
                };
            }
            return null;
        }

    }
}
