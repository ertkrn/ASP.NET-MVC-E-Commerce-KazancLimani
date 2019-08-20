using proje1.BusinessLayer.Abstract;
using proje1.DataAccessLayer.EntityFramework;
using proje1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1.BusinessLayer
{
    public class RenkYonetici : ManagerBase<Renk>
    {
        public List<Renk> RenkAl()
        {
            return List();
        }
        public string RenkKoduAlAdIle(string renkadi)
        {
            Renk renk = Find(x => x.renkAdi == renkadi);
            return renk.renkKodu;
        }
    }
}
