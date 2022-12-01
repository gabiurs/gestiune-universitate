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
    public partial class AdminLOGIn : Form
    {
        public AdminLOGIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form f = new Enrolement();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form f = new Payment();
            f.ShowDialog();

        }

        private void button5_Click(object sender, EventArgs e) //exit button
        {
            Application.Exit();
        }
    }
}
