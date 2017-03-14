using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using SeckinUmurControl.Model;

namespace SeckinUmurControl
{
    public partial class AnaForm : MetroForm
    {
        public Giris girisFormu = (Giris)Application.OpenForms["Giris"];
        public OnlineSistemi OS;
        public int Kullaniciid;
        public bool sayac = true;
        public int yanitlaid;
        public int Silmeid;
        public string PK;
        public AnaForm()
        {
            InitializeComponent();
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'seckinumurDataSet1.Kullanicilars' table. You can move, or remove it, as needed.
            this.kullanicilarsTableAdapter1.Fill(this.seckinumurDataSet1.Kullanicilars);
            this.kullanicilarsTableAdapter.Fill(this.seckinumurDataSet.Kullanicilars);

            try
            {
                bunifuCustomDataGrid1.Refresh();
                Kisisecdata.Refresh();
                OS = new OnlineSistemi(Kullaniciid);
                VeriAl al = new VeriAl(Kullaniciid);
                KisiİsmiYeri.LabelText = al.kisi.Adi;
                SifreAl.Text = al.kisi.Sifresi;
                BtnGelen.Text = " GELEN KUTUSU  (" + al.GelenMesajAdedi().ToString() + ")";
                BtnGiden.Text = " GİDEN KUTUSU  (" + al.GidenMesajAdedi().ToString() + ")";
                BtnSilinenler.Text = " SİLİNENLER          (" + al.SilinenMesajAdedi().ToString() + ")";
                timer1.Interval = 1000;
                timer1.Start();
            }
            catch
            {
                this.Close();
                girisFormu.Visible = true;
                girisFormu.ProgressBar.Visible = true;
                girisFormu.HataMesaji.Visible = true;
                girisFormu.BtnTekarDene.Visible = true;
                girisFormu.kullaniciAdiAl.Visible = false;
                girisFormu.SifreAl.Visible = false;
                girisFormu.bunifuCustomLabel2.Visible = false;
                girisFormu.bunifuCustomLabel1.Visible = false;
                girisFormu.bunifuCustomLabel3.Visible = false;
                girisFormu.bunifuSeparator1.Visible = false;
                girisFormu.bunifuSeparator2.Visible = false;
                girisFormu.Giris1.Visible = false;
                girisFormu.KayitOl.Visible = false;
                girisFormu.timer1.Interval = 100;
                girisFormu.timer1.Start();
                MessageBox.Show("İnternet Bağlantısı Kesildi!\n İnternete Bağlanıp Yeniden Giriş Yapın!", "HATA");
            }


        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            MesajGonderAlani.Visible = true;
            if (yanitlaid != 0)
            {
                richTextBox2.Text = richTextBox1.Text;
                try
                {
                    using (seckinumurEntities db = new seckinumurEntities())
                    {
                        var bul = db.Kullanicilars.Where(p => p.KullanicilarID == yanitlaid).FirstOrDefault();
                        bunifuFlatButton7.Text = bul.Adi;
                    }
                }
                catch
                {
                    MessageBox.Show("Yanıtla Butonu Döngüsel Veri Akışı İstedi \n Ama Veritabanı Cevap Vermedi Tekrar Deneyin");
                }
            }
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            MesajGonderAlani.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progresbar.Value == 0)
            {
                progresbar.ProgressColor = Color.SeaGreen;
                timer1.Stop();
                progresbar.Value = 100;
                AnaForm_Load(sender, e);
            }
            else if (progresbar.Value == 40) { progresbar.ProgressColor = Color.Orange; }
            else if (progresbar.Value == 20) { progresbar.ProgressColor = Color.Red; }
            progresbar.Value -= 10;
        }

        private void BtnGelen_Click(object sender, EventArgs e)
        {
            MesajlarBolgesi.Visible = true;
            MesajBasligi.Visible = false;
            yanitlaid = 0;
            Silmeid = 0;
            PK = "";
            richTextBox1.Clear();
            richTextBox2.Clear();
            bunifuFlatButton7.Text = " KİŞİ SEÇ";
            MesajlarGosterdata.Rows.Clear();
            try
            {
                using (seckinumurEntities db = new seckinumurEntities())
                {
                    VeriAl al = new VeriAl(Kullaniciid);
                    int i = 0;
                    foreach (var m in al.gelenmesajlar())
                    {
                        Kullanicilars gonderenkisi = new Kullanicilars();
                        int gonderenid = m.Gonderen;
                        gonderenkisi = db.Kullanicilars.Where(p => p.KullanicilarID == gonderenid).FirstOrDefault();
                        MesajlarGosterdata.Rows.Add();
                        MesajlarGosterdata.Rows[i].Cells[0].Value = m.GelenMesajlarID;
                        MesajlarGosterdata.Rows[i].Cells[1].Value = gonderenkisi.Adi;
                        MesajlarGosterdata.Rows[i].Cells[2].Value = "GE";
                        if (m.Okundumu == false)
                        {
                            MesajlarGosterdata.RowsDefaultCellStyle.ForeColor = Color.Red;
                        }
                        i++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Mesaj Yok!");
            }

        }

        private void BtnGiden_Click(object sender, EventArgs e)
        {
            MesajlarBolgesi.Visible = true;
            MesajBasligi.Visible = false;
            yanitlaid = 0;
            Silmeid = 0;
            PK = "";
            richTextBox1.Clear();
            richTextBox2.Clear();
            bunifuFlatButton7.Text = " KİŞİ SEÇ";
            MesajlarGosterdata.Rows.Clear();
            try
            {
                using (seckinumurEntities db = new seckinumurEntities())
                {
                    VeriAl al = new VeriAl(Kullaniciid);
                    int i = 0;
                    foreach (var m in al.gidenmesajlar())
                    {
                        Kullanicilars GonderilenKisisi = new Kullanicilars();
                        int gonderilenid = m.KullanicilarID;
                        GonderilenKisisi = db.Kullanicilars.Where(p => p.KullanicilarID == gonderilenid).FirstOrDefault();
                        MesajlarGosterdata.Rows.Add();
                        MesajlarGosterdata.Rows[i].Cells[0].Value = m.GidenMesajlarID;
                        MesajlarGosterdata.Rows[i].Cells[1].Value = GonderilenKisisi.Adi;
                        MesajlarGosterdata.Rows[i].Cells[2].Value = "GI";
                        i++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Mesaj Yok!");
            }
        }

        private void BtnSilinenler_Click(object sender, EventArgs e)
        {
            MesajlarBolgesi.Visible = true;
            MesajBasligi.Visible = false;
            yanitlaid = 0;
            Silmeid = 0;
            PK = "";
            richTextBox1.Clear();
            richTextBox2.Clear();
            bunifuFlatButton7.Text = " KİŞİ SEÇ";
            MesajlarGosterdata.Rows.Clear();
            try
            {
                using (seckinumurEntities db = new seckinumurEntities())
                {
                    SilinenMesajlar silinen = new SilinenMesajlar(Kullaniciid);
                    int i = 0;
                    foreach (var m in silinen.silinengiden())
                    {
                        Kullanicilars GonderilenKisisi = new Kullanicilars();
                        int gonderilenid = m.KullanicilarID;
                        GonderilenKisisi = db.Kullanicilars.Where(p => p.KullanicilarID == gonderilenid).FirstOrDefault();
                        MesajlarGosterdata.Rows.Add();
                        MesajlarGosterdata.Rows[i].Cells[0].Value = m.GidenMesajlarID;
                        MesajlarGosterdata.Rows[i].Cells[1].Value = GonderilenKisisi.Adi;
                        MesajlarGosterdata.Rows[i].Cells[2].Value = "SGI";
                        i++;
                    }
                    int n = 0;
                    foreach (var m in silinen.silinengelen())
                    {
                        Kullanicilars gonderenkisi = new Kullanicilars();
                        int gonderenid = m.Gonderen;
                        gonderenkisi = db.Kullanicilars.Where(p => p.KullanicilarID == gonderenid).FirstOrDefault();
                        MesajlarGosterdata.Rows.Add();
                        MesajlarGosterdata.Rows[i].Cells[0].Value = m.GelenMesajlarID;
                        MesajlarGosterdata.Rows[i].Cells[1].Value = gonderenkisi.Adi;
                        MesajlarGosterdata.Rows[i].Cells[2].Value = "SGE";
                        n++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Mesaj Yok!");
            }
        }

        private void btnMesajYaz_Click(object sender, EventArgs e)
        {
            yanitlaid = 0;
            Silmeid = 0;
            PK = "";
            richTextBox1.Clear();
            richTextBox2.Clear();
            bunifuCustomLabel1.Text = "Mesaj Yaz";
            bunifuFlatButton7.Text = " KİŞİ SEÇ";
            MesajGonderAlani.Visible = true;
            MesajBasligi.Visible = false;
        }

        private void MesajlarGosterdata_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) //mesaj tıklama datagrid
        {
            richTextBox1.Clear();
            PK = "";
            Silmeid = 0;
            yanitlaid = 0;
            try
            {
                int ykoordinat = MesajlarGosterdata.CurrentCellAddress.Y;  //Seçili satırın Y koordinatı
                int SecilenCellID;
                string SecilenMesajKutusu = "";
                SecilenCellID = int.Parse(MesajlarGosterdata.Rows[ykoordinat].Cells[0].Value.ToString());
                SecilenMesajKutusu = MesajlarGosterdata.Rows[ykoordinat].Cells[2].Value.ToString();
                if (e.RowIndex == -1)
                {
                    return;
                }
                try
                {
                    using (seckinumurEntities db = new seckinumurEntities())
                    {
                        if (SecilenMesajKutusu == "GE")
                        {
                            MesajBasligi.Visible = true;
                            var bul = db.GelenMesajlars.Where(p => p.KullanicilarID == Kullaniciid && p.GelenMesajlarID == SecilenCellID && p.Silindimi == false).FirstOrDefault();
                            MesajBasligi.Text = bul.Tarih;
                            yanitlaid = bul.Gonderen;
                            PK = "GE";
                            Silmeid = bul.GelenMesajlarID;
                            richTextBox1.Text = bul.Mesaj;
                            bul.Okundumu = true;
                        }
                        else if (SecilenMesajKutusu == "GI")
                        {
                            MesajBasligi.Visible = true;
                            var bul = db.GidenMesajlars.Where(p => p.Gonderilen == Kullaniciid && p.GidenMesajlarID == SecilenCellID && p.Silindimi == false).FirstOrDefault();
                            MesajBasligi.Text = bul.Tarih;
                            yanitlaid = 0;
                            PK = "GI";
                            Silmeid = bul.GidenMesajlarID;
                            richTextBox1.Text = bul.Mesaj;
                        }
                        else if (SecilenMesajKutusu == "SGI")
                        {
                            MesajBasligi.Visible = true;
                            var bul = db.GidenMesajlars.Where(p => p.Gonderilen == Kullaniciid && p.GidenMesajlarID == SecilenCellID && p.Silindimi == true).FirstOrDefault();
                            MesajBasligi.Text = bul.Tarih;
                            yanitlaid = 0;
                            richTextBox1.Text = bul.Mesaj;
                        }
                        else if (SecilenMesajKutusu == "SGE")
                        {
                            MesajBasligi.Visible = true;
                            var bul = db.GelenMesajlars.Where(p => p.KullanicilarID == Kullaniciid && p.GelenMesajlarID == SecilenCellID && p.Silindimi == true).FirstOrDefault();
                            MesajBasligi.Text = bul.Tarih;
                            yanitlaid = 0;
                            richTextBox1.Text = bul.Mesaj;
                        }
                        db.SaveChanges();
                    }
                }
                catch
                {
                    MessageBox.Show("Mesaj Yok!");
                }
            }
            catch
            {
                MessageBox.Show("Veri Yok!");
            }
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            if (sayac == false)
            {
                KisiSecmeKarti.Visible = false;
                sayac = true;
            }
            else
            {
                KisiSecmeKarti.Visible = true;
                sayac = false;
            }
        }

        private void bunifuCustomDataGrid2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ykoordinat = Kisisecdata.CurrentCellAddress.Y;  //Seçili satırın Y koordinatı
            string SecilenKisi = "";
            SecilenKisi = Kisisecdata.Rows[ykoordinat].Cells[0].Value.ToString();
            if (e.RowIndex == -1)
            {
                return;
            }
            KisiSecmeKarti.Visible = false;
            sayac = true;
            bunifuFlatButton7.Text = SecilenKisi;
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text != "")
            {
                if (bunifuFlatButton7.Text != " KİŞİ SEÇ")
                {
                    try
                    {
                        using (seckinumurEntities db = new seckinumurEntities())
                        {
                            var bul = db.Kullanicilars.Where(p => p.Adi == bunifuFlatButton7.Text).FirstOrDefault();
                            MesajGonderme gonder = new MesajGonderme();
                            gonder.Gonder(Kullaniciid, bul.KullanicilarID, richTextBox2.Text);
                            MessageBox.Show("Mesaj Gönderildi");
                            yanitlaid = 0;
                            Silmeid = 0;
                            PK = "";
                            richTextBox1.Clear();
                            richTextBox2.Clear();
                            bunifuFlatButton7.Text = " KİŞİ SEÇ";
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Mesaj Gönderme Butonu Döngüsel Veri Akışı İstedi \n Ama Veritabanı Cevap Vermedi Tekrar Deneyin");
                    }
                }
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                try
                {
                    using (seckinumurEntities db = new seckinumurEntities())
                    {
                        var bul = db.Kullanicilars.Where(p => p.KullanicilarID == yanitlaid).FirstOrDefault();
                        if (PK == "GE")
                        {
                            MesajSilme sil = new MesajSilme(Kullaniciid, Silmeid, true);
                            yanitlaid = 0;
                            Silmeid = 0;
                            PK = "";
                            richTextBox1.Clear();
                            richTextBox2.Clear();
                            bunifuFlatButton7.Text = " KİŞİ SEÇ";
                            MesajlarGosterdata.Rows.Clear();
                            MesajBasligi.Visible = false;
                        }
                        else if (PK == "GI")
                        {
                            MesajSilme sil = new MesajSilme(Kullaniciid, Silmeid, false);
                            yanitlaid = 0;
                            Silmeid = 0;
                            PK = "";
                            richTextBox1.Clear();
                            richTextBox2.Clear();
                            bunifuFlatButton7.Text = " KİŞİ SEÇ";
                            MesajlarGosterdata.Rows.Clear();
                            MesajBasligi.Visible = false;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Silme Butonu Döngüsel Veri Akışı İstedi \n Ama Veritabanı Cevap Vermedi Tekrar Deneyin");
                }
            }
        }

        private void bunifuiOSSwitch1_OnValueChange(object sender, EventArgs e)
        {
            OS = new OnlineSistemi(Kullaniciid);
            if (Onlinetext.Text == "Çevrimiçi")
            {
                Onlinetext.Text = "Çevrimdışı";
                Onlinetext.ForeColor = Color.Red;
                bunifuCustomLabel6.ForeColor = Color.Red;
                bunifuCustomLabel6.Text = "OFFLINE";
                OS.Offline();
            }
            else
            {
                Onlinetext.Text = "Çevrimiçi";
                Onlinetext.ForeColor = Color.DarkGreen;
                bunifuCustomLabel6.ForeColor = Color.DarkGreen;
                bunifuCustomLabel6.Text = "ONLINE";
                OS.Online();
            }

        }

        private void AnaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
