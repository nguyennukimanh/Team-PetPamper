using PetPamper.Lib.SQL;
using PetPamper.Models;
using PetPamper.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace PetPamper.Controllers
{
    public class HomeController : Controller
    {
        private object loginVM;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [AllowAnonymous]
        public ActionResult SignUp()
        {
            var model = new UserModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignUp(UserModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            // Mã hoá password

            model.Matkhau = Helper.Encrypt(model.Matkhau);
            var isSuccess = UserSQL.Insert(model);
            if (isSuccess)
            {
                TempData["IsCreate"] = true;
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Forgot()
        {
            var model = new ForgotModel();

            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Forgot(ForgotModel userForgotModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userForgotModel);
            }
            var userEmail = UserSQL.GetEmailByUserName(userForgotModel.Tendangnhap);

            if (userEmail == userForgotModel.Email)
            {
                //1. Cập nhật mật khẩu xuống DB
                var randomPassword = Helper.RandomNumber(100000, 999999).ToString();
                var userId = UserSQL.GetUserIdByUserName(userForgotModel.Tendangnhap);
                if (userId != null)
                {
                    var updatePassResult = UserSQL.UpdateUserPassword(userId, randomPassword);
                    //2. Gửi email 
                    if (updatePassResult)
                    {
                        Helper.SendEmail(userEmail, randomPassword);
                    }
                    else
                    {

                    }
                }
                else
                {
                    // ToDo: Do Something
                }
            }
            else
                return View(userForgotModel);
            return Redirect("/");
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginModel);
            }

            var isSuccess = UserSQL.IsValidLogin(userLoginModel);

            if (isSuccess)
            {
                FormsAuthentication.SetAuthCookie(userLoginModel.Tendangnhap, true);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("abcdef", "Dữ liệu không hợp lệ");
            return View(userLoginModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }


    }
}