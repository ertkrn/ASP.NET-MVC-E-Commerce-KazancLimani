using proje1.BusinessLayer.Abstract;
using proje1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1.BusinessLayer
{
    public class KategoriYonetici : ManagerBase<Kategori>
    {
        //private Repository<Kategori> repo_kategori = new Repository<Kategori>();
        
        //public List<Kategori> KategoriAl()
        //{
        //    return repo_kategori.List();
        //}

        //public Kategori KategoriAlIdile(int id)
        //{
        //    return repo_kategori.Find(x => x.kategoriId == id);
        //}
    }
}