using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1.Entities
{
    [Table("Fatura")]
    public class Fatura
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long faturaId { get; set; }

        public long faturaNo { get; set; }

        public DateTime satistarihsaat { get; set; }

        public long kullaniciId { get; set; }

        [ForeignKey("kullaniciId")]
        public virtual Kullanici Kullanici1 { get; set; }

        public virtual List<Sepet> Sepetler1 { get; set; }

        public Fatura()
        {
            Sepetler1 = new List<Sepet>();
        }
    }
}
