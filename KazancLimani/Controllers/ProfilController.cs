using proje1.BusinessLayer;
using proje1.BusinessLayer.Results;
using proje1.Entities;
using proje1.Entities.ValueObjects;
using TemplateArayisi.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TemplateArayisi.Controllers
{
    public class ProfilController : Controller
    {
        public MesajViewModel mvm = new MesajViewModel();

        public ActionResult Hesabim()
        {
            if (Session["giris"] == null)
                return RedirectToAction("Giris", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult Hesabim(KullaniciViewModel kvm, List<HttpPostedFileBase> file)
        {
            var filename = ImageNameGenerator.ProfilFotoIsmiUret(file[0]);
            var path = "null";
            kvm.fotograf = filename;
            KullaniciYönetici ky = new KullaniciYönetici();
            Kullanici klnc = Session["giris"] as Kullanici;
            BusinessLayerResult<Kullanici> res = new BusinessLayerResult<Kullanici>();
            if (klnc != null)
            {
                kvm.KullaniciId = klnc.kullaniciId;
            }
            res = ky.KullaniciGuncelle(kvm, file[0]);
            if (file[0] != null)
            {
                //filename = Path.GetFileName(file[0].FileName);
                path = Path.Combine(Server.MapPath("~/Content/Image/Profil"), filename);
                Image imgnew = ResizeImage.Resize(Image.FromStream(file[0].InputStream) , 0, 473); //son eklediğim alan
                
                //file[0].SaveAs(path);
                imgnew.Save(path);
            }
            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View(kvm);
            }
            if (file != null && file.Count > 1)
            {
                for (int i = 1; i <= file.Count - 1; i++)
                {
                    if (file != null && file[i].ContentLength > 0)
                    {
                        filename = Path.GetFileName(file[i].FileName);
                        path = Path.Combine(Server.MapPath("~/Content/Image/Profil"), filename);
                        kvm.fotograf = file[i].FileName;
                        KullaniciYönetici ky1 = new KullaniciYönetici();
                        BusinessLayerResult<KullaniciFoto> res1 = ky1.KullaniciFotosuYukle(kvm.Username, file[i]);
                        file[i].SaveAs(path);
                    }
                }
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult Mesajlarim()
        {
            if (Session["giris"] == null)
                return RedirectToAction("Giris", "Home");
            KullaniciYönetici ky = new KullaniciYönetici();
            MesajYonetici my = new MesajYonetici();
            List<Mesajlasma> msjlar = new List<Mesajlasma>();
            Kullanici giris = Session["giris"] as Kullanici;
            long id = 0;
            msjlar = my.MesajlariAl(giris).OrderByDescending(x => x.mesajtarihsaat).ToList();
            if (msjlar.Count > 0)
            {
                if (msjlar[0].kullaniciAliciId != giris.kullaniciId)
                {
                    id = msjlar[0].kullaniciAliciId;
                }
                else if (msjlar[0].kullaniciGonderenId != giris.kullaniciId)
                {
                    id = msjlar[0].kullaniciGonderenId;
                }
                Kullanici klnc = ky.SecilenKullaniciIdIle(id);
                mvm.msjlar = msjlar;
                mvm.klnc = klnc;
            }
            return View("Mesajlarim", mvm);
        }

        [HttpPost]
        public ActionResult Mesajlarim(MesajViewModel model)
        {
            if(model.klnc.kullaniciId != 0)
            {
                MesajYonetici my = new MesajYonetici();
                Mesajlasma mesaj = new Mesajlasma();
                Kullanici klnc = Session["giris"] as Kullanici;
                if (model.MesajMetni != null)
                {
                    mesaj.kullaniciGonderenId = klnc.kullaniciId;
                    mesaj.kullaniciAliciId = model.klnc.kullaniciId;
                    mesaj.mesajmetni = model.MesajMetni;
                    mesaj.mesajtarihsaat = DateTime.Now;
                    mvm.klnc = model.klnc;
                    my.Insert(mesaj);
                }
                mvm.klnc = model.klnc;
            }
            return View(mvm);
        }

        public ActionResult Sattiklarim()
        {
            UrunYonetici uy = new UrunYonetici();
            Kullanici klnc = Session["giris"] as Kullanici;
            if(Session["giris"]==null)
                return RedirectToAction("Giris", "Home");
            List<Urun> urnler = uy.List(x => x.kullaniciId == klnc.kullaniciId).OrderByDescending(x=>x.satistarihsaat).ToList();
            return View(urnler);
        }

        public ActionResult Sepetim()
        {
            return View();
        }

        public ActionResult HesabiSil()
        {
            if (Session["giris"] == null)
                return RedirectToAction("Giris", "Home");
            KullaniciYönetici ky = new KullaniciYönetici();
            Kullanici klnc = Session["giris"] as Kullanici;
            Session.Remove("giris");
            ky.Delete(klnc);
            return RedirectToAction("Index", "Home");
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult MesajGetir(long id)
        {
            MesajYonetici my = new MesajYonetici();
            KullaniciYönetici ky = new KullaniciYönetici();
            Kullanici klnc = ky.Find(x => x.kullaniciId == id);
            List<Mesajlasma> msjlar = my.MesajlariAl(klnc).OrderByDescending(x => x.mesajtarihsaat).ToList();
            mvm.msjlar = msjlar;
            mvm.klnc = klnc;
            return Json(mvm);
        }

        public ActionResult Mesaj(int id)
        {
            if (Session["giris"] == null)
                return RedirectToAction("Giris", "Home");
            KullaniciYönetici ky = new KullaniciYönetici();
            Kullanici giris = Session["giris"] as Kullanici;
            Kullanici klnc = ky.SecilenKullaniciIdIle(id);
            mvm.klnc = klnc;
            return View("Mesajlarim", mvm);
        }

        public ActionResult Select(int? id)
        {
            if (Session["giris"] == null)
                return RedirectToAction("Giris", "Home");
            KullaniciYönetici ky = new KullaniciYönetici();
            KullaniciViewModel kvm = new KullaniciViewModel();
            Kullanici klnc = null;
            Kullanici giris = Session["giris"] as Kullanici;

            if (id == null)
            {
                klnc = Session["giris"] as Kullanici;
                kvm.Username = klnc.kullaniciAdi;
                kvm.Firstname = klnc.isim;
                kvm.Lastname = klnc.soyisim;
                kvm.Email = klnc.kullaniciEmail;
                if (klnc.dogumtarihi != null)
                {
                    if (klnc.dogumtarihi.Value.Day < 10)
                    {
                        kvm.GunAlani = "0" + klnc.dogumtarihi.Value.Day.ToString();
                    }
                    if (klnc.dogumtarihi.Value.Month < 10)
                    {
                        kvm.AyAlani = "0" + klnc.dogumtarihi.Value.Month.ToString();
                    }
                    if(klnc.dogumtarihi.Value.Day >= 10 && klnc.dogumtarihi.Value.Month >= 10)
                    {
                        kvm.GunAlani = klnc.dogumtarihi.Value.Day.ToString();
                        kvm.AyAlani = klnc.dogumtarihi.Value.Month.ToString();
                    }
                    kvm.YilAlani = klnc.dogumtarihi.Value.Year.ToString();
                }
                if (klnc.ilPlaka != null)
                {
                    kvm.SehirAlani = ky.SehiradiAl(klnc.ilPlaka.Value);
                }
                kvm.CinsiyetAlani = klnc.cinsiyet;
                return View("Hesabim", kvm);
            }
            else if (id != null && giris != null && giris.kullaniciId == id)
            {
                klnc = Session["giris"] as Kullanici;

                kvm.Username = klnc.kullaniciAdi;
                kvm.Firstname = klnc.isim;
                kvm.Lastname = klnc.soyisim;
                kvm.Email = klnc.kullaniciEmail;
                kvm.GunAlani = klnc.dogumtarihi.Value.Day.ToString();
                kvm.AyAlani = klnc.dogumtarihi.Value.Month.ToString();
                kvm.YilAlani = klnc.dogumtarihi.Value.Year.ToString();
                kvm.SehirAlani = ky.SehiradiAl(klnc.ilPlaka.Value);
                kvm.CinsiyetAlani = klnc.cinsiyet;
                return View("Hesabim", kvm);
            }
            else
            {
                klnc = ky.SecilenKullaniciIdIle(id.Value);
                if (klnc == null)
                {
                    //return HttpNotFound();
                    return RedirectToAction("Index", "Home");
                }
                MesajViewModel mvm = new MesajViewModel();
                mvm.AliciKullaniciId = (klnc.kullaniciId).ToString();
                return View("Kullanici", mvm);
            }
        }
    }
}