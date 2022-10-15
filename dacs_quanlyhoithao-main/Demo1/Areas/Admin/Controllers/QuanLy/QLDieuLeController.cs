using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Areas.Admin.Controllers.QuanLy
{
    public class QLDieuLeController : Controller
    {
        // GET: Admin/QLDieuLe
        MyDataDataContext context = new MyDataDataContext();
        public ActionResult ListDieuLe(int? page)
        {
            int pageNum = (page ?? 1);
            return View(context.DIEULEs.OrderBy(model => model.MaDieuLe).ToList());
        }
        public ActionResult TaoDieuLe()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]

        public ActionResult TaoDieuLe(FormCollection collection, DIEULE DL)
        {
            var DL_TenDieule = collection["TenDieuLe"];
            var DL_DuongDan = collection["DuongDan"];
            var DL_TomTat = collection["TomTat"];
            var DL_NoiDung = collection["NoiDung"];

            {
                DL.TenDieuLe = DL_TenDieule.ToString();
                DL.DuongDan = DL_DuongDan.ToString();
                DL.TomTat = DL_TomTat.ToString();
                DL.NoiDung = DL_NoiDung.ToString();
                DL.NgayTao = DateTime.Now;
                DL.TrangThai = 1;
                context.DIEULEs.InsertOnSubmit(DL);
                context.SubmitChanges();
            }
            return RedirectToAction("ListDieuLe");
        }
        public ActionResult SuaDieuLe(int Id)
        {
            var DL = context.DIEULEs.First(m => m.MaDieuLe == Id);
            return View(DL);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SuaDieuLe(int Id, FormCollection collection)
        {
            var DL = context.DIEULEs.First(dl => dl.MaDieuLe == Id);
            var DL_TenDieule = collection["TenDieuLe"];
            var DL_DuongDan = collection["DuongDan"];
            var DL_TomTat = collection["TomTat"];
            var DL_NoiDung = collection["NoiDung"];
            var DL_TrangThai = collection["TrangThai"];
            DL.MaDieuLe = Id;
            {
                DL.TenDieuLe = DL_TenDieule;
                DL.DuongDan = DL_DuongDan;
                DL.TomTat = DL_TomTat;
                DL.NoiDung = DL_NoiDung;
                DL.NgayChinhSua = DateTime.Now;
                DL.TrangThai = int.Parse(DL_TrangThai);
                UpdateModel(DL);
                context.SubmitChanges();
            }
            return RedirectToAction("ListDieuLe");
        }

    }
}