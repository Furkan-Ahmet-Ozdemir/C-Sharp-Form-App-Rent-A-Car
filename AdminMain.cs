using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace Rent_A_Car
{
    public partial class AdminMain : Form
    {
        public AdminMain()
        {
            InitializeComponent();
        }
        int id = 0;

        string resimPath;
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void AdminMain_Load(object sender, EventArgs e)
        {
            Load1();
            verilerigosteri();

        }

        public void Load1()
        {
            System.Threading.Thread.Sleep(1);
            verilerigosteri();

            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = "Data Source=DESKTOP-MO050TQ\\; Initial Catalog=rent-a-car; Integrated Security=True";
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT * FROM CAR1";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["BRAND_MODEL_NAME"]);
            }
            baglanti.Close();
            //comboBox2.Text = "";
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Resim Aç";

            openFileDialog1.Filter = "Jpeg Dosyası (*.jpg)|*.jpg|Gif Dosyası (*.gif)|*.gif|Png Dosyası (*.png)|*.png|Tif Dosyası (*.tif)|*.tif";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)

            {

                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);

                resimPath = openFileDialog1.FileName.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Price ve BRAND MODEL name alanları boş bırakılamaz ");
            }

            else
            {

                string name = textBox1.Text;
                int price = Convert.ToInt32(textBox2.Text);

                //Resimimizi FileStream metoduyla okuma modunda açıyoruz.

                FileStream fs = new FileStream(resimPath, FileMode.Open, FileAccess.Read);

                //BinaryReader ile byte dizisi ile FileStream arasında veri akışı sağlanıyor.

                BinaryReader br = new BinaryReader(fs);

                /*ReadBytes ile FileStreamde belirtilen resim dosyasındaki byte lar

                byte dizisine aktarılıyor.

                */

                byte[] resim = br.ReadBytes((int)fs.Length);

                br.Close();

                fs.Close();

                //Sql Veritabanı ve Kayıt işlemleri

                SqlConnection bag = new SqlConnection("Data Source=DESKTOP-MO050TQ\\; Initial Catalog=rent-a-car; Integrated Security=True");

                SqlCommand kmt = new SqlCommand("insert into [CAR1] (BRAND_MODEL_NAME,PRICE,PICTURE) Values (@name,@price,@image) ", bag);
                kmt.Parameters.AddWithValue("@name", name);
                kmt.Parameters.AddWithValue("@price", price);

                kmt.Parameters.Add("@image", SqlDbType.Image, resim.Length).Value = resim;

                try

                {

                    bag.Open();

                    kmt.ExecuteNonQuery();
                    MessageBox.Show(" Veritabanına kayıt yapıldı.");

                }

                catch (Exception ex)

                {

                    MessageBox.Show(ex.Message.ToString());

                }

                finally

                {

                    bag.Close();

                }

                textBox1.Text = "";
                textBox2.Text = "";

                comboBox2.Items.Clear();
                System.Threading.Thread.Sleep(1);
                Load1();



            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Load1();


            if (comboBox2.SelectedItem.ToString() == "" || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Lütfen Bir Araba Seçiniz");
            }
            else
            {


                string deger = comboBox2.SelectedItem.ToString();
                SqlConnection sqlBaglantisi = new SqlConnection("Data Source=DESKTOP-MO050TQ\\; Initial Catalog=rent-a-car; Integrated Security=True");
                sqlBaglantisi.Open();
                SqlCommand verisil = new SqlCommand("delete from [CAR1] where BRAND_MODEL_NAME = '" + deger + "'", sqlBaglantisi);
                verisil.ExecuteNonQuery();
                sqlBaglantisi.Close();

                comboBox2.Items.Clear();
                System.Threading.Thread.Sleep(1);
                Load1();


            }


        }
        private void verilerigosteri()
        {
            SqlConnection baglanti1 = new SqlConnection("Data Source=DESKTOP-MO050TQ\\; Initial Catalog=rent-a-car; Integrated Security=True");

            listView1.Items.Clear();
            baglanti1.Open();
            SqlCommand komut = new SqlCommand("select * from [RESERVAT]", baglanti1);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {

                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["ID"].ToString();
                ekle.SubItems.Add(oku["USER_NAME"].ToString());
                ekle.SubItems.Add(oku["CAR_NAME"].ToString());
                ekle.SubItems.Add(oku["TOTAL_PRICE"].ToString());
                listView1.Items.Add(ekle);

            }
            baglanti1.Close();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti1 = new SqlConnection("Data Source=DESKTOP-MO050TQ\\; Initial Catalog=rent-a-car; Integrated Security=True");
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            baglanti1.Open();
            SqlCommand komut = new SqlCommand("delete from [RESERVAT] where  ID ='" + id + "'", baglanti1);
            komut.ExecuteNonQuery();
            baglanti1.Close();
            verilerigosteri();

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            columnHeader1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            columnHeader1.Text = listView1.SelectedItems[0].SubItems[2].Text;
            columnHeader3.Text = listView1.SelectedItems[0].SubItems[3].Text;
            
        }
    }
}
