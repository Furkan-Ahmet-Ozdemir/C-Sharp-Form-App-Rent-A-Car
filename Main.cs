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
using System.Data.Sql;
using System.IO;

namespace Rent_A_Car
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            
        }
        public string kullaniciAdi { get; set; }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        
        public void hesapla()
        {
            try {
                DateTime trh1 = Convert.ToDateTime(dateTimePicker1.Text);
                DateTime trh2 = Convert.ToDateTime(dateTimePicker2.Text);
                TimeSpan sonuc = trh2 - trh1;
                label5.Text = sonuc.Days.ToString();
                int gun = Convert.ToInt32(label5.Text);


                string secili = comboBox1.SelectedItem.ToString();
                SqlConnection baglanti = new SqlConnection();
                baglanti.ConnectionString = "Data Source=DESKTOP-MO050TQ\\; Initial Catalog=rent-a-car; Integrated Security=True";
                SqlCommand komut = new SqlCommand();
                komut.CommandText = "SELECT * FROM [CAR1] where BRAND_MODEL_NAME='" + secili + " '";
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                SqlDataReader dr;
                baglanti.Open();
                dr = komut.ExecuteReader();
                dr.Read();
                var price = dr["PRICE"];
                int money = Convert.ToInt32(price);

                label6.Text = price.ToString();
                int total = money * gun;
                label9.Text = total.ToString();
                baglanti.Close();
             }
             catch (Exception hata)
            {
                MessageBox.Show("Hata meydana geldi !!!!!!!!!!!!" + hata.Message);
            }

        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            checkDate();
            hesapla();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            hesapla();
            try { 
                string car = comboBox1.SelectedItem.ToString();
                SqlConnection baglanti = new SqlConnection();
                baglanti.ConnectionString = "Data Source=DESKTOP-MO050TQ\\; Initial Catalog=rent-a-car; Integrated Security=True";
                SqlCommand komut = new SqlCommand();
                komut.CommandText = "select * from [CAR1] WHERE BRAND_MODEL_NAME='" + car + "' ";
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                SqlDataReader dr;
                baglanti.Open();
                dr = komut.ExecuteReader();
                dr.Read();
                if (dr["PICTURE"] != null)
                {
                    Byte[] data = new Byte[0];
                    data = (Byte[])(dr["PICTURE"]);
                    MemoryStream mem = new MemoryStream(data);
                    pictureBox1.Image = Image.FromStream(mem);
                }
                baglanti.Close();
            }
             catch (Exception hata)
            {
                MessageBox.Show("Hata meydana geldi !!!!!!!!!!!!" + hata.Message);
            }


        }
        public void checkDate()
        {
            DateTime trh1 = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime trh2 = Convert.ToDateTime(dateTimePicker2.Text);
            if (trh1 > trh2 || comboBox1.SelectedItem== null)
            {
                MessageBox.Show("Başlangıç tarihi bitiş tarihinden büyük olomaz .");
            }
            else
            {
                
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
            try
            {
                DateTime trh1 = Convert.ToDateTime(dateTimePicker1.Text);
                DateTime trh2 = Convert.ToDateTime(dateTimePicker2.Text);

                if (label5.Text == "0" || label9.Text == "0")
                {
                    MessageBox.Show("Gün sayısını veya araba modelini yanlış girdiniz !!");
                }
                else
                {
                    checkDate();
                   

                    MessageBox.Show(trh1.ToString() + "-" + trh2.ToString() + " tarihlerinde kiraladınız." + "\n"
                                    + "Car Model:" + comboBox1.SelectedItem.ToString() + "\n"
                                    + "Day Count:" + label5.Text + "\n"
                                    + "Total price:" + label9.Text
                                    );
                }
                string conStrnig = "Data Source=DESKTOP-MO050TQ\\; Initial Catalog=rent-a-car; Integrated Security=True";
                SqlConnection connect = new SqlConnection(conStrnig);

                if (connect.State == ConnectionState.Closed)
                    connect.Open();
          
                string car_name = comboBox1.SelectedItem.ToString();
                int a = Convert.ToInt32(label9.Text);
                string kayit = "insert into [RESERVAT] (USER_NAME,CAR_NAME,TOTAL_PRICE,START_DATE,END_DATE) VALUES(@user_name,@car_name,@total_price,@start_dt,@end_dt)";
                SqlCommand komut = new SqlCommand(kayit, connect);
                komut.Parameters.AddWithValue("@user_name", kullaniciAdi);
                komut.Parameters.AddWithValue("@car_name", car_name);
                komut.Parameters.AddWithValue("@total_price",a);
                komut.Parameters.AddWithValue("@start_dt", trh1);
                komut.Parameters.AddWithValue("@end_dt", trh2);


                komut.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata meydana geldi !!!!!!!!!!!!" + hata.Message);
            }

        }

        private void Main_Load(object sender, EventArgs e)
        {

       
            
            try
            {
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
                    comboBox1.Items.Add(dr["BRAND_MODEL_NAME"]);
                }
                baglanti.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata meydana geldi !!!!!!!!!!!!" + hata.Message);
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            checkDate();
            hesapla();
        }
    }
}
