using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1.Entities
{
    [Table("Kategori")]
    public class Kategori
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long kategoriId { get; set; }

        [Required, StringLength(30)]
        public string kategoriAdi { get; set; }

        public virtual List<Urun> Urunler3 { get; set; }

        public Kategori()
        {
            Urunler3 = new List<Urun>();
        }
    }
}
