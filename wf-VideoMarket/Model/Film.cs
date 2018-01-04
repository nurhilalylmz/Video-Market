using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wf_VideoMarket.Model
{
    public class Film
    {
        private int _filmNo;
        private string _filmAd;
        private int _filmTurNo;
        private string _yonetmen;
        private string _oyuncular;
        private string _ozet;
        private decimal _fiyat;
        private int _miktar;

        #region Properties
        public int FilmNo
        {
            get
            {
                return _filmNo;
            }

            set
            {
                _filmNo = value;
            }
        }

        public string FilmAd
        {
            get
            {
                return _filmAd;
            }

            set
            {
                _filmAd = value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower(); ;
            }
        }

        public int FilmTurNo
        {
            get
            {
                return _filmTurNo;
            }

            set
            {
                _filmTurNo = value;
            }
        }

        public string Yonetmen
        {
            get
            {
                return _yonetmen;
            }

            set
            {
                _yonetmen = value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower(); ;
            }
        }

        public string Oyuncular
        {
            get
            {
                return _oyuncular;
            }

            set
            {
                _oyuncular = value;
            }
        }

        public string Ozet
        {
            get
            {
                return _ozet;
            }

            set
            {
                _ozet = value;
            }
        }

        public decimal Fiyat
        {
            get
            {
                return _fiyat;
            }

            set
            {
                _fiyat = value;
            }
        }

        public int Miktar
        {
            get
            {
                return _miktar;
            }

            set
            {
                _miktar = value;
            }
        }
        #endregion

        SqlConnection conn = new SqlConnection(Genel.connStr);

        public void FilmleriGoster(ListView liste)
        {
            liste.Items.Clear();
            SqlCommand comm = new SqlCommand("Select FilmNo, FilmAd, Filmler.FilmTurNo, TurAd, Yonetmen, Oyuncular, Ozet, Fiyat, Miktar from Filmler inner join FilmTurleri on Filmler.FilmTurNo = FilmTurleri.FilmTurNo where Varmi=1", conn);
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataReader dr = comm.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                liste.Items.Add(dr[0].ToString());
                liste.Items[i].SubItems.Add(dr[1].ToString());
                liste.Items[i].SubItems.Add(dr[2].ToString());
                liste.Items[i].SubItems.Add(dr[3].ToString());
                liste.Items[i].SubItems.Add(dr[4].ToString());
                liste.Items[i].SubItems.Add(dr[5].ToString());
                liste.Items[i].SubItems.Add(dr[6].ToString());
                liste.Items[i].SubItems.Add(dr[7].ToString());
                liste.Items[i].SubItems.Add(dr[8].ToString());
                i++;
            }
            dr.Close();
            conn.Close();
        }
        public void FilmleriGosterBySorgulama(string AdaGore, string TureGore, string YonetmeneGore, string OyuncularaGore,ListView liste)
        {
            liste.Items.Clear();
            SqlCommand comm = new SqlCommand("Select FilmNo, FilmAd, Filmler.FilmTurNo, TurAd, Yonetmen, Oyuncular, Ozet, Fiyat, Miktar from Filmler inner join FilmTurleri on Filmler.FilmTurNo = FilmTurleri.FilmTurNo where Varmi=1 and FilmAd like @AdaGore + '%' and TurAd like @TureGore + '%' and Yonetmen like @YonetmeneGore + '%' and Oyuncular like '%' + @OyuncularaGore + '%'", conn);
            comm.Parameters.Add("@AdaGore", SqlDbType.VarChar).Value = AdaGore;
            comm.Parameters.Add("@TureGore", SqlDbType.VarChar).Value = TureGore;
            comm.Parameters.Add("@YonetmeneGore", SqlDbType.VarChar).Value = YonetmeneGore;
            comm.Parameters.Add("@OyuncularaGore", SqlDbType.VarChar).Value = OyuncularaGore;
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataReader dr = comm.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                liste.Items.Add(dr[0].ToString());
                liste.Items[i].SubItems.Add(dr[1].ToString());
                liste.Items[i].SubItems.Add(dr[2].ToString());
                liste.Items[i].SubItems.Add(dr[3].ToString());
                liste.Items[i].SubItems.Add(dr[4].ToString());
                liste.Items[i].SubItems.Add(dr[5].ToString());
                liste.Items[i].SubItems.Add(dr[6].ToString());
                liste.Items[i].SubItems.Add(dr[7].ToString());
                liste.Items[i].SubItems.Add(dr[8].ToString());
                i++;
            }
            dr.Close();
            conn.Close();
        }
        public bool FilmKontrol(Film f)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Select count(*) from Filmler where FilmAd = @FilmAd and Yonetmen = @Yonetmen and Varmi=1", conn);
            comm.Parameters.Add("@FilmAd", SqlDbType.VarChar).Value = f.FilmAd;
            comm.Parameters.Add("@Yonetmen", SqlDbType.VarChar).Value = f.Yonetmen;
            if (conn.State == ConnectionState.Closed) conn.Open();
            int sayisi = Convert.ToInt32(comm.ExecuteScalar());
            if (sayisi > 0)
            {
                Sonuc = true;
            }
            conn.Close();
            return Sonuc;
        }
        public bool FilmKontrolByDegistir(Film f)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Select count(*) from Filmler where FilmAd = @FilmAd and Yonetmen = @Yonetmen and FilmNo != @FilmNo and Varmi=1", conn);
            comm.Parameters.Add("@FilmAd", SqlDbType.VarChar).Value = f.FilmAd;
            comm.Parameters.Add("@Yonetmen", SqlDbType.VarChar).Value = f.Yonetmen;
            comm.Parameters.Add("@FilmNo", SqlDbType.Int).Value = f.FilmNo;
            if (conn.State == ConnectionState.Closed) conn.Open();
            int sayisi = Convert.ToInt32(comm.ExecuteScalar());
            if (sayisi > 0)
            {
                Sonuc = true;
            }
            conn.Close();
            return Sonuc;
        }
        public bool FilmEkle(Film yeni)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Insert into Filmler(FilmAd, FilmTurNo, Yonetmen, Oyuncular, Ozet, Fiyat, Miktar) values(@FilmAd, @FilmTurNo, @Yonetmen, @Oyuncular, @Ozet, @Fiyat, @Miktar)", conn);
            comm.Parameters.Add("@FilmAd", SqlDbType.VarChar).Value = yeni._filmAd;
            comm.Parameters.Add("@FilmTurNo", SqlDbType.Int).Value = yeni.FilmTurNo;
            comm.Parameters.Add("@Yonetmen", SqlDbType.VarChar).Value = yeni._yonetmen;
            comm.Parameters.Add("@Oyuncular", SqlDbType.VarChar).Value = yeni._oyuncular;
            comm.Parameters.Add("@Ozet", SqlDbType.VarChar).Value = yeni._ozet;
            comm.Parameters.Add("@Fiyat", SqlDbType.Money).Value = yeni._fiyat;
            comm.Parameters.Add("@Miktar", SqlDbType.Int).Value = yeni._miktar;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return Sonuc;
        }
        public bool FilmGuncelle(Film yeni)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Update Filmler set FilmAd=@FilmAd, FilmTurNo=@FilmTurNo, Yonetmen=@Yonetmen, Oyuncular=@Oyuncular, Ozet=@Ozet, Fiyat=@Fiyat, Miktar=@Miktar where FilmNo=@FilmNo", conn);
            comm.Parameters.Add("@FilmAd", SqlDbType.VarChar).Value = yeni._filmAd;
            comm.Parameters.Add("@FilmTurNo", SqlDbType.Int).Value = yeni.FilmTurNo;
            comm.Parameters.Add("@Yonetmen", SqlDbType.VarChar).Value = yeni._yonetmen;
            comm.Parameters.Add("@Oyuncular", SqlDbType.VarChar).Value = yeni._oyuncular;
            comm.Parameters.Add("@Ozet", SqlDbType.VarChar).Value = yeni._ozet;
            comm.Parameters.Add("@Fiyat", SqlDbType.Money).Value = yeni._fiyat;
            comm.Parameters.Add("@Miktar", SqlDbType.Int).Value = yeni._miktar;
            comm.Parameters.Add("@FilmNo", SqlDbType.Int).Value = yeni.FilmNo;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return Sonuc;
        }
        public bool FilmSil(int silinecekNo)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Update Filmler set Varmi=0 where FilmNo=@No", conn);
            comm.Parameters.Add("@No", SqlDbType.Int).Value = silinecekNo;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return Sonuc;
        }
        public bool StokGuncelleFromSatisEkle(int FilmNo, int Adet)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Update Filmler set Miktar=Miktar - @Adet where FilmNo=@FilmNo", conn);
            comm.Parameters.Add("@Adet", SqlDbType.Int).Value = Adet;
            comm.Parameters.Add("@FilmNo", SqlDbType.Int).Value = FilmNo;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return Sonuc;
        }
        public bool StokGuncelleFromSatisSil(int FilmNo, int Adet)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Update Filmler set Miktar=Miktar + @Adet where FilmNo=@FilmNo", conn);
            comm.Parameters.Add("@Adet", SqlDbType.Int).Value = Adet;
            comm.Parameters.Add("@FilmNo", SqlDbType.Int).Value = FilmNo;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return Sonuc;
        }
        public bool StokGuncelleFromSatisDegistir(int FilmNo, int Adet, int orjAdet)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Update Filmler set Miktar=Miktar + @orjAdet - @Adet where FilmNo=@FilmNo", conn);
            comm.Parameters.Add("@Adet", SqlDbType.Int).Value = Adet;
            comm.Parameters.Add("@orjAdet", SqlDbType.Int).Value = orjAdet;
            comm.Parameters.Add("@FilmNo", SqlDbType.Int).Value = FilmNo;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return Sonuc;
        }
    }
}
