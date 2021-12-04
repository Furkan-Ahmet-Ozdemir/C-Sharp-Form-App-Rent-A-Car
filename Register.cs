using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Windows.Forms;

namespace Rent_A_Car
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        static string conStrnig = "Data Source=DESKTOP-MO050TQ\\; Initial Catalog=rent-a-car; Integrated Security=True";
        SqlConnection connect = new SqlConnection(conStrnig);
        private void Register_Load(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String Name = textBox1.Text;
            String Surname = textBox3.Text;
            String User_name = textBox4.Text;

            int TC = Convert.ToInt32(textBox5.Text);


            try
            {
                if(connect.State == ConnectionState.Closed)
                    connect.Open();

                string kayit = "insert into [USER] (ID,NAME,SURNAME,USER_NAME) VALUES(@id,@name,@surname,@user_name)";
                SqlCommand komut = new SqlCommand(kayit, connect);
                komut.Parameters.AddWithValue("@name",Name);
                komut.Parameters.AddWithValue("@surname", Surname);
                komut.Parameters.AddWithValue("@user_name", User_name);
                komut.Parameters.AddWithValue("@id", TC);

                komut.ExecuteNonQuery();
                connect.Close();

                MessageBox.Show("Kayıt Eklendi");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata meydana geldi !!!!!!!!!!!!"+ hata.Message);

            }














        }

        
    }
}
