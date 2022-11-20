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
    public partial class WELCOME : Form
    {
        public WELCOME()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e) // prof button 
        {
            Form f = new Teacher();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) //admin button
        {
            Form f = new ADMIN();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) //buton student
        {
            Form f = new Student();
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e) //buton de iesire
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form f = new Help();
            f.ShowDialog();
        }

        private void WELCOME_Load(object sender, EventArgs e)
        {

        }
    }
}
