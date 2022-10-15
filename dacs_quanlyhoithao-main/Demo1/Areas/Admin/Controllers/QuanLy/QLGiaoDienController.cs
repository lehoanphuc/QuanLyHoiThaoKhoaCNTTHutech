using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Areas.Admin.Controllers.QuanLy
{
    public class QLGiaoDienController : Controller
    {
        // GET: Admin/QLGiaoDien
        MyDataDataContext context = new MyDataDataContext();
        public ActionResult ThanhMenuTop()
        {
            return View(context.MONTHIDAUs.Where(m => m.TrangThai == 1).OrderBy(m => m.MaMon).ToList());
        }
        public ActionResult SuaMenuTop(int id)
        {
            var mon = context.MONTHIDAUs.FirstOrDefault(m => m.MaMon == id);
            return View(mon);
        }
        [HttpPost]
        public ActionResult SuaMenuTop(int id, FormCollection collection)
        {
            var mon = context.MONTHIDAUs.FirstOrDefault(m => m.MaMon == id);
            var TrangThai = collection["TrangThaiMenu"];
            mon.TrangThaiMenu = int.Parse(TrangThai);
            UpdateModel(mon);
            context.SubmitChanges();
            return RedirectToAction("ThanhMenuTop");
        }
    }
}