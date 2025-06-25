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

namespace dbproject
{
    public partial class Transport : Form
    {
        string connectionString = "Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False";
        private int _providerID;


        public Transport(int providerID)
        {
            InitializeComponent();
            _providerID = providerID;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            service_mainpage reg = new service_mainpage(_providerID);
            reg.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please enter the Transport ID.");
                return;
            }

            if (!int.TryParse(textBox3.Text, out int transportId))
            {
                MessageBox.Show("Invalid Transport ID.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Step 1: Check if transport belongs to current provider
                string checkQuery = @"SELECT TransportID FROM Transport 
                              WHERE TransportID = @TransportID AND ProviderID = @ProviderID";

                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@TransportID", transportId);
                checkCmd.Parameters.AddWithValue("@ProviderID", _providerID);

                conn.Open();
                object result = checkCmd.ExecuteScalar();
                conn.Close();

                if (result == null)
                {
                    MessageBox.Show("No matching transport found for your provider account.");
                    return;
                }

                // Step 2: Nullify references in AssignedServices
                string nullifyQuery = @"UPDATE AssignedServices 
                                SET TransportProviderID = NULL 
                                WHERE TransportProviderID = @TID";

                SqlCommand nullifyCmd = new SqlCommand(nullifyQuery, conn);
                nullifyCmd.Parameters.AddWithValue("@TID", transportId);

                conn.Open();
                nullifyCmd.ExecuteNonQuery();
                conn.Close();

                // Step 3: Delete transport record
                string deleteQuery = @"DELETE FROM Transport WHERE TransportID = @TID";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@TID", transportId);

                conn.Open();
                int rowsAffected = deleteCmd.ExecuteNonQuery();
                conn.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Transport deleted successfully.");
                    LoadTransportData(); // Refresh the grid
                }
                else
                {
                    MessageBox.Show("Deletion failed.");
                }
            }
        }

        private void ClearForm()
        {
            comboBox1.SelectedIndex = -1;
            numericUpDown1.Value = 1;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder("SELECT * FROM Transport WHERE 1=1");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                // Filter by Transport Mode
                if (comboBox1.SelectedItem != null)
                {
                    query.Append(" AND TransportMode = @Mode");
                    cmd.Parameters.AddWithValue("@Mode", comboBox1.SelectedItem.ToString());
                }

                // Filter by Capacity
                if (numericUpDown1.Value > 0)
                {
                    query.Append(" AND Capacity = @Capacity");
                    cmd.Parameters.AddWithValue("@Capacity", (int)numericUpDown1.Value);
                }

                // Filter by AC Availability (if a radio is selected)
                if (radioButton1.Checked)
                {
                    query.Append(" AND ACAvailable = 1");
                }
                else if (radioButton2.Checked)
                {
                    query.Append(" AND ACAvailable = 0");
                }

                // Filter by Transport ID (optional)
                if (int.TryParse(textBox3.Text, out int transportID))
                {
                    query.Append(" AND TransportID = @TransportID");
                    cmd.Parameters.AddWithValue("@TransportID", transportID);
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
                    MessageBox.Show("No matching transport found.");
                }
            }
        }

        private void AddServiceTypeForProvider()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string checkQuery = @"SELECT COUNT(*) FROM ProviderServiceTypes 
                              WHERE ProviderID = @ProviderID AND ServiceType = @ServiceType";

                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@ProviderID", _providerID);
                checkCmd.Parameters.AddWithValue("@ServiceType", "Transport");

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
                        insertCmd.Parameters.AddWithValue("@ServiceType", "Transport");

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



        private void button12_Click(object sender, EventArgs e)
        {
            string selectedMode = comboBox1.SelectedItem?.ToString();
            int capacity = (int)numericUpDown1.Value;
            int acAvailable = radioButton1.Checked ? 1 : radioButton2.Checked ? 0 : -1;

            if (string.IsNullOrEmpty(selectedMode) || acAvailable == -1)
            {
                MessageBox.Show("Please complete all fields.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string insertQuery = @"INSERT INTO Transport (ProviderID, TransportMode, Capacity, ACAvailable)
                               VALUES (@ProviderID, @Mode, @Capacity, @AC)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@ProviderID", _providerID);
                    cmd.Parameters.AddWithValue("@Mode", selectedMode);
                    cmd.Parameters.AddWithValue("@Capacity", capacity);
                    cmd.Parameters.AddWithValue("@AC", acAvailable);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Transport added successfully.");
                        AddServiceTypeForProvider();
                        LoadTransportData(); 
                    }
                    else
                    {
                        MessageBox.Show("Failed to add transport.");
                    }
                }
            }
        }

        private void LoadTransportData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Transport WHERE ProviderID = @ProviderID";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerID);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Transport";
           
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
        

