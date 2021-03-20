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
namespace DigitalDiary
{
    public partial class Signup : Form
    {
        DataAccess dataAccess;
        Login login;
        Welcome wel;
        public Signup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Field Required");
            }
            else
            {
                if (textBox5.Text == textBox6.Text)
                {
                    try
                    {
                        dataAccess = new DataAccess();
                        string sql = "INSERT INTO Users(Name,UserName,Password,Email,PhoneNo) VALUES('" + textBox1.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox3.Text + "','" + textBox2.Text + "')";
                        int result = dataAccess.ExecuteQuery(sql);
                        dataAccess.Dispose();
                        if (result > 0)
                        {
                            MessageBox.Show("ACCOUNT CREATED sUCCESSFULLY");
                            this.Hide();
                            login = new Login();
                            login.Show();

                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("insert all values correctly");
                    }
                   
                }
                else
                {
                    MessageBox.Show("passwords needs to be matched");
                }


            }
        }

        private void Signup_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            wel = new Welcome();
            wel.Show();
        }
    }
}
