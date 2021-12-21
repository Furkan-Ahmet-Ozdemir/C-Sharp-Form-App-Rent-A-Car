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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        static string conStrnig = "Data Source=DESKTOP-MO050TQ\\; Initial Catalog=rent-a-car; Integrated Security=True";
        SqlConnection connect = new SqlConnection(conStrnig);
        String gender;
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
            try{
                int Age = Convert.ToInt32(textBox6.Text);
                int TCC = Convert.ToInt32(textBox5.Text);
            }catch(System.FormatException )
            {
                MessageBox.Show("TC(Tc 1000000000 dan büyük olmak zorunda ) ve Age(0 dan büyük olmak zorunda) sayı olmak zorunda ");
                Form register = new Register();
                this.Visible = false;
                register.ShowDialog();
                this.Visible = true;
            }
            int TC = Convert.ToInt32(textBox5.Text);

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" /*|| TC <1000000000 || Age < 0*/ && (radioButton1.Checked && radioButton2.Checked) != true)
            {
                if (TC <= 1000000000)
                {
                    MessageBox.Show("Hiçbir alan boş bırakılamaz" + "TC sayı olmak zorunda(1000000000 dan büyük olmak zorunda.)");
                }
                
                Form register = new Register();
                this.Visible = false;
                register.ShowDialog();
                this.Visible = true;
            }

            String Name = textBox1.Text;
            String Last_name = textBox3.Text;
            String User_name = textBox4.Text;
            int Agee = Convert.ToInt32(textBox6.Text);

            int TCc = Convert.ToInt32(textBox5.Text);

            String Password = textBox2.Text;

            if (radioButton1.Checked)
            {
                gender = "Man";
            }
            else if (radioButton2.Checked)
            {
                gender = "Woman";
            }

            try
            {
                if(connect.State == ConnectionState.Closed)
                    connect.Open();

                string kayit = "insert into [USER] (USER_NAME,NAME,LAST_NAME,TC,AGE,GENDER,PASSWORD) VALUES(@user_name,@name,@last_name,@tc,@age,@gender,@password)";
                SqlCommand komut = new SqlCommand(kayit, connect);
                komut.Parameters.AddWithValue("@name",Name);
                komut.Parameters.AddWithValue("@last_name", Last_name);
                komut.Parameters.AddWithValue("@user_name", User_name);
                komut.Parameters.AddWithValue("@tc", TCc);
                komut.Parameters.AddWithValue("@age", Agee);
                komut.Parameters.AddWithValue("@gender", gender);
                komut.Parameters.AddWithValue("@password", Password);

                komut.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Kayıt Eklendi");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata meydana geldi !!!!!!!!!!!!"+ hata.Message);
            }


        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }
    }
}
