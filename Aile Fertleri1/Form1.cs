using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;//kutuphanemızı eklıyoruz

namespace Aile_Fertleri1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ProductDAL productDAL = new ProductDAL();

        private void btnEkle_Click(object sender, EventArgs e)
        {
            productDAL.Add(
                new Product
                {
                    Ad = txtAd.Text,
                    Soyad = txtSoyad.Text,
                    Cinsiyet = comboBox1.Text,
                    Yas = Convert.ToInt32(txtYas.Text),
                    Sehir = txtSehir.Text,
                    meslek = txtMeslek.Text,
                    Resim = txtResim.Text
                }
                );
            MessageBox.Show("Ürün Eklendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            dgvUrunListesi.DataSource = productDAL.TumKayitlar();
        }

        private void button1ResimEkle_Click(object sender, EventArgs e)
        {
            OpenFileDialog resim = new OpenFileDialog();

            if (resim.ShowDialog() == DialogResult.OK)
            {
                txtResim.Text = resim.FileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // dgvUrunListesi.DataSource = productDAL.TumKayitlar();
            dgvUrunListesi.DataSource = productDAL.TumKayitlar1();
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            productDAL.Update(
               new Product
               {
                   Id = Convert.ToInt32(lblId.Text),
                   Ad = txtAd.Text,
                   Soyad = txtSoyad.Text,
                   Cinsiyet = comboBox1.Text,
                   Yas = Convert.ToInt32(txtYas.Text),
                   Sehir = txtSehir.Text,
                   meslek = txtMeslek.Text,
                   Resim = txtResim.Text
               }
               );
            MessageBox.Show("Ürün Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            dgvUrunListesi.DataSource = productDAL.TumKayitlar();
        }

        private void dgvUrunListesi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblId.Text = dgvUrunListesi.CurrentRow.Cells["Id"].Value.ToString();
            txtAd.Text = dgvUrunListesi.CurrentRow.Cells["Ad"].Value.ToString();
            txtSoyad.Text = dgvUrunListesi.CurrentRow.Cells["Soyad"].Value.ToString();
            comboBox1.Text = dgvUrunListesi.CurrentRow.Cells["Cinsiyet"].Value.ToString();
            txtYas.Text = dgvUrunListesi.CurrentRow.Cells["Yas"].Value.ToString();
            txtSehir.Text = dgvUrunListesi.CurrentRow.Cells["Sehir"].Value.ToString();
            txtMeslek.Text = dgvUrunListesi.CurrentRow.Cells["meslek"].Value.ToString();
            txtResim.Text = dgvUrunListesi.CurrentRow.Cells["Resim"].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Simek istediginizde eminisiniz..!!", "Bilgilendirme", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                productDAL.Delete(Convert.ToInt32(dgvUrunListesi.CurrentRow.Cells["Id"].Value.ToString()));
                MessageBox.Show("Ürün Silindi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dgvUrunListesi.DataSource = productDAL.TumKayitlar();
            }
        }
    }
}
