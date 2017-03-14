using SeckinUmurControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeckinUmurControl
{
   public class MesajSilme
    {
        public MesajSilme(int id,int mesajid, bool gelenmesaj)
        {
            using (seckinumurEntities db = new seckinumurEntities())
            {
                if (gelenmesaj == true)
                {
                    var sil = db.GelenMesajlars.Where(p => p.GelenMesajlarID == mesajid && p.KullanicilarID==id).FirstOrDefault();
                    sil.Silindimi = true;
                    db.SaveChanges();
                }
                else
                {
                    var sil = db.GidenMesajlars.Where(p => p.GidenMesajlarID == mesajid && p.Gonderilen == id).FirstOrDefault();
                    sil.Silindimi = true;
                    db.SaveChanges();
                }
            }
        }
       
        public Kullanicilars Kisi { get; set; }

    }
}
