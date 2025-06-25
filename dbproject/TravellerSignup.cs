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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace dbproject
{
    public partial class TravellerSignup : Form
    {
        string connectionString = "Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False";
        public TravellerSignup()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 newForm = new Form1();  // Create instance of Form1
            newForm.Show();               // Show the new form
            this.Hide();                  // Hide the current form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Step 1: Read input values
            string email = textBox9.Text.Trim();
            string password = textBox10.Text.Trim();
            string contact = textBox8.Text.Trim();
            string city = textBox7.Text.Trim();
            string region = textBox6.Text.Trim();
            string country = textBox5.Text.Trim();
            string firstName = textBox2.Text.Trim();
            string middleName = textBox3.Text.Trim();
            string lastName = textBox4.Text.Trim();
            string gender = radioButton2.Checked ? "Male" : radioButton1.Checked ? "Female" : "Other";
            DateTime dob = dateTimePicker1.Value;
            string role = "Traveler";

            // Step 2: Validate required fields
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(contact) ||
                string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(region) || string.IsNullOrWhiteSpace(country) ||
                string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Please fill in all required fields.", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Step 3: Validate email format
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Step 4: Check if email already exists
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Email", email);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Email is already registered. Please use a different email.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Step 5: Insert new user and get generated UserID
                    string insertUserQuery = @"
                INSERT INTO Users (Email, Password, ContactNo, DOB, Gender, City, Region, Country, FirstName, MiddleName, LastName, Role)
                OUTPUT INSERTED.UserID
                VALUES (@Email, @Password, @ContactNo, @DOB, @Gender, @City, @Region, @Country, @FirstName, @MiddleName, @LastName, @Role)";

                    int newUserId;
                    using (SqlCommand insertCmd = new SqlCommand(insertUserQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@Email", email);
                        insertCmd.Parameters.AddWithValue("@Password", password);
                        insertCmd.Parameters.AddWithValue("@ContactNo", contact);
                        insertCmd.Parameters.AddWithValue("@DOB", dob);
                        insertCmd.Parameters.AddWithValue("@Gender", gender);
                        insertCmd.Parameters.AddWithValue("@City", city);
                        insertCmd.Parameters.AddWithValue("@Region", region);
                        insertCmd.Parameters.AddWithValue("@Country", country);
                        insertCmd.Parameters.AddWithValue("@FirstName", firstName);
                        insertCmd.Parameters.AddWithValue("@MiddleName", middleName);
                        insertCmd.Parameters.AddWithValue("@LastName", lastName);
                        insertCmd.Parameters.AddWithValue("@Role", role);

                        newUserId = (int)insertCmd.ExecuteScalar();
                    }

                    // Step 6: Insert into Traveler table using the returned UserID
                    string insertTravelerQuery = "INSERT INTO Traveler (UserID) VALUES (@UserID)";
                    using (SqlCommand travelerCmd = new SqlCommand(insertTravelerQuery, conn))
                    {
                        travelerCmd.Parameters.AddWithValue("@UserID", newUserId);
                        travelerCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    new Form1().Show(); // Navigate to login/dashboard
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}
