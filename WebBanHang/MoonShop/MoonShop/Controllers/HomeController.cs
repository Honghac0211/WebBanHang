using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoonShop.Models;
namespace MoonShop.Controllers
{
    public class HomeController : Controller
    {
        QLMPEntities2 db = new QLMPEntities2();
        // GET: Home
        public ActionResult HomeTrangChu()
        {
            var item = db.SanPhams.ToList();
            return View(item);
        }
        [HttpPost]
        public ActionResult HomeTrangChu(string txt_search)
        {
            var item = db.SanPhams.Where(n =>n.TenSanPham.Contains(txt_search)).ToList();
            return View(item);
        }
        
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SinUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SinUp(string txt_username, string txt_email, string txt_password)
        {
            if (ModelState.IsValid)
            {
                KhachHang k = new KhachHang();
                k.ID_KhachHang = 1;
                k.HoTen_KH = txt_username;
                k.GioiTinh = "";
                k.Email = txt_email;
                k.TenDangNhap = txt_email;
                k.DiaChi = "";
                k.MatKhau = txt_password;
                db.KhachHangs.Add(k);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return RedirectToAction("HomeTrangChu", "Home");

        }
        [HttpPost]
        public ActionResult Login(string txt_email, string txt_pass)
        {
            KhachHang item = db.KhachHangs.FirstOrDefault(n => n.TenDangNhap == txt_email && n.MatKhau == txt_pass);
            if (item == null)

            {
                return View();
            }
            else
            {
                Session["user1"] = item;
                return RedirectToAction("HomeTrangChu");
            }

        }
      

    }
}