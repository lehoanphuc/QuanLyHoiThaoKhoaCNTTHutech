using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Areas.Admin.Controllers
{
    public class BoMonThiDauAdminController : Controller
    {
        MyDataDataContext db = new MyDataDataContext();
        // GET: Admin/BoMonThiDau
        [HttpGet]
        public ActionResult BongDaAdmin()
        {
            ViewBag.MADOI = new SelectList(db.DOIs.ToList().OrderBy(n => n.MaDoi), "MADOI", "TENDOI");
            ViewBag.MABANGDAU = new SelectList(db.BANGDAUs.ToList().OrderBy(n => n.MaBangDau), "MABANGDAU", "TENBANGDAU");
            return View();
        }
        //[HttpPost]
        //public ActionResult BongDaAdmin(FormCollection collection, CHITIETBANGDAU ctBangDau)
        //{
        //    ViewBag.MADOI = new SelectList(db.DOIs.ToList().OrderBy(n => n.MaDoi), "MADOI", "TENDOI");
        //    ViewBag.MABANGDAU = new SelectList(db.BANGDAUs.ToList().OrderBy(n => n.MaBangDau), "MABANGDAU", "TENBANGDAU");

        //    var check = db.CHITIETBANGDAUs.Where(m => m.MaDoi == ctBangDau.MaDoi).FirstOrDefault();
        //    var soluongdoi = db.CHITIETBANGDAUs.Count(m => m.MaBangDau == ctBangDau.MaBangDau);
        //    if (check != null)
        //    {
        //        ViewData["CheckDoi"] = "Đội này đã được xếp vào Bảng đấu khác";
        //        return this.BongDaAdmin();
        //    }
        //    else if (soluongdoi > 4)
        //    {
        //        ViewData["SoLuongDoi"] = "Bảng đấu này đã đủ 4 đội rồi";
        //        return this.BongDaAdmin();
        //    }

        //    else
        //    {
        //        var maDoi = collection["MADOI"];
        //        var bangDau = collection["MABANGDAU"];

        //        //Gán giá trị cho đổi tượng được tạo mới (kh)
        //        ctBangDau.MaDoi = int.Parse(maDoi);
        //        ctBangDau.MaBangDau = int.Parse(bangDau);

        //        db.CHITIETBANGDAUs.InsertOnSubmit(ctBangDau);
        //        db.SubmitChanges();
        //    }
        //    return RedirectToAction("BongDaAdmin", "BoMonThiDauAdmin");
        //}
        public ActionResult BongChuyenAdmin()
        {
            return View();
        }
        public ActionResult CauLongAdmin()
        {
            return View();
        }
        public ActionResult KeoCoAdmin()
        {
            return View();
        }
        public ActionResult CoVua_CoTuongAdmin()
        {
            return View();
        }
        public ActionResult DienKinhAdmin()
        {
            return View();
        }
    }
}