using proje1.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1.DataAccessLayer.EntityFramework
{
    public class ProjeContext : DbContext
    {
        public DbSet<Sehir> Sehirler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Mesajlasma> Mesajlasmalar { get; set; }
        public DbSet<Renk> Renkler { get; set; }

        public ProjeContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}
