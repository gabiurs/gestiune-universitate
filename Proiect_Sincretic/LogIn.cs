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
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form f = new Pay();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            textBox1.Text = "FirstName :" + Studenti.Firstname;
            textBox1.AppendText(Environment.NewLine);
            textBox1.Text = "LastName :" + Studenti.LastName;
            textBox1.AppendText(Environment.NewLine);
            textBox1.Text += "NCP :" + Studenti.ncp;
            textBox1.AppendText(Environment.NewLine);
            textBox1.Text += "Age :" + Studenti.age.ToString();
            textBox1.AppendText(Environment.NewLine);
            textBox1.Text += "Sex :" + Studenti.sex;
            textBox1.AppendText(Environment.NewLine);
            textBox1.Text += "Number :" + Studenti.number.ToString();
            //............................................................................................................
            //imi afiseara datele de la studentull acesta din catalog
            string connect = @"Data Source=GABI\WINCC;Initial Catalog=Universitate;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(connect);
            cnn.Open();
            string tabel_date = "select NumeMaterie,UserName,Nota,Status from Inscrieri where UserName='"+ Studenti.username+"'";
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
