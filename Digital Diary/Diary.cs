using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DigitalDiary
{
    public partial class Diary : Form
    {
        string s1; string s2;
        DataAccess dataAccess;
        int uId; string sqle;
        public Diary(int id)
        {
            InitializeComponent();
            uId = id;
            dataAccess = new DataAccess();
            string sqlu = "SELECT * FROM Users WHERE UserId=" + id;
            SqlDataReader reader = dataAccess.GetData(sqlu);
            reader.Read();
            String userName = reader["UserName"].ToString();

           
            label5.Text = "hello " + userName + " welcome to your Digital Diary";
            this.UpdateGridView();

            
        }

        private void Diary_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            dataGridView1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Field Required");
            }
            else
            {
                try
                {
                    dataAccess = new DataAccess();
                    string sql = "INSERT INTO Event(Date,EventName,Description,UserId,Priority) VALUES('" + dateTimePicker1.Value + "','" + textBox1.Text + "','" + textBox2.Text + "','" + this.uId + "','" + comboBox1.Text + "')";
                    int result = dataAccess.ExecuteQuery(sql);
                    dataAccess.Dispose();
                    if (result > 0)
                    {
                        MessageBox.Show("EVENT ADDED sUCCESSFULLY");
                        groupBox1.Visible = false;
                        dataGridView1.Visible = true;
                        this.UpdateGridView();

                    }
                    else
                    {
                        MessageBox.Show("Nothing is Added in your diary");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("exception occured in groupbox");
                }

            }
        }
        void ClearText()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = comboBox1.Text = comboBox2.Text = dateTimePicker1.Text = dateTimePicker2.Text = string.Empty;
        }

        public void UpdateGridView()
        {
            try
            {
                dataAccess = new DataAccess();
                sqle = "SELECT * FROM Event WHERE UserId=" + uId;
                SqlDataReader sqlDataReader = dataAccess.GetData(sqle);

                while (sqlDataReader.HasRows)
                {
                    DataTable dt = new DataTable();

                    dt.Load(sqlDataReader);

                    dataGridView1.DataSource = dt;

                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[4].Visible = false;
                    dataGridView1.Visible = true;

                    ClearText();
                    dataAccess.Dispose();

                }
               
               
               
            }
            catch(Exception ex)
            {
               // MessageBox.Show("exception in event creation");
            
            }
           
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
           /* this.Hide();*/
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            UpdateGridView();
            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox4.Text == "")
            {
            
                groupBox1.Visible = false;
                UpdateGridView();
                MessageBox.Show("please select a value from the list and click Modify");

            }
            else
            {
                groupBox1.Visible = false;
                groupBox2.Visible = true;
                dataGridView1.Visible = false;
            }
          
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Field Required");
            }
            else
            {
                try
                {
                    dataAccess = new DataAccess();
                  
                    string sql = "UPDATE Event SET EventName = '" + textBox4.Text + "',Description = '" + textBox3.Text + "',Priority = '" + comboBox2.Text + "',Date = '" + dateTimePicker2.Value + "' WHERE Description = '" + this.s1 + "' AND EventName ='"+this.s2 + "'";
                    int result = dataAccess.ExecuteQuery(sql);
                    dataAccess.Dispose();
                    if (result > 0)
                    {
                        MessageBox.Show("EVENT modified sUCCESSFULLY");
                        groupBox1.Visible = false;
                        groupBox2.Visible = false;
                        dataGridView1.Visible = true;
                        this.UpdateGridView();

                    }
                    else
                    {
                        MessageBox.Show("Nothing is modified in your diary");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("exception occured in Modify");
                }

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dateTimePicker2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            

            MessageBox.Show("\tClick \n\t Modify \n\t or \n\t DELETE");
             s1 = textBox3.Text;
             s2 = textBox4.Text;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox4.Text == "")
            {

                groupBox1.Visible = false;
                groupBox2.Visible = false;
                UpdateGridView();
                MessageBox.Show("please select a value from the list and click Delete");

            }
            else
            {
                try
                {
                    
                    dataAccess = new DataAccess();
                    string sql = "DELETE FROM Event WHERE EventName='" + s2 + "' AND Description='" + s1 + "'";
                    int result = dataAccess.ExecuteQuery(sql);
                    dataAccess.Dispose();
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    
                    MessageBox.Show("event successfully deleted");
                    UpdateGridView();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("exception occured to delete");
                }
            }
        }
    }
}