using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoonShop.Models;
using Newtonsoft.Json;


namespace MoonShop.Controllers
{
    public class GioHangController : Controller
    {
        QLMPEntities2 db = new QLMPEntities2();
        // GET: GioHang

        //xay dung gio hang
        
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = db.GioHangs.ToList();
            return View(lstGioHang);
            

        }
        public ActionResult ThemGioHang(int id)
        {
            SanPham sp = db.SanPhams.FirstOrDefault(n => n.ID_SanPham == id);
            GioHang gh = new GioHang();
            var item = db.GioHangs.FirstOrDefault(n => n.ID_SanPham == id);
            if (item != null)
            {
                item.SoLuong = item.SoLuong + 1;
                item.ThanhTien = sp.GiaBan * item.SoLuong;
                db.SaveChanges();
            }
            else
            {
                gh.ID_SanPham = sp.ID_SanPham;
                gh.TenSanPham = sp.TenSanPham;
                gh.SoLuong = 1;
                gh.Gia = sp.GiaBan;
                gh.ThanhTien = sp.GiaBan * gh.SoLuong;
                db.GioHangs.Add(gh);
                db.SaveChanges();

            }
            return RedirectToAction("GioHang");

        }
        public ActionResult XoaGioHang(int id)
        {
            var item = db.GioHangs.FirstOrDefault(n => n.ID_GioHang == id);
            if (item != null)
            {
                db.GioHangs.Remove(item);
                db.SaveChanges();
                return RedirectToAction("HomeTrangChu", "Home");
            }
            return View();
        }
      
        // [HttpPost]
        //public ActionResult Update(int id, int sl)
        //{
        //    var item = db.GioHangs.FirstOrDefault(n => n.ID_GioHang == id);
        //    if (item != null)
        //    {
        //        var sll = mydb.Carts.Where(n => n.proID == id).FirstOrDefault();
        //        if (sll != null)
        //        {
        //            sll.CartQuantity = quantity;
        //            sll.CartMoney = quantity * sll.proPrice;
        //            mydb.Carts.Attach(sll);
        //            mydb.Entry(sll).State = EntityState.Modified;
        //            mydb.SaveChanges();
        //            ViewBag.Message = "Thêm giỏ hàng thành công";
        //            return RedirectToAction("DetailsCart");
        //        }
        //    }
        //    return RedirectToAction("DetailsCart");


        //}

    }
}