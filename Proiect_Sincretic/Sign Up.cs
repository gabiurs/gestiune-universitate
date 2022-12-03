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
            Shown += OnShown;
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
                Random r = new Random();
                nTB.Text = "" + r.Next(200);
            }
            else
                MessageBox.Show("CNP incorect!");
        }


        //...........................................................................................................
        private void OnShown(object sender, EventArgs e)
        {
            //ca sa imi arate toate materiile direct cand se incarca forma 

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
                    m.Nume = oReader["Nume"].ToString();
                    Invoke(new Action(() => checkedListBox1.Items.Add(m, false)));
                }

                cnn.Close();
            }
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

            string connect = @"Data Source=GABI\WINCC;Initial Catalog=Universitate;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(connect);
            cnn.Open();
            SqlCommand sc;
            string stmt;
            
            foreach (var item in checkedListBox1.CheckedItems)
            {
                MessageBox.Show(item.ToString());
                string v_profname;
                int v_ID, v_locuri;
                stmt = "select * from MaterieSiProfesori where Materie= @mat and Locuri>0";
                SqlCommand oCmd = new SqlCommand(stmt, cnn);
                oCmd.Parameters.AddWithValue("@mat", item.ToString());
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {//executa query ul de mai sus cu select si il pune in oReader
                    if (oReader.HasRows==false) {
                        MessageBox.Show("This class is full!\n");
                        return;

                    }

                    oReader.Read();//read citeste prima linie 
                    v_ID = int.Parse(oReader["ID"].ToString()); //citeste coloana materie din materiesi profesor
                    v_profname = oReader["UserName"].ToString();
                    v_locuri = int.Parse(oReader["Locuri"].ToString());
                    v_locuri--;


                    //MessageBox.Show(oReader["Materie"].ToString());
                    oReader["Materie"].ToString();
                    //cnn.Close();
                }


                stmt = "insert into Inscrieri  ([NumeMaterie], [UserName], [Profesor]) values (@nm, @us, @p)";
                sc = new SqlCommand(stmt, cnn);
                sc.Parameters.AddWithValue("@nm", item.ToString()); //materia 
                sc.Parameters.AddWithValue("@us", usTB.Text);  //pt username
                sc.Parameters.AddWithValue("@p", v_profname); //pentru profesor 


                sc.ExecuteNonQuery();

                //..........................................................................................................
                //inserram informatii in tabelul de plati ca sa stim fiecare student cat are de platit
                stmt = "insert into Plati  ([UserName], [Materia], [Suma],[SumaTotala],[SumaRamasa],[Data]) values (@us, @m, @s,@st,@sr,@d)";
                sc = new SqlCommand(stmt, cnn);
                sc.Parameters.AddWithValue("@us", usTB.Text);  //asta ia username ul
                sc.Parameters.AddWithValue("@m", item.ToString());  //asta ia materia 
                sc.Parameters.AddWithValue("@s", 0); 
                sc.Parameters.AddWithValue("@st",1500 ); 
                sc.Parameters.AddWithValue("@sr",1500); 
                sc.Parameters.AddWithValue("@d", DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt"));
                sc.ExecuteNonQuery();
                //.............................................................................................
                //executam query ul pt update la baza de date pt locuri
                stmt = "update MaterieSiProfesori SET Locuri=@loc where ID = @id";

                sc = new SqlCommand(stmt, cnn);
                sc.Parameters.AddWithValue("@loc", v_locuri);
                sc.Parameters.AddWithValue("@id", v_ID);
                sc.ExecuteNonQuery();

            }


            if (ncpCB.Text != string.Empty)
            {
                
                stmt = "insert into Studenti ([FirstName], [LastName], [Class], [NCP], [Age], [Sex], [Data], [Number], [UserName], [Password]) values (@fn, @ln, @c, @ncp, @a, @s, @d,@n, @us, @ps)";
                sc = new SqlCommand(stmt, cnn);
                sc.Parameters.AddWithValue("@fn", fnTB.Text);
                sc.Parameters.AddWithValue("@ln", lnTB.Text);
                sc.Parameters.AddWithValue("@c", checkedListBox1.ValueMember);
                sc.Parameters.AddWithValue("@ncp", ncpCB.Text);
                sc.Parameters.AddWithValue("@a", aTB.Text);
                sc.Parameters.AddWithValue("@s", sex);
                sc.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                sc.Parameters.AddWithValue("@n", nTB.Text);
                sc.Parameters.AddWithValue("@us", usTB.Text);
                sc.Parameters.AddWithValue("@ps", ps.Text);
                sc.ExecuteNonQuery();
                //..................................................
                //aici adaugam materiile in tabeleul de inscrieri
                //comboBox1
              
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

        private void button4_Click(object sender, EventArgs e)//clear button
        {
             
                Action<Control.ControlCollection> func = null;

                func = (controls) =>
                {
                    foreach (Control control in controls)
                        if (control is TextBox)
                            (control as TextBox).Clear();
                        else
                            func(control.Controls);
                };

                func(Controls);
            
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            for (int i=0;i<checkedListBox1.Items.Count;i++)
            { checkedListBox1.SetItemChecked(i, false); }
        }
    }

    }

