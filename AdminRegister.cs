using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Rent_A_Car
{
    public partial class AdminRegister : Form
    {
        public AdminRegister()
        {
            InitializeComponent();
        }
        static string conStrnig = "Data Source=DESKTOP-MO050TQ\\; Initial Catalog=rent-a-car; Integrated Security=True";
        SqlConnection connect = new SqlConnection(conStrnig);
        


    private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int TCC = Convert.ToInt32(textBox5.Text);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("TC(Tc 1000000000 dan büyük olmak zorunda ) sayı olmak zorunda ");
                Form register = new Register();
                this.Visible = false;
                register.ShowDialog();
                this.Visible = true;
            }
            int TC = Convert.ToInt32(textBox5.Text);
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" )
            {
                if (TC <=1000000000)
                {
                    MessageBox.Show("Hiçbir alan boş bırakılamaz" + "TC sayı olmak zorunda(1000000000 dan büyük olmak zorunda.)");
                }
                
                Form register = new Register();
                this.Visible = false;
                register.ShowDialog();
                this.Visible = true;
            }


            if (textBox3.Text == "123456")
            {

                String Name = textBox4.Text;
                String User_name = textBox1.Text;
                //String TC = textBox5.Text;
                String Password = textBox2.Text;

                try
                {
                    if (connect.State == ConnectionState.Closed)
                        connect.Open();

                    string kayit = "insert into [ADMIN] (USER_NAME,NAME,TC,PASSWORD) VALUES(@user_name,@name,@tc,@password)";
                    SqlCommand komut = new SqlCommand(kayit, connect);
                    komut.Parameters.AddWithValue("@name", Name);
                    komut.Parameters.AddWithValue("@user_name", User_name);
                    komut.Parameters.AddWithValue("@tc", TC);
                    komut.Parameters.AddWithValue("@password", Password);

                    komut.ExecuteNonQuery();
                    connect.Close();

                    MessageBox.Show("Admin Eklendi");
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Hata meydana geldi !!!!!!!!!!!!" + hata.Message);
                }


            }
            else
                MessageBox.Show("Ref. Code Yanlış");






        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
