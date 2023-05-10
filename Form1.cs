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
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            int number = (int)numericUpDown1.Value;
            String connString = "Server=localhost;User ID=exam;Password=123456;Database=exam";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();

            label1.Text = conn.State.ToString();

            String sel = "SELECT Name, number_of_students, leader FROM groups WHERE number_of_students>@param1";
            MySqlCommand selCom = new MySqlCommand(sel,conn);
            selCom.Parameters.AddWithValue("@param1",number);
            selCom.ExecuteNonQuery();

            MySqlDataReader selReader = selCom.ExecuteReader();
            while (selReader.Read()) 
            {
                listBox1.Items.Add(selReader.GetString(0) + " "+ selReader.GetInt32(1)+
                    " "+selReader.GetString(2) );
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            checkBox1.Checked = false;
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
    }
}
