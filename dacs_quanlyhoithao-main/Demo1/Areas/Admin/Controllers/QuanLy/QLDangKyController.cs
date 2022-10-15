using Demo1.Areas.Admin.Mail;
using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Areas.Admin.Controllers.QuanLy
{
    public class QLDangKyController : Controller
    {

        // GET: Admin/QLDangKy
        MyDataDataContext context = new MyDataDataContext();

        public string AccountLogin()
        {
            USERADMIN user = (USERADMIN)Session["TaiKhoanAdmin"];
            return (user.Account);
        }
        public ActionResult DangKyTaiKhoan(int? page)
        {
            int pageNum = (page ?? 1);
            return View(context.USERs.OrderBy(model => model.TrangThai).ToList());

        }
        public int Status(string id)
        {
            var user = context.USERs.FirstOrDefault(m => m.MaSo == id);
            user.TrangThai = 1;
            context.SubmitChanges();
            return (int)user.TrangThai;
        }
        [HttpPost]
        public JsonResult XacNhanDangKy(string id)
        {
            //var user = Status(id);
            //return Json(user);
            var user = context.USERs.FirstOrDefault(m => m.MaSo == id);
            user.TrangThai = 1;
            UpdateModel(user);
            context.SubmitChanges();
            return Json(new USER().TrangThai);
        }
        public ActionResult XoaDangKy(string id)
        {
            var user = context.USERs.FirstOrDefault(m => m.MaSo == id);
            context.USERs.DeleteOnSubmit(user);
            context.SubmitChanges();
            return RedirectToAction("DangKyHoiThao");
        }
        public ActionResult DanhSachDangKy(int? page)
        {
            int pageNum = (page ?? 1);
            return View(context.PHIEUDANGKies.Where(m => m.TrangThai == 0).OrderBy(m => m.MaPhieuDK).ToList());
        }
        public ActionResult CTDanhSachDangKy(int id)
        {
            var MaPDK = context.PHIEUDANGKies.FirstOrDefault(m => m.MaPhieuDK == id);
            var ChiTiet = context.CHITIETDOIs.Where(m => m.MaDoi == MaPDK.MaDoi).OrderBy(m => m.MaSo).ToList();
            return View(ChiTiet);
        }
        public ActionResult XacNhanDangKyHoiThao(int id)
        {
            var MaPDK = context.PHIEUDANGKies.FirstOrDefault(m => m.MaPhieuDK == id);
            return View(MaPDK);
        }
        [HttpPost]
        public ActionResult XacNhanDangKyHoiThao(FormCollection collection, CTPHIEUDK CTPDK, PHIEUDANGKY PDK, int id)
        {
            var MaPDK = context.PHIEUDANGKies.FirstOrDefault(m => m.MaPhieuDK == id);
            CTPDK.MaPhieuDK = MaPDK.MaPhieuDK;
            CTPDK.Account = AccountLogin();
            CTPDK.TongTien = 0;
            CTPDK.HinhThucThanhToan = 0;
            CTPDK.TrangThai = 1;
            MaPDK.TrangThai = 1;
            UpdateModel(PDK);
            context.CTPHIEUDKs.InsertOnSubmit(CTPDK);
            context.SubmitChanges();
            try
            {
                if (ModelState.IsValid)
                {
                    Gmail Gmail = new Gmail();
                    string emailsv = MaPDK.DOI.USER.Email;
                    string hotensv = MaPDK.DOI.USER.Ten;
                    string tendoi = MaPDK.DOI.TenDoi;
                    string mssv = MaPDK.DOI.USER.MaSo;
                    string tenmon = MaPDK.DOI.MONTHIDAU.TenMon;
                    var senderEmail = new MailAddress(Gmail.Email(), "Hội Thao IT HUTECH");
                    var receiverEmail = new MailAddress(emailsv, "Receiver");
                    var password = Gmail.PassEmail();
                    var sub = "Xác Nhận Đăng Ký Hội Thao Thành Công";
                    var body = "Xin Chào " + hotensv + ".\n" + "Chúc mừng bạn đã đăng ký thành công Hội Thao Khoa Công Nghệ Thông Tin HUTECH." +
                        "\nBạn vui lòng hoàn tất lệ phí qua: \n\t Ngân Hàng: Số Tài Khoản - Tên Tài Khoản \n\t MoMo: Số Điện Thoại - Tên" +
                        "\nVới nội Dung: " + mssv + "-" + hotensv + "-" + tendoi + "-" + tenmon + "(MSSV Đội Trưởng - Họ Tên Đội Trưởng - Tên Đội Đăng Ký - Môn Thi Đấu)" +
                        "\nHoặc đóng trực tiếp tại VPK E1.02.21 Để hoàn tất thủ tục đăng ký." +
                        "\nCảm ơn bạn.";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }

                }
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
            return RedirectToAction("DanhSachDangKy");

        }
        public ActionResult DanhSachLePhi(int? page)
        {
            int pageNum = (page ?? 1);
            return View(context.PHIEUDANGKies.Where(m => m.TrangThai == 1).OrderBy(m => m.MaPhieuDK).ToList());
        }
        public ActionResult LePhi(int id)
        {
            var MaPDK = context.PHIEUDANGKies.FirstOrDefault(m => m.MaPhieuDK == id);
            return View();
        }
        [HttpPost]
        public ActionResult LePhi(FormCollection collection, CTPHIEUDK CTPDK, PHIEUDANGKY PDK, int id)
        {
            var MaPDK = context.PHIEUDANGKies.FirstOrDefault(m => m.MaPhieuDK == id);
            var TongTien = collection["TongTien"];
            var HinhThucThanhToan = collection["HinhThucThanhToan"];

            CTPDK.MaPhieuDK = MaPDK.MaPhieuDK;
            CTPDK.Account = AccountLogin();
            CTPDK.TongTien = Convert.ToDecimal(TongTien);
            CTPDK.HinhThucThanhToan = int.Parse(HinhThucThanhToan);
            CTPDK.TrangThai = 2;
            CTPDK.NgayThanhToan = DateTime.Now;
            MaPDK.TrangThai = 2;
            UpdateModel(PDK);
            UpdateModel(CTPDK);
            context.SubmitChanges();
            try
            {
                if (ModelState.IsValid)
                {
                    Gmail Gmail = new Gmail();
                    var nguoixacnhan = context.USERADMINs.FirstOrDefault(m => m.Account == AccountLogin());
                    string emailsv = MaPDK.DOI.USER.Email;
                    string hotensv = MaPDK.DOI.USER.Ten;
                    string tendoi = MaPDK.DOI.TenDoi;
                    string mssv = MaPDK.DOI.USER.MaSo;
                    string tenmon = MaPDK.DOI.MONTHIDAU.TenMon;
                    var senderEmail = new MailAddress(Gmail.Email(), "Hội Thao IT HUTECH");
                    var receiverEmail = new MailAddress(emailsv, "Receiver");
                    var password = Gmail.PassEmail();
                    var sub = "Xác Nhận Đóng Lệ Phí Hội Thao IT HUTECH";
                    var body = "Xin Chào " + hotensv + ".\n" + "Bạn vừa đóng lệ phí Hội Thao Khoa Công Nghệ Thông Tin HUTECH thành công." +
                        "\n\tĐội Đăng Ký: " + tendoi +
                        "\n\tMôn Đăng Ký: " + tenmon +
                        "\n\tSố Tiền: " + TongTien + " VNĐ." +
                        "\n\tNgày Xác Nhận: " + DateTime.Now +
                        "\n\tNgười Xác Nhận: " + nguoixacnhan.HoTen +
                        "\nCảm ơn bạn.";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }

                }
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
            return RedirectToAction("DanhSachLePhi");
        }
    }
}