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
    public partial class frmFilmSorgulama : Form
    {
        public frmFilmSorgulama()
        {
            InitializeComponent();
        }

        private void frmFilmSorgulama_Load(object sender, EventArgs e)
        {
            Film f = new Film();
            f.FilmleriGoster(lvFilmler);

            FilmTuru ft = new FilmTuru();
            ft.FilmTurleriGoster(cbFilmTurleri);
            //cbFilmTurleri.Items.Add("Tüm Türler"); sona ekler.
            cbFilmTurleri.Items.Insert(0, "Tüm Türler"); //İlk eleman polarak ekler.
            cbFilmTurleri.SelectedIndex = 0;
        }
        private void txtAdaGore_TextChanged(object sender, EventArgs e)
        {
            FilmleriGoster();
        }
        private void cbFilmTurleri_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilmleriGoster();
        }
        private void txtYonetmeneGore_TextChanged(object sender, EventArgs e)
        {
            FilmleriGoster();
        }
        private void txtOyuncularaGore_TextChanged(object sender, EventArgs e)
        {
            FilmleriGoster();
        }
        private void FilmleriGoster()
        {
            string TureGore = "";
            if (cbFilmTurleri.SelectedItem.ToString() != "Tüm Türler")
                TureGore = cbFilmTurleri.SelectedItem.ToString();
            Film f = new Film();
            f.FilmleriGosterBySorgulama(txtAdaGore.Text, TureGore,txtYonetmeneGore.Text, txtOyuncularaGore.Text, lvFilmler);
        }
        private void lvFilmler_DoubleClick(object sender, EventArgs e)
        {
            Genel.secilenfilmno = Convert.ToInt32(lvFilmler.SelectedItems[0].SubItems[0].Text);
            Genel.secilenfilm = lvFilmler.SelectedItems[0].SubItems[1].Text;
            Genel.secilenmiktar = Convert.ToInt32(lvFilmler.SelectedItems[0].SubItems[8].Text);
            Genel.secilenfiyat = Convert.ToDecimal(lvFilmler.SelectedItems[0].SubItems[7].Text);
            this.Close();
        }
    }
}
