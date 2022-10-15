using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Areas.Admin.Controllers
{
    public class LichThiDauController : Controller
    {
        MyDataDataContext db = new MyDataDataContext();
        // GET: Admin/LichThiDau
        [HttpGet]
        public ActionResult TaoBangDau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaoBangDau(FormCollection collection, BANGDAU bangdau)
        {
            var check = db.BANGDAUs.Where(m => m.MaBangDau == bangdau.MaBangDau).FirstOrDefault();
            if (check != null)
            {
                ViewData["CheckBangDau"] = "Bảng Đấu Này Đã tồn tại";
                return this.TaoBangDau();
            }
            else
            {
                var tenBangDau = collection["TENBANGDAU"];

                //Gán giá trị cho đổi tượng được tạo mới (kh)
                bangdau.TenBangDau = tenBangDau;

                //db.BANGDAUs.InsertOnSubmit(bangdau);
                db.BANGDAUs.InsertOnSubmit(bangdau);
                db.SubmitChanges();
            }
            return this.TaoBangDau();
        }
        //public ActionResult DeleteBangDau(int id)
        //{
        //    var DeleteBangDau = db.BANGDAUs.First(m => m.MaBangDau == id);
        //    return View(DeleteBangDau);
        //}
        //[HttpPost]
        //public ActionResult DeleteBangDau(int id, FormCollection collection)
        //{
        //    var DeleteBangDau = db.BANGDAUs.Where(m => m.MaBangDau == id).First();
        //    db.BANGDAUs.DeleteOnSubmit(DeleteBangDau);
        //    db.SubmitChanges();
        //    return RedirectToAction("ChiTietBangDau", "LichThiDau");
        //}
        public ActionResult Edit(int id)
        {
            var E_sach = db.CHITIETBANGDAUs.First(m => m.ID == id);
            return View(E_sach);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, CHITIETBANGDAU ctBangDau)
        {
            var EditBangDau = db.CHITIETBANGDAUs.First(m => m.ID == id);
            var EditTran = collection["TRAN"];
            var EditThang = collection["THANG"];
            var EditHoa = collection["HOA"];
            var EditThua = collection["THUA"];
            var EditBanThang = collection["BANTHANG"];
            var EditBanThua = collection["BANTHUA"];
            var EditHieuSo = collection["HIEUSO"];
            var EditDiem = collection["DIEM"];
            var EditLoi = collection["LOI"];

            //Gán giá trị mới
            if (string.IsNullOrEmpty(EditTran) || string.IsNullOrEmpty(EditThang) || string.IsNullOrEmpty(EditHoa) || string.IsNullOrEmpty(EditThua) || string.IsNullOrEmpty(EditBanThang)
                || string.IsNullOrEmpty(EditBanThua) || string.IsNullOrEmpty(EditHieuSo) || string.IsNullOrEmpty(EditDiem) || string.IsNullOrEmpty(EditLoi))
            {
                ViewData["CheckEmpty"] = "Không được để trống!";
                return this.Edit(id);
            }
            else if (int.Parse(EditTran) < 0 || int.Parse(EditThang) < 0 || int.Parse(EditHoa) < 0 || int.Parse(EditThua) < 0 || int.Parse(EditBanThang) < 0 || int.Parse(EditBanThua) < 0 
                || int.Parse(EditLoi) < 0 || int.Parse(EditDiem) < 0)
            {
                ViewData["Check<0"] = "Các giá trị bạn nhập ko được bé hơn 0";
                return this.Edit(id);
            }
            else
            {
                EditBangDau.ID = id;
                EditBangDau.Tran = int.Parse(EditTran);
                EditBangDau.Thang = int.Parse(EditThang);
                EditBangDau.Hoa = int.Parse(EditHoa);
                EditBangDau.Thua = int.Parse(EditThua);
                EditBangDau.BanThang = int.Parse(EditBanThang);
                EditBangDau.BanThua = int.Parse(EditBanThua);
                EditBangDau.HieuSo = int.Parse(EditHieuSo);
                EditBangDau.Diem = int.Parse(EditDiem);
                EditBangDau.Loi = int.Parse(EditLoi);
            }
                

            UpdateModel(EditBangDau);
            db.SubmitChanges();

            return RedirectToAction("ChiTietBangDau", "LichThiDau");

        }


        public ActionResult BangDau()
        {
            ViewBag.MABANGDAU = new SelectList(db.BANGDAUs.ToList().OrderBy(n => n.MaBangDau), "MABANGDAU", "TENBANGDAU");
            ViewBag.MADOI = new SelectList(db.DOIs.Where(m => m.TrangThai == 1).ToList(), "MADOI", "TENDOI");
            return View();
        }
        [HttpPost]
        public ActionResult BangDau(FormCollection collection, CHITIETBANGDAU ctBangDau, DOI doi)
        {
            ViewBag.MABANGDAU = new SelectList(db.BANGDAUs.ToList().OrderBy(n => n.MaBangDau), "MABANGDAU", "TENBANGDAU");
            ViewBag.MADOI = new SelectList(db.DOIs.Where(m => m.TrangThai == 1).ToList(), "MADOI", "TENDOI");

            var check = db.CHITIETBANGDAUs.Where(m => m.MaDoi == ctBangDau.MaDoi).FirstOrDefault();
            var soluongdoi = db.CHITIETBANGDAUs.Count(m => m.MaBangDau == ctBangDau.MaBangDau);
            var idBangDau = db.CHITIETBANGDAUs.Count(m => m.ID == ctBangDau.ID);

            var maDoi = collection["MADOI"];
            var bangDau = collection["MABANGDAU"];

            if (check != null)
            {
                ViewData["CheckDoi"] = "Đội này đã được xếp vào Bảng đấu khác";
                return this.BangDau();
            }
            if (soluongdoi > 4)
            {
                ViewData["SoLuongDoi"] = "Bảng đấu này đã đủ 5 đội rồi, vui lòng chọn bảng đấu khác";
                return this.BangDau();
            }
            else
            {
                //Gán giá trị cho đổi tượng được tạo mới (kh)
                if (idBangDau == 0)
                {
                    ctBangDau.ID = 0;
                    ctBangDau.MaDoi = int.Parse(maDoi);
                    ctBangDau.MaBangDau = int.Parse(bangDau);
                    ctBangDau.Tran = 0;
                    ctBangDau.Thang = 0;
                    ctBangDau.Hoa = 0;
                    ctBangDau.Thua = 0;
                    ctBangDau.BanThang = 0;
                    ctBangDau.BanThua = 0;
                    ctBangDau.HieuSo = 0;
                    ctBangDau.Diem = 0;
                    ctBangDau.Loi = 0;
                    db.CHITIETBANGDAUs.InsertOnSubmit(ctBangDau);
                    db.SubmitChanges();
                }
                else
                {
                    var id = db.CHITIETBANGDAUs.Max(m => m.ID).ToString();
                    ctBangDau.ID = int.Parse(id) + 1;
                    ctBangDau.MaDoi = int.Parse(maDoi);
                    ctBangDau.Tran = 0;
                    ctBangDau.Thang = 0;
                    ctBangDau.Hoa = 0;
                    ctBangDau.Thua = 0;
                    ctBangDau.BanThang = 0;
                    ctBangDau.BanThua = 0;
                    ctBangDau.HieuSo = 0;
                    ctBangDau.Diem = 0;
                    ctBangDau.Loi = 0;
                    ctBangDau.MaBangDau = int.Parse(bangDau);
                    db.CHITIETBANGDAUs.InsertOnSubmit(ctBangDau);
                    db.SubmitChanges();
                }
            }
            ViewData["ThemBangDauThanhCong"] = "Bạn đã thêm thành công bảng đấu";
            return this.BangDau();
        }
        public ActionResult ChiTietBangDau()
        {
            var CTBangDau = db.CHITIETBANGDAUs.OrderBy(model => model.MaBangDau).ToList();
            return View(CTBangDau);
        }

        public ActionResult TaoSanDau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaoSanDau(FormCollection collection, SANDAU san)
        {

            //var id = db.CHITIETBANGDAUs.Max(m => m.ID).ToString();

            var tenSanDau = collection["TENSANDAU"];

            //Gán giá trị cho đổi tượng được tạo mới (kh)

            //ctBangDau.ID = int.Parse(id) + 1;
            var check = db.SANDAUs.Where(m => m.TenSanDau == san.TenSanDau).FirstOrDefault();
            var maSan = db.SANDAUs.OrderBy(m => m.MaSanDau);
            if (check != null)
            {
                ViewData["CheckSanDau"] = "Sân đấu này đã tồn tại!";
                return this.TaoSanDau();
            }
            else
            {
                if (maSan == null)
                {
                    san.MaSanDau = 0;
                    san.TenSanDau = tenSanDau;

                    db.SANDAUs.InsertOnSubmit(san);
                    db.SubmitChanges();
                }
                else
                {
                    var id = db.SANDAUs.Max(m => m.MaSanDau).ToString();
                    san.MaSanDau = int.Parse(id) + 1;
                    san.TenSanDau = tenSanDau;

                    db.SANDAUs.InsertOnSubmit(san);
                    db.SubmitChanges();
                }

            }

            ViewData["ThemBangDauThanhCong"] = "Bạn đã thêm thành công bảng đấu";
            return this.TaoSanDau();
        }
        public ActionResult TaoLichThiDau()
        {
            ViewBag.MASANDAU = new SelectList(db.SANDAUs.ToList().OrderBy(n => n.MaSanDau), "MASANDAU", "TENSANDAU");

            return View();
        }
        [HttpPost]
        public ActionResult TaoLichThiDau(FormCollection collection, CHITIETLICHTHIDAU ctLich, LICHTHIDAU Lich)
        {
            ViewBag.MASANDAU = new SelectList(db.SANDAUs.ToList().OrderBy(n => n.MaSanDau), "MASANDAU", "TENSANDAU");

            var ngayThiDau = collection["NGAYTHIDAU"];
            var sanDau = collection["MASANDAU"];

            //Gán giá trị cho đổi tượng được tạo mới (kh)
            var check = db.LICHTHIDAUs.Where(m => m.NgayThiDau == Lich.NgayThiDau && m.MaSanDau == Lich.MaSanDau).FirstOrDefault();
            if (check != null)
            {
                ViewData["CheckLich"] = "Lịch thi đấu này đã tồn tại";
                return this.TaoLichThiDau();
            }
            else
            {
                Lich.MaSanDau = int.Parse(sanDau);
                Lich.NgayThiDau = DateTime.Parse(ngayThiDau);

                db.LICHTHIDAUs.InsertOnSubmit(Lich);
                db.SubmitChanges();
            }
            ViewData["ThemBangDauThanhCong"] = "Bạn đã thêm thành công lịch thi đấu";
            return this.TaoLichThiDau();
        }

        public ActionResult XepLichThiDau()
        {
            ViewBag.MALICHTHIDAU = new SelectList(db.LICHTHIDAUs.ToList().OrderBy(n => n.MaLichThiDau), "MALICHTHIDAU", "NGAYTHIDAU");
            ViewBag.MADOI = new SelectList(db.DOIs.Where(m => m.TrangThai == 1).ToList(), "MADOI", "TENDOI");
            ViewBag.MASANDAU = new SelectList(db.SANDAUs.ToList().OrderBy(n => n.MaSanDau), "MASANDAU", "TENSANDAU");

            return View();
        }
        [HttpPost]
        public ActionResult XepLichThiDau(FormCollection collection, CHITIETLICHTHIDAU ctLich, DOI doi)
        {
            ViewBag.MALICHTHIDAU = new SelectList(db.LICHTHIDAUs.ToList().OrderBy(n => n.MaLichThiDau), "MALICHTHIDAU", "NGAYTHIDAU");
            ViewBag.MASANDAU = new SelectList(db.SANDAUs.ToList().OrderBy(n => n.MaSanDau), "MASANDAU", "TENSANDAU");
            ViewBag.MADOI = new SelectList(db.DOIs.Where(m => m.TrangThai == 1).ToList(), "MADOI", "TENDOI");

            var soluongdoi = db.CHITIETLICHTHIDAUs.Count(m => m.MaLichThiDau == ctLich.MaLichThiDau);
            var soluongdoi1 = db.CHITIETLICHTHIDAUs.Count(m => m.MaSanDau == ctLich.MaSanDau);
            var soluongID = db.CHITIETLICHTHIDAUs.Count(m => m.ID == ctLich.ID);
            var check = db.CHITIETLICHTHIDAUs.Where(m => m.MaDoi == ctLich.MaDoi && m.MaLichThiDau == ctLich.MaLichThiDau).FirstOrDefault();
            var check1 = db.CHITIETLICHTHIDAUs.Where(m => m.MaDoi == ctLich.MaDoi && m.MaSanDau == ctLich.MaSanDau).FirstOrDefault();
            if (soluongdoi > 1 && soluongdoi1 > 1)
            {
                ViewData["SoLuongDoi"] = "Lịch đấu này đã đủ 2 đội/trận rồi, vui lòng chọn lịch thi đấu khác";
                return this.XepLichThiDau();
            }
            if (check != null || check1 != null)
            {
                ViewData["CheckTrungDoi"] = "Đội này đã tồn tại hoặc bị trùng lịch!";
                return this.XepLichThiDau();
            }
            else
            {
                var maDoi = collection["MADOI"];
                var ngayThiDau = collection["MALICHTHIDAU"];
                var sanDau = collection["MASANDAU"];

                //Gán giá trị cho đổi tượng được tạo mới 
                if (soluongID == 0)
                {
                    ctLich.ID = 0;
                    ctLich.MaDoi = int.Parse(maDoi.First().ToString());
                    ctLich.MaLichThiDau = int.Parse(ngayThiDau);
                    ctLich.MaSanDau = int.Parse(sanDau);
                    db.CHITIETLICHTHIDAUs.InsertOnSubmit(ctLich);
                    db.SubmitChanges();
                }
                else
                {
                    var id = db.CHITIETLICHTHIDAUs.Max(m => m.ID);
                    ctLich.ID = id + 1;
                    ctLich.MaDoi = int.Parse(maDoi.First().ToString());
                    ctLich.MaLichThiDau = int.Parse(ngayThiDau);
                    ctLich.MaSanDau = int.Parse(sanDau);
                    db.CHITIETLICHTHIDAUs.InsertOnSubmit(ctLich);
                    db.SubmitChanges();
                }
            }
            ViewData["ThemBangDauThanhCong"] = "Bạn đã xếp lịch thành công cho đội này";
            return this.XepLichThiDau();
        }
        public ActionResult TaoLichBongDa()
        {
            return View(db.CHITIETLICHTHIDAUs.OrderBy(model => model.MaLichThiDau).ToList());
        }

        public ActionResult LichBongDa()
        {
            return View(db.CHITIETLICHTHIDAUs.OrderBy(m => m.ID).ToList());
        }


        public ActionResult LichBongChuyen()
        {
            return View();
        }
        public ActionResult LichCauLong()
        {
            return View();
        }
        public ActionResult LichKeoCo()
        {
            return View();
        }
        public ActionResult LichCoVua_CoTuong()
        {
            return View();
        }
        public ActionResult LichDienKinh()
        {
            return View();
        }
    }
}