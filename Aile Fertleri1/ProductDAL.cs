using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;//kutuphanemiz
using System.Windows.Forms;

namespace Aile_Fertleri1
{
    public class ProductDAL
    {
        SqlConnection _baglan = new SqlConnection(@"Data Source=DESKTOP-5QARI7A\SQLEXPRESS;Initial Catalog=Aile_fertleri;Integrated Security=True");


        void BaglantiKontrolEt()
        {
            if (_baglan.State == ConnectionState.Closed)
            {
                _baglan.Open();
            }
        }

        public void Add(Product product)//disıraıdan urun ekleme istegi parametresı alıor
        {
            BaglantiKontrolEt();
            SqlCommand command = new SqlCommand("insert into Aile values (@ad,@Soyad,@Cinsiyet,@Yas,@Sehir,@meslek,@Resim)", _baglan);
            command.Parameters.AddWithValue("@ad", product.Ad);
            command.Parameters.AddWithValue("@Soyad", product.Soyad);
            command.Parameters.AddWithValue("@Cinsiyet", product.Cinsiyet);
            command.Parameters.AddWithValue("@Yas", product.Yas);
            command.Parameters.AddWithValue("@Sehir", product.Sehir);
            command.Parameters.AddWithValue("@meslek", product.meslek);
            command.Parameters.AddWithValue("@Resim", product.Resim);
            command.ExecuteNonQuery();
            _baglan.Close();
        }

        public List<Product> TumKayitlar()//aynı
        {
            BaglantiKontrolEt();
            SqlCommand command = new SqlCommand("select * from Aile", _baglan);
            SqlDataReader Reader = command.ExecuteReader();
            List<Product> products = new List<Product>();

            while (Reader.Read())
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(Reader["Id"]),
                    Ad = Reader["Ad"].ToString(),
                    Soyad = Reader["Soyad"].ToString(),
                    Cinsiyet = Reader["Cinsiyet"].ToString(),
                    Yas = Convert.ToInt32(Reader["Yas"].ToString()),
                    Sehir = Reader["Sehir"].ToString(),
                    meslek = Reader["meslek"].ToString(),
                    Resim = Reader["Resim"].ToString()
                };
                products.Add(product);
            }
            Reader.Close();
            _baglan.Close();

            return products;
        }
         public DataTable TumKayitlar1()//aynı
        {
            BaglantiKontrolEt();
            SqlCommand command = new SqlCommand("select * from Aile", _baglan);
            SqlDataReader Reader = command.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(Reader);
            Reader.Close();
            _baglan.Close(); 
            return data;
        }


        public void Update(Product product)
        {
            BaglantiKontrolEt();
            SqlCommand command = new SqlCommand("Update Aile set Ad=@ad,Soyad=@Soyad,Cinsiyet=@Cinsiyet,Yas=@Yas,Sehir=@Sehir,meslek=@meslek,Resim=@Resim where Id=@Id", _baglan);
            command.Parameters.AddWithValue("@Id", product.Id);
            command.Parameters.AddWithValue("@ad", product.Ad);
            command.Parameters.AddWithValue("@Soyad", product.Soyad);
            command.Parameters.AddWithValue("@Cinsiyet", product.Cinsiyet);
            command.Parameters.AddWithValue("@Yas", product.Yas);
            command.Parameters.AddWithValue("@Sehir", product.Sehir);
            command.Parameters.AddWithValue("@meslek", product.meslek);
            command.Parameters.AddWithValue("@Resim", product.Resim);
            command.ExecuteNonQuery();
            _baglan.Close();
        }

        public void Delete(int Id)
        {
            BaglantiKontrolEt();
            SqlCommand command = new SqlCommand("delete  from Aile where Id=@Id", _baglan);
            command.Parameters.AddWithValue("@Id", Id);
            command.ExecuteNonQuery();
            _baglan.Close();

        }
    }
}
