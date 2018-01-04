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
    public class Musteri
    {
        private int _musteriNo;
        private string _musteriAd;
        private string _musteriSoyad;
        private string _telefon;
        private string _adres;

        #region Properties
        public int MusteriNo
        {
            get
            {
                return _musteriNo;
            }

            set
            {
                _musteriNo = value;
            }
        }

        public string MusteriAd
        {
            get
            {
                return _musteriAd;
            }

            set
            {
                _musteriAd = value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower();
            }
        }

        public string MusteriSoyad
        {
            get
            {
                return _musteriSoyad;
            }

            set
            {
                _musteriSoyad = value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower();
            }
        }

        public string Telefon
        {
            get
            {
                return _telefon;
            }

            set
            {
                _telefon = value;
            }
        }

        public string Adres
        {
            get
            {
                return _adres;
            }

            set
            {
                _adres = value;
            }
        }
        #endregion

        SqlConnection conn = new SqlConnection(Genel.connStr);
        public void MusterileriGoster(ListView liste)
        {
            liste.Items.Clear();
            SqlCommand comm = new SqlCommand("Select * from Musteriler where Silindi=0", conn);
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
                i++;
            }
            dr.Close();
            conn.Close();
        }
        public void MusterileriGosterByAdaGore(ListView liste, string AdaGore)
        {
            liste.Items.Clear();
            SqlCommand comm = new SqlCommand("Select * from Musteriler where Silindi=0 and MusteriAd like @AdaGore + '%'", conn);
            comm.Parameters.Add("@AdaGore", SqlDbType.VarChar).Value = AdaGore;
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
                i++;
            }
            dr.Close();
            conn.Close();
        }
        public void MusterileriGosterBySorgulama(ListView liste, string AdaGore, string SoyadaGore, string TelefonaGore, string AdreseGore)
        {
            liste.Items.Clear();
            SqlCommand comm = new SqlCommand("Select * from Musteriler where Silindi=0 and MusteriAd like @AdaGore + '%' and MusteriSoyad like @SoyadaGore + '%' and Telefon like @TelefonaGore + '%' and Adres like '%' + @AdreseGore + '%'", conn);
            comm.Parameters.Add("@AdaGore", SqlDbType.VarChar).Value = AdaGore;
            comm.Parameters.Add("@SoyadaGore", SqlDbType.VarChar).Value = SoyadaGore;
            comm.Parameters.Add("@TelefonaGore", SqlDbType.VarChar).Value = TelefonaGore;
            comm.Parameters.Add("@AdreseGore", SqlDbType.VarChar).Value = AdreseGore;
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
                i++;
            }
            dr.Close();
            conn.Close();
        }
        public bool MusteriKontrol(string Ad, string Soyad, string Telefon)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Select * from Musteriler where MusteriAd = @Ad and MusteriSoyad = @Soyad and Telefon = @Telefon and Silindi=0", conn);
            comm.Parameters.Add("@Ad", SqlDbType.VarChar).Value = Ad;
            comm.Parameters.Add("@Soyad", SqlDbType.VarChar).Value = Soyad;
            comm.Parameters.Add("@Telefon", SqlDbType.VarChar).Value = Telefon;
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
        public bool MusteriKontrolFromDegistir(Musteri m)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Select * from Musteriler where MusteriAd = @Ad and MusteriSoyad = @Soyad and Telefon = @Telefon and MusteriNo != @MusteriNo and Silindi=0", conn);
            comm.Parameters.Add("@Ad", SqlDbType.VarChar).Value = m._musteriAd;
            comm.Parameters.Add("@Soyad", SqlDbType.VarChar).Value = m._musteriSoyad;
            comm.Parameters.Add("@Telefon", SqlDbType.VarChar).Value = m._telefon;
            comm.Parameters.Add("@MusteriNo", SqlDbType.Int).Value = m._musteriNo;
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
        public bool MusteriEkle(Musteri yeni)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Insert into Musteriler(MusteriAd, MusteriSoyad, Telefon, Adres) values(@MusteriAd, @MusteriSoyad, @Telefon, @Adres)", conn);
            comm.Parameters.Add("@MusteriAd", SqlDbType.VarChar).Value = yeni._musteriAd;
            comm.Parameters.Add("@MusteriSoyad", SqlDbType.VarChar).Value = yeni._musteriSoyad;
            comm.Parameters.Add("@Telefon", SqlDbType.VarChar).Value = yeni._telefon;
            comm.Parameters.Add("@Adres", SqlDbType.VarChar).Value = yeni._adres;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally {conn.Close();}
            return Sonuc;
        }
        public bool MusteriGuncelle(Musteri guncel)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Update Musteriler set MusteriAd=@MusteriAd, MusteriSoyad=@MusteriSoyad, Telefon=@Telefon, Adres=@Adres where MusteriNo = @MusteriNo", conn);
            comm.Parameters.Add("@MusteriAd", SqlDbType.VarChar).Value = guncel._musteriAd;
            comm.Parameters.Add("@MusteriSoyad", SqlDbType.VarChar).Value = guncel._musteriSoyad;
            comm.Parameters.Add("@Telefon", SqlDbType.VarChar).Value = guncel._telefon;
            comm.Parameters.Add("@Adres", SqlDbType.VarChar).Value = guncel._adres;
            comm.Parameters.Add("@MusteriNo", SqlDbType.Int).Value = guncel._musteriNo;
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
        public bool MusteriSil(int silinecekNo)
        {
            bool Sonuc = false;
            SqlCommand comm = new SqlCommand("Update Musteriler set Silindi=1 where MusteriNo=@No", conn);
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
    }
}
