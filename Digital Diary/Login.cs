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
    public partial class Login : Form
    {
        DataAccess dataAccess;
        Welcome wel;
        Diary diary;
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Username or Password can not be empty");
            }
            else
            {

                try
                {
                    dataAccess = new DataAccess();
                    string sql = "SELECT * FROM Users WHERE Username='" + textBox1.Text + "' AND Password='" + textBox2.Text + "'";
                    SqlDataReader reader = dataAccess.GetData(sql);
                    reader.Read();
                    String userName =reader["UserName"].ToString();
                    String pass = reader["Password"].ToString();
                    int userId = (int)reader["UserId"];
                    if (userName == textBox1.Text && pass == textBox2.Text)
                    {
                        MessageBox.Show("Login sUCCESSFULLY");
                        diary = new Diary(userId);
                        diary.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("No account found,Signup first");
                        wel = new Welcome();
                        wel.Show();
                        this.Hide();
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show("Exception occured in login ");
                }
            }

  
                
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Welcome wel = new Welcome();
            wel.Show();

        }
    }
}
