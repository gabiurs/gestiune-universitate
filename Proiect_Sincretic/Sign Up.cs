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
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Proiect_Sincretic
{
    public partial class SignUp: Form
    {


        public static bool verificCNP(string cnp)
        {

            int s, a1, a2, l1, l2, z1, z2, j1, j2, n1, n2, n3, cifc, u;
            if (cnp.Trim().Length != 13)
                return false;
            else
            {
                s = Convert.ToInt16(cnp.Substring(0, 1));
                a1 = Convert.ToInt16(cnp.Substring(1, 1));
                a2 = Convert.ToInt16(cnp.Substring(2, 1));
                l1 = Convert.ToInt16(cnp.Substring(3, 1));
                l2 = Convert.ToInt16(cnp.Substring(4, 1));
                z1 = Convert.ToInt16(cnp.Substring(5, 1));
                z2 = Convert.ToInt16(cnp.Substring(6, 1));
                j1 = Convert.ToInt16(cnp.Substring(7, 1));
                j2 = Convert.ToInt16(cnp.Substring(8, 1));
                n1 = Convert.ToInt16(cnp.Substring(9, 1));
                n2 = Convert.ToInt16(cnp.Substring(10, 1));
                n3 = Convert.ToInt16(cnp.Substring(11, 1));
                cifc = Convert.ToInt16(((s * 2 + a1 * 7 + a2 * 9 + l1 * 1 + l2 * 4 + z1
               * 6 + z2 * 3 + j1 * 5 + j2 * 8 + n1 * 2 + n2 * 7 + n3 * 9) % 11));
                if (cifc == 10)
                {
                    cifc = 1;
                }
                u = Convert.ToInt16(cnp.Substring(12, 1));
                if (cifc == u)
                    return true;
                else
                    return false;
            }
        }
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
            
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }
        //..............................................................................
        //verificam CNP-ul
          

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (verificCNP(ncpCB.Text) == true)
                //facem asta doar daca avem un cnp corect
            {    //MessageBox.Show("CNP corect!");
                string year, mounth, day;
                year = ncpCB.Text.Substring(1, 2);
                mounth= ncpCB.Text.Substring(3, 2);
                day = ncpCB.Text.Substring(5, 2);
                // transformam in int ca sa calculam daca anul este 1900 sau 2000
                int year_int;
                year_int = Int32.Parse(year);
                if (year_int < 20)
                    year_int += 2000;
                else
                    year_int += 1900;

                var birthday = new DateTime(year_int, Int32.Parse(mounth), Int32.Parse(day)); 

                int age = (int)((DateTime.Now - birthday).TotalDays / 365.242199);
                //MessageBox.Show(year);
                aTB.Text = "" + age;
                Random r = new Random();
                nTB.Text = "" + r.Next(200);
            }
            else
                MessageBox.Show("CNP incorect!");
        }
        // buton de salvare pentru adaugarea de date in baza de date in tabelul de studenti
        private void button1_Click(object sender, EventArgs e)  
        {   string sex;
             if (checkBox1.Checked == true)
                {
                    sex = checkBox1.Text;     
                }

                else if (checkBox2.Checked == true)
                    sex = checkBox2.Text;
                  else
                         sex = checkBox3.Text;

            if (ncpCB.Text != string.Empty)
            {
                string connect = @"Data Source=GABI\WINCC;Initial Catalog=Universitate;Integrated Security=True";
                SqlConnection cnn = new SqlConnection(connect);
                cnn.Open();
                string stmt = "insert into Studenti ([FirstName], [LastName], [Class], [NCP], [Age], [Sex], [Data], [Number], [UserName], [Password]) values (@fn, @ln, @c, @ncp, @a, @s, @d,@n, @us, @ps)";
                SqlCommand sc = new SqlCommand(stmt, cnn);
                sc.Parameters.AddWithValue("@fn", fnTB.Text);
                sc.Parameters.AddWithValue("@ln", lnTB.Text);
                sc.Parameters.AddWithValue("@c", comboBox1.ValueMember);
                sc.Parameters.AddWithValue("@ncp", ncpCB.Text);
                sc.Parameters.AddWithValue("@a", aTB.Text);
                sc.Parameters.AddWithValue("@s", sex);
                sc.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                sc.Parameters.AddWithValue("@n", nTB.Text);
                sc.Parameters.AddWithValue("@us", usTB.Text);
                sc.Parameters.AddWithValue("@ps", ps.Text);
                
                sc.ExecuteNonQuery();
                cnn.Close();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Nu ati introdus CNP-ul");
            }

        }

        private void ncpCB_TextChanged(object sender, EventArgs e)
        {
            
        }
    }

    }

