using PetPamper.Areas.Admin.Models;
using PetPamper.Lib.SQL;
using PetPamper.Models.Abstractions;
using System.Web.Mvc;
using System.Web.Security;
using System;
using System.Collections.Generic;

namespace PetPamper.Areas.Admin.Controllers
{
    public class KhachHangController : BaseController
    {
        public ActionResult DanhSach()
        {
            var models = KhachHangSQL.GetAll();
            return View(models);
        }

        public ActionResult Them()
        {
            var model = new KhachHangModel();
            return View(model);
        }

        public ActionResult Xoa(string id)
        {
            var model = KhachHangSQL.GetById(id);
            if (model == null)
                return RedirectToAction("DanhSach");
            model.TrangThai = 0;
            KhachHangSQL.Update(model);
            return RedirectToAction("danhsach");
        }

        [HttpPost]
        public ActionResult Them(KhachHangModel model)
        {
            if (ModelState.IsValid == false) return View(model);
            KhachHangSQL.Insert(model);
            return RedirectToAction("DanhSach");
        }

        public ActionResult Sua(string id)
        {
            var model = KhachHangSQL.GetById(id);
            if (model == null)
                return RedirectToAction("DanhSach");
            return View(model);
        }

       
        [HttpPost]
        public ActionResult Sua(KhachHangModel profile)
        {
            if (ModelState.IsValid == false) return View(profile);
            KhachHangSQL.Update(profile);
            ViewBag.IsUpdate = true;
            return View(profile);
        }
        
    }
}