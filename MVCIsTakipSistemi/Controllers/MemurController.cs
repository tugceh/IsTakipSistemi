using MVCIsTakipSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCIsTakipSistemi.Controllers
{
    public class MemurController : Controller
    {
        MVCIsTakipDBEntities entity = new MVCIsTakipDBEntities();
        // GET: Memur
        public ActionResult Index()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);
            if (yetkiId == 3)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Yap()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);
            if (yetkiId == 3)
            {
                int personelId = Convert.ToInt32(Session["personelId"]);
                var isler = entity.Isler.Where(x => x.isPersonelId == personelId && x.isDurum == "yapılıyor").ToList();

                ViewBag.isler = isler;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Yap(int isId)
        {
            var tekIs = entity.Isler.Where(x => x.isId == isId).FirstOrDefault();

            tekIs.isDurum = "yapıldı";
            tekIs.yapilanTarih = DateTime.Now;
            entity.SaveChanges();

            return RedirectToAction("Index", "Memur");
        }


        public ActionResult Takip()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);
            if (yetkiId == 3)
            {
                int personelId = Convert.ToInt32(Session["personelId"]);

                var isler = entity.Isler.Where(x => x.isPersonelId == personelId).ToList();

                ViewBag.isler = isler;                

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
    }
}