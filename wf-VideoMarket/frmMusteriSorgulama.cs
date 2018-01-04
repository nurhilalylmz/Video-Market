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
    public partial class frmMusteriSorgulama : Form
    {
        public frmMusteriSorgulama()
        {
            InitializeComponent();
        }

        private void frmMusteriSorgulama_Load(object sender, EventArgs e)
        {
            Musteri m = new Musteri();
            m.MusterileriGoster(lvMusteriler);
        }
        private void txtAdaGore_TextChanged(object sender, EventArgs e)
        {
            MusterileriGetir();
        }
        private void txtSoyadaGore_TextChanged(object sender, EventArgs e)
        {
            MusterileriGetir();
        }
        private void txtTelefonaGore_TextChanged(object sender, EventArgs e)
        {
            MusterileriGetir();
        }
        private void txtAdreseGore_TextChanged(object sender, EventArgs e)
        {
            MusterileriGetir();
        }
        private void MusterileriGetir()
        {
            Musteri m = new Musteri();
            m.MusterileriGosterBySorgulama(lvMusteriler, txtAdaGore.Text, txtSoyadaGore.Text, txtTelefonaGore.Text, txtAdreseGore.Text);
        }
        private void lvMusteriler_DoubleClick(object sender, EventArgs e)
        {
            Genel.secilenmusterino = Convert.ToInt32(lvMusteriler.SelectedItems[0].SubItems[0].Text);
            Genel.secilenmusteri = lvMusteriler.SelectedItems[0].SubItems[1].Text + " " + lvMusteriler.SelectedItems[0].SubItems[2].Text;
            this.Close();
        }
    }
}
