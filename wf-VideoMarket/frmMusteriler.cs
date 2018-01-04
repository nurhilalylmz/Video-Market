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
    public partial class frmMusteriler : Form
    {
        public frmMusteriler()
        {
            InitializeComponent();
        }
        private int SecilenMusteriNo;
        private void frmMusteriler_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            Musteri m = new Musteri();
            m.MusterileriGoster(lvMusteriler);
        }
        private void Temizle()
        {
            txtAdi.Clear();
            txtSoyadi.Clear();
            txtTelefon.Clear();
            txtAdres.Clear();
            txtAdi.Focus();
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
            if (txtAdi.Text.Trim() != "" && txtSoyadi.Text.Trim() != "" && txtTelefon.Text != "")
            {
                Musteri m = new Musteri();
                if (m.MusteriKontrol(txtAdi.Text, txtSoyadi.Text, txtTelefon.Text))
                {
                    MessageBox.Show("Girdiğiniz müşteri önceden kayıtlı!", "Zaten Var!");
                    txtAdi.Focus();
                }
                else
                {
                    m.MusteriAd = txtAdi.Text;
                    m.MusteriSoyad = txtSoyadi.Text;
                    m.Telefon = txtTelefon.Text;
                    m.Adres = txtAdres.Text;
                    if (m.MusteriEkle(m))
                    {
                        MessageBox.Show("Müşteri eklendi.", "Kayıt Tamamlandı.");
                        Temizle();
                        btnKaydet.Enabled = false;
                        m.MusterileriGoster(lvMusteriler);
                    }
                    else { MessageBox.Show("Kayıt Gerçekleşmedi!", "Dikkat! İşlem Tamamlanmadı!"); txtAdi.Focus(); }
                }
            }
            else { MessageBox.Show("Zorunlu alanları girmelisiniz!", "Eksik Bilgi"); txtAdi.Focus(); }
        }

        private void btnDegistir_Click(object sender, EventArgs e)
        {
            if (txtAdi.Text.Trim() != "" && txtSoyadi.Text.Trim() != "" && txtTelefon.Text != "")
            {
                Musteri m = new Musteri();
                m.MusteriNo = SecilenMusteriNo;
                m.MusteriAd = txtAdi.Text;
                m.MusteriSoyad = txtSoyadi.Text;
                m.Telefon = txtTelefon.Text;
                if (m.MusteriKontrolFromDegistir(m))
                {
                    MessageBox.Show("Girdiğiniz müşteri önceden kayıtlı!", "Zaten Var!");
                    txtAdi.Focus();
                }
                else
                {
                    //Diğerlerini yukarda girmiştik.
                    m.Adres = txtAdres.Text;
                    if (m.MusteriGuncelle(m))
                    {
                        MessageBox.Show("Müşteri güncellendi.", "Değişiklik Tamamlandı.");
                        Temizle();
                        btnKaydet.Enabled = false;
                        m.MusterileriGoster(lvMusteriler);
                    }
                    else { MessageBox.Show("Kayıt Gerçekleşmedi!", "Dikkat! İşlem Tamamlanmadı!"); txtAdi.Focus(); }
                }
            }
            else { MessageBox.Show("Zorunlu alanları girmelisiniz!", "Eksik Bilgi"); txtAdi.Focus(); }
        }

        private void lvMusteriler_DoubleClick(object sender, EventArgs e)
        {
            SecilenMusteriNo = Convert.ToInt32(lvMusteriler.SelectedItems[0].SubItems[0].Text);
            txtAdi.Text = lvMusteriler.SelectedItems[0].SubItems[1].Text;
            txtSoyadi.Text = lvMusteriler.SelectedItems[0].SubItems[2].Text;
            txtTelefon.Text = lvMusteriler.SelectedItems[0].SubItems[3].Text;
            txtAdres.Text = lvMusteriler.SelectedItems[0].SubItems[4].Text;
            btnDegistir.Enabled = true;
            btnSil.Enabled = true;
            btnKaydet.Enabled = false;
            txtAdi.Focus();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmek İstiyor musunuz?", "Silinsin mi?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Musteri m = new Musteri();
                if (m.MusteriSil(SecilenMusteriNo))
                {
                    MessageBox.Show("Müşteri bilgileri silindi.", "Silme Gerçekleşti.");
                    Temizle();
                    btnDegistir.Enabled = false;
                    btnSil.Enabled = false;
                    m.MusterileriGoster(lvMusteriler);
                }
                else { MessageBox.Show("Silme Gerçekleşmedi!", "Dikkat! İşlem Tamamlanmadı!"); txtAdi.Focus(); }
            }
        }
        private void txtAdaGore_TextChanged(object sender, EventArgs e)
        {
            Musteri m = new Model.Musteri();
            m.MusterileriGosterByAdaGore(lvMusteriler, txtAdaGore.Text);
        }
    }
}
