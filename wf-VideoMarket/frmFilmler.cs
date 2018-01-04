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
    public partial class frmFilmler : Form
    {
        public frmFilmler()
        {
            InitializeComponent();
        }
        int SecilenFilmNo;
        int SecilenTurNo;
        private void frmFilmler_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;

            Film f = new Film();
            f.FilmleriGoster(lvFilmler);

            FilmTuru ft = new FilmTuru();
            ft.FilmTurleriGoster(cbFilmTurleri);
        }
        private void cbFilmTurleri_SelectedIndexChanged(object sender, EventArgs e)
        {            
            //txtFilmTuru.Text = cbFilmTurleri.SelectedItem.ToString();  
            //SecilenTurNo = ft.FilmTurNoGetirByFilmTuru(txtFilmTuru.Text);

            //new ile değerleri olmayan yani property'leri boş nesneler oluşurken, biz cbFilmTurleri'nden seçilen FilmTuru nesnesinin değerlerine sahip yeni bir nesne oluşturuyoruz.
            FilmTuru ft = (FilmTuru)cbFilmTurleri.SelectedItem;
            //FilmTuru ft = cbFilmTurleri.SelectedItem as FilmTuru;
            txtFilmTuru.Text = ft.TurAd;
            SecilenTurNo = ft.FilmTurNo;
            txtYonetmen.Focus();
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
            if (txtFilmAdi.Text.Trim() != "" && txtFilmTuru.Text.Trim() != "" && txtYonetmen.Text != "")
            {
                Film f = new Film();
                f.FilmAd = txtFilmAdi.Text;
                f.Yonetmen = txtYonetmen.Text;
                if (f.FilmKontrol(f))
                {
                    MessageBox.Show("Girdiğiniz film önceden kayıtlı!", "Zaten Var!");
                    txtFilmAdi.Focus();
                }
                else
                {
                    //eksik property'leri tamamlıyoruz.
                    f.FilmTurNo = SecilenTurNo;
                    f.Oyuncular = txtOyuncular.Text;
                    f.Ozet = txtOzet.Text;
                    //Sayısal alan kontrolü
                    try
                    {
                        f.Fiyat = Convert.ToDecimal(txtFiyat.Text);
                    }
                    catch(FormatException)
                    {
                        f.Fiyat = 0;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Fiyat alanını kontrol ediniz!", "Dikkat! Hatalı Fiyat Girişi");txtFiyat.Focus(); return;
                    }
                    try
                    {
                        f.Miktar = Convert.ToInt32(txtMiktar.Text);
                    }
                    catch (FormatException)
                    {
                        f.Miktar = 1;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Miktar alanını kontrol ediniz!", "Dikkat! Hatalı Miktar Girişi"); txtMiktar.Focus(); return;
                    }
                    if (f.FilmEkle(f))
                    {
                        MessageBox.Show("Film eklendi.", "Kayıt Tamamlandı.");
                        Temizle();
                        btnKaydet.Enabled = false;
                        f.FilmleriGoster(lvFilmler);
                    }
                    else { MessageBox.Show("Kayıt Gerçekleşmedi!", "Dikkat! İşlem Tamamlanmadı!"); txtFilmAdi.Focus(); }
                }
            }
            else { MessageBox.Show("Zorunlu alanları girmelisiniz!", "Eksik Bilgi"); txtFilmAdi.Focus(); }
        }
        private void Temizle()
        {
            txtFilmAdi.Clear();
            txtYonetmen.Clear();
            txtOyuncular.Clear();
            txtOzet.Clear();
            txtFiyat.Text = "0";
            txtMiktar.Text = "1";
            txtFilmAdi.Focus();
        }
        private void lvFilmler_DoubleClick(object sender, EventArgs e)
        {
            SecilenFilmNo = Convert.ToInt32(lvFilmler.SelectedItems[0].SubItems[0].Text);
            SecilenTurNo = Convert.ToInt32(lvFilmler.SelectedItems[0].SubItems[2].Text);
            txtFilmAdi.Text = lvFilmler.SelectedItems[0].SubItems[1].Text;
            txtFilmTuru.Text = lvFilmler.SelectedItems[0].SubItems[3].Text;
            txtYonetmen.Text = lvFilmler.SelectedItems[0].SubItems[4].Text;
            txtOyuncular.Text = lvFilmler.SelectedItems[0].SubItems[5].Text;
            txtOzet.Text = lvFilmler.SelectedItems[0].SubItems[6].Text;
            txtFiyat.Text = lvFilmler.SelectedItems[0].SubItems[7].Text;
            txtMiktar.Text = lvFilmler.SelectedItems[0].SubItems[8].Text;
            btnKaydet.Enabled = false;
            btnDegistir.Enabled = true;
            btnSil.Enabled = true;
            txtFilmAdi.Focus();
        }
        private void btnDegistir_Click(object sender, EventArgs e)
        {
            if (txtFilmAdi.Text.Trim() != "" && txtFilmTuru.Text.Trim() != "" && txtYonetmen.Text != "")
            {
                Film f = new Film();
                f.FilmNo = SecilenFilmNo;
                f.FilmAd = txtFilmAdi.Text;
                f.Yonetmen = txtYonetmen.Text;
                if (f.FilmKontrolByDegistir(f))
                {
                    MessageBox.Show("Girdiğiniz film önceden kayıtlı!", "Zaten Var!");
                    txtFilmAdi.Focus();
                }
                else
                {
                    //eksik property'leri tamamlıyoruz.
                    f.FilmTurNo = SecilenTurNo;
                    f.Oyuncular = txtOyuncular.Text;
                    f.Ozet = txtOzet.Text;
                    //Sayısal alan kontrolü
                    try
                    {
                        f.Fiyat = Convert.ToDecimal(txtFiyat.Text);
                    }
                    catch (FormatException)
                    {
                        f.Fiyat = 0;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Fiyat alanını kontrol ediniz!", "Dikkat! Hatalı Fiyat Girişi"); txtFiyat.Focus(); return;
                    }
                    try
                    {
                        f.Miktar = Convert.ToInt32(txtMiktar.Text);
                    }
                    catch (FormatException)
                    {
                        f.Miktar = 1;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Miktar alanını kontrol ediniz!", "Dikkat! Hatalı Miktar Girişi"); txtMiktar.Focus(); return;
                    }
                    if (f.FilmGuncelle(f))
                    {
                        MessageBox.Show("Film güncellendi.", "Değişiklik Tamamlandı.");
                        Temizle();
                        btnDegistir.Enabled = false;
                        btnSil.Enabled = false;
                        f.FilmleriGoster(lvFilmler);
                    }
                    else { MessageBox.Show("Değişiklik Gerçekleşmedi!", "Dikkat! İşlem Tamamlanmadı!"); txtFilmAdi.Focus(); }
                }
            }
            else { MessageBox.Show("Zorunlu alanları girmelisiniz!", "Eksik Bilgi"); txtFilmAdi.Focus(); }
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmek İstiyor musunuz?", "Silinsin mi?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Film f = new Film();
                if (f.FilmSil(SecilenFilmNo))
                {
                    MessageBox.Show("Film bilgileri silindi.", "Silme Gerçekleşti.");
                    Temizle();
                    btnDegistir.Enabled = false;
                    btnSil.Enabled = false;
                    f.FilmleriGoster(lvFilmler);
                }
                else { MessageBox.Show("Silme Gerçekleşmedi!", "Dikkat! İşlem Tamamlanmadı!"); txtFilmAdi.Focus(); }
            }
        }
    }
}
