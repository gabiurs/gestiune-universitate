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
    
    public partial class Teacher : Form
    {
        public Teacher()
        {
            InitializeComponent();
        }

        private void Teacher_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //login buttton care verifica daca user name si passs sunt in baza de date si deschide forma urmatoare
        {

            string connect = @"Data Source=GABI\WINCC;Initial Catalog=Universitate;Integrated Security=True";
            SqlConnection con = new SqlConnection(connect);
            con.Open();
            string stmt = "select * from Profesori where UserName='" + usernamebox.Text + "' and Password='" + Passwordbox.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(stmt, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Profesori");
            if ((ds.Tables["Profesori"].Rows.Count) == 1)
            {
                con.Close();

                LogdTeachers.name = "" + ds.Tables["Profesori"].Rows[0]["Name"];
                LogdTeachers.ncp = "" + ds.Tables["Profesori"].Rows[0]["NCP"];
                LogdTeachers.age = int.Parse(ds.Tables["Profesori"].Rows[0]["Age"].ToString());
                LogdTeachers.address= "" + ds.Tables["Profesori"].Rows[0]["Address"];
                LogdTeachers.sex= "" + ds.Tables["Profesori"].Rows[0]["Sex"];
                LogdTeachers.username= "" + ds.Tables["Profesori"].Rows[0]["UserName"];
                LogdTeachers.function= "" + ds.Tables["Profesori"].Rows[0]["Function"];

                //MessageBox.Show(LogdTeachers.name);

                Form f = new LogTeacher();
                f.ShowDialog();
            }
            else
                MessageBox.Show("Wrong Username/ Password");
        


       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
