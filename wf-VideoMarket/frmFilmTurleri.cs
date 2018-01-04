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
    public partial class frmFilmTurleri : Form
    {
        public frmFilmTurleri()
        {
            InitializeComponent();
        }
        private int SecilenTurNo;
        private void frmFilmTurleri_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            FilmTuru ft = new FilmTuru();
            ft.FilmTurleriGoster(lvFilmTurleri);
        }
        private void Temizle()
        {
            txtFilmTuru.Clear();
            txtAciklama.Clear();
            txtFilmTuru.Focus();
        }
        private void btnYeni_Click(object sender, EventArgs e)
        {
            btnKaydet.Enabled = true;
            btnDegistir.Enabled = false;
            btnSil.Enabled = false;
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if(txtFilmTuru.Text.Trim() != "")
            {
                FilmTuru ft = new FilmTuru();
                if(ft.FilmTuruKontrol(txtFilmTuru.Text))
                {
                    MessageBox.Show("Girdiğiniz Film Türü önceden kayıtlı!", "Zaten Var!");
                    txtFilmTuru.Focus();
                }
                else
                {
                    ft.TurAd = txtFilmTuru.Text;
                    ft.Aciklama = txtAciklama.Text;
                    if(ft.FilmTuruEkle(ft))
                    {
                        MessageBox.Show("Film Türü eklendi.", "Kayıt Tamamlandı.");
                        Temizle();
                        btnKaydet.Enabled = false;
                        ft.FilmTurleriGoster(lvFilmTurleri);
                    }else { MessageBox.Show("Kayıt Gerçekleşmedi!", "Dikkat! İşlem Tamamlanmadı!"); txtFilmTuru.Focus(); }
                }
            }
            else { MessageBox.Show("Film Türü girmelisiniz!", "Eksik Bilgi"); txtFilmTuru.Focus(); }
        }
        private void lvFilmTurleri_DoubleClick(object sender, EventArgs e)
        {
            SecilenTurNo = Convert.ToInt32(lvFilmTurleri.SelectedItems[0].SubItems[0].Text);
            txtFilmTuru.Text = lvFilmTurleri.SelectedItems[0].SubItems[1].Text;
            txtAciklama.Text = lvFilmTurleri.SelectedItems[0].SubItems[2].Text;
            btnDegistir.Enabled = true;
            btnSil.Enabled = true;
            btnKaydet.Enabled = false;
            txtFilmTuru.Focus();
        }
        private void btnDegistir_Click(object sender, EventArgs e)
        {
            if (txtFilmTuru.Text.Trim() != "")
            {
                FilmTuru ft = new FilmTuru();
                if (ft.FilmTuruKontrolFromDegistir(txtFilmTuru.Text, SecilenTurNo))
                {
                    MessageBox.Show("Girdiğiniz Film Türü önceden kayıtlı!", "Zaten Var!");
                    txtFilmTuru.Focus();
                }
                else
                {
                    ft.FilmTurNo = SecilenTurNo;
                    ft.TurAd = txtFilmTuru.Text;
                    ft.Aciklama = txtAciklama.Text;
                    if (ft.FilmTuruGuncelle(ft))
                    {
                        MessageBox.Show("Film Türü güncellendi.", "Değişiklik Gerçekleşti.");
                        Temizle();
                        btnDegistir.Enabled = false;
                        btnSil.Enabled = false;
                        ft.FilmTurleriGoster(lvFilmTurleri);
                    }
                    else { MessageBox.Show("Değişiklik Gerçekleşmedi!", "Dikkat! İşlem Tamamlanmadı!"); txtFilmTuru.Focus(); }
                }
            }
            else { MessageBox.Show("Film Türü girmelisiniz!", "Eksik Bilgi"); txtFilmTuru.Focus(); }
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Silmek İstiyor musunuz?", "Silinsin mi?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FilmTuru ft = new Model.FilmTuru();
                if(ft.FilmTuruSil(SecilenTurNo))
                {
                    MessageBox.Show("Film Türü silindi.", "Silme Gerçekleşti.");
                    Temizle();
                    btnDegistir.Enabled = false;
                    btnSil.Enabled = false;
                    ft.FilmTurleriGoster(lvFilmTurleri);
                }
                else { MessageBox.Show("Silme Gerçekleşmedi!", "Dikkat! İşlem Tamamlanmadı!"); txtFilmTuru.Focus(); }
            }
        }
    }
}
