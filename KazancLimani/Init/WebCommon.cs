﻿using proje1.Common;
using proje1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateArayisi.Init
{
    public class WebCommon : ICommon
    {
        public long GetCurrentUsernameId()
        {
            if (HttpContext.Current.Session["giris"] != null)
            {
                Kullanici klnc = HttpContext.Current.Session["giris"] as Kullanici;
                return klnc.kullaniciId;
            }
            return 0;
        }
        public byte[] GetCurrentValue()
        {
            if (HttpContext.Current.Session["value"] != null)
            {
                return ((byte[])HttpContext.Current.Session["value"]);
            }
            return null;
        }
        public string GetCurrentSizeValue()
        {
            if (HttpContext.Current.Session["value"] != null)
            {
                return ((byte[])HttpContext.Current.Session["value"]).Length.ToString();
            }
            return null;
        }
        public long GetUrunId()
        {
            if (HttpContext.Current.Session["urun"] != null)
            {
                Urun urn = HttpContext.Current.Session["urun"] as Urun;
                return urn.urunId;
            }
            return 0;
        }
    }
}