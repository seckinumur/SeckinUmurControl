using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeckinUmurControl.Model;

namespace SeckinUmurControl
{
    public class OnlineSistemi
    {
        public OnlineSistemi(int id)
        {
            ID = id;
            SystemCheck();
            AI();
        }
        private int ID { get; set; }
        private void AI() // Online Kontrol Mekanizması
        {
            using (seckinumurEntities db = new seckinumurEntities())
            {
                var Kontrol = db.Kullanicilars.ToList();
                DateTime time = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
                foreach (var m in Kontrol)
                {
                    if (m.Online == true)
                    {
                        DateTime Nowtime = Convert.ToDateTime(m.EklenmeTarihi);
                        TimeSpan ai = time - Nowtime;
                        int XTime = ai.Minutes;
                        if (XTime > 1)
                        {
                            m.Aktifmi = false;
                            db.SaveChanges();
                        }
                        else
                        {
                            m.Aktifmi = true;
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
        public void Online()
        {
            using (seckinumurEntities db = new seckinumurEntities())
            {
                var m = db.Kullanicilars.Where(p => p.KullanicilarID == ID).FirstOrDefault();
                m.EklenmeTarihi = DateTime.Now.ToShortTimeString();
                m.Online = true;
                m.Aktifmi = true;
                db.SaveChanges();
            }
        }
        public void Offline()
        {
            using (seckinumurEntities db = new seckinumurEntities())
            {
                var m = db.Kullanicilars.Where(p => p.KullanicilarID == ID).FirstOrDefault();
                m.Online = false;
                m.Aktifmi = false;
                db.SaveChanges();
            }
        }
        private void SystemCheck()
        {
            using (seckinumurEntities db = new seckinumurEntities())
            {
                var m = db.Kullanicilars.Where(p => p.KullanicilarID == ID).FirstOrDefault();
                if (m.Online == true)
                {
                    m.EklenmeTarihi= DateTime.Now.ToShortTimeString();
                    db.SaveChanges();
                }
            }
        }
    }
}
