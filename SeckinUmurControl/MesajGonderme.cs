using SeckinUmurControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeckinUmurControl
{
   public class MesajGonderme
    {
        public  void Gonder(int gonderenid,int gidenid,string mesaj)
        {
            using (seckinumurEntities db = new seckinumurEntities())
            {
              var  GonderenKisi = db.Kullanicilars.Where(p => p.KullanicilarID == gonderenid).FirstOrDefault();
               var GonderilenKisi = db.Kullanicilars.Where(p => p.KullanicilarID == gidenid).FirstOrDefault();
                GidenMesajlars GidenMesaj = new GidenMesajlars();
                GelenMesajlars GelenMesaj = new GelenMesajlars();
                GidenMesaj.Gonderilen = GonderenKisi.KullanicilarID;
                GelenMesaj.Gonderen= GonderenKisi.KullanicilarID;
                GidenMesaj.MesajKlasoruID = 2;
                GidenMesaj.ChoiceDriveID = 1;
                GelenMesaj.ChoiceDriveID = 1;
                GelenMesaj.MesajKlasoruID = 1;
                GidenMesaj.Okundumu = true;
                GelenMesaj.Okundumu = false;
                GidenMesaj.Silindimi = false;
                GelenMesaj.Silindimi = false;
                GidenMesaj.Tarih = DateTime.Now.ToString();
                GelenMesaj.Tarih= DateTime.Now.ToString();
                GidenMesaj.KullanicilarID = GonderilenKisi.KullanicilarID;
                GelenMesaj.KullanicilarID= GonderilenKisi.KullanicilarID;
                GidenMesaj.Mesaj = mesaj;
                GelenMesaj.Mesaj = mesaj;
                db.GidenMesajlars.Add(GidenMesaj);
                db.GelenMesajlars.Add(GelenMesaj);
                db.SaveChanges();
            }
        }
    }
}
