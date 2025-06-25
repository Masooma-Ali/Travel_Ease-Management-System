using dbproject;
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

namespace TOUROPERATOR_INTERFACE
{
    public partial class operatorRegistrationForm : Form
    {
        public operatorRegistrationForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 newForm = new Form1();  // Create instance of Form1
            newForm.Show();               // Show the new form
            this.Hide();                  // Hide the current form
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = @"Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // 1. Insert into USERS
                    string insertUserQuery = @"
                    INSERT INTO USERS (
                    FirstName, MiddleName, LastName,
                    Country, Region, City,
                    Email, Password, DOB, Gender,
                    ContactNo, Role
                    )
                    VALUES (
                    @FirstName, @MiddleName, @LastName,
                    @Country, @Region, @City,
                    @Email, @Password, @DOB, @Gender,
                    @ContactNo, @Role
                    );
                    SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdUser = new SqlCommand(insertUserQuery, conn);
                    cmdUser.Parameters.AddWithValue("@FirstName", txtfirstname.Text);
                    cmdUser.Parameters.AddWithValue("@MiddleName", txtmiddlename.Text);
                    cmdUser.Parameters.AddWithValue("@LastName", txtlastname.Text);
                    cmdUser.Parameters.AddWithValue("@Country", txtcountry.Text);
                    cmdUser.Parameters.AddWithValue("@Region", txtregion.Text);
                    cmdUser.Parameters.AddWithValue("@City", txtcity.Text);
                    cmdUser.Parameters.AddWithValue("@Email", txtemail.Text);
                    cmdUser.Parameters.AddWithValue("@Password", txtpassword.Text);
                    cmdUser.Parameters.AddWithValue("@DOB", dateTimePicker1.Value);
                    cmdUser.Parameters.AddWithValue("@Gender", radioButton2.Checked ? "Male" : "Female");
                    cmdUser.Parameters.AddWithValue("@ContactNo", txtcontactno.Text);
                    cmdUser.Parameters.AddWithValue("@Role", "TourOperator");

                    int userId = Convert.ToInt32(cmdUser.ExecuteScalar());

                    // 2. Insert into TourOperator
                    string insertOpQuery = @"
                    INSERT INTO TourOperator (CompanyName, UserID)
                    VALUES (@CompanyName, @UserID);
                    SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdOp = new SqlCommand(insertOpQuery, conn);
                    cmdOp.Parameters.AddWithValue("@CompanyName", txtcompanyname.Text);
                    cmdOp.Parameters.AddWithValue("@UserID", userId);

                    int operatorId = Convert.ToInt32(cmdOp.ExecuteScalar());

                    MessageBox.Show("Registration successful!");

                    // Pass to menu/dashboard
                    menuform mf = new menuform(operatorId);
                    mf.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Registration failed: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
