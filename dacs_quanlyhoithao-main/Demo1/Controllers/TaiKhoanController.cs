using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Controllers
{

    public class TaiKhoanController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: TaiKhoan
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendangnhap = collection["MaSo"];
            var matkhau = collection["Pass"];

            // Gán giá trị cho đối tượng được tạo mới(kh)
            USER user = data.USERs.SingleOrDefault(n => n.MaSo == tendangnhap && n.Pass == matkhau);
            // USER checkDK = data.USERs.SingleOrDefault(n => n.MaSo == tendangnhap && n.TrangThai == 0);
            var checkDK = data.USERs.Where(m => m.MaSo == tendangnhap && m.TrangThai == 0).FirstOrDefault();

            if (user != null)
            {
                if (checkDK == null)
                {
                    Session["TaiKhoanSV"] = user;
                    return RedirectToAction("TrangChu", "Home");
                }
                else
                {
                    ViewBag.Thongbao = "Tài khoản của bạn đang trong quá trình kiểm duyệt đăng ký \n Chúng tôi sẽ gửi Mail cho bạn khi hoàn tất !!!";
                }
            }

            else
            {
                ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }


            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.MaLop = new SelectList(data.LOPs.ToList().OrderBy(n => n.TenLop).Where(n => n.TrangThai == 1), "MaLop", "TenLop");
            return View();
        }


        [HttpPost]
        public ActionResult Register(FormCollection collection, USER user)
        {

            var checkUser = data.USERs.Where(m => m.MaSo == user.MaSo).FirstOrDefault();
            var checkMail = data.USERs.Where(m => m.Email == user.Email).FirstOrDefault();
            if (checkMail != null && checkUser != null)
            {
                ViewData["CheckUser"] = "Tài khoản hoặc Gmail này đã được sử dụng vui lòng đăng ký lại";
                return this.Register();
            }
            else
            {
                ViewBag.MaLop = new SelectList(data.LOPs.ToList().OrderBy(n => n.TenLop).Where(n => n.TrangThai == 1), "MaLop", "TenLop");
                var MaSo = collection["MaSo"];
                var pass = collection["Pass"];
                var TenSV = collection["TEN"];
                var NgaySinh = string.Format("{0:dd/MM/yyyy}", collection["NgaySinh"]);
                var GioiTinh = collection["GIOITINH"];
                var sdt = collection["SDT"];
                var Email = collection["EMAIL"];
                var Quyen = collection["Quyen"];
                var TrangThai = collection["TrangThai"];
                //   var Lop = collection["MaLop"];


                //Gán giá trị cho đổi tượng được tạo mới (kh)
                user.MaSo = MaSo;
                user.Pass = pass;
                user.Ten = TenSV;
                user.NgaySinh = DateTime.Parse(NgaySinh);
                user.GioiTinh = int.Parse(GioiTinh);
                user.SDT = sdt;
                user.Email = Email;
                user.Quyen = 0;
                user.TrangThai = 0;
                //    user.MaLop = int.Parse(Lop);
                data.USERs.InsertOnSubmit(user);
                data.SubmitChanges();

            }
            return RedirectToAction("Login", "TaiKhoan");
        }
        public ActionResult LogOut()
        {
            Session["TaiKhoanSV"] = null;
            return RedirectToAction("TrangChu", "Home");
        }

        public ActionResult EditUser()
        {
            return View();
        }

    }
}