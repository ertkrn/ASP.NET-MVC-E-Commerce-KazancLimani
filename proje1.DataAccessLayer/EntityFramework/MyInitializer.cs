using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using proje1.Entities;

namespace proje1.DataAccessLayer.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<ProjeContext>
    {
        protected override void Seed(ProjeContext context)
        {
            Kategori[] cat = { new Kategori() { kategoriAdi = "Araba" }, new Kategori() { kategoriAdi = "Teknoloji" }, new Kategori() { kategoriAdi = "Taşıtlar" }, new Kategori() { kategoriAdi = "Spor" }, new Kategori() { kategoriAdi = "Ev" }, new Kategori() { kategoriAdi = "Eğlence" }, new Kategori() { kategoriAdi = "Moda" }, new Kategori() { kategoriAdi = "Çocuk" }, new Kategori() { kategoriAdi = "Bahçe" }, new Kategori() { kategoriAdi = "Diğer" } };
            Sehir[] shr = {new Sehir(){ilPlaka = 1,ilAdi = "Adana"},new Sehir(){ilPlaka = 2,ilAdi = "Adıyaman"}, new Sehir() { ilPlaka = 3, ilAdi = "Afyon" }, new Sehir() { ilPlaka = 4, ilAdi = "Ağrı" }, new Sehir() { ilPlaka = 5, ilAdi = "Amasya" }, new Sehir() { ilPlaka = 6, ilAdi = "Ankara" }, new Sehir() { ilPlaka = 7, ilAdi = "Antalya" }, new Sehir() { ilPlaka = 8, ilAdi = "Artvin" }, new Sehir() { ilPlaka = 9, ilAdi = "Aydın" }, new Sehir() { ilPlaka = 10, ilAdi = "Balıkesir" }, new Sehir() { ilPlaka = 11, ilAdi = "Bilecik" }, new Sehir() { ilPlaka = 12, ilAdi = "Bingöl" }, new Sehir() { ilPlaka = 13, ilAdi = "Bitlis" }, new Sehir() { ilPlaka = 14, ilAdi = "Bolu" }, new Sehir() { ilPlaka = 15, ilAdi = "Burdur" }, new Sehir() { ilPlaka = 16, ilAdi = "Bursa" }, new Sehir() { ilPlaka = 17, ilAdi = "Çanakkale" }, new Sehir() { ilPlaka = 18, ilAdi = "Çankırı" }, new Sehir() { ilPlaka = 19, ilAdi = "Çorum" }, new Sehir() { ilPlaka = 20, ilAdi = "Denizli" }, new Sehir() { ilPlaka = 21, ilAdi = "Diyarbakır" }, new Sehir() { ilPlaka = 22, ilAdi = "Edirne" }, new Sehir() { ilPlaka = 23, ilAdi = "Elazığ" }, new Sehir() { ilPlaka = 24, ilAdi = "Erzincan" }, new Sehir() { ilPlaka = 25, ilAdi = "Erzurum" }, new Sehir() { ilPlaka = 26, ilAdi = "Eskişehir" }, new Sehir() { ilPlaka = 27, ilAdi = "Gaziantep" }, new Sehir() { ilPlaka = 28, ilAdi = "Giresun" }, new Sehir() { ilPlaka = 29, ilAdi = "Gümüşhane" }, new Sehir() { ilPlaka = 30, ilAdi = "Hakkari" }, new Sehir() { ilPlaka = 31, ilAdi = "Hatay" }, new Sehir() { ilPlaka = 32, ilAdi = "Isparta" }, new Sehir() { ilPlaka = 33, ilAdi = "İçel" }, new Sehir() { ilPlaka = 34, ilAdi = "İstanbul" }, new Sehir() { ilPlaka = 35, ilAdi = "İzmir" }, new Sehir() { ilPlaka = 36, ilAdi = "Kars" }, new Sehir() { ilPlaka = 37, ilAdi = "Kastamonu" }, new Sehir() { ilPlaka = 38, ilAdi = "Kayseri" }, new Sehir() { ilPlaka = 39, ilAdi = "Kırklareli" }, new Sehir() { ilPlaka = 40, ilAdi = "Kırşehir" }, new Sehir() { ilPlaka = 41, ilAdi = "Kocaeli" }, new Sehir() { ilPlaka = 42, ilAdi = "Konya" }, new Sehir() { ilPlaka = 43, ilAdi = "Kütahya" }, new Sehir() { ilPlaka = 44, ilAdi = "Malatya" }, new Sehir() { ilPlaka = 45, ilAdi = "Manisa" }, new Sehir() { ilPlaka = 46, ilAdi = "Kahramanmaraş" }, new Sehir() { ilPlaka = 47, ilAdi = "Mardin" }, new Sehir() { ilPlaka = 48, ilAdi = "Muğla" }, new Sehir() { ilPlaka = 49, ilAdi = "Muş" }, new Sehir() { ilPlaka = 50, ilAdi = "Nevşehir" }, new Sehir() { ilPlaka = 51, ilAdi = "Niğde" }, new Sehir() { ilPlaka = 52, ilAdi = "Ordu" }, new Sehir() { ilPlaka = 53, ilAdi = "Rize" }, new Sehir() { ilPlaka = 54, ilAdi = "Sakarya" }, new Sehir() { ilPlaka = 55, ilAdi = "Samsun" }, new Sehir() { ilPlaka = 56, ilAdi = "Siirt" }, new Sehir() { ilPlaka = 57, ilAdi = "Sinop" }, new Sehir() { ilPlaka = 58, ilAdi = "Sivas" }, new Sehir() { ilPlaka = 59, ilAdi = "Tekirdağ" }, new Sehir() { ilPlaka = 60, ilAdi = "Tokat" }, new Sehir() { ilPlaka = 61, ilAdi = "Trabzon" }, new Sehir() { ilPlaka = 62, ilAdi = "Tunceli" }, new Sehir() { ilPlaka = 63, ilAdi = "Şanlıurfa" }, new Sehir() { ilPlaka = 64, ilAdi = "Uşak" }, new Sehir() { ilPlaka = 65, ilAdi = "Van" }, new Sehir() { ilPlaka = 66, ilAdi = "Yozgat" }, new Sehir() { ilPlaka = 67, ilAdi = "Zonguldak" }, new Sehir() { ilPlaka = 68, ilAdi = "Aksaray" }, new Sehir() { ilPlaka = 69, ilAdi = "Bayburt" }, new Sehir() { ilPlaka = 70, ilAdi = "Karaman" }, new Sehir() { ilPlaka = 71, ilAdi = "Kırıkkale" }, new Sehir() { ilPlaka = 72, ilAdi = "Batman" }, new Sehir() { ilPlaka = 73, ilAdi = "Şırnak" }, new Sehir() { ilPlaka = 74, ilAdi = "Bartın" }, new Sehir() { ilPlaka = 75, ilAdi = "Ardahan" }, new Sehir() { ilPlaka = 76, ilAdi = "Iğdır" }, new Sehir() { ilPlaka = 77, ilAdi = "Yalova" }, new Sehir() { ilPlaka = 78, ilAdi = "Karabük" }, new Sehir() { ilPlaka = 79, ilAdi = "Kilis" }, new Sehir() { ilPlaka = 80, ilAdi = "Osmaniye" }, new Sehir() { ilPlaka = 81, ilAdi = "Düzce" } };
            Renk[] rnk = { new Renk() { renkAdi = "Siyah", renkKodu = "#000000" }, new Renk() { renkAdi = "Mavi", renkKodu = "#0000FF" }, new Renk() { renkAdi = "Kahverengi", renkKodu = "#A52A2A" }, new Renk() { renkAdi = "Lacivert", renkKodu = "#00008B" }, new Renk() { renkAdi = "Bordo", renkKodu = "#8B0000" }, new Renk() { renkAdi = "Altın", renkKodu = "#FFD700" }, new Renk() { renkAdi = "Gri", renkKodu = "#808080" }, new Renk() { renkAdi = "Yeşil", renkKodu = "#008000" }, new Renk() { renkAdi = "Turuncu", renkKodu = "#FFA500" }, new Renk() { renkAdi = "Pembe", renkKodu = "#FFC0CB" }, new Renk() { renkAdi = "Mor", renkKodu = "#800080" }, new Renk() { renkAdi = "Kırmızı", renkKodu = "#FF0000" }, new Renk() { renkAdi = "Gümüş", renkKodu = "#C0C0C0" }, new Renk() { renkAdi = "Beyaz", renkKodu = "#FFFFFF" }, new Renk() { renkAdi = "Sarı", renkKodu = "#FFFF00" } };
            Kullanici[] klnc = { new Kullanici() { kullaniciAdi = "ertkrn", kullaniciSifre = "asd123", kullaniciKonum = "Trabzon", kullaniciEmail = "abcd@gmail.com", activateGuid = Guid.NewGuid(), kullaniciPuan = FakeData.NumberData.GetNumber(0, 100), kullaniciAktifmi = true, isim = "Ertuğrul", adminmi=true, soyisim = "Kuran", cinsiyet = "BAY", dogumtarihi = DateTime.ParseExact("1997-10-29 12:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture) }};
            shr[60].Kullanicilar1.Add(klnc[0]);
            
            for (int n = 0; n < 15; n++)
            {
                context.Renkler.Add(rnk[n]);
            }
            for (int m = 0; m < 81; m++)
            {
                context.Sehirler.Add(shr[m]);
            }
            for (int k = 0; k < 1; k++)
            {
                context.Kullanicilar.Add(klnc[k]);
            }
            for (int i = 0; i < 10; i++)
            {
                context.Kategoriler.Add(cat[i]);
                context.SaveChanges();
                List<Kullanici> klnc1 = context.Kullanicilar.ToList();
                for (int j = 0; j < FakeData.NumberData.GetNumber(2, 5); j++)
                {
                    long sayi = klnc1[FakeData.NumberData.GetNumber(0, klnc1.Count)].kullaniciId;
                    long sayi2 = klnc1[FakeData.NumberData.GetNumber(0, klnc1.Count)].kullaniciId;
                    if (sayi != sayi2)
                    {
                        Mesajlasma msj = new Mesajlasma()
                        {
                            kullaniciAliciId = sayi2,
                            mesajmetni = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(1, 100)),
                            mesajtarihsaat = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now)
                        };
                        klnc1[Convert.ToInt32(sayi-1)].Mesajlasmalar1.Add(msj);
                    }
                    Urun urn = new Urun()
                    {
                        ilanId = FakeData.NumberData.GetNumber(100000000, 999999999),
                        urunBaslik = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        urunFiyati = FakeData.NumberData.GetNumber(100, 1000),
                        satistarihsaat = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        urunKonum = FakeData.PlaceData.GetAddress(),
                        ilPlaka = FakeData.NumberData.GetNumber(1, 81),
                        takasyapilirmi = FakeData.BooleanData.GetBoolean(),
                        halasatilikmi = FakeData.BooleanData.GetBoolean(),
                        durumu = FakeData.BooleanData.GetBoolean()
                    };
                    rnk[i].Urunler.Add(urn);
                    klnc1[Convert.ToInt32(sayi-1)].Urunler2.Add(urn);
                    cat[i].Urunler3.Add(urn);
                }
            }
        }
    }
}
