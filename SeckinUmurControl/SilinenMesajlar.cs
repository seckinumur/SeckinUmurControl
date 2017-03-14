using SeckinUmurControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeckinUmurControl
{
   public class SilinenMesajlar
    {
        public SilinenMesajlar(int id)
        {
                ID = id;
        }
       public List<GelenMesajlars> silinengelen()
        {
            using (seckinumurEntities db = new seckinumurEntities())
            {
                var m = db.GelenMesajlars.Where(p => p.KullanicilarID == ID && p.Silindimi == true).ToList();
                return m;
            }
        }
        public List<GidenMesajlars> silinengiden()
        {
            using (seckinumurEntities db = new seckinumurEntities())
            {
                var m = db.GidenMesajlars.Where(p => p.Gonderilen == ID && p.Silindimi == true).ToList();
                return m;
            }
        }
        private int ID { get; set; }
       
    }
}
