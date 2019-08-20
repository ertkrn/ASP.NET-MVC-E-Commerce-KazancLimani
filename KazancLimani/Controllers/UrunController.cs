using proje1.BusinessLayer;
using proje1.BusinessLayer.Results;
using proje1.Common;
using proje1.Entities;
using proje1.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TemplateArayisi.Classes;

namespace TemplateArayisi.Controllers
{
    public class UrunController : Controller
    {
        public ActionResult Yukle()
        {
            if (Session["giris"] == null)
                return RedirectToAction("Giris", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult Yukle(UrunViewModel model, List<HttpPostedFileBase> file)
        {
            if (model.takasdegeri == "0")
                model.Takas = false;
            if (model.takasdegeri == "1")
                model.Takas = true;
            if (model.durumdegeri == "0")
            {
                model.durumu = false;
            }
            if (model.durumdegeri == "1")
            {
                model.durumu = true;
            }
            if (ModelState.IsValid && file.Count > 0)
            {
                UrunYonetici uy = new UrunYonetici();
                var filename = ImageNameGenerator.UrunFotoIsmiUret(file[0]);
                //filename += Path.GetExtension(file[0].FileName);
                model.Fotograf = filename;
                var path = "null";
                BusinessLayerResult<Urun> res = new BusinessLayerResult<Urun>();
                res = uy.UrunKayit(model, file[0]);
                if (file[0] != null)
                {
                    //filename = Path.GetFileName(file[0].FileName);
                    path = Path.Combine(Server.MapPath("~/Content/Image/Urun"), filename);
                    Image imgnew = ResizeImage.Resize(Image.FromStream(file[0].InputStream), 0, 473); //son eklediğim alan
                    //file[0].SaveAs(path);
                    imgnew.Save(path);
                }
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                if (file != null && file.Count > 1)
                {
                    for (int i = 1; i <= file.Count - 1; i++)
                    {
                        if (file != null && file[i].ContentLength > 0)
                        {
                            filename = ImageNameGenerator.UrunFotoIsmiUret(file[i]);
                            //filename = Path.GetFileName(file[i].FileName);
                            model.Fotograf = filename;
                            path = Path.Combine(Server.MapPath("~/Content/Image/Urun"), filename);
                            UrunYonetici uy1 = new UrunYonetici();
                            BusinessLayerResult<UrunFoto> res1 = uy1.UrunFotosuYukle(model.UrunBaslik, file[i]);
                            Image imgnew = ResizeImage.Resize(Image.FromStream(file[i].InputStream), 0, 473); //son eklediğim alan
                            imgnew.Save(path);
                        }
                    }
                }
                Session["urun"] = res.Result;
                return RedirectToAction("Index", "Home");
            }
            return View(model);
            //return RedirectToAction("Index","Home");
        }

        public ActionResult UrunDetay(long? id)
        {
            if (Session["giris"] == null)
                return RedirectToAction("Giris", "Home");
            UrunYonetici uy = new UrunYonetici();
            if (id.Value == 0)
                return RedirectToAction("Sattiklarim", "Profil");
            Urun urn = uy.Find(x => x.urunId == id);
            return View(urn);
        }

        public ActionResult UrunDuzenle(long? id)
        {
            if (Session["giris"] == null)
                return RedirectToAction("Giris", "Home");
            UrunYonetici uy = new UrunYonetici();
            SehirYonetici sy = new SehirYonetici();
            UrunViewModel uvm = new UrunViewModel();
            KategoriYonetici ky = new KategoriYonetici();
            RenkYonetici ry = new RenkYonetici();
            if (id.Value == 0)
                return RedirectToAction("Sattiklarim", "Profil");
            Urun urn = uy.Find(x => x.urunId == id);
            Sehir shr = sy.Find(x => x.ilPlaka == urn.ilPlaka);
            Kategori kat = ky.Find(x => x.kategoriId == urn.kategoriId);
            Renk rnk = ry.Find(x => x.renkId == urn.renkId);
            if (urn.takasyapilirmi)
                uvm.takasdegeri = "UYGUN";
            else
                uvm.takasdegeri = "UYGUN DEĞİL";
            if (urn.durumu)
                uvm.durumdegeri = "İKİNCİ EL";
            else
                uvm.durumdegeri = "SIFIR";
            uvm.Takas = urn.takasyapilirmi;
            uvm.durumu = urn.durumu;
            uvm.UrunAciklama = urn.urunAciklama;
            uvm.UrunBaslik = urn.urunBaslik;
            uvm.UrunFiyat = urn.urunFiyati;
            uvm.UrunKonum = urn.urunKonum;
            uvm.SehirAlani = shr.ilAdi;
            uvm.Fotograf = urn.urunFotosu;
            uvm.KategoriAdi = kat.kategoriAdi;
            uvm.UrunRenkAdi = rnk.renkAdi;
            uvm.IlanId = urn.ilanId;
            return View(uvm);
        }

        [HttpPost]
        public ActionResult UrunDuzenle(UrunViewModel uvm, HttpPostedFileBase file)
        {

            var filename = ImageNameGenerator.ProfilFotoIsmiUret(file);
            var path = "null";
            uvm.Fotograf = filename;
            UrunYonetici uy = new UrunYonetici();
            BusinessLayerResult<Urun> res = new BusinessLayerResult<Urun>();
            if (file != null)
            {
                //filename = Path.GetFileName(file[0].FileName);
                path = Path.Combine(Server.MapPath("~/Content/Image/Urun"), filename);
                Image imgnew = ResizeImage.Resize(Image.FromStream(file.InputStream), 0, 473); //son eklediğim alan
                //file[0].SaveAs(path);
                imgnew.Save(path);
            }
            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View(uvm);
            }
            if (ModelState.IsValid)
            {
                Urun urnyeni = uy.Find(x => x.ilanId == uvm.IlanId);
                RenkYonetici ry = new RenkYonetici();
                SehirYonetici sy = new SehirYonetici();
                KategoriYonetici ky = new KategoriYonetici();
                if (uvm.takasdegeri == "UYGUN")
                    uvm.Takas = true;
                else
                    uvm.Takas = false;
                //Burayı düzelt...
                urnyeni.ilanId = uvm.IlanId;
                urnyeni.urunBaslik = uvm.UrunBaslik;
                urnyeni.urunFiyati = uvm.UrunFiyat;
                urnyeni.renkId = ry.Find(x => x.renkAdi == uvm.UrunRenkAdi).renkId;
                urnyeni.urunKonum = uvm.UrunKonum;
                urnyeni.ilPlaka = sy.Find(x=>x.ilAdi==uvm.SehirAlani).ilPlaka;
                urnyeni.kategoriId = ky.Find(x => x.kategoriAdi == uvm.KategoriAdi).kategoriId;
                urnyeni.takasyapilirmi = uvm.Takas;
                urnyeni.durumu = uvm.durumu;
                urnyeni.urunAciklama = uvm.UrunAciklama;
                urnyeni.halasatilikmi = true;
                urnyeni.kullaniciId = App.Common.GetCurrentUsernameId();
                //urnyeni.satistarihsaat = DateTime.Now; //Ürün güncellendiği zaman yükleme tarihi aynı kalsın
                if (file != null)
                {
                    urnyeni.urunFotosu = filename;
                }

                uy.Update(urnyeni);
                return RedirectToAction("Sattiklarim","Profil");
            }
            return View(uvm);
        }

        public ActionResult UrunSil(long? id)
        {
            if (Session["giris"] == null)
                return RedirectToAction("Giris", "Home");
            if (id.Value == 0)
                return RedirectToAction("Sattiklarim", "Profil");
            UrunYonetici uy = new UrunYonetici();
            uy.Delete(uy.Find(x=>x.urunId==id));
            return RedirectToAction("Sattiklarim", "Profil");
        }

        public ActionResult UrunBaslikAl(long? id)
        {
            UrunYonetici uy = new UrunYonetici();
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Urun urn = uy.Find(x => x.urunId == id);
            if (urn == null)
                return HttpNotFound();
            return PartialView("_PartialUrunDetay",urn);
        }
    }
}