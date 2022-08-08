using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoonShop.Models;
namespace MoonShop.Controllers
{
    public class MuaHangController : Controller
    {
        QLMPEntities2 db = new QLMPEntities2();
        // GET: MuaHang
        public ActionResult ThanhToan()
        {
            var item = db.GioHangs.ToList();
            return View(item);
        }  
        public ActionResult MuaHang( int id)
        {
            var item = db.SanPhams.FirstOrDefault(n => n.ID_SanPham == id);
            return View(item);
        }

        [HttpPost]
        public ActionResult MuaHang(string txt_id, string quantity, string txt_gia, string txt_diachi, string txt_quanhuyen, string txt_tinhtp)
        {
            if (ModelState.IsValid)
            {
                ChiTietHoaDonBanHang d = new ChiTietHoaDonBanHang();
                d.ID_SanPham = int.Parse(txt_id);
                d.ID_KhachHang = 1;
                d.SoLuong = int.Parse(quantity);
                d.Gia = int.Parse(txt_gia);
                d.diachi = txt_diachi + "," + txt_quanhuyen + "," + txt_tinhtp;
                db.ChiTietHoaDonBanHangs.Add(d);
                db.SaveChanges();
                return RedirectToAction("HomeTrangChu", "Home");
            }
            return RedirectToAction("HomeTrangChu", "Home");
        }
        [HttpPost]
        public ActionResult ThanhToan(string txt_diachi, string txt_quanhuyen, string txt_tinhtp)
        {
            if (ModelState.IsValid)
            {
                var giohang = db.GioHangs.ToList();
                foreach(var item in giohang)
                {
                    ChiTietHoaDonBanHang d = new ChiTietHoaDonBanHang();
                   
                    d.ID_SanPham =(int)item.ID_SanPham;
                    d.ID_KhachHang = 1;
                    d.SoLuong = (int)item.SoLuong;
                    d.Gia = (int)item.Gia;
                    d.diachi = txt_diachi + "," + txt_quanhuyen + "," + txt_tinhtp;
                    db.ChiTietHoaDonBanHangs.Add(d);
                    foreach(var a in giohang)
                    {
                        db.GioHangs.Remove(a);
                    }    
                    
                    db.SaveChanges();
                }             
                return RedirectToAction("HomeTrangChu", "Home");
            }
            return RedirectToAction("HomeTrangChu", "Home");
        }
    }
}