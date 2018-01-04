using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wf_VideoMarket.Model;

namespace wf_VideoMarket
{
    public partial class frmFilmSatis : Form
    {
        public frmFilmSatis()
        {
            InitializeComponent();
        }
        int SecilenSatisNo;
        int orjAdet;
        private void frmFilmSatis_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;

            txtTarih.Text = DateTime.Now.ToShortDateString();
            FilmSatis fs = new Model.FilmSatis();
            fs.SatislariGoster(lvSatislar, txtToplamAdet, txtToplamTutar);
        }
        private void dtpTarih_ValueChanged(object sender, EventArgs e)
        {
            txtTarih.Text = dtpTarih.Value.ToShortDateString();
        }
        private void btnMusteriBul_Click(object sender, EventArgs e)
        {
            frmMusteriSorgulama frm = new wf_VideoMarket.frmMusteriSorgulama();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            txtMusteri.Text = Genel.secilenmusteri;
        }
        private void btnFilmBul_Click(object sender, EventArgs e)
        {
            frmFilmSorgulama frm = new frmFilmSorgulama();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            txtFilm.Text = Genel.secilenfilm;
            txtStok.Text = Genel.secilenmiktar.ToString();
            txtFiyat.Text = string.Format("{0:#,##0}", Genel.secilenfiyat);
        }
        private void btnYeni_Click(object sender, EventArgs e)
        {
            btnKaydet.Enabled = true;
            btnDegistir.Enabled = false;
            btnSil.Enabled = false;
            Temizle();
            btnMusteriBul.Enabled = true;
            btnFilmBul.Enabled = true;
            btnMusteriBul_Click(sender, e);
            btnFilmBul_Click(sender, e);
            txtAdet.Focus();
        }
        private void Temizle()
        {
            txtAdet.Text = "1";
            txtFiyat.Text = "0";
            txtMusteri.Clear();
            txtFilm.Clear();
            txtStok.Clear();
            Genel.secilenfilm = "";
            Genel.secilenfilmno = 0;
            Genel.secilenmusteri = "";
            Genel.secilenmusterino = 0;
            Genel.secilenmiktar = 0;
            Genel.secilenfiyat = 0;
        }
        private void txtAdet_TextChanged(object sender, EventArgs e)
        {
            if (txtAdet.Text.Trim() == "") { txtAdet.Text = "1"; txtAdet.SelectAll(); }
            if (!string.IsNullOrEmpty(txtAdet.Text) && !string.IsNullOrEmpty(txtFiyat.Text))
            {
                txtTutar.Text = Convert.ToString(Convert.ToInt32(txtAdet.Text) * Convert.ToDecimal(txtFiyat.Text));
            }
            else { txtTutar.Text = txtFiyat.Text; }

        }
        private void txtFiyat_TextChanged(object sender, EventArgs e)
        {
            if (txtFiyat.Text.Trim() == "") { txtFiyat.Text = "0"; txtFiyat.SelectAll(); }
            if (!string.IsNullOrEmpty(txtAdet.Text) && !string.IsNullOrEmpty(txtFiyat.Text))
            {
                txtTutar.Text = Convert.ToString(Convert.ToInt32(txtAdet.Text) * Convert.ToDecimal(txtFiyat.Text));
            }
            else { txtTutar.Text = txtFiyat.Text; }
        }
        private void txtAdet_MouseClick(object sender, MouseEventArgs e)
        {
            txtAdet.SelectAll();
        }
        private void txtFiyat_MouseClick(object sender, MouseEventArgs e)
        {
            txtFiyat.SelectAll();
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtMusteri.Text != "" && txtFilm.Text != "")
            {
                if (Convert.ToInt32(txtAdet.Text) <= Convert.ToInt32(txtStok.Text))
                {
                    FilmSatis fs = new FilmSatis();
                    fs.Tarih = Convert.ToDateTime(txtTarih.Text);
                    fs.FilmNo = Genel.secilenfilmno;
                    fs.MusteriNo = Genel.secilenmusterino;
                    fs.Adet = Convert.ToInt32(txtAdet.Text);
                    fs.BirimFiyat = Convert.ToDecimal(txtFiyat.Text);
                    if (fs.SatisEkle(fs))
                    {
                        MessageBox.Show("Satış bilgileri kayıt edildi.");
                        //Film Stok miktarı güncellenmeli (satış adedi kadar azaltılmalı).
                        Film f = new Film();
                        if (f.StokGuncelleFromSatisEkle(fs.FilmNo, fs.Adet))
                        {
                            MessageBox.Show("Stok Güncellendi.");
                            fs.SatislariGoster(lvSatislar, txtToplamAdet, txtToplamTutar);
                            btnKaydet.Enabled = false;
                            btnMusteriBul.Enabled = false;
                            btnFilmBul.Enabled = false;
                            Temizle();
                        }
                        else { MessageBox.Show("Stok Güncellenemedi!"); }
                    }
                    else { MessageBox.Show("Satış Tamamlanamadı!"); }
                }
                else { MessageBox.Show("Stoktan fazla satamazsınız!", "Dikkat! Yetersiz Stok!"); txtAdet.Text = txtStok.Text; }
            }
            else { MessageBox.Show("Müşteri ve Film mutlaka seçilmelidir!", "Dikkat! Eksik Bilgi!"); }
        }
        private void lvSatislar_DoubleClick(object sender, EventArgs e)
        {
            SecilenSatisNo = Convert.ToInt32(lvSatislar.SelectedItems[0].SubItems[0].Text);
            txtTarih.Text = lvSatislar.SelectedItems[0].SubItems[1].Text;
            txtFilm.Text = lvSatislar.SelectedItems[0].SubItems[2].Text;
            txtMusteri.Text = lvSatislar.SelectedItems[0].SubItems[3].Text;
            txtAdet.Text = lvSatislar.SelectedItems[0].SubItems[5].Text;
            orjAdet = Convert.ToInt32(lvSatislar.SelectedItems[0].SubItems[5].Text);
            txtFiyat.Text = lvSatislar.SelectedItems[0].SubItems[4].Text;
            txtTutar.Text = lvSatislar.SelectedItems[0].SubItems[6].Text;
            txtStok.Text = lvSatislar.SelectedItems[0].SubItems[7].Text;
            Genel.secilenfilmno = Convert.ToInt32(lvSatislar.SelectedItems[0].SubItems[8].Text);
            Genel.secilenmusterino = Convert.ToInt32(lvSatislar.SelectedItems[0].SubItems[9].Text);
            btnKaydet.Enabled = false;
            btnDegistir.Enabled = true;
            btnSil.Enabled = true;
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmek İstiyor musunuz?", "Silinsin mi?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FilmSatis fs = new FilmSatis();
                if (fs.SatisSil(SecilenSatisNo))
                {
                    //Film Stok miktarı güncellenmeli (satış adedi kadar artırılmalı).
                    Film f = new Film();
                    if (f.StokGuncelleFromSatisSil(Genel.secilenfilmno, Convert.ToInt32(txtAdet.Text)))
                    {
                        MessageBox.Show("Stok Güncellendi.");
                        fs.SatislariGoster(lvSatislar, txtToplamAdet, txtToplamTutar);
                        btnKaydet.Enabled = false;
                        btnMusteriBul.Enabled = false;
                        btnFilmBul.Enabled = false;
                        Temizle();
                    }
                    else { MessageBox.Show("Stok Güncellenemedi!"); }
                }
                else { MessageBox.Show("Satış Tamamlanamadı!"); }
            }
            else { MessageBox.Show("Silme Gerçekleşmedi!", "Dikkat! İşlem Tamamlanmadı!"); }
        }
        private void btnDegistir_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtAdet.Text) <= (Convert.ToInt32(txtStok.Text) + orjAdet))
            {
                FilmSatis fs = new FilmSatis();
                //fs.Tarih = Convert.ToDateTime(txtTarih.Text);
                //fs.FilmNo = Genel.secilenfilmno;
                //fs.MusteriNo = Genel.secilenmusterino;
                fs.SatisNo = SecilenSatisNo;
                fs.Adet = Convert.ToInt32(txtAdet.Text);
                fs.BirimFiyat = Convert.ToDecimal(txtFiyat.Text);
                if (fs.SatisGuncelle(fs))
                {
                    MessageBox.Show("Satış bilgileri değiştirildi.");
                    if (fs.Adet != orjAdet) //Adet değişmediyse stok güncellemeye gerek yok
                    {
                        //Film Stok miktarı güncellenmeli
                        Film f = new Film();
                        if (f.StokGuncelleFromSatisDegistir(Genel.secilenfilmno,  fs.Adet, orjAdet))
                        {
                            MessageBox.Show("Stok Güncellendi.");
                        }
                        else { MessageBox.Show("Stok Güncellenemedi!"); }
                    }
                    fs.SatislariGoster(lvSatislar, txtToplamAdet, txtToplamTutar);
                    btnKaydet.Enabled = false;
                    btnMusteriBul.Enabled = false;
                    btnFilmBul.Enabled = false;
                    Temizle();
                }
                else { MessageBox.Show("Satış Tamamlanamadı!"); }
            }
            else { MessageBox.Show("Stoktan fazla satamazsınız!", "Dikkat! Yetersiz Stok!"); }
        }
    }
}

