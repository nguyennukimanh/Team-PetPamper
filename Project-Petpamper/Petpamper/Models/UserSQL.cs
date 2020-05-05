using PetPamper.Areas.Admin.Models;
using PetPamper.Lib.SQL;
using PetPamper.Models.Abstractions;
using System.Web.Mvc;
using System.Web.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PetPamper.Models
{
    public class UserSQL
    {
        public static bool Insert(UserModel model)
        {
            var statusKH = MSSQL.Execute(@"Insert into KHACHHANG(MaKH, TenKH, SoCMND, SDT, Email, DiaChi, Trangthai) values(('KH'+ RIGHT('00'+ CAST(next value for contact_MaKH AS NVARCHAR(3)),3)), @TenKH, @SoCMND, @SDT, @Email, @DiaChi, 1)", new string[] { "TenKH", "SoCMND", "SDT", "Email", "DiaChi", }, new object[] { model.TenKH, model.SoCMND, model.SDT, model.Email, model.Diachi, model.Trangthai });
            if(statusKH)
            {
                var MaKH = MSSQL.GetData($"SELECT  'KH'+ RIGHT('00'+ CAST(current_value AS NVARCHAR(3)),3) as MaKH FROM sys.sequences WHERE name = 'contact_MaKH'").Rows[0]["MaKH"];

                var statusND = MSSQL.Execute(@"
                    Insert into NGUOIDUNG(MaND, Tendangnhap, Matkhau, MaKH, Trangthai, Quanly, DaXoa)  values ('ND' + RIGHT('00' + CAST(next value for contact_MaND AS NVARCHAR(3)),3), @Tendangnhap, @Matkhau, @MaKH,1,0,0)", new string[] {  "Tendangnhap", "Matkhau", "MaKH" }, new object[] { model.Tendangnhap, model.Matkhau, MaKH});

                return statusND;
            }

            return false;
        }
       
        public static bool UpdateUserPassword(string maND, string password)
        {
            var query = $@" UPDATE NGUOIDUNG set Matkhau = '{Helper.Encrypt(password)}' where MaND ='{maND}'";

            var update = MSSQL.Execute(query);

            return true;
        }

        public static bool IsValidLogin(LoginModel loginModel)
        {
            var queryGetUserInfo = $"select * from NGUOIDUNG where Tendangnhap = '{loginModel.Tendangnhap}' and Matkhau = '{Helper.Encrypt(loginModel.Matkhau)}'";
            var userInfo = MSSQL.GetItemByQuery<LoginModel>(queryGetUserInfo);

            return userInfo != null;
        }

        public static string GetEmailByUserName(string userName)
        {
            var queryGetUserInfo = $"select KHACHHANG.Email " +
                $"from KHACHHANG" +
                $" join NGUOIDUNG on KHACHHANG.MaKH = NGUOIDUNG.MaKH where Tendangnhap = '{userName}'";
            if (queryGetUserInfo == null)
                return null;
            else
                return MSSQL.GetData(queryGetUserInfo).Rows[0]["Email"].ToString();
        }

        public static string GetUserIdByUserName(string userName)
        {
            var queryGetUserInfo = $"select MaND from NGUOIDUNG where Tendangnhap = '{userName}'";
            return MSSQL.GetData(queryGetUserInfo).Rows[0]["MaND"].ToString();
        }
    }
}