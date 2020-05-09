using PetPamper.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetPamper.Areas.Admin.Controllers
{
    public class DatLichController : BaseController
    {
        // GET: Admin/DatLich
        public ActionResult DanhSach()
        {
            var models = DatLichSQL.GetAll();
            return View(models);
        }

        public ActionResult Sua(string id)
        {
            var model = DatLichSQL.GetById(id);
            if (model == null)
                return RedirectToAction("DanhSach");
            return View(model);
        }

        [HttpPost]
        public ActionResult Sua(DatLichModel profile)
        {
            if (ModelState.IsValid == false) return View(profile);
            DatLichSQL.Update(profile);
            ViewBag.IsUpdate = true;
            return View(profile);
        }

        public ActionResult Xoa(string id)
        {
            var model = DatLichSQL.GetById(id);
            if (model == null)
                return RedirectToAction("DanhSach");
            model.Trangthai = 0;
            DatLichSQL.Update(model);
            return RedirectToAction("DanhSach");
        }

        public ActionResult Them()
        {
            var model = new DatLichModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Them(DatLichModel model)
        {
            if (ModelState.IsValid == false) return View(model);
            DatLichSQL.Insert(model);
            return RedirectToAction("DanhSach");
        }
        public ActionResult Chitiet(string id)
        {
            var models = DatLichSQL.GetDetail(id);
            if (models == null)
                return NotFound();

            return View(models);
        }
    }
}