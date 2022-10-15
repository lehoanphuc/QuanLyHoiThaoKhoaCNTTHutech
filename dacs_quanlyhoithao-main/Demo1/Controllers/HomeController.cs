using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        MyDataDataContext context = new MyDataDataContext();
        public ActionResult TrangChu()
        {
            return View(context.MONTHIDAUs.Where(m => m.TrangThaiMenu == 1).OrderBy(m => m.MaMon).ToList());
        }
        public ActionResult MenuTop()
        {
            return View(context.MONTHIDAUs.OrderBy(m => m.MaMon).ToList());
        }
        public ActionResult ChiTiet(int id)
        {

            var Mon = context.MONTHIDAUs.Where(m => m.MaMon == id).FirstOrDefault();
            // Session["MaDieuLe"] = Mon;
            return View(Mon);
        }
        //public ActionResult ChiTiet(int id, FormCollection collection, PHIEUDANGKY PDK)
        //{
        //    var Mon = context.MONTHIDAUs.Where(m => m.MaMon == id).FirstOrDefault();
        //    USER user = (USER)Session["TaiKhoanSV"];
        //    ViewBag.MaMon = new SelectList(context.DOIs.ToList().OrderBy(n => n.MaMon).Where(n => n.MaSo == user.MaSo), "MaMon", "TenMon");
        //    PDK.NgayDangKy = DateTime.Now;
        //    PDK.TrangThai = 0;
        //    context.PHIEUDANGKies.InsertOnSubmit(PDK);
        //    context.SubmitChanges();
        //    return RedirectToAction("ChiTiet" + "/" + Mon.MaMon);
        //}
    }
}