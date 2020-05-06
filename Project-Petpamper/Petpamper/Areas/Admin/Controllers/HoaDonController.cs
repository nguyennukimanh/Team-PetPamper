using PetPamper.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetPamper.Areas.Admin.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: Admin/HoaDon
       
             public ActionResult DanhSach()
        {
            var models = HoaDonSQL.GetAll();
            return View(models);
        }
       /* public ActionResult Chitiet(string id)//e ko dung dc cai ten nay dau nhe
        {
            
            var models = HoaDonSQL.GetDetail(id);
            if (models == null)
                return NotFound();

            return View(models);
        }*/


        public ActionResult Them()
        {
            var model = new HoaDonModel();
            return View(model);
        }
    

        [HttpPost]
        public ActionResult Them(HoaDonModel model)
        {
            if (ModelState.IsValid == false) return View(model);
            HoaDonSQL.Insert(model);
            return RedirectToAction("DanhSach");
        }


    }
}