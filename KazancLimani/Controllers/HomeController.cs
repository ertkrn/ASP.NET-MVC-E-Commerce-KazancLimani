using proje1.BusinessLayer;
using proje1.BusinessLayer.Results;
using proje1.Entities;
using proje1.Entities.Messages;
using proje1.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace TemplateArayisi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UrunYonetici uy = new UrunYonetici();
            Kullanici giris = GirisKontrol();
            if (giris != null)
            {
                KullaniciYönetici ky = new KullaniciYönetici();
                LoginViewModel lvm = new LoginViewModel();
                lvm.Username = giris.kullaniciAdi;
                lvm.Password = giris.kullaniciSifre;
                BusinessLayerResult<Kullanici> res = ky.GirisKullanici(lvm);
                if (res.Errors.Count > 0)
                {
                    HttpCookie grs = new HttpCookie("giris");
                    grs.Expires = DateTime.Now.AddSeconds(-1d);
                    Response.Cookies.Add(grs);

                    HttpCookie sfr = new HttpCookie("sifre");
                    sfr.Expires = DateTime.Now.AddSeconds(-1d);
                    Response.Cookies.Add(sfr);
                }
                else
                {
                    Session["giris"] = res.Result;
                }
            }
            return View(uy.UrunleriAl().OrderByDescending(x => x.satistarihsaat).ToList());
        }

        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                KullaniciYönetici ky = new KullaniciYönetici();
                BusinessLayerResult<Kullanici> res = ky.GirisKullanici(lvm);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(lvm);
                }
                if (res.Errors.Count == 0 && lvm.BeniHatirla)
                {
                    HttpCookie grs = new HttpCookie("giris");
                    grs.Expires = DateTime.Now.AddSeconds(3600);
                    grs.Value = lvm.Username;
                    Response.Cookies.Add(grs);

                    HttpCookie sfr = new HttpCookie("sifre");
                    sfr.Expires = DateTime.Now.AddSeconds(3600);
                    sfr.Value = lvm.Password;
                    Response.Cookies.Add(sfr);
                }
                Session["giris"] = res.Result;
                return RedirectToAction("Index");
            }

            return View();
        }

        public Kullanici GirisKontrol()
        {
            Kullanici klnc = new Kullanici();
            string grs = string.Empty;
            string sfr = string.Empty;
            if (Response.Cookies["giris"] != null)
                grs = Response.Cookies["giris"].Value;
            if(Response.Cookies["sifre"] != null)
                sfr = Response.Cookies["sifre"].Value;
            if (!String.IsNullOrEmpty(grs) && !String.IsNullOrEmpty(sfr))
                klnc = new Kullanici { kullaniciAdi = grs, kullaniciSifre = sfr };
            return klnc;
        }

        public ActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kayit(KullaniciViewModel kvm)
        {
            if (ModelState.IsValid)
            {
                KullaniciYönetici ky = new KullaniciYönetici();
                BusinessLayerResult<Kullanici> res = new BusinessLayerResult<Kullanici>();
                Kullanici klnc = new Kullanici();
                res = ky.HizliKullaniciKaydi(kvm);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(kvm);
                }
                return RedirectToAction("KayitOnay");
            }
            return View();
        }

        public ActionResult KayitOnay()
        {
            return View();
        }

        public ActionResult Cikis()
        {
            Session.Remove("giris");
            if (Response.Cookies["giris"] != null)
            {
                HttpCookie grs = new HttpCookie("giris");
                grs.Expires = DateTime.Now.AddSeconds(-1d);
                Response.Cookies.Add(grs);
            }
            if(Response.Cookies["sifre"]!=null)
            {
                HttpCookie sfr = new HttpCookie("sifre");
                sfr.Expires = DateTime.Now.AddSeconds(-1d);
                Response.Cookies.Add(sfr);
            }
            return View("Index");
        }

        public ViewResult SiteMap()
        {
            Response.ContentType = "text/xml";
            Response.Write(SiteMapXML()); //Oluşturdumuz metodu burada yazdırıyoruz.
            Response.End();

            return View();
        }

        public ActionResult ErrorPage()
        {
            return View();
        }

        public static string SiteMapXML()
        {
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            strBuilder.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

            #region Anasayfa

            strBuilder.AppendLine("<url>");
            strBuilder.AppendLine("<loc>");

            //Ana sayfamızın linki sabit olduğundan kendimiz ekledik.
            string URL = String.Format("http://kazanclimani.com");
            strBuilder.AppendLine(URL);
            strBuilder.AppendLine("</loc>");
            strBuilder.AppendLine("<changefreq>");
            strBuilder.AppendLine("always");
            strBuilder.AppendLine("</changefreq>");
            strBuilder.AppendLine("<priority>");
            strBuilder.AppendLine("1");
            strBuilder.AppendLine("</priority>");
            strBuilder.AppendLine("</url>");
            #endregion

            #region GirisYap
            strBuilder.AppendLine("<url>");
            strBuilder.AppendLine("<loc>");
            //burada gelen girişin linkini oluşturuyoruz.
            URL = "http://kazanclimani.com/kullanici/giris-yap";
            strBuilder.AppendLine(URL);
            strBuilder.AppendLine("</loc>");
            strBuilder.AppendLine("<changefreq>");
            strBuilder.AppendLine("weekly");
            strBuilder.AppendLine("</changefreq>");
            strBuilder.AppendLine("<priority>");
            strBuilder.AppendLine("0.5");
            strBuilder.AppendLine("</priority>");
            strBuilder.AppendLine("</url>");
            #endregion

            #region KayitOl
            strBuilder.AppendLine("<url>");
            strBuilder.AppendLine("<loc>");
            //burada gelen kayitin linkini oluşturuyoruz.
            URL = "http://kazanclimani.com/kullanici/kayit-ol";
            strBuilder.AppendLine(URL);
            strBuilder.AppendLine("</loc>");
            strBuilder.AppendLine("<changefreq>");
            strBuilder.AppendLine("weekly");
            strBuilder.AppendLine("</changefreq>");
            strBuilder.AppendLine("<priority>");
            strBuilder.AppendLine("0.5");
            strBuilder.AppendLine("</priority>");
            strBuilder.AppendLine("</url>");
            #endregion

            strBuilder.AppendLine("</urlset>");

            //oluşturduğumuz string ifadeyi geri döndürüyoruz.
            return strBuilder.ToString();
        }
    }
}