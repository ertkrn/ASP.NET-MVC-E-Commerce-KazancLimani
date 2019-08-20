using proje1.BusinessLayer.Abstract;
using proje1.BusinessLayer.Results;
using proje1.Common;
using proje1.DataAccessLayer.EntityFramework;
using proje1.Entities;
using proje1.Entities.Messages;
using proje1.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace proje1.BusinessLayer
{
    public class UrunYonetici : ManagerBase<Urun>
    {
        private Repository<Kullanici> repo_kullanici = new Repository<Kullanici>();
        private Repository<Kategori> repo_kategori = new Repository<Kategori>();
        private Repository<Sehir> repo_shr = new Repository<Sehir>();
        private Repository<Renk> repo_rnk = new Repository<Renk>();
        private Repository<UrunFoto> repo_urnfoto = new Repository<UrunFoto>();

        private bool isValidContentType(string contentType)
        {
            return contentType.Equals("image/png") || contentType.Equals("image/gif") || contentType.Equals("image/jpg") || contentType.Equals("image/jpeg");
        }
        private bool isValidContentLength(int contentLength)
        {
            return ((contentLength / 1024) / 2014) < 10;
        }

        public BusinessLayerResult<Urun> UrunKayit(UrunViewModel data, HttpPostedFileBase file)
        {
            Urun urn = Find(x => x.urunBaslik == data.UrunBaslik && x.urunAciklama == data.UrunAciklama);
            BusinessLayerResult<Urun> res = new BusinessLayerResult<Urun>();

            if (urn != null)
            {
                res.AddError(ErrorMessageCode.UrunKayitli, "Ürün kayıtlı.");
            }
            else if (!isValidContentType(file.ContentType))
            {
                res.AddError(ErrorMessageCode.DosyaFormat, "Sadece jpg, jpeg, png ve gif formatında yükleyebilirsiniz.");
            }
            else if (!isValidContentLength(file.ContentLength))
            {
                res.AddError(ErrorMessageCode.DosyaBoyut, "10MB'dan az bir görsel yükleyiniz.");
            }
            else
            {
                Random rastgele = new Random();
                long sayi = 0;
                while (true)
                {
                    sayi = rastgele.Next(11111111, 99999999);
                    if (Find(x => x.ilanId == sayi) == null)
                        break;
                }
                Kategori kat = repo_kategori.Find(x => x.kategoriAdi == data.KategoriAdi);
                Sehir shr = repo_shr.Find(x => x.ilAdi == data.SehirAlani);
                Renk rnk = repo_rnk.Find(x => x.renkAdi == data.UrunRenkAdi);
                int dbResult = Insert(new Urun()
                {
                    ilanId = sayi,
                    urunBaslik = data.UrunBaslik,
                    urunFiyati = data.UrunFiyat,
                    renkId = rnk.renkId,
                    urunKonum = data.UrunKonum,
                    ilPlaka = shr.ilPlaka,
                    kategoriId = kat.kategoriId,
                    takasyapilirmi = data.Takas,
                    durumu = data.durumu,
                    urunAciklama = data.UrunAciklama,
                    halasatilikmi = true,
                    kullaniciId = App.Common.GetCurrentUsernameId(),
                    satistarihsaat = DateTime.Now,
                    urunFotosu = data.Fotograf
                });
                if (dbResult > 0)
                {
                    res.Result = Find(x => x.urunBaslik == data.UrunBaslik && x.urunKonum == data.UrunKonum);
                }
            }
            return res;
        }

        public BusinessLayerResult<Urun> ilanIdSorgulama(long ilanId, long urunId)
        {
            BusinessLayerResult<Urun> res = new BusinessLayerResult<Urun>();
            Urun urn = Find(x => x.ilanId == ilanId);
            if (urn != null && urn.urunId != urunId)
            {
                res.AddError(ErrorMessageCode.IlanIdKayitli, "ilanId zaten kayıtlı...");
            }
            return res;
        }

        public string UrunDurumu(bool type)
        {
            if (!type)
            {
                return "Sıfır";
            }
            else
            {
                return "İkinci El";
            }
        }

        public string Takas(bool type)
        {
            if (!type)
            {
                return "Hayır";
            }
            else
            {
                return "Evet";
            }
        }

        public BusinessLayerResult<UrunFoto> UrunFotosuYukle(string urunbaslik, HttpPostedFileBase file)
        {
            BusinessLayerResult<UrunFoto> res = new BusinessLayerResult<UrunFoto>();
            Urun urn = Find(x => x.urunBaslik == urunbaslik);
            if (urn == null)
            {
                res.AddError(ErrorMessageCode.UrunBulunamadi, "Ürün Bulunamadı.");
            }
            else if (!isValidContentType(file.ContentType))
            {
                res.AddError(ErrorMessageCode.DosyaFormat, "Sadece jpg, jpeg, png ve gif formatında yükleyebilirsiniz.");
            }
            else if (!isValidContentLength(file.ContentLength))
            {
                res.AddError(ErrorMessageCode.DosyaBoyut, "1MB'dan az bir görsel yükleyiniz.");
            }
            else
            {
                int dbResult = repo_urnfoto.Insert(new UrunFoto()
                {
                    fotograf = file.FileName,
                    urunId = urn.urunId
                });
            }
            return res;
        }


        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Username { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password)]
        public string Password { get; set; }

        public List<Urun> UrunleriAl()
        {
            return List();
        }

        public IQueryable<Urun> UrunleriAlQueryable()
        {
            return ListQueryable();
        }

        public string Klncadi(long i)
        {
            Kullanici klnc = repo_kullanici.Find(x => x.kullaniciId == i);
            if (klnc != null)
            {
                return klnc.kullaniciAdi;
            }
            else
            {
                return "Bilinmeyen Kullanici";
            }
        }
        public string AdSoyad(long i)
        {
            Kullanici klnc = repo_kullanici.Find(x => x.kullaniciId == i);
            if (klnc != null)
            {
                return klnc.isim + " " + klnc.soyisim;
            }
            else
            {
                return "Bilinmeyen Kullanici";
            }
        }
        public string AdGetir(long i)
        {
            Kullanici klnc = repo_kullanici.Find(x => x.kullaniciId == i);
            if (klnc != null)
            {
                return klnc.isim;
            }
            else
            {
                return "Bilinmeyen Kullanici Adi";
            }
        }
        public string Kategoriadi(long i)
        {
            Kategori kat = repo_kategori.Find(x => x.kategoriId == i);
            if (kat != null)
            {
                return kat.kategoriAdi;
            }
            else
            {
                return "Bilinmeyen Kategori";
            }
        }
        public string SehirAl(int plaka)
        {
            Sehir shr = repo_shr.Find(x => x.ilPlaka == plaka);
            return shr.ilAdi;
        }
    }
}
