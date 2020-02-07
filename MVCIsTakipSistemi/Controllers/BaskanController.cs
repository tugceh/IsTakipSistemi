using MVCIsTakipSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCIsTakipSistemi.Controllers
{
    public class BaskanController : Controller
    {
        MVCIsTakipDBEntities entity = new MVCIsTakipDBEntities();
        // GET: Baskan
        public ActionResult Index()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);
            if (yetkiId == 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }
   
        public ActionResult Ata()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);
            if (yetkiId == 1)
            {
                var yetkiPersonel = (from y in entity.Yetkiler where y.yetkiTurId == 1 select y).ToList();
                int personelId = Convert.ToInt32(yetkiPersonel[0].personelId);


                var personeller = entity.Personeller.Where(x => x.personelId != personelId).ToList();
                ViewBag.personeller = personeller;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        } 
        
        [HttpPost]
        public ActionResult Ata(string txtIs, string txtAciklama, int selectPer)
        {
            Isler isAta = new Isler();
            isAta.isAd = txtIs;
            isAta.isAciklama = txtAciklama;
            isAta.isTarih = DateTime.Now;
            isAta.isPersonelId = selectPer;
            isAta.isDurum = "İletiliyor";

            entity.Isler.Add(isAta);
            entity.SaveChanges();

            return RedirectToAction("Index", "Baskan");
        }

        public ActionResult TakipIlet()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);
            
            if (yetkiId == 1)
            {
                int personelId = Convert.ToInt32(Session["personelId"]);
                var personeller = entity.Personeller.Where(x => x.personelId != personelId).ToList();
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

            return RedirectToAction("Takip", "Baskan");
        }

        public ActionResult Takip()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);
            if (yetkiId == 1)
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

    }
}