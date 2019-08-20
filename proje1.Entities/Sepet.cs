using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1.Entities
{
    [Table("Sepet")]
    public class Sepet
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long sepetId { get; set; }

        public long? faturaId { get; set; }
        public long? kullaniciId { get; set; }
        public long? urunId { get; set; }

        [ForeignKey("faturaId")]
        public virtual Fatura Fatura1 { get; set; }
        [ForeignKey("kullaniciId")]
        public virtual Kullanici Kullanici1 { get; set; }
        [ForeignKey("urunId")]
        public virtual Urun Urun1 { get; set; }
    }
}
