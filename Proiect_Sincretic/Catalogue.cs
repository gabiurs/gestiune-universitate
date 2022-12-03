using MySql.Data.MySqlClient;
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
    public partial class Catalogue : Form
    {
        public Catalogue()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)   //EXIT BUTTON
        {
            Application.Exit();
        }

        private void Catalogue_Load(object sender, EventArgs e)
        {

            string connect = @"Data Source=GABI\WINCC;Initial Catalog=Universitate;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(connect);
            cnn.Open();
            string tabel_date = "select NumeMaterie,UserName,Nota,Status from Inscrieri";
            SqlDataAdapter da = new SqlDataAdapter(tabel_date, connect);
            DataSet ds = new DataSet();
            da.Fill(ds, "Inscrieri");
            dataGridView1.DataSource = ds.Tables["Inscrieri"].DefaultView;
            cnn.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
