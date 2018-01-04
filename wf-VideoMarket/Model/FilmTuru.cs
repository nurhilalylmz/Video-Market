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
    public class FilmTuru
    {
        private int _filmTurNo;
        private string _turAd;
        private string _aciklama;

        #region Properties
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

        public string TurAd
        {
            get
            {
                return _turAd;
            }

            set
            {
                _turAd = value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower();
            }
        }

        public string Aciklama
        {
            get
            {
                return _aciklama;
            }

            set
            {
                _aciklama = value;
            }
        }
        #endregion

        //Genel gnl = new Genel();

        SqlConnection conn = new SqlConnection(Genel.connStr);

        public void FilmTurleriGoster(ListView liste)
        {
            liste.Items.Clear();
            SqlCommand comm = new SqlCommand("Select * from FilmTurleri where Silindi=0", conn);
            if(conn.State == ConnectionState.Closed) conn.Open();
            SqlDataReader dr = comm.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                liste.Items.Add(dr[0].ToString());
                liste.Items[i].SubItems.Add(dr[1].ToString());
                liste.Items[i].SubItems.Add(dr[2].ToString());
                i++;
            }
            dr.Close();
            conn.Close();
        }
        //public void FilmTurleriGoster(ComboBox liste)
        //{
        //    liste.Items.Clear();
        //    SqlCommand comm = new SqlCommand("Select * from FilmTurleri where Silindi=0", conn);
        //    if (conn.State == ConnectionState.Closed) conn.Open();
        //    SqlDataReader dr = comm.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        liste.Items.Add(dr["TurAd"].ToString());
        //    }
        //    dr.Close();
        //    conn.Close();
        //}
        public void FilmTurleriGoster(ComboBox liste)
        {
            liste.Items.Clear();
            SqlCommand comm = new SqlCommand("Select * from FilmTurleri where Silindi=0", conn);
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                FilmTuru ft = new FilmTuru();
                ft._turAd = dr["TurAd"].ToString();
                ft._filmTurNo = Convert.ToInt32(dr["FilmTurNo"]);
                liste.Items.Add(ft);    //ComboBox item olarak object aldığından, TurAd ve FilmTurNo bilgileri girilen FilmTuru nesnelerini item olarak ekliyoruz.
            }
            dr.Close();
            conn.Close();
        }
        public int FilmTurNoGetirByFilmTuru(string FilmTuru)
        {
            int TurNo = 0;
            SqlCommand comm = new SqlCommand("Select FilmTurNo from FilmTurleri where TurAd = @TurAd and Silindi=0", conn);
            comm.Parameters.Add("@TurAd", SqlDbType.VarChar).Value = FilmTuru;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                TurNo  = Convert.ToInt32(comm.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally {conn.Close(); }
            return TurNo;
        }
        public bool FilmTuruKontrol(string FilmTuru)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Select TurAd from FilmTurleri where TurAd = @TurAd and Silindi=0", conn);
            comm.Parameters.Add("@TurAd", SqlDbType.VarChar).Value = FilmTuru;
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                Sonuc = true;
                //while (dr.Read())
                //{
                //    Sonuc = true; //önceden bu tür kayıt edilmiş.
                //}
            }
            dr.Close();
            conn.Close();
            return Sonuc;
        }
        public bool FilmTuruKontrolFromDegistir(string FilmTuru, int TurNo)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Select TurAd from FilmTurleri where TurAd = @TurAd and FilmTurNo != @FilmTurNo and Silindi=0", conn);
            comm.Parameters.Add("@TurAd", SqlDbType.VarChar).Value = FilmTuru;
            comm.Parameters.Add("@FilmTurNo", SqlDbType.Int).Value = TurNo;
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                Sonuc = true;
            }
            dr.Close();
            conn.Close();
            return Sonuc;
        }
        public bool FilmTuruEkle(FilmTuru yeniTur)
        {
            SqlCommand comm = new SqlCommand("Insert into FilmTurleri(TurAd, Aciklama) values(@TurAd, @Aciklama)", conn);
            comm.Parameters.Add("@TurAd", SqlDbType.VarChar).Value = yeniTur._turAd;
            comm.Parameters.Add("@Aciklama", SqlDbType.VarChar).Value = yeniTur._aciklama;
            if (conn.State == ConnectionState.Closed) conn.Open();
            bool Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            conn.Close();
            return Sonuc;
        }
        public bool FilmTuruGuncelle(FilmTuru guncelTur)
        {
            SqlCommand comm = new SqlCommand("Update FilmTurleri Set TurAd=@TurAd, Aciklama=@Aciklama where FilmTurNo=@FilmTurNo", conn);
            comm.Parameters.Add("@TurAd", SqlDbType.VarChar).Value = guncelTur._turAd;
            comm.Parameters.Add("@Aciklama", SqlDbType.VarChar).Value = guncelTur._aciklama;
            comm.Parameters.Add("@FilmTurNo", SqlDbType.Int).Value = guncelTur._filmTurNo;
            if (conn.State == ConnectionState.Closed) conn.Open();
            bool Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            conn.Close();
            return Sonuc;
        }
        //public bool FilmTuruSil(int silinecekTurNo)
        //{
        //    bool Sonuc = false;
        //    SqlCommand comm = new SqlCommand("Delete from FilmTurleri where FilmTurNo=@FilmTurNo", conn);
        //    comm.Parameters.Add("@FilmTurNo", SqlDbType.Int).Value = silinecekTurNo;
        //    if (conn.State == ConnectionState.Closed) conn.Open();
        //    try
        //    {
        //        Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
        //        //return Sonuc;     Yine de finally çalışır.
        //    }
        //    catch (SqlException ex)
        //    {
        //        string hata = ex.Message;
        //    }
        //    finally {  conn.Close();}           
        //    return Sonuc;
        //}
        public bool FilmTuruSil(int silinecekTurNo)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Update FilmTurleri set Silindi=1 where FilmTurNo=@FilmTurNo", conn);
            comm.Parameters.Add("@FilmTurNo", SqlDbType.Int).Value = silinecekTurNo;
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
        public override string ToString()
        {
            return TurAd; //FilmTuru türünden nesneler ToString() uygulandığında string olarak TurAd değerini döndürecek.
        }
    }
}
