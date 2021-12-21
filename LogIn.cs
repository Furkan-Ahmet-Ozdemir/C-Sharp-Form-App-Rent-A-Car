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

namespace Rent_A_Car
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if ((textBox1.Text == "") || (textBox2.Text == ""))
            {
                MessageBox.Show("Bu alanları boş bırakamazsınız.", "Chat Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-MO050TQ\\; Initial Catalog=rent-a-car; Integrated Security=True";
                conn.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter("select * from [USER] WHERE USER_NAME='" + textBox1.Text + "' AND PASSWORD='" + textBox2.Text + "'", conn);
                sda.Fill(ds);
                if (ds.Tables.Count == 0)
                {
                    MessageBox.Show("Geçersiz Kullanıcı.", "Chat Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ds.Tables.Count == 1)
                {
                    //MessageBox.Show("Hoşgeldiniz.", "Chat Giriş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Main main = new Main();
                    main.kullaniciAdi = textBox1.Text;

                    this.Visible = false;
                    main.ShowDialog();
                    this.Visible = true;
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                conn.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata meydana geldi !!!!!!!!!!!!" + hata.Message);
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form register = new Register();
            this.Visible = false;
            register.ShowDialog();
            this.Visible = true;

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form admin = new Form1();
            this.Visible = false;
            admin.ShowDialog();
            this.Visible = true;

        }
    }
}
