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
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //button de login
        {
            string connect = @"Data Source=GABI\WINCC;Initial Catalog=Universitate;Integrated Security=True";
            SqlConnection con = new SqlConnection(connect);
            con.Open();
            string stmt = "select * from Studenti where UserName='" + usernamebox.Text + "' and Password='" +Passwordbox.Text+"'";
            SqlDataAdapter da = new SqlDataAdapter(stmt, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Studenti");
            if ((ds.Tables["Studenti"].Rows.Count) == 1)
            {
                con.Close();
               Studenti.Firstname = "" + ds.Tables["Studenti"].Rows[0]["FirstName"];
                Studenti.LastName = "" + ds.Tables["Studenti"].Rows[0]["LastName"];
                Studenti.ncp = "" + ds.Tables["Studenti"].Rows[0]["NCP"];
                Studenti.age = int.Parse(ds.Tables["Studenti"].Rows[0]["Age"].ToString());
                Studenti.number = int.Parse(ds.Tables["Studenti"].Rows[0]["Number"].ToString());
                Studenti.sex = "" + ds.Tables["Studenti"].Rows[0]["Sex"];
                Studenti.username = "" + ds.Tables["Studenti"].Rows[0]["UserName"];

                
                Form f = new LogIn();
                f.ShowDialog();
            }
            else
                MessageBox.Show("Wrong Username/ Password");
        }

        private void button2_Click(object sender, EventArgs e) //exit button
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e) // back button
        {
            this.Close();
          
        }
    }
}
