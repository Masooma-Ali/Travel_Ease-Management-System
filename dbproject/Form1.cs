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
using TOUROPERATOR_INTERFACE;


namespace dbproject
{
    public partial class Form1 : Form
    {
        string connectionString = "Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False";

        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT Email FROM Users ";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ComboBox1.Items.Add(reader["Email"].ToString());
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = ComboBox1.Text;
            string password = textBox3.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT UserID FROM USERS WHERE Email = @Email AND Password = @Password AND Role = 'Traveler'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    int userID = Convert.ToInt32(result);
                    TravellerMaincs travellerForm = new TravellerMaincs(userID);
                    travellerForm.Show();
                    this.Hide();

                }
                else if (result == null)
                {
                    
                    query = " SELECT Sp.ProviderID\r\n                        FROM USERS U\r\n                        JOIN ServiceProvider Sp ON U.UserID = Sp.userid\r\n                        WHERE U.Email = @Email\r\n                          AND U.Password = @Password\r\n                          AND U.Role = 'serviceprovider';";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                     result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        int userID = Convert.ToInt32(result);
                        service_mainpage service = new service_mainpage(userID);
                        service.Show();
                        this.Hide();
                    }
                    else if (result == null)
                    {
                        query = @"
                        SELECT Tp.OperatorID
                        FROM USERS U
                        JOIN TourOperator Tp ON U.UserID = Tp.userid
                        WHERE U.Email = @Email
                          AND U.Password = @Password
                          AND U.Role = 'TourOperator';"; 
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int operatorID = Convert.ToInt32(result);
                            menuform mf = new menuform(operatorID); // pass operatorID instead of userID
                            mf.Show();
                            this.Hide();
                        }
                        else if (result == null)
                        {

                            query = @"
                            SELECT A.AdminID
                            FROM USERS U
                            JOIN Admins A ON U.UserID = A.userid
                            WHERE U.Email = @Email
                              AND U.Password = @Password
                              AND U.Role = 'Admin';";
                            cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@Password", password);

                            result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                Admin newForm = new Admin();  // Create instance of Form1
                                newForm.Show();               // Show the new form
                                this.Hide();                  // Hide the current form
                            }
                            else
                            {
                                MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                    }
                }
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
