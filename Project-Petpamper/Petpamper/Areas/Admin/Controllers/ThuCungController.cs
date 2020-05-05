using PetPamper.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetPamper.Areas.Admin.Controllers
{
    public class ThuCungController : BaseController
    {
        // GET: Admin/ThuCung
        public ActionResult DanhSach()
        {
            var models = ThuCungSQL.GetAll();
            return View(models);
        }

        public ActionResult Sua(string id)
        {
            var model = ThuCungSQL.GetById(id);
            if (model == null)
                return RedirectToAction("DanhSach");
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Sua(ThuCungModel profile)
        {
            if (ModelState.IsValid == false) return View(profile);
            ThuCungSQL.Update(profile);
            ViewBag.IsUpdate = true;
            return View(profile);
        }

        public ActionResult Xoa(string id)
        {
            var model = ThuCungSQL.GetById(id);
            if (model == null)
                return RedirectToAction("DanhSach");
            model.Trangthai = 0;
            ThuCungSQL.Update(model);
            return RedirectToAction("danhsach");
        }

        public ActionResult Them()
        {
            var model = new ThuCungModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Them(ThuCungModel model)
        {
            if (ModelState.IsValid == false) return View(model);
            ThuCungSQL.Insert(model);
            return RedirectToAction("DanhSach");
        }
    }
}