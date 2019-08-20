using proje1.BusinessLayer;
using proje1.BusinessLayer.Results;
using proje1.Common;
using proje1.Entities;
using proje1.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TemplateArayisi.Controllers
{
    public class KullaniciController : Controller
    {
        public ActionResult Aktivasyon(Guid id)
        {
            KullaniciYönetici ky = new KullaniciYönetici();
            BusinessLayerResult<Kullanici> res = ky.KullaniciAktiflestir(id);

            if (res.Errors.Count > 0)
            {
                TempData["errors"] = res.Errors;
                return RedirectToAction("AktivasyonIptal");
            }
            return RedirectToAction("AktivasyonTamam");
        }

        public ActionResult AktivasyonTamam()
        {
            return View();
        }

        public ActionResult AktivasyonIptal()
        {
            List<ErrorMessageObj> errors = null;
            if (TempData["errors"] != null)
            {
                errors = TempData["errors"] as List<ErrorMessageObj>;
            }
            return View(errors);
        }

        public ActionResult OdemeSatinAlim()
        {
            return View();
        }

        [HttpGet]
        public JsonResult SepeteEkle(long Id)
        {
            Kullanici klnc = Session["giris"] as Kullanici;
            if (klnc != null)
            {
                int sayi = 0;
                UrunYonetici uy = new UrunYonetici();
                SepetYonetici sy = new SepetYonetici();
                List<Sepet> sptler = sy.List(x=>x.kullaniciId==klnc.kullaniciId);
                Urun urn = uy.Find(x => x.ilanId == Id);
                for (int i = 0; i < sptler.Count; i++)
                {
                    if (sptler[i].urunId == urn.urunId)
                        sayi++;
                }
                if (sayi == 0)
                {
                    Sepet spt = new Sepet();
                    spt.urunId = urn.urunId;
                    spt.kullaniciId = App.Common.GetCurrentUsernameId();
                    sy.Insert(spt);
                }
            }
            return Json("dENEME");
        }
        
        [HttpGet]
        public JsonResult SepetSil(long Id)
        {
            Kullanici klnc = Session["giris"] as Kullanici;
            if (klnc != null)
            {
                int sayi = 0;
                UrunYonetici uy = new UrunYonetici();
                SepetYonetici sy = new SepetYonetici();
                List<Sepet> sptler = sy.List(x => x.kullaniciId == klnc.kullaniciId);
                Urun urn = uy.Find(x => x.ilanId == Id);
                for (int i = 0; i < sptler.Count; i++)
                {
                    if (sptler[i].urunId == urn.urunId)
                        sayi++;
                }
                if (sayi != 0)
                {
                    Sepet spt = sy.Find(x => x.urunId == urn.urunId);
                    sy.Delete(spt);
                }
            }
            return Json("deneme1");
        }

        [HttpGet]
        public JsonResult SepetleriSil()
        {
            Kullanici klnc = Session["giris"] as Kullanici;
            SepetYonetici sy = new SepetYonetici();
            if (klnc != null)
            {
                List<Sepet> sptler = sy.List(x => x.kullaniciId == klnc.kullaniciId);
                for (int i = 0; i < sptler.Count; i++)
                {
                    if (sptler[i].faturaId == null)
                    {
                        sy.Delete(sptler[i]);
                    }
                }
            }
            return Json("deneme1");
        }
    }
}