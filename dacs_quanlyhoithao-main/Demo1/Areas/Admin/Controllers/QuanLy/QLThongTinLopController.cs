using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Areas.Admin.Controllers.QuanLy
{
    public class QLThongTinLopController : Controller
    {
        // GET: Admin/QLThongTinLop
        MyDataDataContext context = new MyDataDataContext();
        public ActionResult DanhSachLop(int? page)
        {
            int pageNum = (page ?? 1);
            return View(context.LOPs.OrderBy(model => model.TenLop).ToList());
        }
        public ActionResult TaoLop()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TaoLop(FormCollection collection, LOP L)
        {
            var L_TenLop = collection["TenLop"];
            var L_SiSo = collection["SiSo"];
            var L_TrangThai = collection["TrangThai"];
            // var M_MaDieuLe = collection["MaDieuLe"];
            {
                L.TenLop = L_TenLop.ToString();
                L.SiSo = int.Parse(L_SiSo);
                L.NgayTao = DateTime.Now;
                L.TrangThai = int.Parse(L_TrangThai);
                context.LOPs.InsertOnSubmit(L);
                context.SubmitChanges();
            }

            return RedirectToAction("DanhSachLop");
        }
        public ActionResult SuaLop(int id)
        {
            var lop = context.LOPs.First(m => m.MaLop == id);
            return View(lop);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SuaLop(int ID, FormCollection collection)
        {
            var L = context.LOPs.First(m => m.MaLop == ID);

            var L_TenLop = collection["TenLop"];
            var L_SiSo = collection["SiSo"];
            var L_TrangThai = collection["TrangThai"];
            // var M_MaDieuLe = collection["MaDieuLe"];
            {
                L.TenLop = L_TenLop;
                L.SiSo = int.Parse(L_SiSo);
                L.NgayChinhSua = DateTime.Now;
                L.TrangThai = int.Parse(L_TrangThai);
                UpdateModel(L);
                context.SubmitChanges();
            };
            return RedirectToAction("DanhSachLop");
        }

    }
}