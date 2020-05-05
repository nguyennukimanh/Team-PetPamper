using Microsoft.CSharp.RuntimeBinder;
using PetPamper.Areas.Admin.Models;
using PetPamper.Lib.SQL;
using PetPamper.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace PetPamper.Areas.Admin.Controllers
{


    public class UserController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SignIn(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = Url.IsLocalUrl(ReturnUrl) ? ReturnUrl : "/";

            return User.Identity.IsAuthenticated ? (ActionResult)Redirect(ViewBag.ReturnUrl) : View();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn(LoginModel loginVM, string returnUrl = null)
        {
            var userRow = MSSQL.GetRow(@"
SELECT MaND, Tendangnhap
FROM NGUOIDUNG
WHERE Tendangnhap = @Tendangnhap AND Matkhau = @Matkhau AND Trangthai = 1 AND Quanly = 1 AND DaXoa = 0
", new string[] { "Tendangnhap", "Matkhau" }, new object[] { loginVM.Username, Helper.Encrypt(loginVM.Password) });
            if (userRow == null)
                return Json(new BaseResponseModel("Tên đăng nhập hoặc mật khẩu không đúng."), JsonRequestBehavior.DenyGet);

            FormsAuthentication.SetAuthCookie(loginVM.Username, loginVM.RememberMe);

            return Json(new BaseResponseModel(), JsonRequestBehavior.DenyGet);
        }

        public ActionResult Profile()
        {
            var userRow = MSSQL.GetRow(@"
                SELECT kh.TenKH, kh.SDT, kh.SoCMND, kh.Email, kh.Diachi
                FROM KHACHHANG kh
	                INNER JOIN NGUOIDUNG nd ON kh.MaKH = nd.MaKH
                WHERE nd.Tendangnhap = @Tendangnhap", new string[] { "Tendangnhap" }, new object[] { User.Identity.Name });
            var profileModel = new UserProfileModel();
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
        public new ActionResult Profile(UserProfileModel profile)
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


            var OldpassFromModalEncrypt = Helper.Encrypt(model.OldPassword); 
            if (oldPassword != OldpassFromModalEncrypt)
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

            return RedirectToAction("ChangePassSuccess", "User", new { area = "Admin" });
        }

        [AllowAnonymous]
        public ActionResult ChangePassSuccess()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ResetPassword()
        {
            // Hien thi view cho nguoi dung nhap vao email
            var model = new ResetPasswordModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {

            if (!ModelState.IsValid)
                return HttpNotFound();

            var changeToken = Guid.NewGuid().ToString();
            // Save change token to database

            var message = new MailMessage();
            message.From = new MailAddress("vie******@gmail.com");
            message.To.Add(new MailAddress(model.Email));
            message.Subject = "Test Send mail";
            message.Body = string.Format("Change <a href='http://localhost:44396/home/verify?token={0}'>change</a>", changeToken);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential("vie*******@gmail.com", "ozb*********lffkj"),
                EnableSsl = true
            })
            {
                await smtp.SendMailAsync(message);
                return RedirectToAction("About");
            }
        }

        [HttpGet]
        public ActionResult Verify(string token)
        {
            // Kiem tra token trong csdl
            // Neu tim thay token thi Redirect qua trang doi pass
            // Nguoc lai thong bao token khong hop le
            return View();
        }

    }

}