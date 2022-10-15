using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Areas.Admin.Controllers
{
    public class TaiKhoanAdminController : Controller
    {
        // GET: Admin/TaiKhoan
        MyDataDataContext db = new MyDataDataContext();
        public ActionResult Index(int? page)
        {
            int pageNum = (page ?? 1);
            return View(db.USERADMINs.OrderBy(model => model.MAQUYEN).ToList());
        }
        public ActionResult Edit(string id)
        {
            var user = db.USERADMINs.First(m => m.Account == id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var user = db.USERADMINs.First(m => m.Account == id);
            var E_Account = collection["ACCOUNT"];
            var E_Pass = collection["PASS"];
            var E_Repass = collection["MatKhauXacNhan"];
            var E_HoTen = collection["HOTEN"];
            var E_Sdt = collection["SDT"];
            var E_Gmail = collection["GMAIL"];
            var E_TrangThai = collection["TRANGTHAI"];
            var E_MaQuyen = collection["MAQUYEN"];
            user.Account = id;
            if (string.IsNullOrEmpty(E_Repass))
            {
                ViewData["NhapMKXN"] = "Bạn phải nhập mật khẩu xác nhận!";
            }
            else
            {
                if (!E_Pass.Equals(E_Repass))
                {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau!";
                }
                else
                {
                    user.Account = E_Account;
                    user.Pass = E_Pass;
                    user.HoTen = E_HoTen;
                    user.SDT = E_Sdt;
                    user.Mail = E_Gmail;
                    user.TrangThai = int.Parse(E_TrangThai);
                    user.MAQUYEN = int.Parse(E_MaQuyen);

                    UpdateModel(user);
                    db.SubmitChanges();
                }
            }
            return RedirectToAction("Index", "TaiKhoanAdmin");
        }

    }
}