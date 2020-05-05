using PetPamper.Lib.SQL;
using PetPamper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PetPamper.Controllers
{
    public class UserProController : Controller
    {
        // GET: UserPro
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Profile()
        {
            var userRow = MSSQL.GetRow(@"
SELECT kh.TenKH, kh.SDT, kh.SoCMND, kh.Email, kh.Diachi
FROM KHACHHANG kh
	INNER JOIN NGUOIDUNG nd ON kh.MaKH = nd.MaKH
WHERE nd.Tendangnhap = @Tendangnhap", new string[] { "Tendangnhap" }, new object[] { User.Identity.Name });
            var profileModel = new UserProModel();
            if (userRow != null)
            {
                profileModel.Name = userRow["TenKH"] + string.Empty;
                profileModel.Phone = userRow["SDT"] + string.Empty;
                profileModel.IdentifyNumber = userRow["SoCMND"] + string.Empty;
                profileModel.Email = userRow["Email"] + string.Empty;
                profileModel.Address = userRow["Diachi"] + string.Empty;
            }

            return View(profileModel);
        }

        [HttpPost]
        public new ActionResult Profile(UserProModel profile)
        {
            if (ModelState.IsValid == false) return View(profile);
            var status = MSSQL.Execute(@"
UPDATE KHACHHANG 
SET TenKH = @TenKH, 
	SoCMND = @SoCMND,
	SDT = @SDT,
	Email = @Email,
	Diachi = @DiaChi
FROM KHACHHANG kh, NGUOIDUNG nd
WHERE kh.MaKH = nd.MaKH AND nd.Tendangnhap = @Tendangnhap", new string[] { "TenKH", "SoCMND", "SDT", "Email", "DiaChi", "Tendangnhap" }, new object[] { profile.Name, profile.IdentifyNumber, profile.Phone, profile.Email, profile.Address, User.Identity.Name });
            ViewBag.IsUpdate = true;
            return View(profile);
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangePass()
        {
            var model = new ChangePasswordModel();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePass(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // change pass
            var userRow = MSSQL.GetRow(@"
                SELECT Matkhau
                FROM  NGUOIDUNG 
                WHERE Tendangnhap = @Tendangnhap",
                    new string[] {
                        "Tendangnhap"
                    },
                    new object[] {
                        User.Identity.Name
                    });

            string oldPassword = (userRow["Matkhau"] + string.Empty);

            if (oldPassword == string.Empty)
            {
                ModelState.AddModelError(string.Empty, "User account is not valid");
                return View(model);
            }

            if (oldPassword != Helper.Encrypt(model.OldPassword))
            {
                ModelState.AddModelError(string.Empty, "The password user enter is not valid");
                return View(model);
            }

            var status = MSSQL.Execute(@"
                UPDATE NGUOIDUNG
                SET Matkhau = @Matkhau
                WHERE Tendangnhap = @Tendangnhap",
                    new string[] {
                        "Matkhau",
                        "Tendangnhap"
                    },
                    new object[] {
                        Helper.Encrypt(model.NewPassword),
                        User.Identity.Name
                    });


            ViewBag.IsUpdate = true;

            FormsAuthentication.SignOut();

            return RedirectToAction("ChangePassSuccess");
        }

        [AllowAnonymous]
        public ActionResult ChangePassSuccess()
        {
            return View();
        }
    }
}