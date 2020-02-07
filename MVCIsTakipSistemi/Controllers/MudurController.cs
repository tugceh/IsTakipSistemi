using MVCIsTakipSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCIsTakipSistemi.Controllers
{
    public class MudurController : Controller
    {
        MVCIsTakipDBEntities entity = new MVCIsTakipDBEntities();
        // GET: Mudur
        public ActionResult Index()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);
            if (yetkiId == 2)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Ilet()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);
            if (yetkiId == 2)
            {
                var personelId = Convert.ToInt32(Session["personelId"]);

                var personel = entity.Personeller.Where(a => a.personelId == personelId).FirstOrDefault();

                var personeller = entity.Personeller.Where(x => x.personelBirimId == personel.personelBirimId).ToList();

                ViewBag.personeller = personeller;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Ilet(int selectPer)
        {
            var secilen = entity.Personeller.Where(x => x.personelId == selectPer).FirstOrDefault();

            TempData["secilen"] = secilen;

            return RedirectToAction("Ata", "Mudur");

        }

        public ActionResult Ata()
        {
            Personeller personel = (Personeller)TempData["secilen"];

            var isler = entity.Isler.Where(x => x.isPersonelId == personel.personelId && x.isDurum == "İletiliyor").ToList();

            ViewBag.isler = isler;
            ViewBag.personel = personel;

            return View();
        }

        [HttpPost]
        public ActionResult Ata(int isId)
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);
            if (yetkiId == 2)
            {
                var tekIs = entity.Isler.Where(x => x.isId == isId).FirstOrDefault();

                tekIs.isDurum = "yapılıyor";
                tekIs.iletilenTarih = DateTime.Now;
                entity.SaveChanges();

                return RedirectToAction("Index", "Mudur");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult TakipIlet()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);
            if (yetkiId == 2)
            {
                var personelId = Convert.ToInt32(Session["personelId"]);

                var personel = entity.Personeller.Where(a => a.personelId == personelId).FirstOrDefault();

                var personeller = entity.Personeller.Where(x => x.personelBirimId == personel.personelBirimId).ToList();

                ViewBag.personeller = personeller;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult TakipIlet(int selectPer)
        {
            var secilen = entity.Personeller.Where(x => x.personelId == selectPer).FirstOrDefault();

            TempData["secilen"] = secilen;

            return RedirectToAction("Takip", "Mudur");
        }

        public ActionResult Takip()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);
            if (yetkiId == 2)
            {
                Personeller personel = (Personeller)TempData["secilen"];

                var isler = entity.Isler.Where(x => x.isPersonelId == personel.personelId).ToList();

                ViewBag.isler = isler;
                ViewBag.personel = personel;

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
            if (yetkiId == 2)
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

            return RedirectToAction("Index", "Mudur");
        }
    }
}