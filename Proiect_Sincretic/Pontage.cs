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
    public partial class Pontage : Form
    {
        public Pontage()
        {
            InitializeComponent();
        }

        private void Pontage_Load(object sender, EventArgs e)//BUTON DE EXIT
        {
            Application.Exit();
        }
    }
}
