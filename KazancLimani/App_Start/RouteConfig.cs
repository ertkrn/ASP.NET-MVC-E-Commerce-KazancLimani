using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace TemplateArayisi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ErrorPage",
                url: "yakinda",
                defaults: new { controller = "Home", action = "ErrorPage" }
            );

            routes.MapRoute(
                name: "UrunDetay",
                url: "admin/urun-detayi",
                defaults: new { controller = "Yonetim", action = "UrunDetay" }
            );

            routes.MapRoute(
                name: "UrunDuzenle",
                url: "admin/urunleri-duzenle",
                defaults: new { controller = "Yonetim", action = "UrunDuzenle" }
            );

            routes.MapRoute(
                name: "UrunListele",
                url: "admin/urunleri-listele",
                defaults: new { controller = "Yonetim", action = "UrunListele" }
            );

            routes.MapRoute(
                name: "UrunOlustur",
                url: "admin/urunleri-olustur",
                defaults: new { controller = "Yonetim", action = "UrunOlustur" }
            );

            routes.MapRoute(
                name: "UrunSil",
                url: "admin/urunleri-sil",
                defaults: new { controller = "Yonetim", action = "UrunSil" }
            );

            routes.MapRoute(
                name: "UrunYonetim",
                url: "admin/urunleri-yonet",
                defaults: new { controller = "Yonetim", action = "UrunYonetim" }
            );

            routes.MapRoute(
                name: "SehirDetay",
                url: "admin/sehir-detayi",
                defaults: new { controller = "Yonetim", action = "SehirDetay" }
            );

            routes.MapRoute(
                name: "SehirDuzenle",
                url: "admin/sehirleri-duzenle",
                defaults: new { controller = "Yonetim", action = "SehirDuzenle" }
            );

            routes.MapRoute(
                name: "SehirOlustur",
                url: "admin/sehirleri-olustur",
                defaults: new { controller = "Yonetim", action = "SehirOlustur" }
            );

            routes.MapRoute(
                name: "SehirSil",
                url: "admin/sehirleri-sil",
                defaults: new { controller = "Yonetim", action = "SehirSil" }
            );

            routes.MapRoute(
                name: "SehirYonetim",
                url: "admin/sehirleri-yonet",
                defaults: new { controller = "Yonetim", action = "SehirYonetim" }
            );

            routes.MapRoute(
                name: "RenkDetay",
                url: "admin/renk-detayi",
                defaults: new { controller = "Yonetim", action = "RenkDetay" }
            );

            routes.MapRoute(
                name: "RenkDuzenle",
                url: "admin/renkleri-duzenle",
                defaults: new { controller = "Yonetim", action = "RenkDuzenle" }
            );

            routes.MapRoute(
                name: "RenkOlustur",
                url: "admin/renkleri-olustur",
                defaults: new { controller = "Yonetim", action = "RenkOlustur" }
            );

            routes.MapRoute(
                name: "RenkSil",
                url: "admin/renkleri-sil",
                defaults: new { controller = "Yonetim", action = "RenkSil" }
            );

            routes.MapRoute(
                name: "RenkYonetim",
                url: "admin/renkleri-yonet",
                defaults: new { controller = "Yonetim", action = "RenkYonetim" }
            );

            routes.MapRoute(
                name: "MesajDetay",
                url: "admin/mesaj-detayi",
                defaults: new { controller = "Yonetim", action = "MesajDetay" }
            );

            routes.MapRoute(
                name: "MesajDuzenle",
                url: "admin/mesajlari-duzenle",
                defaults: new { controller = "Yonetim", action = "MesajDuzenle" }
            );

            routes.MapRoute(
                name: "MesajOlustur",
                url: "admin/mesajlari-olustur",
                defaults: new { controller = "Yonetim", action = "MesajOlustur" }
            );

            routes.MapRoute(
                name: "MesajSil",
                url: "admin/mesajlari-sil",
                defaults: new { controller = "Yonetim", action = "MesajSil" }
            );

            routes.MapRoute(
                name: "MesajYonetim",
                url: "admin/mesajlari-yonet",
                defaults: new { controller = "Yonetim", action = "MesajYonetim" }
            );

            routes.MapRoute(
                name: "KullaniciDetay",
                url: "admin/kullanici-detaylari",
                defaults: new { controller = "Yonetim", action = "KullaniciDetay" }
            );

            routes.MapRoute(
                name: "KullaniciDuzenle",
                url: "admin/kullanicilari-duzenle",
                defaults: new { controller = "Yonetim", action = "KullaniciDuzenle" }
            );

            routes.MapRoute(
                name: "KullaniciOlustur",
                url: "admin/kullanicilari-olustur",
                defaults: new { controller = "Yonetim", action = "KullaniciOlustur" }
            );

            routes.MapRoute(
                name: "KullaniciSil",
                url: "admin/kullanicilari-sil",
                defaults: new { controller = "Yonetim", action = "KullaniciSil" }
            );

            routes.MapRoute(
                name: "KullaniciYonetim",
                url: "admin/kullanicilari-yonet",
                defaults: new { controller = "Yonetim", action = "KullaniciYonetim" }
            );

            routes.MapRoute(
                name: "KategoriDetay",
                url: "admin/kategori-detayi",
                defaults: new { controller = "Yonetim", action = "KategoriDetay" }
            );

            routes.MapRoute(
                name: "KategoriDuzenle",
                url: "admin/kategorileri-duzenle",
                defaults: new { controller = "Yonetim", action = "KategoriDuzenle" }
            );

            routes.MapRoute(
                name: "KategoriOlustur",
                url: "admin/yeni-kategori-olustur",
                defaults: new { controller = "Yonetim", action = "KategoriOlustur" }
            );

            routes.MapRoute(
                name: "KategoriSil",
                url: "admin/kategorileri-sil",
                defaults: new { controller = "Yonetim", action = "KategoriSil" }
            );

            routes.MapRoute(
                name: "KategoriYonetim",
                url: "admin/kategorileri-yonet",
                defaults: new { controller = "Yonetim", action = "KategoriYonetim" }
            );

            routes.MapRoute(
                name: "SattigimiDuzenle",
                url: "sattiklarim/duzenle/{id}",
                defaults: new { controller = "Urun", action = "UrunDuzenle", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SattigimDetay",
                url: "sattiklarim/detay/{id}",
                defaults: new { controller = "Urun", action = "UrunDetay", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Yukle",
                url: "urun-yukle",
                defaults: new { controller = "Urun", action = "Yukle" }
            );

            routes.MapRoute(
                name: "Sepetim",
                url: "sepetim",
                defaults: new { controller = "Profil", action = "Sepetim" }
            );

            routes.MapRoute(
                name: "HesabiSil",
                url: "hesabi-sil",
                defaults: new { controller = "Profil", action = "HesabiSil" }
            );

            routes.MapRoute(
                name: "Select",
                url: "profil/{id}",
                defaults: new { controller = "Profil", action = "Select", id = "" }
            );

            routes.MapRoute(
                name: "Sattiklarim",
                url: "sattiklarim",
                defaults: new { controller = "Profil", action = "Sattiklarim" }
            );

            routes.MapRoute(
                name: "Mesajlarim",
                url: "mesajlarim",
                defaults: new { controller = "Profil", action = "Mesajlarim" }
            );

            routes.MapRoute(
                name: "Hesabim",
                url: "hesabim",
                defaults: new { controller = "Profil", action = "Hesabim" }
            );

            routes.MapRoute(
                name: "OdemeSatinAlim",
                url: "odeme-yap",
                defaults: new { controller = "Kullanici", action = "OdemeSatinAlim" }
            );

            routes.MapRoute(
                name: "AktivasyonIptal",
                url: "hesap/aktivasyon-iptal",
                defaults: new { controller = "Kullanici", action = "AktivasyonIptal" }
            );

            routes.MapRoute(
                name: "AktivasyonTamam",
                url: "hesap/aktivasyon-basarili",
                defaults: new { controller = "Kullanici", action = "AktivasyonTamam" }
            );

            routes.MapRoute(
                name: "KayitOnay",
                url: "kullanici/kayit-onaylandi",
                defaults: new { controller = "Home", action = "KayitOnay" }
            );

            routes.MapRoute(
                name: "Kayit",
                url: "kullanici/kayit-ol",
                defaults: new { controller = "Home", action = "Kayit" }
            );

            routes.MapRoute(
                name: "Giris",
                url: "kullanici/giris-yap",
                defaults: new { controller = "Home", action = "Giris" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        //private class EncryptedRoute : Route, IRequiresSessionState
        //{
        //    RouteCollection routes = new RouteCollection();
        //    public EncryptedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
        //        : base(url, defaults, routeHandler)
        //    {
        //        RegisterRoutesInternal(routes);
        //    }

        //    public override RouteData GetRouteData(HttpContextBase httpContext)
        //    {
        //        string encodedUrl = httpContext.Request.RawUrl;
        //        string[] splitUrl = encodedUrl.Split(new string[] { "?url=" }, StringSplitOptions.None);
        //        string url = string.Empty;
        //        RouteData rd = new RouteData();
        //        if (splitUrl.Count() > 1)
        //        {
        //            url = Decrypt(splitUrl[1]).Replace("?id=", "");
        //            rd = routes.GetRouteData(new HttpContextInjector(HttpContext.Current, new HttpRequestInjector(HttpContext.Current.Request, "~" + url)));
        //        }
        //        else
        //        {
        //            rd = routes.GetRouteData(new HttpContextInjector(HttpContext.Current, new HttpRequestInjector(HttpContext.Current.Request, "~" + splitUrl[0])));
        //        }


        //        return rd;
        //    }

        //    private string Decrypt(string encryptedText)
        //    {
        //        string key = "www.ozerkaya.info";
        //        byte[] DecryptKey = { };
        //        byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
        //        byte[] inputByte = new byte[encryptedText.Length];
        //        DecryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
        //        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //        inputByte = Convert.FromBase64String(encryptedText);
        //        MemoryStream ms = new MemoryStream();
        //        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write);
        //        cs.Write(inputByte, 0, inputByte.Length);
        //        cs.FlushFinalBlock();
        //        System.Text.Encoding encoding = System.Text.Encoding.UTF8;
        //        return encoding.GetString(ms.ToArray());
        //    }

        //    private sealed class HttpContextInjector : HttpContextWrapper
        //    {
        //        private HttpRequestBase _request;

        //        public HttpContextInjector(HttpContext httpContext, HttpRequestBase request)
        //            : base(httpContext)
        //        {
        //            _request = request;
        //        }

        //        public override HttpRequestBase Request
        //        {
        //            get { return _request; }
        //        }
        //    }

        //    private sealed class HttpRequestInjector : HttpRequestWrapper
        //    {
        //        private string _appRelativeCurrentExecutionFilePath;

        //        public HttpRequestInjector(HttpRequest httpRequest, string appRelativeCurrentExecutionFilePath)
        //            : base(httpRequest)
        //        {
        //            _appRelativeCurrentExecutionFilePath = appRelativeCurrentExecutionFilePath;
        //        }

        //        public override string AppRelativeCurrentExecutionFilePath
        //        {
        //            get { return _appRelativeCurrentExecutionFilePath; }
        //        }
        //    }


        //}
        //public static void RegisterRoutesInternal(RouteCollection routes)
        //{
        //    //routes.MapRoute(
        //    //           name: "About",
        //    //           url: "hakkimda/hakkimda",
        //    //         defaults: new { controller = "Home", action = "About" });
        //    routes.MapRoute(
        //         name: "Default",
        //         url: "{controller}/{action}/{id}",
        //         defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        //     );
        //}
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.Add(new EncryptedRoute("EncodeUrl/{url}", null, null));
        //    RegisterRoutesInternal(routes);
        //}
    }
}
