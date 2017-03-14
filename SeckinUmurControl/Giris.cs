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
    public partial class Giris : MetroForm
    {
        int ayar = 0;
        public Giris()
        {
            InitializeComponent();
        }

        private void Giris_Load(object sender, EventArgs e)
        {
            try
            {
                using (seckinumurEntities db = new seckinumurEntities())
                {
                    var bul = db.Kullanicilars.Where(p => p.Adi == "seckinumur").FirstOrDefault();
                    if (bul.Adi == "seckinumur")
                    {
                        ayar = 1;
                        timer1.Interval = 100;
                        timer1.Start();
                    }
                    if (kullaniciAdiAl.Visible == false)
                    {
                        HataMesaji.Visible = false;
                        BtnTekarDene.Visible = false;
                        kullaniciAdiAl.Visible = true;
                        SifreAl.Visible = true;
                        bunifuCustomLabel2.Visible = true;
                        bunifuCustomLabel1.Visible = true;
                        bunifuCustomLabel3.Visible = true;
                        bunifuSeparator1.Visible = true;
                        bunifuSeparator2.Visible = true;
                        Giris1.Visible = true;
                        KayitOl.Visible = true;
                    }
                }
            }
            catch
            {
                try
                {
                    using (seckinumurEntities db = new seckinumurEntities())
                    {
                        Kullanicilars olustur = new Kullanicilars();
                        Ayarlars ayarlar = new Ayarlars();
                        ayarlar.DiskSiniri = 500;
                        ayarlar.KullanicilarID = 1;
                        ayarlar.UyelikTipi = "admin";
                        db.Ayarlars.Add(ayarlar);
                        db.SaveChanges();

                        olustur.Adi = "seckinumur";
                        olustur.Aktifmi = true;
                        olustur.AyarlarID = 1;
                        olustur.EklenmeTarihi = DateTime.Now.ToShortDateString();
                        olustur.Online = true;
                        olustur.ProfilResmi = "";
                        olustur.Sifresi = "cassini9916";
                        olustur.Yetkisi = "admin";
                        db.Kullanicilars.Add(olustur);
                        db.SaveChanges();

                        Ayarlars ayarlars = new Ayarlars();
                        ayarlars.DiskSiniri = 50;
                        ayarlars.UyelikTipi = "yerel";
                        db.Ayarlars.Add(ayarlars);
                        db.SaveChanges();
                    }
                }
                catch
                {
                    ProgressBar.Visible = true;
                    HataMesaji.Visible = true;
                    BtnTekarDene.Visible = true;
                    kullaniciAdiAl.Visible = false;
                    SifreAl.Visible = false;
                    bunifuCustomLabel2.Visible = false;
                    bunifuCustomLabel1.Visible = false;
                    bunifuCustomLabel3.Visible = false;
                    bunifuSeparator1.Visible = false;
                    bunifuSeparator2.Visible = false;
                    Giris1.Visible = false;
                    KayitOl.Visible = false;
                    timer1.Interval = 100;
                    timer1.Start();
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ProgressBar.Visible = true;
            if (ayar == 0)
            {
                if (ProgressBar.Value == 100) { ProgressBar.Value = 0; }
                ProgressBar.Value += 10;
            }
            else if (ayar == 1)
            {
                if (ProgressBar.Value == 100) { ayar = 0; timer1.Stop(); ProgressBar.Value = 0; ProgressBar.Visible = false; }
                else
                {
                    ProgressBar.Value += 10;
                }
            }
            else if (ayar == 2)
            {
                SifreYanlis.Visible = true;
                ProgressBar.ProgressColor = Color.Red;
                if (ProgressBar.Value == 100)
                {
                    ayar = 0; timer1.Stop(); ProgressBar.Value = 0; ProgressBar.Visible = false; ProgressBar.ProgressColor = Color.Orange; SifreYanlis.Visible = false;
                }
                else
                {
                    ProgressBar.Value += 10;
                }
            }
            else if (ayar == 3)
            {
                Bulunamadi.Visible = true;
                ProgressBar.ProgressColor = Color.Red;
                if (ProgressBar.Value == 100)
                {
                    ayar = 0; timer1.Stop(); ProgressBar.Value = 0; ProgressBar.Visible = false; ProgressBar.ProgressColor = Color.Orange; Bulunamadi.Visible = false;
                }
                else
                {
                    ProgressBar.Value += 10;
                }
            }
            else if (ayar == 4)
            {
                ProgressBar.ProgressColor = Color.Red;
                if (ProgressBar.Value == 100)
                {
                    ayar = 0; timer1.Stop(); ProgressBar.Value = 0; ProgressBar.Visible = false; ProgressBar.ProgressColor = Color.Orange; Bulunamadi.Visible = false; SifreAl.BackColor = Color.FromArgb(255, 255, 255);
                    kullaniciAdiAl.BackColor = Color.FromArgb(255, 255, 255);
                    KayitOl.IdleFillColor = Color.White;
                }
                else
                {
                    ProgressBar.Value += 10;
                }
            }
        }

        private void Giris1_Click(object sender, EventArgs e)
        {
            if (kullaniciAdiAl.Text == "" && SifreAl.Text == "")
            {
                ayar = 4;
                timer1.Interval = 100;
                timer1.Start();
                SifreAl.Text = "";
                SifreAl.BackColor = Color.Red;
                kullaniciAdiAl.Text = "";
                kullaniciAdiAl.BackColor = Color.Red;
                KayitOl.IdleFillColor = Color.Red;
            }
            else
            {
                try
                {
                    using (seckinumurEntities db = new seckinumurEntities())
                    {
                        var bul = db.Kullanicilars.Where(p => p.Adi == kullaniciAdiAl.Text).FirstOrDefault();
                        if (bul.Sifresi == SifreAl.Text)
                        {
                            kullaniciAdiAl.LineIdleColor = Color.Gray;
                            SifreAl.LineIdleColor = Color.Gray;
                            AnaForm ac = new AnaForm();
                            ac.Kullaniciid = bul.KullanicilarID;
                            ac.Show();
                            this.Hide();
                        }
                        else
                        {
                            ayar = 2;
                            timer1.Interval = 100;
                            timer1.Start();
                            SifreAl.Text = "";
                            SifreAl.LineIdleColor = Color.Red;
                        }
                    }
                }
                catch
                {
                    ayar = 3;
                    timer1.Interval = 100;
                    timer1.Start();
                    SifreAl.Text = "";
                    SifreAl.LineIdleColor = Color.Red;
                    kullaniciAdiAl.Text = "";
                    kullaniciAdiAl.LineIdleColor = Color.Red;
                }
            }
        }

        private void KayitOl_Click(object sender, EventArgs e)
        {
            if (kullaniciAdiAl.Text == "" && SifreAl.Text == "")
            {
                ayar = 4;
                timer1.Interval = 100;
                timer1.Start();
                SifreAl.Text = "";
                SifreAl.BackColor = Color.Red;
                kullaniciAdiAl.Text = "";
                kullaniciAdiAl.BackColor = Color.Red;
                KayitOl.IdleFillColor = Color.Red;
            }
            else
            {
                try
                {
                    using (seckinumurEntities db = new seckinumurEntities())
                    {
                        var bul = db.Kullanicilars.Where(p => p.Adi == kullaniciAdiAl.Text).FirstOrDefault();
                        if (bul.Adi == kullaniciAdiAl.Text)
                        {
                            ayar = 2;
                            timer1.Interval = 100;
                            timer1.Start();
                            Bulunamadi.Visible = true;
                            SifreAl.Text = "";
                            SifreAl.LineIdleColor = Color.Red;
                        }
                    }
                }
                catch
                {
                    try
                    {
                        using (seckinumurEntities db = new seckinumurEntities())
                        {
                            Kullanicilars olustur = new Kullanicilars();
                            olustur.Adi = kullaniciAdiAl.Text;
                            olustur.Aktifmi = true;
                            olustur.AyarlarID = 2;
                            olustur.EklenmeTarihi = DateTime.Now.ToShortDateString();
                            olustur.Online = true;
                            olustur.ProfilResmi = "";
                            olustur.Sifresi = SifreAl.Text;
                            olustur.Yetkisi = "yerel";
                            db.Kullanicilars.Add(olustur);
                            db.SaveChanges();
                            kullaniciAdiAl.Text = "";
                            SifreAl.Text = "";
                            MessageBox.Show("Şimdi Sisteme Girin!");
                        }
                    }
                    catch
                    {
                        ProgressBar.Visible = true;
                        HataMesaji.Visible = true;
                        BtnTekarDene.Visible = true;
                        kullaniciAdiAl.Visible = false;
                        SifreAl.Visible = false;
                        bunifuCustomLabel2.Visible = false;
                        bunifuCustomLabel1.Visible = false;
                        bunifuCustomLabel3.Visible = false;
                        bunifuSeparator1.Visible = false;
                        bunifuSeparator2.Visible = false;
                        Giris1.Visible = false;
                        KayitOl.Visible = false;
                        timer1.Interval = 100;
                        timer1.Start();
                    }
                }
            }
        }

        private void BtnTekarDene_Click(object sender, EventArgs e)
        {
            Giris_Load(sender, e);
        }

        private void Giris_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (kullaniciAdiAl.Text == "" && SifreAl.Text == "")
                {
                    ayar = 4;
                    timer1.Interval = 100;
                    timer1.Start();
                    SifreAl.Text = "";
                    SifreAl.BackColor = Color.Red;
                    kullaniciAdiAl.Text = "";
                    kullaniciAdiAl.BackColor = Color.Red;
                    KayitOl.IdleFillColor = Color.Red;
                }
                else
                {
                    try
                    {
                        using (seckinumurEntities db = new seckinumurEntities())
                        {
                            var bul = db.Kullanicilars.Where(p => p.Adi == kullaniciAdiAl.Text).FirstOrDefault();
                            if (bul.Sifresi == SifreAl.Text)
                            {
                                kullaniciAdiAl.LineIdleColor = Color.Gray;
                                SifreAl.LineIdleColor = Color.Gray;
                                AnaForm ac = new AnaForm();
                                ac.Kullaniciid = bul.KullanicilarID;
                                ac.Show();
                                this.Hide();
                            }
                            else
                            {
                                ayar = 2;
                                timer1.Interval = 100;
                                timer1.Start();
                                SifreAl.Text = "";
                                SifreAl.LineIdleColor = Color.Red;
                            }
                        }
                    }
                    catch
                    {
                        ayar = 3;
                        timer1.Interval = 100;
                        timer1.Start();
                        SifreAl.Text = "";
                        SifreAl.LineIdleColor = Color.Red;
                        kullaniciAdiAl.Text = "";
                        kullaniciAdiAl.LineIdleColor = Color.Red;
                    }
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }
    }
}
