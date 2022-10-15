using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Areas.Admin.Controllers.QuanLy
{
    public class QLMonThiDauController : Controller
    {
        // GET: Admin/QLMonThiDau
        MyDataDataContext context = new MyDataDataContext();
        public ActionResult DanhSachMonThiDau(int? page)
        {
            int pageNum = (page ?? 1);
            return View(context.MONTHIDAUs.OrderBy(model => model.MaMon).ToList());
        }
        public ActionResult TaoMonThiDau()
        {
            ViewBag.MaDieuLe = new SelectList(context.DIEULEs.ToList().OrderBy(n => n.MaDieuLe).Where(n => n.TrangThai == 1), "MaDieuLe", "TenDieuLe");
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TaoMonThiDau(FormCollection collection, MONTHIDAU MTD)
        {
            ViewBag.MaDieuLe = new SelectList(context.DIEULEs.ToList().OrderBy(n => n.MaDieuLe).Where(n => n.TrangThai == 1), "MaDieuLe", "TenDieuLe");
            var M_TenMon = collection["TenMon"];
            var M_TheLoai = collection["TheLoai"];
            var M_SoLuongDoi = collection["SoLuongDoi"];
            var M_SoLuongThanhVien = collection["SoLuongThanhVien"];
            var M_LePhi = collection["LePhi"];
            var M_NgayTao = string.Format("{0:dd/MM/yyyy}", collection["NgayTao"]);
            var M_GhiChu = collection["GhiChu"];
            var ngaybatdau = string.Format("{0:dd/MM/yyyy}", collection["NgayBatDau"]);
            var ngayketthuc = string.Format("{0:dd/MM/yyyy}", collection["NgayKetThuc"]);
            var noidung = collection["NoiDung"];
            var M_TrangThai = collection["TrangThai"];
            // var M_MaDieuLe = collection["MaDieuLe"];
            {
                MTD.TenMon = M_TenMon.ToString();
                MTD.TheLoai = int.Parse(M_TheLoai);
                MTD.SoLuongDoi = int.Parse(M_SoLuongDoi);
                MTD.SoLuongThanhVien = int.Parse(M_SoLuongThanhVien);
                MTD.LePhi = Convert.ToDecimal(M_LePhi);
                MTD.NgayTao = DateTime.Now;
                MTD.NgayBatDau = DateTime.Parse(ngaybatdau);
                MTD.NgayKetThuc = DateTime.Parse(ngayketthuc);
                MTD.GhiChu = MTD.GhiChu.ToString();
                MTD.NoiDung = noidung.ToString();
                MTD.TrangThai = int.Parse(M_TrangThai);
                context.MONTHIDAUs.InsertOnSubmit(MTD);
                context.SubmitChanges();
            }

            return RedirectToAction("DanhSachMonThiDau");
        }
        public ActionResult SuaMonThidau(int ID, FormCollection collection)
        {
            var MTD = context.MONTHIDAUs.First(m => m.MaMon == ID);
            ViewBag.MaDieuLe = new SelectList(context.DIEULEs.ToList().OrderBy(n => n.MaDieuLe).Where(n => n.TrangThai == 1), "MaDieuLe", "TenDieuLe");
            return View(MTD);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SuaMonThiDau(int ID, FormCollection collection)
        {
            var MTD = context.MONTHIDAUs.First(m => m.MaMon == ID);
            ViewBag.MaDieuLe = new SelectList(context.DIEULEs.ToList().OrderBy(n => n.MaDieuLe).Where(n => n.TrangThai == 1), "MaDieuLe", "TenDieuLe");

            var M_TenMon = collection["TenMon"];
            var M_TheLoai = collection["TheLoai"];
            var M_SoLuongDoi = collection["SoLuongDoi"];
            var M_SoLuongThanhVien = collection["SoLuongThanhVien"];
            var M_LePhi = Convert.ToDecimal(collection["LePhi"]);
            var M_NgayChinhSua = string.Format("{0:dd/MM/yyyy}", collection["NgayTao"]);
            var M_GhiChu = collection["GhiChu"];
            var ngaybatdau = string.Format("{0:dd/MM/yyyy}", collection["NgayBatDau"]);
            var ngayketthuc = string.Format("{0:dd/MM/yyyy}", collection["NgayKetThuc"]);
            var noidung = collection["NoiDung"];
            var M_TrangThai = collection["TrangThai"];
            {
                MTD.MaMon = ID;
                MTD.TenMon = M_TenMon.ToString();
                MTD.TheLoai = int.Parse(M_TheLoai);
                MTD.SoLuongDoi = int.Parse(M_SoLuongDoi);
                MTD.SoLuongThanhVien = int.Parse(M_SoLuongThanhVien);
                MTD.LePhi = M_LePhi;
                MTD.NgayBatDau = DateTime.Parse(ngaybatdau);
                MTD.NgayKetThuc = DateTime.Parse(ngayketthuc);
                MTD.NgayChinhSua = DateTime.Now;
                MTD.GhiChu = MTD.GhiChu.ToString();
                MTD.NoiDung = noidung.ToString();
                MTD.TrangThai = int.Parse(M_TrangThai);
                UpdateModel(MTD);
                context.SubmitChanges();
            }

            return RedirectToAction("DanhSachMonThiDau");
        }
        public ActionResult HuongDanDK()
        {
            return View(context.MONTHIDAUs.Where(m => m.TrangThai == 1).OrderBy(m => m.MaMon).ToList());
        }
        public ActionResult SuaHuongDan(int id)
        {
            var mon = context.MONTHIDAUs.FirstOrDefault(m => m.MaMon == id);
            return View(mon);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SuaHuongDan(int id, FormCollection collection)
        {
            var mon = context.MONTHIDAUs.FirstOrDefault(m => m.MaMon == id);
            var HuongDan = collection["HuongDanDK"];
            mon.HuongDanDK = HuongDan;
            UpdateModel(mon);
            context.SubmitChanges();
            return RedirectToAction("HuongDanDk");
        }

    }
}