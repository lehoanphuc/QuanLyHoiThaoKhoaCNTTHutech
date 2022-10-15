using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Areas.Admin.Controllers
{
    public class PhanQuyenController : Controller
    {
        // GET: Admin/PhanQuyen
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TaoQuyen()
        {
            return View();
        }
    }
}