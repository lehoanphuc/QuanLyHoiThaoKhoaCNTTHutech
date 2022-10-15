using Demo1.Areas.Admin.Common;
using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {
        // GET: Admin/LoginAdmin
        MyDataDataContext db = new MyDataDataContext();
        //test up
        [HttpGet]
        public ActionResult Login()
        {
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                ViewBag.username = Request.Cookies["username"].Value;
                ViewBag.password = Request.Cookies["password"].Value;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection, string username, string password)
        {
            if (Request.Cookies["username"] != null && Request.Cookies["username"] != null)
            {
                username = Request.Cookies["username"].Value;
                password = Request.Cookies["password"].Value;
            }

            if (checkpassword(username, password))
            {
                var tendn = new UserLogin();
                tendn.UserName = username;
                var listQUYEN = GetListQuyen(username);
                Session.Add("TENQUYEN", listQUYEN);
                Session.Add("TENDANGNHAP", tendn);
                return RedirectToAction("Index", "HomeAdmin");
            }
            //return RedirectToAction("Index", "HomeAdmin");
            return this.Login();
        }
        public List<string> GetListQuyen(string userName)
        {
            // var user = db.User.Single(x => x.UserName == userName);

            var data = (from a in db.USERADMINs
                        where a.Account == userName
                        select new
                        {
                            MaQuyen = a.MAQUYEN,
                            TenQuyen = a.QUYEN.TENQUYEN
                        });
            return data.Select(x => x.TenQuyen.ToString()).ToList();
        }

        public bool checkpassword(string username, string password)
        {
            if (db.USERADMINs.Where(x => x.Account == username && x.Pass == password).Count() > 0)
                return true;
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                return false;
            }
        }
        public ActionResult Logout()
        {
            Session["TaiKhoanAdmin"] = null;
            return RedirectToAction("Login", "LoginAdmin");
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.MAQUYEN = new SelectList(db.QUYENs.ToList().OrderBy(n => n.TENQUYEN), "MAQUYEN", "TENQUYEN");
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection collection, USERADMIN nd, LOP lop, QUYEN quyen)
        {
            ViewBag.MAQUYEN = new SelectList(db.QUYENs.ToList().OrderBy(n => n.TENQUYEN), "MAQUYEN", "TENQUYEN");
            var check = db.USERADMINs.Where(m => m.Account == nd.Account).FirstOrDefault();
            if (check != null)
            {
                ViewData["CheckMail"] = "Tài Khoản Này Đã Được Sử Dụng";
                return this.Register();
            }
            else
            {
                var Account = collection["MSSV"];
                var TenSV = collection["HOVATEN"];
                var sdt = collection["SDT"];
                var Gmail = collection["GMAIL"];
                var NgayTao = collection["NGAYTAO"];
                var Quyen = collection["QUYEN"];

                //Gán giá trị cho đổi tượng được tạo mới (kh)
                nd.Account = Account;
                nd.Pass = "123";
                nd.HoTen = TenSV;
                nd.SDT = sdt;
                nd.TrangThai = 1;
                nd.NgayTao = DateTime.Parse(NgayTao);
                nd.Mail = Gmail;
                quyen.TENQUYEN = Quyen;

                db.USERADMINs.InsertOnSubmit(nd);
                db.SubmitChanges();

            }
            return RedirectToAction("Index", "TaiKhoanAdmin");
        }
    }
}