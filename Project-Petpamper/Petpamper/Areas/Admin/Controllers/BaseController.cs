using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetPamper.Areas.Admin.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        [AllowAnonymous]
        [NonAction]
        public ActionResult NotFound()
        {
            return RedirectToAction("NotFound", "Error");
        }
    }
}