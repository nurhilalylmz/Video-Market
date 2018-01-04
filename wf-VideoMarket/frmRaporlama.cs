using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wf_VideoMarket.Model;

namespace wf_VideoMarket
{
    public partial class frmRaporlama : Form
    {
        public frmRaporlama()
        {
            InitializeComponent();
        }

        private void frmRaporlama_Load(object sender, EventArgs e)
        {

            //this.vw_DetayliSatisTableAdapter.Fill(this.VideoMarketDataSet.vw_DetayliSatis);

            //this.reportViewer1.RefreshReport();
        }

        private void btnGetir_Click(object sender, EventArgs e)
        {
            FilmSatis fs = new FilmSatis();
            //fs.SatislariGosterByRaporlama(dtpTarih1.Value, dtpTarih2.Value, cbFilmTurleri.SelectedItem, txtMusteri.Text, txtFilm.Text); 
            //SqlDataAdapter da = new SqlDataAdapter("Select Convert(Date,Tarih,104) as Tarih, FilmAd, MusteriAd + ' ' + MusteriSoyad as Musteri, BirimFiyat, Adet, BirimFiyat * Adet as Tutar from FilmSatis fs inner join Filmler f on fs.FilmNo = f.FilmNo inner join Musteriler m on fs.MusteriNo = m.MusteriNo where fs.Silindi=0 and Convert(Date,Tarih,104) between Convert(Date,@Tarih1,104) and Convert(Date,@Tarih2,104)", conn);
            //da.SelectCommand.Parameters.Add("@Tarih1", SqlDbType.DateTime).Value = Tarih1;
            //da.SelectCommand.Parameters.Add("@Tarih2", SqlDbType.DateTime).Value = Tarih2;
            ////da.Fill(dt); //yerine
            //da.Fill(this.VideoMarketDataSet.vw_DetayliSatis);
            //this.reportViewer1.RefreshReport();
        }
    }
}
