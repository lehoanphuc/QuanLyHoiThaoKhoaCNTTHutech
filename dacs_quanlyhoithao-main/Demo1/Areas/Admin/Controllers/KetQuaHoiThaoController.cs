using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Areas.Admin.Controllers
{
    public class KetQuaHoiThaoController : Controller
    {
        // GET: Admin/KetQuaHoiThao
        public ActionResult KQBongDa()
        {
            return View();
        }
        public ActionResult KQBongChuyen()
        {
            return View();
        }
        public ActionResult KQCauLong()
        {
            return View();
        }
        public ActionResult KQKeoCo()
        {
            return View();
        }
        public ActionResult KQCoVua_CoTuong()
        {
            return View();
        }
        public ActionResult KQDienKinh()
        {
            return View();
        }
    }
}