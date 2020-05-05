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
    public class DichVuSQL
    {
        public static List<DichVuModel> GetAll()
        {
            DataTable datas = MSSQL.GetData(@"SELECT MaDV, TenDV, Thoigian, Chiphi FROM DICHVU Where TrangThai = 1", null, null);
            List<DichVuModel> models = new List<DichVuModel>();
            foreach(DataRow row in datas.Rows)
            {
                models.Add(new DichVuModel
                {
                    MaDV = row["MaDV"] + string.Empty,
                    TenDV = row["TenDV"] + string.Empty,
                    Thoigian = row["Thoigian"] + string.Empty,
                    Chiphi = row["Chiphi"] + string.Empty,


            });
            }
            return models;
        }
        public static DichVuModel GetById(string maDV)
        {
            var DVRow = MSSQL.GetRow(@"
SELECT MaDV, TenDV, Thoigian, Chiphi, TrangThai
FROM DICHVU WHERE MaDV = @MaDV", new string[] { "MaDV" }, new object[] { maDV });

            if(DVRow != null)
            {
                return new DichVuModel
                {
                    MaDV = DVRow["MaDV"] + string.Empty,
                    TenDV = DVRow["TenDV"] + string.Empty,
                    Thoigian = DVRow["Thoigian"] + string.Empty,
                    Chiphi = DVRow["Chiphi"] + string.Empty,
                    TrangThai = int.Parse(DVRow["TrangThai"] + string.Empty)
                };
            }
            return null;
        }
        public static void Update(DichVuModel profile)
        {
            var status = MSSQL.Execute(@"
UPDATE DICHVU
SET 
	TenDV = @TenDV,
	Thoigian = @Thoigian,
	Chiphi = @Chiphi,
    TrangThai = @TrangThai
FROM DICHVU
WHERE MaDV = @MaDV", new string[] { "MaDV", "TenDV", "Thoigian", "Chiphi", "TrangThai" }, new object[]
  { profile.MaDV, profile.TenDV, profile.Thoigian, profile.Chiphi, profile.TrangThai });

        }
        public static void Insert(DichVuModel model)
        {
            var status = MSSQL.Execute(@"
Insert into DICHVU(MaDV, TenDV, Thoigian, Chiphi,TrangThai) values(@MaDV, @TenDV, @Thoigian, @Chiphi,1)", 
new string[] { "MaDV", "TenDV", "Thoigian", "Chiphi","TrangThai" }, 
new object[] { model.MaDV, model.TenDV, model.Thoigian, model.Chiphi,model.TrangThai});
        }
    }
}