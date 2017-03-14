using SeckinUmurControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeckinUmurControl
{
    public class VeriAl
    {
        public VeriAl(int id)
        {
            using (seckinumurEntities db = new seckinumurEntities())
            {
                kisi = db.Kullanicilars.Where(p => p.KullanicilarID == id).FirstOrDefault();
            }
        }
        public Kullanicilars kisi { get; set; }
        public override string ToString()
        {
            return kisi.Adi;
        }
        public  List<GidenMesajlars> gidenmesajlar()
        {
            using (seckinumurEntities db = new seckinumurEntities())
            {
                var m = db.GidenMesajlars.Where(p => p.Gonderilen == kisi.KullanicilarID && p.Silindimi == false).ToList();
                return m;
            }
        }
        public List<GelenMesajlars> gelenmesajlar()
        {
            using (seckinumurEntities db = new seckinumurEntities())
            {
                var m = db.GelenMesajlars.Where(p => p.KullanicilarID == kisi.KullanicilarID && p.Silindimi==false).ToList();
                return m;
            }
        }
        public int OkunmamisMesaj()
        {
            int OkunmamisMesaj;
            using (seckinumurEntities db = new seckinumurEntities())
            {
                var m = db.GelenMesajlars.Where(p => p.Okundumu == false && p.Silindimi == false && p.KullanicilarID == kisi.KullanicilarID).ToList();
               return OkunmamisMesaj = m.Count == 0 ? OkunmamisMesaj = 0 : OkunmamisMesaj = m.Count;
            }
        }
        public int GelenMesajAdedi()
        {
            int GelenMesajAdedi;
            using (seckinumurEntities db = new seckinumurEntities())
            {
                var m = db.GelenMesajlars.Where(p =>  p.Silindimi == false && p.KullanicilarID == kisi.KullanicilarID).ToList();
                return GelenMesajAdedi = m.Count == 0 ? GelenMesajAdedi = 0 : GelenMesajAdedi = m.Count;
            }
        }
        public int GidenMesajAdedi()
        {
            int Gidenmesajadedi;
            using (seckinumurEntities db = new seckinumurEntities())
            {
                var m = db.GidenMesajlars.Where(p =>  p.Silindimi == false && p.Gonderilen== kisi.KullanicilarID).ToList();
                return Gidenmesajadedi = m.Count == 0 ? Gidenmesajadedi = 0 : Gidenmesajadedi = m.Count;
            }
        }
        public int SilinenMesajAdedi()
        {
            int Silinenmesajadedi;
            int toplam = 0;
            using (seckinumurEntities db = new seckinumurEntities())
            {
                var m = db.GidenMesajlars.Where(p => p.Silindimi == true && p.Gonderilen == kisi.KullanicilarID).ToList();
                var n = db.GelenMesajlars.Where(p => p.Silindimi == true && p.KullanicilarID == kisi.KullanicilarID).ToList();
                toplam = m.Count + n.Count;
                return Silinenmesajadedi = toplam == 0 ? Silinenmesajadedi = 0 : Silinenmesajadedi = toplam;
            }
        }
    }
}
