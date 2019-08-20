using proje1.BusinessLayer.Abstract;
using proje1.BusinessLayer.Results;
using proje1.DataAccessLayer.EntityFramework;
using proje1.Entities;
using proje1.Entities.Messages;
using proje1.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1.BusinessLayer
{
    public class MesajYonetici : ManagerBase<Mesajlasma>
    {
        private Repository<Kullanici> repo_klnc = new Repository<Kullanici>();

        public BusinessLayerResult<Mesajlasma> MesajYukle(MesajViewModel mvm)
        {
            var gndrnId = Convert.ToInt32(mvm.GonderenKullaniciId);
            var aliciId = Convert.ToInt32(mvm.AliciKullaniciId);
            Kullanici gndrn = repo_klnc.Find(x => x.kullaniciId == gndrnId);
            Kullanici alici = repo_klnc.Find(x => x.kullaniciId == aliciId);
            BusinessLayerResult<Mesajlasma> res = new BusinessLayerResult<Mesajlasma>();
            if(alici==null || gndrn == null)
            {
                res.AddError(ErrorMessageCode.KullaniciBulunamadi, "Kullanıcı bulunamadı.");
            }
            else
            {
                int dbResult = Insert(new Mesajlasma() {
                    kullaniciGonderenId = gndrn.kullaniciId,
                    kullaniciAliciId = alici.kullaniciId,
                    mesajmetni = mvm.MesajMetni,
                    mesajtarihsaat = DateTime.Now
                });
            }
            return res;
        }
        public List<Mesajlasma> MesajlariAl(Kullanici klnc)
        {
            var kullaniciId = Convert.ToInt64(klnc.kullaniciId);
            return List(x=>x.kullaniciAliciId==kullaniciId || x.kullaniciGonderenId==kullaniciId);
        }
        public List<Kullanici> KullanicilariBul(Kullanici giris)
        {
            var kullaniciId = (giris.kullaniciId);
            int sayac = 0;
            List<Mesajlasma> msjlar = List(x => x.kullaniciAliciId == kullaniciId || x.kullaniciGonderenId == kullaniciId).OrderByDescending(x=>x.mesajtarihsaat).ToList();
            List<Kullanici> klnclar = new List<Kullanici>();
            for(int i = 0; i < msjlar.Count; i++)//Mesajlarda dön
            {
                if (klnclar.Count == 0)
                {
                    if (msjlar[i].kullaniciAliciId != giris.kullaniciId)
                    {
                        long sayi = msjlar[i].kullaniciAliciId;
                        Kullanici yeniklnc = repo_klnc.Find(x => x.kullaniciId == sayi);//Burda Kaldım!!!!!
                        klnclar.Add(yeniklnc);
                        sayac++;
                    }
                    else if(msjlar[i].kullaniciGonderenId != giris.kullaniciId)
                    {
                        long sayi = msjlar[i].kullaniciGonderenId;
                        Kullanici yeniklnc = repo_klnc.Find(x => x.kullaniciId == sayi);
                        klnclar.Add(yeniklnc);
                        sayac++;
                    }
                }
                else
                {
                    if (msjlar[i].kullaniciAliciId != giris.kullaniciId)
                    {
                        long sayi1 = msjlar[i].kullaniciAliciId;
                        Kullanici klnc = repo_klnc.Find(x => x.kullaniciId == sayi1);
                        var sayi = 0;
                        for (int j = 0; j < klnclar.Count; j++)
                        {
                            if(klnclar[j].kullaniciId == klnc.kullaniciId)
                            {
                                sayi++;
                            }
                        }
                        if (sayi == 0)
                        {
                            klnclar.Add(repo_klnc.Find(x => x.kullaniciId == sayi1));
                        }
                    }
                    else if (msjlar[i].kullaniciGonderenId != giris.kullaniciId)
                    {
                        long sayi1 = msjlar[i].kullaniciGonderenId;
                        Kullanici klnc = repo_klnc.Find(x => x.kullaniciId == sayi1);
                        var sayi = 0;
                        for (int j = 0; j < klnclar.Count; j++)
                        {
                            if (klnclar[j].kullaniciId == klnc.kullaniciId)
                            {
                                sayi++;
                            }
                        }
                        if (sayi == 0)
                        {
                            klnclar.Add(repo_klnc.Find(x => x.kullaniciId == sayi1));
                        }
                    }
                }
            }
            return klnclar;
        }
        public List<Mesajlasma> MesajlasmaCek(Kullanici giris, Kullanici gelen)
        {
            var girisId = Convert.ToInt64(giris.kullaniciId);
            var gelenId = Convert.ToInt64(gelen.kullaniciId);
            return List(x => (x.kullaniciAliciId == girisId || x.kullaniciGonderenId == girisId) && (x.kullaniciAliciId == gelenId || x.kullaniciGonderenId == gelenId));
        }
    }
}
