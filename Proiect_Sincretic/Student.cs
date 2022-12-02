using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_Sincretic
{
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e) //exit button
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e) //back buton 
        {
            this.Close(); 
           
             
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form f = new SignUp();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form f = new SignIn();
            f.ShowDialog();

        }

        private void Student_Load(object sender, EventArgs e)
        {

        }
    }
}
