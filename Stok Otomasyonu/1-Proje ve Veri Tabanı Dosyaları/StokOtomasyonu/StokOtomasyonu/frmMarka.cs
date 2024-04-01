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

namespace StokOtomasyonu
{
    public partial class frmMarka : Form
    {
        public frmMarka()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=Database Name;Initial Catalog=Stok_Takip;Integrated Security=True");

        bool durum;
        private void markacontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from markabilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (comboBox1.Text==read["kategori"].ToString() && textBox1.Text == read["marka"].ToString() || comboBox1.Text=="" || textBox1.Text == "")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            markacontrol();
            if (durum==true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into markabilgileri(kategori,marka) values('" + comboBox1.Text + "','" + textBox1.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Marka Eklendi...");
            }
            else
            {
                MessageBox.Show("Böyle Bir Kategori Ve Marka Var...","Uyarı !!!");
            }
            textBox1.Text = "";
            comboBox1.Text = "";
            
        }

        private void frmMarka_Load(object sender, EventArgs e)
        {
            kategorigetir();
        }

        private void kategorigetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["kategori"].ToString());
            }
            baglanti.Close();
        }
    }
}
