//using checkedListBox1.Classes;
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
    public partial class Enrolement : Form
    {

    
        public Enrolement()
        {
            InitializeComponent();
            Shown += OnShown;
        }


        private void OnShown(object sender, EventArgs e)
        {
            
        
            string connect = @"Data Source=GABI\WINCC;Initial Catalog=Universitate;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(connect);
            cnn.Open();
            string stmt = "select * from Materii";
           
            SqlCommand oCmd = new SqlCommand(stmt, cnn);

            using (SqlDataReader oReader = oCmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    
                   
                    Materii m = new Materii();
                    m.Nume =oReader["Nume"].ToString();
                    Invoke(new Action(() => checkedListBox1.Items.Add(m, false)));
                }

                cnn.Close();
            }
        }

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
        private void ncpCB_Leave(object sender, EventArgs e)
        {
            if (verificCNP(ncpCB.Text) == true)
            //facem asta doar daca avem un cnp corect
            {    //MessageBox.Show("CNP corect!");
                string year, mounth, day;
                year = ncpCB.Text.Substring(1, 2);
                mounth = ncpCB.Text.Substring(3, 2);
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
               // Random r = new Random();
               
            }
            else
                MessageBox.Show("CNP incorect!");
        }
        private void button3_Click(object sender, EventArgs e) //exit button
        {
            Application.Exit();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //save button care scrie in 2 baze de date: profesori si materii si profesori
        {
            string sex;
            if (checkBox1.Checked == true)
            {
                sex = checkBox1.Text;
            }

            else if (checkBox2.Checked == true)
                sex = checkBox2.Text;
            else
                sex = checkBox3.Text;
            if (checkedListBox1.CheckedItems.Count == 0)
            {    MessageBox.Show("Select the Subject!\n");
                return ;
            }
            if (ncpCB.Text != string.Empty)
            {
                string connect = @"Data Source=GABI\WINCC;Initial Catalog=Universitate;Integrated Security=True";
                SqlConnection cnn = new SqlConnection(connect);
                cnn.Open();
                string stmt = "insert into Profesori ([Name], [NCP], [Age], [Sex], [Address], [Function], [UserName], [Password]) values (@n, @ncp, @a, @s, @ad,@fc, @us, @ps)";
                SqlCommand sc = new SqlCommand(stmt, cnn);
                sc.Parameters.AddWithValue("@n", nTB.Text);
                sc.Parameters.AddWithValue("@ncp", ncpCB.Text);
                sc.Parameters.AddWithValue("@a", aTB.Text);
                sc.Parameters.AddWithValue("@s", sex);
                sc.Parameters.AddWithValue("@ad", adTB.Text);
                sc.Parameters.AddWithValue("@fc", fcTB.Text);
                sc.Parameters.AddWithValue("@us", usTB.Text);
                sc.Parameters.AddWithValue("@ps", psTB.Text);

                sc.ExecuteNonQuery();
             
                //this.DialogResult = DialogResult.OK;
               
                //aici adaugam in baza de date pt materii si profi 
                //.........................................
                foreach (var item in checkedListBox1.CheckedItems)
                {
                    MessageBox.Show(item.ToString());
                   
                    
                    stmt = "insert into MaterieSiProfesori ([UserName], [Materie], [Locuri]) values (@us, @m, @l)";
                    sc = new SqlCommand(stmt, cnn);
                    sc.Parameters.AddWithValue("@us", usTB.Text);
                    sc.Parameters.AddWithValue("@m", item.ToString());
                    sc.Parameters.AddWithValue("@l", 50);
                    sc.ExecuteNonQuery();
                 
                    

                }
                this.DialogResult = DialogResult.OK;
                    this.Close();
            }
            else
            {
                MessageBox.Show("Nu ati introdus CNP-ul");

            }
           //.......................................................................................

            



        }
    }
}
