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
    public partial class ADMIN : Form
    {
        public ADMIN()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connect = @"Data Source=GABI\WINCC;Initial Catalog=Universitate;Integrated Security=True";
            SqlConnection con = new SqlConnection(connect);
            con.Open();
            string stmt = "select * from Admin where UserName='" + usernamebox.Text + "' and Password='" + Passwordbox.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(stmt, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Admin");
            if ((ds.Tables["Admin"].Rows.Count) == 1)
            {
                con.Close();

                Form f = new AdminLOGIn();
                f.ShowDialog();
            }
            else
                MessageBox.Show("Wrong Username/ Password");
        }

        private void button3_Click(object sender, EventArgs e) //exit button
        {
            Application.Exit();
        }
    }
}
