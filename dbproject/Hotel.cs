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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace dbproject
{
    public partial class Hotel : Form
    {
        string connectionString = "Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False";
        private int _providerID;

        public Hotel(int providerID)
        {
            InitializeComponent();
            _providerID = providerID;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            service_mainpage reg = new service_mainpage(_providerID);
            reg.Show();
            this.Hide();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
          
        }

        private void ClearForm()
        {
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 0;
            textBox3.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void AddServiceTypeForProvider()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string checkQuery = @"SELECT COUNT(*) FROM ProviderServiceTypes 
                              WHERE ProviderID = @ProviderID AND ServiceType = @ServiceType";

                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@ProviderID", _providerID);
                checkCmd.Parameters.AddWithValue("@ServiceType", "Hotel");

                try
                {
                    conn.Open();
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count == 0) // Only insert if not already present
                    {
                        string insertQuery = @"INSERT INTO ProviderServiceTypes (ProviderID, ServiceType)
                                       VALUES (@ProviderID, @ServiceType)";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@ProviderID", _providerID);
                        insertCmd.Parameters.AddWithValue("@ServiceType", "Hotel");

                        insertCmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }



        private void button11_Click_1(object sender, EventArgs e)
        {
          /* if (!int.TryParse(textBoxID.Text, out int hotelID) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please enter valid Hotel ID and Location.");
                return;
            }*/
            
            int stars = (int)numericUpDown1.Value;
            int rooms = (int)numericUpDown2.Value;
            int wifi = radioButton1.Checked ? 1 : radioButton2.Checked ? 0 : -1;

            if (wifi == -1)
            {
                MessageBox.Show("Please select WiFi availability.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Hotel (ProviderID, Location, Stars, RoomsAvailable, WifiAvailable)
                         VALUES (@ID, @Loc, @Stars, @Rooms, @Wifi)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", _providerID);
                cmd.Parameters.AddWithValue("@Loc", textBox3.Text);
                cmd.Parameters.AddWithValue("@Stars", stars);
                cmd.Parameters.AddWithValue("@Rooms", rooms);
                cmd.Parameters.AddWithValue("@Wifi", wifi);


                conn.Open();
               int rows= cmd.ExecuteNonQuery();
                conn.Close();

                if (rows > 0)
                {
                    MessageBox.Show("Hotel added successfully.");
                    AddServiceTypeForProvider();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Hotel WHERE ProviderID = @ProviderID", connectionString))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerID);

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                }
                else
                {
                    MessageBox.Show("Failed to add Hotel.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder("SELECT * FROM Hotel WHERE 1=1");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;


                if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    query.Append(" AND Location = @Loc");
                    cmd.Parameters.AddWithValue("@Loc", textBox3.Text);
                }

                if (numericUpDown1.Value > 0)
                {
                    query.Append(" AND StarRating = @Stars");
                    cmd.Parameters.AddWithValue("@Stars", (int)numericUpDown1.Value);
                }

                if (numericUpDown2.Value > 0)
                {
                    query.Append(" AND RoomsAvailable = @Rooms");
                    cmd.Parameters.AddWithValue("@Rooms", (int)numericUpDown2.Value);
                }

                if (radioButton1.Checked)
                {
                    query.Append(" AND WifiAvailable = 1");
                }
                else if (radioButton2.Checked)
                {
                    query.Append(" AND WifiAvailable = 0");
                }


                if (int.TryParse(textBox2.Text, out int hotelID))
                {
                    query.Append(" AND HotelID = @ID");
                    cmd.Parameters.AddWithValue("@ID", hotelID);
                }

                cmd.CommandText = query.ToString();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    
                }
                else
                {
                    MessageBox.Show("No matching hotel found.");
                    ClearForm();
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter the Hotel ID.");
                return;
            }

            int inputHotelId;
            if (!int.TryParse(textBox2.Text, out inputHotelId))
            {
                MessageBox.Show("Invalid Hotel ID.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Step 1: Confirm that the hotel belongs to the logged-in provider
                string getQuery = @"SELECT HotelID FROM Hotel 
                            WHERE HotelID = @HotelID AND ProviderID = @ProviderID";

                SqlCommand getCmd = new SqlCommand(getQuery, conn);
                getCmd.Parameters.AddWithValue("@HotelID", inputHotelId);
                getCmd.Parameters.AddWithValue("@ProviderID", _providerID);

                conn.Open();
                object hotelIdObj = getCmd.ExecuteScalar();
                conn.Close();

                if (hotelIdObj == null)
                {
                    MessageBox.Show("No matching hotel found for your provider account.");
                    return;
                }

                // Step 2: Nullify foreign key in AssignedServices
                string nullifyQuery = @"UPDATE AssignedServices 
                                SET HotelProviderID = NULL 
                                WHERE HotelProviderID = @HID";

                SqlCommand nullifyCmd = new SqlCommand(nullifyQuery, conn);
                nullifyCmd.Parameters.AddWithValue("@HID", inputHotelId);

                conn.Open();
                nullifyCmd.ExecuteNonQuery();
                conn.Close();

                // Step 3: Delete hotel
                string deleteQuery = @"DELETE FROM Hotel WHERE HotelID = @HID";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@HID", inputHotelId);

                conn.Open();
                int rows = deleteCmd.ExecuteNonQuery();
                conn.Close();

                if (rows > 0)
                {
                    MessageBox.Show("Hotel deleted successfully.");
                    LoadHotelData();  // Refresh grid
                    ClearHotelForm(); // Clear inputs
                }
                else
                {
                    MessageBox.Show("Failed to delete hotel.");
                }
            }
        }

        private void LoadHotelData()
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Hotel WHERE ProviderID = @ProviderID", connectionString))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerID);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void ClearHotelForm()
        {
            textBox3.Clear();                    // Clear Location
            numericUpDown1.Value = 1;            // Reset Stars to default
            numericUpDown2.Value = 1;            // Reset RoomsAvailable to default

            radioButton1.Checked = false;        // Wifi Yes
            radioButton2.Checked = false;        // Wifi No
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Hotel";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }
    }
}
