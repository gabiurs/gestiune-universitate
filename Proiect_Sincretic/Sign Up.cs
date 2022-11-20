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
    public partial class SignUp: Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e) //exit buton 
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e) //back  button
        {
            this.Close();
            Form f = new Student();
            f.ShowDialog();
        }
    }
}
