
using Demo1.Models;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Controllers
{
    public class QuanLyController : Controller
    {
        // GET: QuanLy
        MyDataDataContext context = new MyDataDataContext();
        private string UserLogin()
        {
            USER USER = (USER)Session["TaiKhoanSV"];
            var user = USER.MaSo;
            return (user);
        }

        private bool CheckLogin()
        {
            USER user = (USER)Session["TaiKhoanSV"];
            if (user != null)
                return true;
            else
                return false;
        }
        private string CodeRandom()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
        public ActionResult QuanLyDoi(int? page)
        {
            if (CheckLogin() == true)
            {
                int pageNum = (page ?? 1);
                return View(context.CHITIETDOIs.OrderBy(model => model.MaDoi).ToList());
            }
            else
                return RedirectToAction("Login", "TaiKhoan");
        }
        public ActionResult TaoDoi()
        {
            ViewBag.MaMon = new SelectList(context.MONTHIDAUs.ToList().OrderBy(n => n.MaMon).Where(n => n.TrangThai == 1), "MaMon", "TenMon");
            return View();
        }

        [HttpPost, ValidateInput(false)]

        public ActionResult TaoDoi(FormCollection collection, DOI D, CHITIETDOI CTD)
        {

            USER USER = (USER)Session["TaiKhoanSV"];
            ViewBag.MaMon = new SelectList(context.MONTHIDAUs.ToList().OrderBy(n => n.MaMon).Where(n => n.TrangThai == 1), "MaMon", "TenMon");
            var D_MaThamGia = collection["MaThamGia"];
            var D_TenDoi = collection["TenDoi"];
            var D_SoLuong = collection["SoLuong"];
            var D_TrangThai = collection["TrangThai"];

            {

                D.MaThamGia = CodeRandom();
                D.TenDoi = D_TenDoi.ToString();
                D.SoLuong = 1;
                D.NgayTao = DateTime.Now;
                D.TrangThai = 1;
                D.MaSo = USER.MaSo;
                context.DOIs.InsertOnSubmit(D);
                context.SubmitChanges();
            }
            int ID = context.DOIs.Max(m => m.MaDoi);
            //if (ID == 0)
            //{
            //    CTD.MaDoi = ID + 1;
            //    CTD.MaSo = USER.MaSo;
            //    CTD.TrangThai = 1;
            //    context.CHITIETDOIs.InsertOnSubmit(CTD);
            //    context.SubmitChanges();
            //}
            //else
            {
                CTD.MaDoi = ID;
                CTD.MaSo = USER.MaSo;
                CTD.TrangThai = 1;
                context.CHITIETDOIs.InsertOnSubmit(CTD);
                context.SubmitChanges();
            }
            return RedirectToAction("QuanLyDoi");
        }
        public ActionResult ChiTietDoi(int ID)
        {

            //CHITIETDOI CTD = context.CHITIETDOIs.First(m => m.MaDoi == ID);
            return View(context.CHITIETDOIs.OrderBy(model => model.MaDoi).Where(d => d.MaDoi == ID).ToList());
        }
        public ActionResult XoaThanhVien(int id)
        {

            var user = context.CHITIETDOIs.FirstOrDefault(m => m.ID == id);
            context.CHITIETDOIs.DeleteOnSubmit(user);
            context.SubmitChanges();
            return RedirectToAction("ChiTietDoi");
        }
        public ActionResult ThamGiaDoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThamGiaDoi(FormCollection collection, CHITIETDOI CTD)
        {
            var mathamgia = collection["MaThamGia"];
            DOI D = context.DOIs.FirstOrDefault(d => d.MaThamGia == mathamgia);

            if (D != null)
            {
                CHITIETDOI maso = context.CHITIETDOIs.SingleOrDefault(CT => CT.MaSo == UserLogin() && CT.MaDoi == D.MaDoi);
                if (maso == null)
                {

                    CTD.MaDoi = D.MaDoi;
                    CTD.MaSo = UserLogin();
                    CTD.TrangThai = 1;
                    CTD.NgayThamGia = DateTime.Now;
                    context.CHITIETDOIs.InsertOnSubmit(CTD);
                    context.SubmitChanges();
                    {
                        var SL = context.DOIs.Where(m => m.MaDoi == D.MaDoi).Max(sl => sl.SoLuong);
                        DOI doi = context.DOIs.FirstOrDefault(m => m.MaDoi == D.MaDoi);
                        doi.MaDoi = D.MaDoi;
                        //doi.MaSo = D.MaSo;
                        //doi.MaMon = D.MaMon;
                        doi.SoLuong = SL + 1;
                        UpdateModel(doi);
                        context.SubmitChanges();
                    }
                    return RedirectToAction("QuanLyDoi");
                }
                else
                {
                    ViewBag.Thongbao = "Bạn đã tham gia đội này rồi";
                }

            }
            else
            {
                ViewBag.Thongbao = "Mã bạn nhập không đúng";
            }
            return View();
        }

        public ActionResult DanhSachDangKy(int? page)
        {
            if (CheckLogin() == true)
            {
                int pageNum = (page ?? 1);
                return View(context.PHIEUDANGKies.Where(m => m.DOI.MaSo == UserLogin()).OrderBy(model => model.MaPhieuDK).ToList());
            }
            else
                return RedirectToAction("Login", "TaiKhoan");
        }
        public ActionResult DangKyHoiThao()
        {
            ViewBag.MaDoi = new SelectList(context.DOIs.Where(n => n.MaSo == UserLogin()).OrderBy(n => n.MaDoi).ToList(), "MaDoi", "TenDoi");
            return View();
        }

        [HttpPost]
        public ActionResult DangKyHoiThao(FormCollection collection, PHIEUDANGKY PDK)
        {
            ViewBag.MaDoi = new SelectList(context.DOIs.Where(n => n.MaSo == UserLogin()).OrderBy(n => n.MaDoi).ToList(), "MaDoi", "TenDoi");
            PDK.NgayDangKy = DateTime.Now;
            PDK.TrangThai = 0;
            context.PHIEUDANGKies.InsertOnSubmit(PDK);
            context.SubmitChanges();
            return RedirectToAction("DanhSachDangKy");
        }

    }

}