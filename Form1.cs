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
            String connString = "Server=localhost;User ID=exam;Password=123456;Database=exam";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();

            label1.Text = conn.State.ToString();

            String sel = "SELECT Name, number_of_students, leader FROM groups";
            MySqlCommand selCom = new MySqlCommand(sel,conn);

            MySqlDataReader selReader = selCom.ExecuteReader();
            while (selReader.Read()) 
            {
                listBox1.Items.Add(selReader.GetString(0) + " "+ selReader.GetInt32(1)+
                    " "+selReader.GetString(2) );
            }

        }
    }
}
