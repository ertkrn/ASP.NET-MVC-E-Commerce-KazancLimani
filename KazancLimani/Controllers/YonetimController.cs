using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using proje1.Entities;
using proje1.BusinessLayer;
using proje1.BusinessLayer.Results;
using proje1.Entities.Messages;
using TemplateArayisi.Classes;

namespace TemplateArayisi.Controllers
{
    public class YonetimController : BaseController
    {
        private KategoriYonetici ky = new KategoriYonetici();
        private KullaniciYönetici kuy = new KullaniciYönetici();
        private UrunYonetici uy = new UrunYonetici();
        private SehirYonetici sy = new SehirYonetici();
        private RenkYonetici ry = new RenkYonetici();
        private MesajYonetici my = new MesajYonetici();
        private SepetYonetici spy = new SepetYonetici();
        private BusinessLayerResult<Kullanici> res = new BusinessLayerResult<Kullanici>();
        private Kategori kat = new Kategori();
        private Kullanici klnc = new Kullanici();
        private Urun urn = new Urun();
        private Renk rnk = new Renk();
        private Sehir shr = new Sehir();
        private Sepet spt = new Sepet();
        private Mesajlasma msj = new Mesajlasma();
        private Random rastgele = new Random();

        public Kullanici AdminMi()
        {
            Kullanici admin = Session["giris"] as Kullanici;

            return admin;
        }

        //Kategori Yönetimine Ait
        public ActionResult KategoriYonetim()
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris","Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(ky.List());
        }
        
        public ActionResult KategoriDetay(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kat = ky.Find(x => x.kategoriId == id.Value);
            if (kat == null)
            {
                return HttpNotFound();
            }
            return View(kat);
        }
        
        public ActionResult KategoriOlustur()
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KategoriOlustur(Kategori kat)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (ModelState.IsValid)
            {
                ky.Insert(kat);
                return RedirectToAction("KategoriYonetim");
            }

            return View(kat);
        }

        public ActionResult KategoriDuzenle(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kat = ky.Find(x => x.kategoriId == id.Value);
            if (kat == null)
            {
                return HttpNotFound();
            }
            return View(kat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KategoriDuzenle(Kategori kategori)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (ModelState.IsValid)
            {
                Kategori katyeni = ky.Find(x => x.kategoriId == kategori.kategoriId);
                katyeni.kategoriAdi = kategori.kategoriAdi;
                ky.Update(katyeni);
                return RedirectToAction("KategoriYonetim");
            }
            return View(kategori);
        }

        public ActionResult KategoriSil(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kat = ky.Find(x => x.kategoriId == id.Value);
            if (kat == null)
            {
                return HttpNotFound();
            }
            return View(kat);
        }

        [HttpPost, ActionName("KategoriSil")]
        [ValidateAntiForgeryToken]
        public ActionResult KategoriSilOnayli(long id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            kat = ky.Find(x => x.kategoriId == id);
            ky.Delete(kat);
            return RedirectToAction("KategoriYonetim");
        }
        
        //Kullanici Yönetimine Ait
        public ActionResult KullaniciYonetim()
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(kuy.List());
        }

        public ActionResult KullaniciDetay(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            klnc = kuy.Find(x => x.kullaniciId == id.Value);
            if (klnc == null)
            {
                return HttpNotFound();
            }
            return View(klnc);
        }

        public ActionResult KullaniciOlustur()
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KullaniciOlustur(Kullanici kullanici)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (ModelState.IsValid)
            {
                klnc = kuy.Find(x => x.kullaniciAdi == kullanici.kullaniciAdi || x.kullaniciEmail==kullanici.kullaniciEmail);
                if (klnc == null)
                {
                    Kullanici klncyeni = new Kullanici();
                    Sehir shr = sy.Find(x => x.ilAdi == kullanici.kullaniciKonum);
                    klncyeni.kullaniciAdi = kullanici.kullaniciAdi;
                    klncyeni.kullaniciSifre = kullanici.kullaniciSifre;
                    klncyeni.kullaniciKonum = kullanici.kullaniciKonum;
                    klncyeni.kullaniciEmail = kullanici.kullaniciEmail;
                    klncyeni.activateGuid = Guid.NewGuid();
                    klncyeni.kullaniciAktifmi = kullanici.kullaniciAktifmi;
                    klncyeni.adminmi = kullanici.adminmi;
                    klncyeni.kullaniciPuan = kullanici.kullaniciPuan;
                    klncyeni.isim = kullanici.isim;
                    klncyeni.soyisim = kullanici.soyisim;
                    klncyeni.cinsiyet = kullanici.cinsiyet;
                    klncyeni.dogumtarihi = kullanici.dogumtarihi;
                    klncyeni.profilFoto = kullanici.profilFoto;
                    klncyeni.ilPlaka = shr.ilPlaka;
                    kuy.Insert(klncyeni);
                }
                else
                {
                    if (klnc.kullaniciAdi != null)
                    {
                        if(klnc.kullaniciAdi == kullanici.kullaniciAdi)
                        {
                            res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                            res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                            return View(kullanici);
                        }
                    }
                    if (klnc.kullaniciEmail != null)
                    {
                        if(klnc.kullaniciEmail == kullanici.kullaniciEmail)
                        {
                            res.AddError(ErrorMessageCode.EmailAlreadyExits, "Email adresi kayıtlı.");
                            res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                            return View(kullanici);
                        }
                    }
                }
                return RedirectToAction("KullaniciYonetim");
            }

            return View(kullanici);
        }

        public ActionResult KullaniciDuzenle(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            klnc = kuy.Find(x => x.kullaniciId == id.Value);
            if (klnc == null)
            {
                return HttpNotFound();
            }
            return View(klnc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KullaniciDuzenle(Kullanici kullanici)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (ModelState.IsValid)
            {
                Kullanici klncyeni = kuy.Find(x => x.kullaniciId == kullanici.kullaniciId);
                Sehir shr = sy.Find(x=>x.ilAdi==kullanici.kullaniciKonum);
                klncyeni.kullaniciAdi = kullanici.kullaniciAdi;
                klncyeni.kullaniciSifre = kullanici.kullaniciSifre;
                klncyeni.kullaniciKonum = kullanici.kullaniciKonum;
                klncyeni.kullaniciEmail = kullanici.kullaniciEmail;
                klncyeni.activateGuid = kullanici.activateGuid;
                klncyeni.kullaniciAktifmi = kullanici.kullaniciAktifmi;
                klncyeni.adminmi = kullanici.adminmi;
                klncyeni.kullaniciPuan = kullanici.kullaniciPuan;
                klncyeni.isim = kullanici.isim;
                klncyeni.soyisim = kullanici.soyisim;
                klncyeni.cinsiyet = kullanici.cinsiyet;
                klncyeni.dogumtarihi = kullanici.dogumtarihi;
                klncyeni.profilFoto = kullanici.profilFoto;
                klncyeni.ilPlaka = shr.ilPlaka;
                kuy.Update(klncyeni);
                return RedirectToAction("KullaniciYonetim");
            }
            return View(kullanici);
        }

        public ActionResult KullaniciSil(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            klnc = kuy.Find(x => x.kullaniciId == id.Value);
            if (klnc == null)
            {
                return HttpNotFound();
            }
            return View(klnc);
        }

        [HttpPost, ActionName("KullaniciSil")]
        [ValidateAntiForgeryToken]
        public ActionResult KullaniciSilOnayli(long id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            klnc = kuy.Find(x => x.kullaniciId == id);
            kuy.Delete(klnc);
            return RedirectToAction("KullaniciYonetim");
        }

        //Ürün Yönetimine Ait
        public ActionResult UrunYonetim()
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(ky.List());
        }

        public ActionResult UrunListele(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            List<Urun> urnler = uy.List(x => x.kategoriId == id.Value).OrderByDescending(x => x.satistarihsaat).ToList();
            if (urnler.Count == 0)
            {
                kat = ky.Find(x => x.kategoriId == id.Value);
                ViewData["katadi"] = kat.kategoriAdi;
            }
            return View(urnler);
        }

        public ActionResult UrunDetay(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            urn = uy.Find(x => x.urunId == id.Value);
            if (urn == null)
            {
                return HttpNotFound();
            }
            return View(urn);
        }

        public ActionResult UrunOlustur()
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UrunOlustur(Urun urun)
        {
            Kullanici admin = AdminMi();
            long sayi=0;
            while(true)
            {
                sayi = rastgele.Next(11111111, 99999999);
                if (uy.Find(x => x.ilanId == sayi) == null)
                    break;
            }
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            urun.satistarihsaat = DateTime.Now;
            if (ModelState.IsValid)
            {
                urn = uy.Find(x => x.urunBaslik == urun.urunBaslik || x.urunAciklama == urun.urunAciklama);
                if (urn == null)
                {
                    Urun urnyeni = new Urun();
                    //Burayı Doldur....
                    urnyeni.ilanId = sayi;
                    urnyeni.urunBaslik = urun.urunBaslik;
                    urnyeni.urunFiyati = urun.urunFiyati;
                    urnyeni.renkId = urun.renkId;
                    urnyeni.urunKonum = urun.urunKonum;
                    urnyeni.ilPlaka = urun.ilPlaka;
                    urnyeni.kategoriId = urun.kategoriId;
                    urnyeni.takasyapilirmi = urun.takasyapilirmi;
                    urnyeni.urunAciklama = urun.urunAciklama;
                    urnyeni.halasatilikmi = urun.halasatilikmi;
                    urnyeni.kullaniciId = urun.kullaniciId;
                    urnyeni.satistarihsaat = urun.satistarihsaat;
                    urnyeni.urunFotosu = urun.urunFotosu;
                    
                    uy.Insert(urnyeni);
                }
                else
                {
                    if (urn.urunBaslik != null)
                    {
                        if (urn.urunBaslik == urun.urunBaslik)
                        {
                            res.AddError(ErrorMessageCode.UrunBaslikKayitli, "Ürün başlığı başka ürünün başlığı ile aynı olamaz.");
                            res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                            return View(urun);
                        }
                    }
                    if (urn.urunAciklama != null)
                    {
                        if (urn.urunAciklama == urun.urunAciklama)
                        {
                            res.AddError(ErrorMessageCode.UrunAciklamaKayitli, "Ürün açıklaması başka ürünün açıklaması ile aynı olamaz.");
                            res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                            return View(urun);
                        }
                    }
                }
                return RedirectToAction("UrunYonetim");
            }

            return View(urun);
        }

        public ActionResult UrunDuzenle(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            urn = uy.Find(x => x.urunId == id.Value);
            if (urn == null)
            {
                return HttpNotFound();
            }
            return View(urn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UrunDuzenle(Urun urun)
        {
            Kullanici admin = AdminMi();
            BusinessLayerResult<Urun> res = uy.ilanIdSorgulama(urun.ilanId, urun.urunId);
            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View(urun);
            }
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (ModelState.IsValid)
            {
                Urun urnyeni = uy.Find(x => x.urunId == urun.urunId);
                //Burayı düzelt...
                urnyeni.ilanId = urun.ilanId;
                urnyeni.urunBaslik = urun.urunBaslik;
                urnyeni.urunFiyati = urun.urunFiyati;
                urnyeni.renkId = urun.renkId;
                urnyeni.urunKonum = urun.urunKonum;
                urnyeni.ilPlaka = urun.ilPlaka;
                urnyeni.kategoriId = urun.kategoriId;
                urnyeni.takasyapilirmi = urun.takasyapilirmi;
                urnyeni.durumu = urun.durumu;
                urnyeni.urunAciklama = urun.urunAciklama;
                urnyeni.halasatilikmi = urun.halasatilikmi;
                urnyeni.kullaniciId = urun.kullaniciId;
                urnyeni.satistarihsaat = urun.satistarihsaat;
                urnyeni.urunFotosu = urun.urunFotosu;

                uy.Update(urnyeni);
                return RedirectToAction("UrunYonetim");
            }
            return View(urun);
        }

        public ActionResult UrunSil(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            urn = uy.Find(x => x.urunId == id.Value);
            if (urn == null)
            {
                return HttpNotFound();
            }
            return View(urn);
        }

        public ActionResult UrunleriSil()
        {
            List<Sepet> sptler = spy.List();
            for(int i = 0; i < sptler.Count; i++)
            {
                if (sptler[i].faturaId == null)
                {
                    spy.Delete(sptler[i]);
                }
            }
            List<Urun> urnler = uy.List();
            foreach(Urun item in urnler)
            {
                uy.Delete(item);
            }

            return RedirectToAction("UrunYonetim");
        }

        [HttpPost, ActionName("UrunSil")]
        [ValidateAntiForgeryToken]
        public ActionResult UrunSilOnayli(long id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            urn = uy.Find(x => x.urunId == id);
            uy.Delete(urn);
            return RedirectToAction("UrunYonetim");
        }

        //Şehir Yönetimine Ait
        public ActionResult SehirYonetim()
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(sy.List());
        }

        public ActionResult SehirDetay(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shr = sy.Find(x => x.ilPlaka == id.Value);
            if (shr == null)
            {
                return HttpNotFound();
            }
            return View(shr);
        }

        public ActionResult SehirOlustur()
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SehirOlustur(Sehir sehir)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (ModelState.IsValid)
            {
                shr = sy.Find(x => x.ilPlaka == sehir.ilPlaka || x.ilAdi == sehir.ilAdi);
                if (shr == null)
                {
                    Sehir shryeni = new Sehir();
                    shryeni.ilPlaka = sehir.ilPlaka;
                    shryeni.ilAdi = sehir.ilAdi;
                    sy.Insert(shryeni);
                }
                else
                {
                    if (shr.ilAdi == sehir.ilAdi)
                    {
                        res.AddError(ErrorMessageCode.SehirAdiKayitli, "Girdiğiniz şehir adı zaten kayıtlı.");
                        res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                        return View(shr);
                    }
                    if (shr.ilPlaka == sehir.ilPlaka)
                    {
                        res.AddError(ErrorMessageCode.SehirPlakaKayitli, "Girdiğiniz plaka zaten kayıtlı.");
                        res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                        return View(shr);
                    }
                }
                return RedirectToAction("SehirYonetim");
            }

            return View(shr);
        }

        public ActionResult SehirDuzenle(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shr = sy.Find(x => x.ilPlaka == id.Value);
            if (shr == null)
            {
                return HttpNotFound();
            }
            return View(shr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SehirDuzenle(Sehir sehir)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            shr = sy.Find(x => x.ilPlaka == sehir.ilPlaka || x.ilAdi == sehir.ilAdi);
            if (ModelState.IsValid)
            {
                if (shr == null)
                {
                    Sehir shryeni = sy.Find(x => x.ilPlaka == sehir.ilPlaka);
                    shryeni.ilPlaka = sehir.ilPlaka;
                    shryeni.ilAdi = sehir.ilAdi;
                    sy.Update(shryeni);
                }
                else
                {
                    if (shr.ilAdi == sehir.ilAdi)
                    {
                        res.AddError(ErrorMessageCode.SehirAdiKayitli, "Girdiğiniz şehir adı zaten kayıtlı.");
                        res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                        return View(shr);
                    }
                    if (shr.ilPlaka == sehir.ilPlaka)
                    {
                        res.AddError(ErrorMessageCode.SehirPlakaKayitli, "Girdiğiniz plaka zaten kayıtlı.");
                        res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                        return View(shr);
                    }
                }
                return RedirectToAction("SehirYonetim");
            }
            return View(shr);
        }

        public ActionResult SehirSil(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shr = sy.Find(x => x.ilPlaka == id.Value);
            if (kat == null)
            {
                return HttpNotFound();
            }
            return View(shr);
        }

        [HttpPost, ActionName("SehirSil")]
        [ValidateAntiForgeryToken]
        public ActionResult SehirSilOnayli(long id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            shr = sy.Find(x => x.ilPlaka == id);
            sy.Delete(shr);
            return RedirectToAction("SehirYonetim");
        }

        //Kategori Yönetimine Ait
        public ActionResult RenkYonetim()
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(ry.List());
        }

        public ActionResult RenkDetay(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rnk = ry.Find(x => x.renkId == id.Value);
            if (rnk == null)
            {
                return HttpNotFound();
            }
            return View(rnk);
        }

        public ActionResult RenkOlustur()
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RenkOlustur(Renk renk)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            rnk = ry.Find(x => x.renkAdi == renk.renkAdi || x.renkKodu == renk.renkAdi);
            if (ModelState.IsValid)
            {
                if (rnk == null)
                {
                    Renk rnkyeni = new Renk();
                    rnkyeni.renkAdi = renk.renkAdi;
                    rnkyeni.renkKodu = renk.renkKodu;
                    ry.Insert(rnkyeni);
                }
                else
                {
                    if (rnk.renkAdi== renk.renkAdi)
                    {
                        res.AddError(ErrorMessageCode.RenkAdiKayitli, "Girdiğiniz renk adı zaten kayıtlı.");
                        res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                        return View(rnk);
                    }
                    if (rnk.renkKodu == renk.renkKodu)
                    {
                        res.AddError(ErrorMessageCode.RenkKoduKayitli, "Girdiğiniz renk kodu zaten kayıtlı.");
                        res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                        return View(rnk);
                    }
                }
                return RedirectToAction("RenkYonetim");
            }

            return View(renk);
        }

        public ActionResult RenkDuzenle(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rnk = ry.Find(x => x.renkId == id.Value);
            if (rnk == null)
            {
                return HttpNotFound();
            }
            return View(rnk);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RenkDuzenle(Renk renk)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (ModelState.IsValid)
            {
                Renk rnkyeni = ry.Find(x => x.renkId == renk.renkId);
                rnkyeni.renkId = renk.renkId;
                rnkyeni.renkAdi = renk.renkAdi;
                rnkyeni.renkKodu = renk.renkKodu;
                ry.Update(rnkyeni);

                return RedirectToAction("RenkYonetim");
            }
            return View(renk);
        }

        public ActionResult RenkSil(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rnk = ry.Find(x => x.renkId == id.Value);
            if (rnk == null)
            {
                return HttpNotFound();
            }
            return View(rnk);
        }

        [HttpPost, ActionName("RenkSil")]
        [ValidateAntiForgeryToken]
        public ActionResult RenkSilOnayli(long id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            rnk = ry.Find(x => x.renkId == id);
            ry.Delete(rnk);
            return RedirectToAction("RenkYonetim");
        }

        //Kategori Yönetimine Ait
        public ActionResult MesajYonetim()
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(my.List());
        }

        public ActionResult MesajDetay(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            msj  = my.Find(x => x.mesajId == id.Value);
            if (msj == null)
            {
                return HttpNotFound();
            }
            return View(msj);
        }

        public ActionResult MesajOlustur()
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MesajOlustur(Mesajlasma mesaj)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (ModelState.IsValid)
            {
                Mesajlasma msjyeni = new Mesajlasma();
                msjyeni.kullaniciGonderenId = mesaj.kullaniciGonderenId;
                msjyeni.kullaniciAliciId = mesaj.kullaniciAliciId;
                msjyeni.mesajmetni = mesaj.mesajmetni;
                msjyeni.mesajtarihsaat = DateTime.Now;
                my.Insert(msjyeni);
                return RedirectToAction("MesajYonetim");
            }

            return View(mesaj);
        }

        public ActionResult MesajDuzenle(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            msj = my.Find(x => x.mesajId == id.Value);
            if (msj == null)
            {
                return HttpNotFound();
            }
            return View(msj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MesajDuzenle(Mesajlasma mesaj)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (ModelState.IsValid)
            {
                Mesajlasma msjyeni = my.Find(x => x.mesajId == mesaj.mesajId);
                msjyeni.kullaniciGonderenId = mesaj.kullaniciGonderenId;
                msjyeni.kullaniciAliciId = mesaj.kullaniciAliciId;
                msjyeni.mesajmetni = mesaj.mesajmetni;
                msjyeni.mesajtarihsaat = DateTime.Now;
                my.Update(msjyeni);
                return RedirectToAction("MesajYonetim");
            }
            return View(mesaj);
        }

        public ActionResult MesajSil(long? id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            msj = my.Find(x => x.mesajId == id.Value);
            if (msj == null)
            {
                return HttpNotFound();
            }
            return View(msj);
        }

        public ActionResult MesajlariSil()
        {
            List<Mesajlasma> mesajlar = my.List();
            foreach (Mesajlasma item in mesajlar)
            {
                my.Delete(item);
            }

            return RedirectToAction("MesajYonetim");
        }

        [HttpPost, ActionName("MesajSil")]
        [ValidateAntiForgeryToken]
        public ActionResult MesajSilOnayli(long id)
        {
            Kullanici admin = AdminMi();
            if (admin == null)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
            {
                if (!admin.adminmi)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            msj = my.Find(x => x.mesajId == id);
            my.Delete(msj);
            return RedirectToAction("MesajYonetim");
        }
    }
}
