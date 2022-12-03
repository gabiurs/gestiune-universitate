using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_Sincretic
{
   

    public partial class LogTeacher : Form
    {
        public LogTeacher()
        {
            InitializeComponent();
        }

       

        private void button1_Click_1(object sender, EventArgs e) // button 4 catalogue
        {
            Form f = new Catalogue();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)// BUTTON 4 PONTEGE
        {
            Form f = new Pontage();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LogTeacher_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Name :"+ LogdTeachers.name;
            textBox1.AppendText(Environment.NewLine);
            textBox1.Text += "NCP :" + LogdTeachers.ncp;
            textBox1.AppendText(Environment.NewLine);
            textBox1.Text += "Age :" + LogdTeachers.age.ToString();
            textBox1.AppendText(Environment.NewLine);
            textBox1.Text += "Address :" + LogdTeachers.address;
            textBox1.AppendText(Environment.NewLine);
            textBox1.Text += "Sex :" + LogdTeachers.sex ;
            textBox1.AppendText(Environment.NewLine);
            textBox1.Text += "Function: " + LogdTeachers.function ;

        }
    }
}
