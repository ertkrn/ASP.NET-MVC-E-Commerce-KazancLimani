using proje1.Common.Helpers;
using proje1.DataAccessLayer.EntityFramework;
using proje1.Entities;
using proje1.Entities.Messages;
using proje1.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Web;
using proje1.BusinessLayer.Abstract;
using proje1.BusinessLayer.Results;

namespace proje1.BusinessLayer
{
    public class KullaniciYönetici : ManagerBase<Kullanici>
    {
        private Repository<Sehir> repo_shr = new Repository<Sehir>();
        private Repository<KullaniciFoto> repo_klncfoto = new Repository<KullaniciFoto>();

        private bool isValidContentType(string contentType)
        {
            return contentType.Equals("image/png") || contentType.Equals("image/gif") || contentType.Equals("image/jpg") || contentType.Equals("image/jpeg");
        }
        private bool isValidContentLength(int contentLength)
        {
            return ((contentLength / 1024) / 2014) < 1;
        }

        public BusinessLayerResult<Kullanici> KullaniciGuncelle(KullaniciViewModel kvm, HttpPostedFileBase file)
        {
            BusinessLayerResult<Kullanici> res = new BusinessLayerResult<Kullanici>();
            Kullanici klnc = Find(x => x.kullaniciId==kvm.KullaniciId);
            if (klnc == null)
            {
                res.AddError(ErrorMessageCode.KullaniciBulunamadi, "Kullanıcı bulunamadı.");
            }
            else if (file != null && !isValidContentType(file.ContentType))
            {
                res.AddError(ErrorMessageCode.DosyaFormat, "Sadece jpg, jpeg, png ve gif formatında yükleyebilirsiniz.");
            }
            else if (file != null && !isValidContentLength(file.ContentLength))
            {
                res.AddError(ErrorMessageCode.DosyaBoyut, "1MB'dan az bir görsel yükleyiniz.");
            }
            else
            {
                Sehir shr = repo_shr.Find(x => x.ilAdi == kvm.SehirAlani);
                string foto;
                if (file == null)
                {
                    foto = "NULL";
                }
                else
                {
                    foto = file.FileName;
                }
                klnc.kullaniciAdi = kvm.Username;
                klnc.kullaniciKonum = kvm.SehirAlani;
                klnc.kullaniciEmail = kvm.Email;
                klnc.isim = kvm.Firstname;
                klnc.soyisim = kvm.Lastname;
                klnc.cinsiyet = kvm.CinsiyetAlani;
                klnc.dogumtarihi = DateTime.ParseExact("" + kvm.YilAlani + "-" + kvm.AyAlani + "-" + kvm.GunAlani + " 12:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                klnc.ilPlaka = shr.ilPlaka;
                klnc.profilFoto = kvm.fotograf;
                Update(klnc);
            }
            return res;
        }

        public BusinessLayerResult<Kullanici> HizliKullaniciKaydi(KullaniciViewModel data)
        {
            Kullanici klnc = Find(x => x.kullaniciAdi == data.Username || x.kullaniciEmail == data.Email);
            BusinessLayerResult<Kullanici> res = new BusinessLayerResult<Kullanici>();

            if (klnc != null)
            {
                if (klnc.kullaniciAdi == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }
                if (klnc.kullaniciEmail == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExits, "Email adresi kayıtlı.");
                }
            }
            else if(data.Password != data.Repassword)
            {
                res.AddError(ErrorMessageCode.SifreAynıDegil, "Sifreler uyuşmuyor.");
            }
            else if (data.Password==null || data.Repassword==null)
            {
                res.AddError(ErrorMessageCode.SifreBos, "Sifre alanları boş geçilemez.");
            }
            else
            {
                if (data.Password.Length < 8)
                {
                    res.AddError(ErrorMessageCode.SifreKarakterUzunlugu, "Şifreniz minimum 8 karakter olmalı!");
                }
                else
                {
                    int sayi = 0, buyuk = 0, kucuk = 0, krktr = 0;
                    var sfr = data.Password.ToCharArray();
                    for (int i = 0; i < data.Password.Length; i++)
                    {
                        if (sfr[i] >= 48 && sfr[i] <= 57)
                            sayi++;
                        else if (sfr[i] >= 97 && sfr[i] <= 122)
                            kucuk++;
                        else if (sfr[i] >= 65 && sfr[i] <= 90)
                            buyuk++;
                        else
                            krktr++;
                    }
                    if (sayi > 0 && kucuk > 0 && buyuk > 0 && krktr > 0)
                    {
                        int dbResult = Insert(new Kullanici()
                        {
                            kullaniciAdi = data.Username,
                            kullaniciSifre = data.Password,
                            kullaniciEmail = data.Email,
                            adminmi = false,
                            activateGuid = Guid.NewGuid()
                        });
                        if (dbResult > 0)
                        {
                            res.Result = Find(x => x.kullaniciEmail == data.Email && x.kullaniciAdi == data.Username);
                            string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                            //string aktiveUri = $"http://localhost:65089/Kullanici/Aktivasyon/{res.Result.activateGuid}";
                            string aktiveUri = $"{siteUri}/Kullanici/Aktivasyon/{res.Result.activateGuid}";
                            string body = $"Merhaba {res.Result.kullaniciAdi};<br><br>Hesabınızı aktifleştirmek için <a href='{aktiveUri}' target='_blank'>tıklayınız</a>.";

                            MailHelper.SendMail(body, res.Result.kullaniciEmail, "KAZANÇ LİMANI Hesap Aktifleştirme");
                        }
                    }
                    else
                    {
                        res.AddError(ErrorMessageCode.SifreZorunluKarakterler, "Şifreniz 1 büyük harf, 1 küçük harf, 1 simge( - * _ vb.)'den oluşmalı ve en az 8 karakter olmalıdır!");
                    }
                }
            }
            return res;
        }

        public BusinessLayerResult<Kullanici> GirisKullanici(LoginViewModel data)
        {
            BusinessLayerResult<Kullanici> res = new BusinessLayerResult<Kullanici>();
            res.Result = Find(x => x.kullaniciAdi == data.Username && x.kullaniciSifre == data.Password);

            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı ya da şifre uyuşmuyor.");
            }
            else if (res.Result != null)
            {
                if (res.Result.kullaniciAktifmi != true)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Hesap aktifleştirilmemiş.");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "E-postanızı kontrol ediniz.");
                }
            }
            return res;
        }

        public BusinessLayerResult<Kullanici> KullaniciAktiflestir(Guid activateId)
        {
            BusinessLayerResult<Kullanici> res = new BusinessLayerResult<Kullanici>();
            res.Result = Find(x => x.activateGuid == activateId);

            if (res.Result != null)
            {
                if (res.Result.kullaniciAktifmi)
                {
                    res.AddError(ErrorMessageCode.KullaniciZatenAktif, "Kullanıcı zaten aktif edilmiştir.");
                    return res;
                }

                res.Result.kullaniciAktifmi = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.AktiflestirilecekKullaniciYok, "Aktifleştirilecek kullanıcı bulunamadı.");
            }

            return res;
        }

        public BusinessLayerResult<KullaniciFoto> KullaniciFotosuYukle(string username, HttpPostedFileBase file)
        {
            BusinessLayerResult<KullaniciFoto> res = new BusinessLayerResult<KullaniciFoto>();
            Kullanici klnc = Find(x => x.kullaniciAdi == username);
            if (klnc == null)
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı Bulunamadı.");
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
                int dbResult = repo_klncfoto.Insert(new KullaniciFoto()
                {
                    fotograf = file.FileName,
                    kullaniciId = klnc.kullaniciId
                });
            }
            return res;
        }

        public Kullanici SecilenKullaniciAdiIle(string klncadi)
        {
            Kullanici klnc = Find(x => x.kullaniciAdi == klncadi);
            return klnc;
        }
        public Kullanici SecilenKullaniciIdIle(long i)
        {
            if (i == 0)
            {
                return null;
            }
            else
            {
                Kullanici klnc = Find(x => x.kullaniciId == i);
                return klnc;
            }
        }
        public List<Sehir> SehirAl()
        {
            return repo_shr.List();
        }

        public string SehiradiAl(int i)
        {
            Sehir shr = repo_shr.Find(x => x.ilPlaka == i);
            if (shr != null)
            {
                return shr.ilAdi;
            }
            else
            {
                return "Bilinmeyen Şehir";
            }
        }
    }
}
