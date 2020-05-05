using PetPamper.Areas.Admin.Models;
using PetPamper.Lib.SQL;
using PetPamper.Models.Abstractions;
using System.Web.Mvc;
using System.Web.Security;
using System;
using System.Collections.Generic;

namespace PetPamper.Areas.Admin.Controllers
{
    public class DichVuController : BaseController
    {
        public ActionResult DanhSach()
        {
            var models = DichVuSQL.GetAll();
            return View(models);
        }
        public ActionResult Xoa(string id)
        {
            var model = DichVuSQL.GetById(id);
            if (model == null)
                return RedirectToAction("DanhSach");
            model.TrangThai = 0;
            DichVuSQL.Update(model);
            return RedirectToAction("danhsach");
        }
        public ActionResult Them()
        {
            var model = new DichVuModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Them(DichVuModel model)
        {
            if (ModelState.IsValid == false) return View(model);
            DichVuSQL.Insert(model);
            return RedirectToAction("DanhSach");
        }

        public ActionResult Sua(string id)
        {
            var model = DichVuSQL.GetById(id);
            if (model == null)
                return RedirectToAction("DanhSach");
            return View(model);
        }


        [HttpPost]
        public ActionResult Sua(DichVuModel profile)
        {
            if (ModelState.IsValid == false) return View(profile);
            DichVuSQL.Update(profile);
            ViewBag.IsUpdate = true;
            return View(profile);
        }

    }
}