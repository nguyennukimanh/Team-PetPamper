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
    public class ThuCungSQL
    {
        public static List<ThuCungModel> GetAll()
        {
            DataTable datas = MSSQL.GetData(@"SELECT MaTC, MaKH, TenTC, Loai, Gioitinh, Cannang, Tuoi FROM THUCUNG Where TrangThai = 1", null, null);
            List<ThuCungModel> models = new List<ThuCungModel>();
            foreach (DataRow row in datas.Rows)
            {
                models.Add(new ThuCungModel
                {
                    MaTC = row["MaTC"] + string.Empty,
                    MaKH = row["MaKH"] + string.Empty,
                    TenTC = row["TenTC"] + string.Empty,
                    Loai = row["Loai"] + string.Empty,
                    Gioitinh = row["Gioitinh"] + string.Empty,
                    Cannang = row["Cannang"] + string.Empty,
                    Tuoi = row["Tuoi"] + string.Empty

                });
            }
            return models;
        }

        public static ThuCungModel GetById(string maTC)
        {
            var ThuCungRow = MSSQL.GetRow(@"
SELECT MaTC ,MaKH, TenTC, Loai, Gioitinh, Cannang, Tuoi, Trangthai
FROM THUCUNG WHERE MaTC = @MaTC", new string[] { "MaTC" }, new object[] { maTC });

            if (ThuCungRow != null)
            {
                return new ThuCungModel
                {
                    MaTC = ThuCungRow["MaTC"] + string.Empty,
                    MaKH = ThuCungRow["MaKH"] + string.Empty,
                    TenTC = ThuCungRow["TenTC"] + string.Empty,
                    Loai = ThuCungRow["Loai"] + string.Empty,
                    Gioitinh = ThuCungRow["Gioitinh"] + string.Empty,
                    Cannang = ThuCungRow["Cannang"] + string.Empty,
                    Tuoi = ThuCungRow["Tuoi"] + string.Empty,
                    Trangthai = int.Parse(ThuCungRow["Trangthai"] + string.Empty)
                };
            }
            return null;
        }

        public static void Update(ThuCungModel profile)
        {
            var status = MSSQL.Execute(@"
UPDATE THUCUNG
SET MaKH = @MaKH,
	TenTC = @TenTC,
	Loai = @Loai,
	Gioitinh = @Gioitinh,
	Cannang = @Cannang,
    Tuoi = @Tuoi,
    Trangthai = @Trangthai
FROM THUCUNG
WHERE MaTC = @MaTC", new string[] { "MaTC", "MaKH", "TenTC", "Loai", "Gioitinh", "Cannang", "Tuoi", "Trangthai" }, new object[] { profile.MaTC, profile.MaKH, profile.TenTC, profile.Loai, profile.Gioitinh, profile.Cannang, profile.Tuoi, profile.Trangthai });

        }

        public static void Insert(ThuCungModel model)
        {
            var status = MSSQL.Execute(@"
Insert into THUCUNG(MaTC ,MaKH, TenTC, Loai, Gioitinh, Cannang, Tuoi, Trangthai) values(@MaTC ,@MaKH, @TenTC, @Loai, @Gioitinh, @Cannang, @Tuoi, 1)", new string[] { "MaTC" ,"MaKH", "TenTC", "Loai", "Gioitinh", "Cannang", "Tuoi", "Trangthai" }, new object[] {model.MaTC ,model.MaKH, model.TenTC, model.Loai, model.Gioitinh, model.Cannang, model.Tuoi, model.Trangthai });
        }
    }
}