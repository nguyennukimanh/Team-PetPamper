using PetPamper.Areas.Admin.Models;
using PetPamper.Lib.SQL;
using PetPamper.Models.Abstractions;
using System.Web.Mvc;
using System.Web.Security;
using System;
using System.Collections.Generic;

namespace PetPamper.Areas.Admin.Controllers
{
    public class NguoiDungController : BaseController
    {
        // GET: Admin/NguoiDung
        public ActionResult DanhSach()
        {
            var models = NguoiDungSQL.GetAll();
            return View(models);
        }

        public ActionResult Chitiet(string id)//e ko dung dc cai ten nay dau nhe
        {
            //ua z muon lay detail NGUOIDUNG hay KHACHHANG?
            var models = NguoiDungSQL.GetDetail(id);
            if (models == null)
                return NotFound();

            return View(models);
        }

        public ActionResult Sua(string id)
        {
            var model = NguoiDungSQL.GetDetail(id);
            if (model == null)
                return RedirectToAction("DanhSach");
            return View(model);
        }


        [HttpPost]
        public ActionResult Sua(NguoiDungModel profile)
        {
            if (ModelState.IsValid == false) return View(profile);
            NguoiDungSQL.Update(profile);
            ViewBag.IsUpdate = true;
            return View(profile);
        }

        public ActionResult Xoa(string id)
        {
            var model = NguoiDungSQL.GetDetail(id);
            if (model == null)
                return RedirectToAction("DanhSach");
            model.DaXoa = true;
            NguoiDungSQL.Update(model);
            return RedirectToAction("danhsach");
        }

       

        
    }
}