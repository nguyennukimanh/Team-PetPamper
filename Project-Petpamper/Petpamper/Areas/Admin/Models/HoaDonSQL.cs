using PetPamper.Areas.Admin.Models;
using PetPamper.Lib.SQL;
using PetPamper.Models.Abstractions;
using System.Web.Mvc;
using System.Web.Security;
using System;
using System.Collections.Generic;
using System.Data;
public class HoaDonSQL
{
    public static List<HoaDonModel> GetAll()
    {
        DataTable datas = MSSQL.GetData(@"SELECT MaHD, MaLich, Tongtien FROM HOADON  ", null, null);
        List<HoaDonModel> models = new List<HoaDonModel>();
        foreach (DataRow row in datas.Rows)
        {
            models.Add(new HoaDonModel
            {
                MaHD = row["MaHD"] + string.Empty,
                MaLich = row["MaLich"] + string.Empty,
                Tongtien = row["Tongtien"] + string.Empty,
               /* MaDV = row["MaDV"] + string.Empty,
                Soluong = row["Soluong"] + string.Empty,
                Chiphi = row["Chiphi"] + string.Empty,*/
            });
        }
        return models;
    }

    /*    public static HoaDonModel GetById(string MaHD)
          {
                var HoaDonRow = MSSQL.GetRow(@"
    SELECT MaHD ,MaLich, Tongtien
    FROM HOADON WHERE MaHD = @MaHD", new string[] { "MaHD" }, new object[] { MaHD });

                if (HoaDonRow != null)
                {
                    return new HoaDonModel
                    {
                        MaHD = HoaDonRow["MaHD"] + string.Empty,
                        MaLich = HoaDonRow["MaLich"] + string.Empty,
                        Tongtien = HoaDonRow["Tongtien"] + string.Empty,

                    };
                }
                return null;
            }*/
    public static void Insert(HoaDonModel model)
    {
        var status = MSSQL.Execute(@"
Insert into HOADON(MaHD ,MaLich, Tongtien) values(@MaHD ,@MaLich, @Tongtien)", new string[] { "MaHD", "MaLich", "Tongtien" }, new object[] { model.MaHD, model.MaLich, model.Tongtien });
    }
    /* 
     public static void Update(HoaDonModel profile)
     {
         var status = MSSQL.Execute(@"
 UPDATE HOADON
 SET MaHD = @MaHD,
     MaLich = @MaLich,
     Tongtien = @Tongtien,

 FROM HOADON
 WHERE MaHD = @MaHD", new string[] { "MaHD", "MaLich", "Tongtien" }, new object[] { profile.MaHD, profile.MaLich, profile.Tongtien });
     }
 }
 */

   /* public static HoaDonModel GetDetail(string MaHD)
    {
        var hdRow = MSSQL.GetRow(@"
SELECT MaHD, MaDV, Soluong, Chiphi FROM HOADON JOIN CHITIETHD ON HOADON.MaHD = CHITIETHD.MaHD
WHERE MaHD = @MaHD", new string[] { "MaHD" }, new object[] { MaHD });

        if (hdRow != null)
        {
            return new HoaDonModel
            {
                MaHD = hdRow["MaHD"] + string.Empty,
                MaDV = hdRow["MaDV"] + string.Empty,
                Soluong = hdRow["Soluong"] + string.Empty,
                Chiphi = hdRow["Chiphi"] + string.Empty,
            };
        }
        return null;
    }*/

}