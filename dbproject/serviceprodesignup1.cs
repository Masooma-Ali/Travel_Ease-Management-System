using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace dbproject
{
    public partial class serviceprodesignup1 : Form
    {
        string connectionString = "Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False";

        public serviceprodesignup1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void serviceprodesignup1_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                 /*       // 🔍 Validate all fields
                        if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                            string.IsNullOrWhiteSpace(textBox3.Text) ||
                            string.IsNullOrWhiteSpace(textBox4.Text) ||
                            string.IsNullOrWhiteSpace(textBox5.Text) ||
                            string.IsNullOrWhiteSpace(textBox6.Text) ||
                            string.IsNullOrWhiteSpace(textBox7.Text) ||
                            string.IsNullOrWhiteSpace(textBox8.Text) ||
                            string.IsNullOrWhiteSpace(textBox9.Text) ||
                            string.IsNullOrWhiteSpace(textBox10.Text))
                        {
                            MessageBox.Show("Please fill in all fields.");
                            return;
                        }
           */

            string gender = radioButton2.Checked ? "Male" : "Female";
            string role = "serviceprovider";
            DateTime dob = dateTimePicker1.Value;
            string fullName = $"{textBox2.Text} {textBox3.Text} {textBox4.Text}".Trim();
            int providerID = 0;
            int userID = 0;
            string m=textBox9.Text.Trim();
            string pattern = @"^.+@.+\..+$";

            if (!Regex.IsMatch(m, pattern))
            {
                MessageBox.Show("Invalid email format. Please enter a valid email.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 🔹 Step 1: Insert into USERS table
                    string userQuery = @"
            INSERT INTO USERS (Email, Password, ContactNo, DOB, Gender, City, Region, Country, FirstName, MiddleName, LastName, Role)
            VALUES (@Email, @Password, @Contact, @DOB, @Gender, @City, @Region, @Country, @FirstName, @MiddleName, @LastName, @Role);
            SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmdUser = new SqlCommand(userQuery, conn))
                    {
                        cmdUser.Parameters.AddWithValue("@Email", textBox9.Text);
                        cmdUser.Parameters.AddWithValue("@Password", textBox10.Text);
                        cmdUser.Parameters.AddWithValue("@Contact", textBox3.Text);
                        cmdUser.Parameters.AddWithValue("@DOB", dob);
                        cmdUser.Parameters.AddWithValue("@Gender", gender);
                        cmdUser.Parameters.AddWithValue("@City", textBox5.Text);
                        cmdUser.Parameters.AddWithValue("@Region", textBox6.Text);
                        cmdUser.Parameters.AddWithValue("@Country", textBox7.Text);
                        cmdUser.Parameters.AddWithValue("@FirstName", textBox2.Text);
                        cmdUser.Parameters.AddWithValue("@MiddleName", textBox3.Text);
                        cmdUser.Parameters.AddWithValue("@LastName", textBox4.Text);
                        cmdUser.Parameters.AddWithValue("@Role", role);

                        userID = Convert.ToInt32(cmdUser.ExecuteScalar()); // Get UserID from USERS
                    }

                 

                    string providerQuery = @"
            INSERT INTO ServiceProvider (userid)
            VALUES (@UserID);
            SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmdProvider = new SqlCommand(providerQuery, conn))
                    {
                        cmdProvider.Parameters.AddWithValue("@UserID", userID);
                     //   cmdProvider.Parameters.AddWithValue("@ProviderName", fullName);
                       // cmdProvider.Parameters.AddWithValue("@ContactInfo", textBox3.Text);
                      //  cmdProvider.Parameters.AddWithValue("@ProviderType", comboBox1.SelectedItem.ToString());

                        providerID = Convert.ToInt32(cmdProvider.ExecuteScalar()); // Get ProviderID from ServiceProvider
                    }

                    MessageBox.Show("Service provider registered successfully.");
                    ClearForm();

                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }


            service_mainpage reg = new service_mainpage(providerID);
            reg.Show();
            this.Hide();
        }

        private void ClearForm()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 newForm = new Form1();  // Create instance of Form1
            newForm.Show();               // Show the new form
            this.Hide();                  // Hide the current form
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
