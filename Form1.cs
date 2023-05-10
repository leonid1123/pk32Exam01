using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        MySqlConnection myConn=null;

        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            listBox1.Items.Clear();
            int number = (int)numericUpDown1.Value;          
            label1.Text = myConn.State.ToString();
            String sel = "SELECT Name, number_of_students, leader FROM groups WHERE number_of_students>@param1";
            MySqlCommand selCom = new MySqlCommand(sel, myConn);
            selCom.Parameters.AddWithValue("@param1",number);
            selCom.ExecuteNonQuery();

            MySqlDataReader selReader = selCom.ExecuteReader();
            while (selReader.Read()) 
            {
                listBox1.Items.Add(selReader.GetString(0) + " "+ selReader.GetInt32(1)+
                    " "+selReader.GetString(2) );
            }
            myConn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            button2.Enabled = false;
            checkBox1.Checked = false;
            String insSQL = "INSERT INTO `groups`(`Name`, `number_of_students`, `leader`) VALUES (@name,@num,@lead)";
            MySqlCommand insSQLCom = new MySqlCommand(insSQL,myConn);
            String groupName = textBox1.Text.Trim();
            int numberOfStudents = (int)numericUpDown2.Value;
            String starosta = textBox3.Text.Trim();
            insSQLCom.Parameters.AddWithValue("@name",groupName);
            insSQLCom.Parameters.AddWithValue("@num",numberOfStudents);
            insSQLCom.Parameters.AddWithValue("@lead", starosta);
            insSQLCom.Prepare();
            insSQLCom.ExecuteNonQuery();
            myConn.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                button2.Enabled = true;
            } else
            {
                button2.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String connString = "Server=localhost;User ID=exam;Password=123456;Database=exam";
            myConn = new MySqlConnection(connString);
            myConn.Open();
        }
    }
}
