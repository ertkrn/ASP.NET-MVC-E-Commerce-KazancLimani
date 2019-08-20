using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1.Entities
{
    [Table("Kullanici")]
    public class Kullanici
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long kullaniciId { get; set; }

        [Required, StringLength(16)]
        public string kullaniciAdi { get; set; }

        [Required, StringLength(16)]
        public string kullaniciSifre { get; set; }

        [StringLength(50)]
        public string kullaniciKonum { get; set; }

        [Required,StringLength(50)]
        public string kullaniciEmail { get; set; }

        public Guid activateGuid { get; set; }

        public bool kullaniciAktifmi { get; set; }
        
        public bool adminmi { get; set; }
        
        public int kullaniciPuan { get; set; }

        [StringLength(50)]
        public string isim { get; set; }

        [StringLength(50)]
        public string soyisim { get; set; }

        [StringLength(10)]
        public string cinsiyet { get; set; }
        
        public DateTime? dogumtarihi { get; set; }

        public string profilFoto { get; set; }

        public int? ilPlaka { get; set; }


        public virtual Sehir Sehir { get; set; }
        public virtual List<Mesajlasma> Mesajlasmalar1 { get; set; }
        public virtual List<Sepet> Sepetler { get; set; }
        public virtual List<Urun> Urunler2 { get; set; }
        public virtual List<KullaniciFoto> KullaniciFotolari { get; set; }
        public virtual List<Fatura> Faturalar { get; set; }

        public Kullanici()
        {
            Mesajlasmalar1 = new List<Mesajlasma>();
            Sepetler = new List<Sepet>();
            Urunler2 = new List<Urun>();
            KullaniciFotolari = new List<KullaniciFoto>();
            Faturalar = new List<Fatura>();
        }
    }
}
